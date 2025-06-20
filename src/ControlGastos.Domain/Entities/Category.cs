using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression("^#([A-Fa-f0-9]{6})$",
          ErrorMessage = "Debe ser un hex color válido, p.ej. #FF5733")]
        public string ColorHex { get; set; } = "#FFFFFF";

        public ICollection<RecurringExpense> Expenses { get; set; }
            = new List<RecurringExpense>();
    }
}