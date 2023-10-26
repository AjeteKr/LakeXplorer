namespace LakeXplorerBlazor.Data
{
    // Represents a data transfer object (DTO) for information about a lake.
    public class LakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
