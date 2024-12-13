namespace Traversal.Areas.Member.Models
{
	public class UserEditViewModel
	{
        public string? name { get; set; }
        public string? Surname { get; set; }
        public string? password { get; set; }
        public string? Confirmpassword { get; set; }
        public string? phonenumber { get; set; }
        public string? mail { get; set; }
        public string? imageURL { get; set; }
        public IFormFile? Image { get; set; }
    }
}
