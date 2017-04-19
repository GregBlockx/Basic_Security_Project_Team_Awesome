using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace databaseConnectionTest.Models
{
    public class message
    {
        [Key]
        public int messageID { get; set; }
        public string messageContent { get; set; }
        [Key, ForeignKey("conversation")]
        public int messageConvID { get; set; }
        public DateTime messageDate { get; set; }
    }
}