namespace Domain.Entites;

public class CardRuling
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public DateTime? Date { get; set; }
    public string? Text { get; set; }
}
