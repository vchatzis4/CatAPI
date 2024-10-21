using CatAPI.Models;

namespace CatAPI.Interfaces
{
    public interface ICatRepository
    {
        Task SaveCatsAsync(IEnumerable<CatEntity> cats);
        Task<CatEntity> GetCatByIdAsync(int id);
        Task<IEnumerable<CatEntity>> GetCatsAsync(int page, int pageSize, string tag = null);
    }
}
