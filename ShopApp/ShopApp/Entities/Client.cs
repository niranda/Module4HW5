using System;
using System.Collections.Generic;

namespace ShopApp.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime? OrderDate { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
