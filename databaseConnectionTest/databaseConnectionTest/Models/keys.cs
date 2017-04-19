using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace databaseConnectionTest.Models
{
    public class keys
    {
        public keys()
        {
            
        }
        [Key]
        public int keysID { get; set; }
        [Key, ForeignKey("login")]
        public int keysLoginID { get; set; }
        public int keysPublic { get; set; }
        public int keysPrivate { get; set; }
        public int keysAES { get; set; }
    }
}