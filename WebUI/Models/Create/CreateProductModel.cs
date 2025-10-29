namespace ProductManagment.WebUI.Models
{
    public class CreateProductModel : ProductModel
    {
        public int CategoryId { get; set; }
        public List<CategoryModel>? Categories { get; set; }
    }
}
