using System.ComponentModel.DataAnnotations;

namespace LakeXplorer.Models
{
    // Represents information about a lake.
    public class Lakes : BaseModels
    {

        // Gets or sets the name of the lake.
        [Required]
        public string Name { get; set; }

        // Gets or sets the image path associated with the lake.
        public string Image { get; set; }

        // Gets or sets the description of the lake.
        public string Description { get; set; }
    }
}
