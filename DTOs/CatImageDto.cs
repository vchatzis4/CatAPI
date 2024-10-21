using System.ComponentModel.DataAnnotations;

namespace CatAPI.DTOs
{
    public class CatImageDto
    {
        public string Id {  get; set; }

        [Range(1, int.MaxValue)]
        public int Width {  get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        public string Url {  get; set; }

        public List<BreedDto> Breeds { get; set; }
    }
}
