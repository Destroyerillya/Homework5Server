using System;
using System.Collections.Generic;


namespace Blog
{
    public class Group
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }
        
        public int QtyUsers { get; set; }
        
        public List<UserData> UserGroups { get; set; }
    }
}