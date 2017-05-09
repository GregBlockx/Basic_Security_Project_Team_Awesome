using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace databaseConnectionTest.Models
{
    public class login
    {
        
        public login()
        {
           
        }
        [Key]
        public int loginID { get; set; }
        public string loginUsername { get; set; }
        public string loginPassword { get; set; }
        public string loginSalt { get; set; }
    }
}