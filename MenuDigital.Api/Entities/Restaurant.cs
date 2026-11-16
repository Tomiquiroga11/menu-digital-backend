namespace MenuDigital.Api.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public List<Category> Categories { get; set; } = new();
}