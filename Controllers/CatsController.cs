using CatAPI.Interfaces;
using CatAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : Controller
    {
        private readonly ICatApiService _catApiService;
        private readonly ICatRepository _catRepository;

        public CatsController(ICatApiService catApiService, ICatRepository catRepository)
        {
            _catApiService = catApiService;
            _catRepository = catRepository;
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchCats()
        {
            var cats = await _catApiService.FetchCatImagesAsync();
            var catEntities = cats.Select(cat => new CatEntity
            {
                CatId = cat.Id,
                Width = cat.Width,
                Height = cat.Height,
                Image = FetchCatImage(cat.Url),
                Created = DateTime.UtcNow,
                CatTags = cat.Breeds.SelectMany(b => b.Temperament.Split(",")).Select(t => new CatTagEntity
                {
                    Tag = new TagEntity
                    {
                        Name = t.Trim(),
                        Created = DateTime.UtcNow
                    }
                }).ToList()
            });
            await _catRepository.SaveCatsAsync(catEntities);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCat(int id)
        {
            var cat = await _catRepository.GetCatByIdAsync(id);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpGet]
        public async Task<IActionResult> GetCats([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string tag = null)
        {
            var cats = await _catRepository.GetCatsAsync(page, pageSize, tag);
            return Ok(cats);
        }

        private byte[] FetchCatImage(string url)
        {
            using var webClient = new WebClient();
            return webClient.DownloadData(url);
        }
    }
}
