using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название продукта обязательно для заполнения.")]
        [StringLength(100, MinimumLength =3, ErrorMessage = "Название не может быть меньше 3 превышать 100 символов.")]
        public string Name { get; set; } = null!;
        public string? Category { get; set; }

        [StringLength(255, ErrorMessage = "Описание не может и превышать 255 символов.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Цена обязательна.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Цена должна быть больше 0.")]
        public decimal Cost { get; set; }

        [StringLength(255, ErrorMessage = "Общая примечание не может превышать 255 символов.")]
        public string? GeneralNote { get; set; }

        [StringLength(100, ErrorMessage = "Специальное примечание не может превышать 100 символов.")]
        public string? SpecialNote { get; set; }

        public ProductModel() { }
        public ProductModel(int id, string name, string category, string? description, decimal cost, string? generalNote, string? specialNote ) 
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
            GeneralNote = generalNote;
            SpecialNote = specialNote;
        }
    }
}
