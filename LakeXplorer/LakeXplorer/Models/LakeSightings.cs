using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakeXplorer.Models
{

    public class LakeSightings : BaseModels
    {
     
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }

        [Required]
        public string CloudinaryAssetId { get; set; }



        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string FunFact { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("LakeId")]
        public int LakeId { get; set; }
        public Lakes Lake { get; set; }
    }
}