namespace API.Models;

public class MtgJsonSet
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Series { get; set; }
    public string? Type { get; set; }
    public string? Block { get; set; }
    public string? ParentCode { get; set; }
    public List<MtgJsonCard> Cards { get; set; }
}
