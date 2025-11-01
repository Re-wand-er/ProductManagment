using System.ComponentModel.DataAnnotations;

namespace ProductManagment.WebUI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        //[Required] валидация сейчас не нужна для роли
        [StringLength(50, ErrorMessage = "Название роли не может превышать 50 символов.")]
        public string Name { get; set; } = null!;
    }
}
