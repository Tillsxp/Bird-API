using Bird_API.Models;
using System.Text.Json;

namespace Bird_API.Data;

public static class SeedData
{
    public static async Task LoadBirdsData (BirdContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        if (context.Birds.Any()) return;

        var json = System.IO.File.ReadAllText("./Data/Json/birds.json");

        try
        {
            var birds = JsonSerializer.Deserialize<List<Bird>>(json, options);

            if (birds is not null && birds.Count > 0)
            {
                await context.Birds.AddRangeAsync(birds);
                await context.SaveChangesAsync();
            }
        }
        catch (JsonException ex)
        {
            
            Console.WriteLine(ex.Message);
            Console.WriteLine(json);
        }

    }
}
