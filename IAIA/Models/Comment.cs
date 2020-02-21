using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key]
    public int commentID { set; get; }
    public int projectid { set; get; }
    public int pmid { set; get; }
    public string sentAt { set; get; }
    public string content { set; get; }
}
}