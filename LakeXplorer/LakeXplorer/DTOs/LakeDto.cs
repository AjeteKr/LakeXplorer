namespace LakeXplorer.DTOs
{

    // Data Transfer Object for representing a lake.
    public class LakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CloudinaryAssetId { get; set; }
        //public bool IsLiked { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
