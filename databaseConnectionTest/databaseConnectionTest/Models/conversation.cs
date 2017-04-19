using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace databaseConnectionTest.Models
{
    public class conversation
    {
        public conversation()
        {
            
        }
        [Key]
        public int conversationID { get; set; }
        public int conversationAll { get; set; }
        [Key, ForeignKey("message")]
        public int conversationMessageID { get; set; }
        public DateTime conversationDate { get; set; }
        public bool conversationIsRead { get; set; }
    }
}