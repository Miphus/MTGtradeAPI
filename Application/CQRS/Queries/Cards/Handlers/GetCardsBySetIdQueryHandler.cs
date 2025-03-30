using Application.DTOs;
using Application.Interfaces;
using Application.QCRS.Queries.Cards;
using MediatR;

namespace Application.CQRS.Queries.Cards.Handlers;

public class GetCardsBySetIdQueryHandler : IRequestHandler<GetCardsBySetIdQuery, List<CardDto>>
{
    private readonly ICardRepository _repository;

    public GetCardsBySetIdQueryHandler(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CardDto>> Handle(GetCardsBySetIdQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetCardsBySetIdAsync(request.SetId);

        return cards.Select(card => new CardDto
        {
            Id = card.Id,
            Name = card.Name,
            Number = card.Number,
            ManaCost = card.ManaCost,
            ManaValue = card.ManaValue,
            Type = card.Type,
            Rarity = card.Rarity,
            Layout = card.Layout,
            Text = card.Text,
            FlavorText = card.FlavorText,
            ImageUrl = card.ImageUrl,
            SetCode = card.Set?.Code
        }).ToList();
    }
}
