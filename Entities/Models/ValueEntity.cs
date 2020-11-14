using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Values")]
    public class ValueEntity : AutoincrementEntity
    {
        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Description is 30 characters.")]
        [Column("Description")] 
        public string Description { get; set; }
    }
}
