namespace Application.DTOs;

public class CardDto
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
    public string? FlavorText { get; set; }
    public string? ImageUrl { get; set; }
    public string? SetCode { get; set; }
}
