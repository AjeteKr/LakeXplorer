namespace LakeXplorerBlazor.Data
{
    // Represents a data transfer object (DTO) for user likes related to lake sightings.
    public class LikeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SightingId { get; set; }
    }
}
