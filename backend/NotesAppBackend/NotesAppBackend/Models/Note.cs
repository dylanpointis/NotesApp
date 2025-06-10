using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesAppBackend.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNote { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string TextContent { get; set; }

        [StringLength(16)]
        public string RegistrationDate { get; set; }

        public bool IsArchived { get; set; }

        [StringLength(8)]
        public string Color { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
