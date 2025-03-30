namespace Domain.Entites;

public class CardForeignData
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public string? Language { get; set; }
    public string? Text { get; set; }
    public string? FlavorText { get; set; }
}
