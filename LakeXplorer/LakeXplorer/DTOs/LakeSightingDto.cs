namespace LakeXplorer.DTOs
{

    public class LakeSightingDto
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CloudinaryAssetId { get; set; }

        public string ImageUrl { get; set; }
        public string FunFact { get; set; }
        public int UserId { get; set; }
        public int LakeId { get; set; }
    }
}
