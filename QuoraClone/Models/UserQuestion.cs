using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoraClone.Models
{
    public class UserQuestion 
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserQuestionID { get; set; }

        [Required]
        public Question QuestionAnswered { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public int QuestionID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Payload { get; set; }
    }
}