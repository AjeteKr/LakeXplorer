using System.ComponentModel.DataAnnotations;

namespace LakeXplorerBlazor.Data
{
    // Represents a data transfer object (DTO) for information about a lake sighting.
    public class LakeSightingDto
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CloudinaryAssetId { get; set; }
        public string FunFact { get; set; }
        public int UserId { get; set; }
        public int LakeId { get; set; }
    }
}
