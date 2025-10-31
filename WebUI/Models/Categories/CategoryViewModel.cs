namespace ProductManagment.WebUI.Models
{
    public class CategoryViewModel
    {
        public string? Category {  get; set; }
        public IEnumerable<CategoryModel> categoryModels { get; set; } = [];
    }
}
