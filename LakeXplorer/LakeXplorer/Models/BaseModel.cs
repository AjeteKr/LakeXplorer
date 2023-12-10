using System.ComponentModel.DataAnnotations;

namespace LakeXplorer.Models
{
    public abstract class BaseModels
    {
        [Key]
        public int Id { get; set; }
    }
}