using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace databaseConnectionTest.Models
{
    public class allConversations
    {
        public allConversations()
        {
            
        }
        [Key]
        public int allConverstionsID { get; set; }
        [Key, ForeignKey("login")]
        public int allConversationsLoginUserID1 { get; set; }
        [Key, ForeignKey("login")]
        public int allConversationsLoginUserID2 { get; set; }
    }
}