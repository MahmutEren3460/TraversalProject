using System.ComponentModel.DataAnnotations;

namespace Traversal.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz")]
        public string? username { get; set; }
        [Required(ErrorMessage = "Lütfen parolayı giriniz")]
        public string? password { get; set; }   
    }
}
