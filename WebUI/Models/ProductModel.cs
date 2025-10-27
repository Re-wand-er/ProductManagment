using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public string? GeneralNote { get; set; }
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
