using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace databaseConnectionTest.Models
{
    public class ChatModelContext : DbContext
    {
        public DbSet<allConversations> AllConvs { get; set; }
        public DbSet<conversation> convs{ get; set; }
        public DbSet<keys> Keyses{ get; set; }
        public DbSet<login> Logins{ get; set; }
        public DbSet<message> Messages{ get; set; }
    }
}