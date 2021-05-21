using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoraClone.Models
{

    public class Question
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        
        [Required]
        public string QuestionTitle { get; set; }

        [Required]
        public UserQuestion QuestionBody { get; set; }

        [Required]
        public ICollection<UserQuestion> Responses { get; set; }
    }
}