using System.ComponentModel.DataAnnotations;

namespace CatAPI.Models
{
    public class CatEntity
    {
        public int Id { get; set; }

        [MinLength(1)]
        public string CatId { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        public byte[] Image { get; set; }

        public DateTime Created { get; set; }

        public ICollection<CatTagEntity> CatTags { get; set; }
    }
}
