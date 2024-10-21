using CatAPI.Data;
using CatAPI.DTOs;
using CatAPI.Interfaces;
using CatAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CatAPI.Services
{
    public class CatApiService : ICatApiService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CatApiService(AppDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _apiKey = configuration["CatApi:ApiKey"];
        }

        public async Task<IEnumerable<CatImageDto>> FetchCatImagesAsync(int limit = 25)
        {
            var fetchedCatImages = new List<CatImageDto>();

            try
            {
                string url = $"https://api.thecatapi.com/v1/images/search?limit={limit}&has_breeds=1";
                _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var catImages = JsonConvert.DeserializeObject<List<CatImageDto>>(content);

                foreach (var catImage in catImages)
                {
                    var existingCat = await _context.Cats.AnyAsync(c => c.CatId == catImage.Id);

                    if (!existingCat)
                    {
                        var catEntity = new CatEntity
                          {
                              CatId = catImage.Id,
                              Width = catImage.Width,
                              Height = catImage.Height,
                              Image = await FetchImageDataAsync(catImage.Url),
                              Created = DateTime.UtcNow
                          };

                        if (!ValidationService.IsValidCatId(catEntity.CatId) ||
                            !ValidationService.IsValidImage(catEntity.Image) ||
                            !ValidationService.IsValidDimension(catEntity.Width, catEntity.Height))
                        {
                            throw new Exception("Invalid cat data.");
                        }

                         _context.Cats.Add(catEntity);
                         fetchedCatImages.Add(catImage);
                    }
                    
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch images from Cat API.", ex);
            }

            return fetchedCatImages;
        }

        private async Task<byte[]> FetchImageDataAsync(string imageUrl)
        {
            var response = await _httpClient.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
