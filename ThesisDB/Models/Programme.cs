using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThesisDB.Models
{
    public class Programme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie den Namen des Studiengangs an.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        // Navigation Property: Ein Studiengang kann mehrere Abschlussarbeiten haben (1:n)
        public ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
    }
}
