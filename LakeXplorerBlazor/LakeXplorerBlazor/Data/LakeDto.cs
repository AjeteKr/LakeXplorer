using System.ComponentModel.DataAnnotations;

namespace LakeXplorerBlazor.Data
{
    // Represents a data transfer object (DTO) for information about a lake.
    public class LakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CloudinaryAssetId { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
