using Domain.Entites;

namespace Application.Interfaces;

public interface ICardRepository
{
    Task<List<Card>> GetCardsBySetIdAsync(Guid setId);
}
