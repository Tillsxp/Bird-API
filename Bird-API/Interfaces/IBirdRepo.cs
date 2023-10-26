using Bird_API.Models;

namespace Bird_API.Interfaces;

public interface IBirdRepo
{
    Task<IList<Bird>> ListAllAsync();
    Task<Bird> FindByIdAsync(int id);
    Task<Bird> FindByNameAsync(string name);

    Task<bool> AddAsync(Bird bird);
    Task<bool> UpdateAsync(Bird bird);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveAsync();
}
