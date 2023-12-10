using System.ComponentModel.DataAnnotations;

namespace LakeXplorer.Models
{
    // Represents information about a lake.
    public class Lakes : BaseModels
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CloudinaryAssetId { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        //public bool IsLiked { get; set; }

    }
}
