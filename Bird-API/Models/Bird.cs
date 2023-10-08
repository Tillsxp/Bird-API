namespace Bird_API.Models;

public class Bird
{
    public Guid BirdID { get; set; }
    public string Name { get; set; } = ""; //Breed
    public string ImageUrl { get; set; } = "";
    public string Description { get; set; } = "";
    public string Habitat { get; set; } = "";
}
