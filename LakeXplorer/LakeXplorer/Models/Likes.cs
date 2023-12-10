using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LakeXplorer.Models
{
    public class Likes : BaseModels
    {

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("SightingId")]
        public int SightingId { get; set; }
        public LakeSightings Sighting { get; set; }
    }
}
