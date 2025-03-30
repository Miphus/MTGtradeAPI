using Domain.Entities;

namespace Domain.Entites
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string? ManaCost { get; set; }
        public double? ManaValue { get; set; }
        public string? Type { get; set; }
        public string? Rarity { get; set; }
        public string? Layout { get; set; }
        public string? Text { get; set; }
        public string? OriginalText { get; set; }
        public string? OriginalType { get; set; }
        public string? FlavorText { get; set; }
        public string? Power { get; set; }
        public string? Toughness { get; set; }
        public string? Artist { get; set; }
        public List<string>? ArtistIds { get; set; }
        public string? BorderColor { get; set; }
        public string? FrameVersion { get; set; }
        public string? Language { get; set; }
        public List<string>? Colors { get; set; }
        public List<string>? ColorIdentity { get; set; }
        public List<string>? BoosterTypes { get; set; }
        public List<string>? Finishes { get; set; }
        public List<string>? Keywords { get; set; }
        public bool? HasFoil { get; set; }
        public bool? HasNonFoil { get; set; }
        public bool? IsStarter { get; set; }
        public double? ConvertedManaCost { get; set; }
        public int? EdhrecRank { get; set; }

        public int? MultiverseId { get; set; }
        public Guid? ScryfallId { get; set; }
        public string? MtgjsonV4Id { get; set; }
        public string? ScryfallOracleId { get; set; }
        public string? ScryfallIllustrationId { get; set; }
        public string? ScryfallCardBackId { get; set; }

        public string? ImageUrl { get; set; }
        public Guid SetId { get; set; }
        public Set Set { get; set; }

        public List<CardRuling>? Rulings { get; set; }
        public List<CardForeignData>? ForeignData { get; set; }
    }
}
