using System.ComponentModel.DataAnnotations;

namespace TechnoMate.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }
    }
}
