using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class CategoryViewModel
    {
        [StringLength(50, ErrorMessage = "Название категории не может превышать 50 символов.")]
        public string? Category {  get; set; }
        public IEnumerable<CategoryModel> categoryModels { get; set; } = [];
    }
}
