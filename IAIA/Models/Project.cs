using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("projects")]
    public class Project
    {
        [Key]
        public int ProjectID { set; get; }
        public string ProjectName { set; get; }
        public string ProjectDescription { set; get; }
        public int Projectstatus { set; get; }
        public string StartDate { set; get; }
        public string endData { set; get; }
        public int price { set; get; }
        public IList<UserProject> userProjects { get; set; }


    }
}