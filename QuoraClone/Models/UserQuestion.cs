using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoraClone.Models
{
    public class UserQuestion 
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserQuestionID { get; set; }

        public Question QuestionAnswered { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public int QuestionID { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "You must provide a message")]
        public string Payload { get; set; }


        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            UserQuestion other = (UserQuestion) obj;

            // TODO: write your implementation of Equals() here
             return Username == other.Username && QuestionID == other.QuestionID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}