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
        
        [Required(ErrorMessage = "You must specify a question")]
        public string QuestionTitle { get; set; }

    }
}