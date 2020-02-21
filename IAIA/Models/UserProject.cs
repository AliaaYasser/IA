using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("userProject")]
    public class UserProject
    {
        public int ProjectId { get; set; }
         public Project project { get; set; }

        public int userId { get; set; }
        
        public User user { get; set; }

       
    }
}