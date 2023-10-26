using Bird_API.Data;
using Bird_API.Interfaces;
using Bird_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Bird_API.Repository;

public class BirdRepo : IBirdRepo
{
    private readonly BirdContext _context;
    public BirdRepo(BirdContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(Bird bird)
    {
        _context.Add(bird);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var bird = await _context.Birds.FindAsync(id);
        _context.Remove(bird);
        return true;
    }

    public async Task<Bird> FindByIdAsync(int id)
    {
        var bird = await _context.Birds.FindAsync(id);
        return bird;
    }

    public async Task<Bird> FindByNameAsync(string name)
    {
        var bird = await _context.Birds.FirstOrDefaultAsync(b => b.Name == name);
        return bird;
    }

    public async Task<IList<Bird>> ListAllAsync()
    {
        return await _context.Birds.ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while saving{ex.Message} - {ex.InnerException}");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Bird bird)
    {
        try
        {
            _context.Birds.Update(bird);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Unable to update {bird}. Error: {ex.Message}");
            return false;
        }
    }
}
