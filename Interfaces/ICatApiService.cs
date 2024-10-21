using CatAPI.DTOs;

namespace CatAPI.Interfaces
{
    public interface ICatApiService
    {
        Task<IEnumerable<CatImageDto>> FetchCatImagesAsync(int limit = 25);
    }
}
