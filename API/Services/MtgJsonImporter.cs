using API.Models;
using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MTGtrade.API.Services;

public class MtgJsonImporter
{
    private readonly MTGtradeDbContext _context;
    private readonly ILogger<MtgJsonImporter> _logger;

    public MtgJsonImporter(MTGtradeDbContext context, ILogger<MtgJsonImporter> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ImportAllAsync(string path)
    {
        try
        {
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var root = await JsonSerializer.DeserializeAsync<MtgJsonRoot>(fs, options);
            int imported = 0;

            foreach (var setData in root.Data.Values)
            {
                if (string.IsNullOrWhiteSpace(setData.Code) || string.IsNullOrWhiteSpace(setData.Name))
                    continue;

                _logger.LogInformation($"➡️ Importing set: {setData.Code} - {setData.Name}");

                var existingSet = await _context.Sets.FirstOrDefaultAsync(s => s.Code == setData.Code);
                if (existingSet == null)
                {
                    existingSet = new Domain.Entities.Set
                    {
                        Code = setData.Code,
                        Name = setData.Name,
                        ReleaseDate = DateTime.TryParse(setData.ReleaseDate, out var date) ? date : null,
                        Type = setData.Type,
                        Series = setData.Series,
                        Block = setData.Block,
                        ParentCode = setData.ParentCode
                    };
                    _context.Sets.Add(existingSet);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var seenCards = new HashSet<string>();

                foreach (var card in setData.Cards)
                {
                    try
                    {
                        var key = $"{card.Name}::{card.Number}::{existingSet.Id}::{card.Layout}";
                        if (seenCards.Contains(key)) continue;
                        seenCards.Add(key);

                        if (string.IsNullOrWhiteSpace(card.Name) || string.IsNullOrWhiteSpace(card.Number))
                            continue;

                        bool cardExists = await _context.Cards.AnyAsync(c =>
                            c.Name == card.Name &&
                            c.Number == card.Number &&
                            c.SetId == existingSet.Id &&
                            c.Layout == card.Layout);

                        if (cardExists) continue;

                        var scryfallId = card.Identifiers?.ScryfallId;

                        var entity = new Card
                        {
                            Name = card.Name,
                            Number = card.Number,
                            ManaCost = card.ManaCost,
                            ManaValue = card.ManaValue,
                            Type = card.Type,
                            Rarity = card.Rarity,
                            Layout = card.Layout,
                            Text = card.Text,
                            OriginalText = card.OriginalText,
                            OriginalType = card.OriginalType,
                            FlavorText = card.FlavorText,
                            Power = card.Power,
                            Toughness = card.Toughness,
                            Artist = card.Artist,
                            ArtistIds = card.ArtistIds,
                            BorderColor = card.BorderColor,
                            FrameVersion = card.FrameVersion,
                            Language = card.Language,
                            Colors = card.Colors,
                            ColorIdentity = card.ColorIdentity,
                            BoosterTypes = card.BoosterTypes,
                            Finishes = card.Finishes,
                            Keywords = card.Keywords,
                            HasFoil = card.HasFoil,
                            HasNonFoil = card.HasNonFoil,
                            IsStarter = card.IsStarter,
                            ConvertedManaCost = card.ConvertedManaCost,
                            EdhrecRank = card.EdhrecRank,
                            MultiverseId = card.Identifiers?.MultiverseId,
                            ScryfallId = scryfallId,
                            MtgjsonV4Id = card.Identifiers?.MtgjsonV4Id,
                            ScryfallOracleId = card.Identifiers?.ScryfallOracleId,
                            ScryfallIllustrationId = card.Identifiers?.ScryfallIllustrationId,
                            ScryfallCardBackId = card.Identifiers?.ScryfallCardBackId,
                            ImageUrl = scryfallId != null
                                ? $"https://cards.scryfall.io/normal/front/{scryfallId.Value.ToString()[0]}/{scryfallId.Value.ToString()[1]}/{scryfallId}.jpg"
                                : null,
                            SetId = existingSet.Id
                        };

                        // Lägg till rulings
                        if (card.Rulings?.Any() == true)
                        {
                            entity.Rulings = card.Rulings
                                .Select(r => new CardRuling
                                {
                                    Date = DateTime.Parse(r.Date),
                                    Text = r.Text
                                }).ToList();
                        }

                        // Lägg till foreign data
                        if (card.ForeignData?.Any() == true)
                        {
                            entity.ForeignData = card.ForeignData
                                .Select(f => new CardForeignData
                                {
                                    Language = f.Language,
                                    Text = f.Text,
                                    FlavorText = f.FlavorText
                                }).ToList();
                        }

                        _context.Cards.Add(entity);
                        imported++;

                        if (imported % 100 == 0)
                        {
                            _logger.LogInformation($"💾 Saving batch... Imported so far: {imported}");
                            await _context.SaveChangesAsync();
                            _context.ChangeTracker.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"⚠️  Skipped card '{card.Name}' (Set: {setData.Code}) due to error: {ex.Message} | Inner: {ex.InnerException?.Message}");
                    }
                }
            }

            if (_context.ChangeTracker.HasChanges())
            {
                _logger.LogInformation($"💾 Final save... Total imported: {imported}");
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            _logger.LogInformation($"✅ Import completed. Total cards imported: {imported}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"❌ Failed to import JSON: {ex.Message}");
        }
    }

}
