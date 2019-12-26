using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAIA.Models
{
    [Table("requests")]
    public class Requst
    {
        [Key]
    public int  requestID { set; get; }
    public int projectid  { set; get; }
    public int FromUser { set; get; }
    public int  ToUser  { set; get; }
    public int isAccepted { set; get; }
    public string sentAt { set; get; }
    }
}