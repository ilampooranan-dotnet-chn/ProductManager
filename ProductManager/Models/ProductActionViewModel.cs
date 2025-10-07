using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class ProductActionViewModel
    {
        [Required(ErrorMessage = "Product ID is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "ID must be a positive integer.")]
        public string Id { get; set; } // Changed to string to handle non-integer input for validation

        [Required(ErrorMessage = "Please select an action.")]
        public string ActionName { get; set; }
    }
}