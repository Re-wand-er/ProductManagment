namespace ProductManagment.WebUI.Models
{
    public class ProductCatalogViewModel
    {
        public string? Search {  get; set; }
        public int? SelectedCategory { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; } = [];
        public IEnumerable<ProductModel> Products { get; set; } = [];
    }
}
