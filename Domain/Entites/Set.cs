using Domain.Entites;

namespace Domain.Entities;

public class Set
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } // T.ex. "LEA"
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Type { get; set; }
    public string? Series { get; set; }
    public string? Block { get; set; }
    public string? ParentCode { get; set; }

    public ICollection<Card> Cards { get; set; }
}

