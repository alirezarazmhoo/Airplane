using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirAjencyy.Models
{
    public class AirAjencyyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AirAjencyyContext() : base("name=AirAjencyyContext")
        {
        }

		public System.Data.Entity.DbSet<AirAjencyy.Models.Country> Countries { get; set; }

		public System.Data.Entity.DbSet<AirAjencyy.Models.City> Cities { get; set; }

		public System.Data.Entity.DbSet<AirAjencyy.Models.AirPlane> AirPlanes { get; set; }

		public System.Data.Entity.DbSet<AirAjencyy.Models.PlaneTicket> PlaneTickets { get; set; }

		//public System.Data.Entity.DbSet<AirAjencyy.Models.PlaneTicket> PlaneTickets { get; set; }

		public System.Data.Entity.DbSet<AirAjencyy.Models.Factors> Factors { get; set; }

		public System.Data.Entity.DbSet<AirAjencyy.Models.AdminUser> AdminUser { get; set; }

		

	}
}
