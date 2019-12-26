using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("feedback")]
    public class Feedback
    {
        [Key]
        public int feedbackID { get; set; }
        public string feedbackContent { get; set; }

        public int Evaluate { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }

    }
}