using System.ComponentModel.DataAnnotations;

namespace AspnetBBS.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter user ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage ="Please enter password")]
        public string UserPassword { get; set; }
    }
}
