using Application.Interfaces;
using Domain.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly MTGtradeDbContext _context;

    public CardRepository(MTGtradeDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetCardsBySetIdAsync(Guid setId)
    {
        return await _context.Cards
            .Include(c => c.Set)
            .Where(c => c.SetId == setId)
            .ToListAsync();
    }
}
