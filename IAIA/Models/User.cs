using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("users")]
        public class User
    {
        [Key]
        public int UserId { set; get; }
        [Display(Name = "Photo")]
        public string UserPhoto { set; get; }
        public int UserExpirienceYears { set; get; }
        [Display(Name = "Email")]
        public string UserEmail { set; get; }
        public string UserJob_description { set; get; }
        public string UserFirstName { set; get; }
        public string UserLasttName { set; get; }
        public string UserPhone { set; get; }
        [Display(Name = "Role")]
        public string UserRole { set; get; }
        [Display(Name ="Pword")]
        [DataType(DataType.Password)]

        public string UserPassword { set; get; }
        public int NumOFProject { set; get; }
        public string Qualifications { set; get; }
        public IList<UserProject> userProjects { get; set; }
    }
}