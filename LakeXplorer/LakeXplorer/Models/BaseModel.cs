using System.ComponentModel.DataAnnotations;

namespace LakeXplorer.Models
{

    // Base model class with an ID property.
    public abstract class BaseModels
    {
        [Key]
        public int Id { get; set; }
    }
}