using System.ComponentModel.DataAnnotations;

namespace LakeXplorer.Models
{
    // Represents information about a lake.
    public class Lakes : BaseModels
    {

        // Gets or sets the name of the lake.
        [Required]
        public string Name { get; set; }

        [Required]
        public string CloudinaryAssetId { get; set; }


        // Gets or sets the image path associated with the lake.

        [Required]
        public string ImageUrl { get; set; }
        // Gets or sets the description of the lake.
        public string Description { get; set; }
    }
}
