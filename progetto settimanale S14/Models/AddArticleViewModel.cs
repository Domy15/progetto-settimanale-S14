using System.ComponentModel.DataAnnotations;

namespace progetto_settimanale_S14.Models
{
    public class AddArticleViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Il nome è obbligatorio!")]
        public string Name { get; set; }

        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Il prezzo è obbligatorio!")]
        public decimal Price { get; set; }

        [Display(Name = "Descrizione")]
        [Required(ErrorMessage = "La descrizione è obbligatoria!")]
        public string Description { get; set; }

        public string? Cover { get; set; }

        public List<string> Images { get; set; } = new List<string>();
    }
}
