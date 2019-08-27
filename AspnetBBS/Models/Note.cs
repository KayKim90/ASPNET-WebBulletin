using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetBBS.Models
{
    public class Note
    {
        //In class, Using annotation can define specific requirement such as not null.
        [Key]
        public int NoteNo { get; set; }

        [Required(ErrorMessage ="Title required")]
        public string NoteTitle { get; set; }

        [Required(ErrorMessage = "Contents required")]
        public string NoteContents { get; set; }

        [Required]
        public int UserNo { get; set; }

        //In code first, How to join(Connect btw 2 tables)?
        //In Note class UserNo => UserName in User class
        //Why virtual? : Lazy logging
        [ForeignKey("UserNo")]
        public virtual User User { get; set; }
    }
}
