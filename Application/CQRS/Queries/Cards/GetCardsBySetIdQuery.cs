using Application.DTOs;
using MediatR;

namespace Application.QCRS.Queries.Cards;

public record GetCardsBySetIdQuery(Guid SetId) : IRequest<List<CardDto>>;