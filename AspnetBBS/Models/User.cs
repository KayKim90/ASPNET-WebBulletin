using System.ComponentModel.DataAnnotations;

namespace AspnetBBS.Models
{
    public class User
    {
        /// <summary>
        /// User Number
        /// </summary>
        [Key] //define pk 
        public int UserNo { get; set; }

        [Required(ErrorMessage ="Please enter user name")] //define not null
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please enter user ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage ="Please enter your password")]
        public string UserPassword { get; set; }

    }
}
