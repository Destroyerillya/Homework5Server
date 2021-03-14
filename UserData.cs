using System;
using System.Collections.Generic;

namespace Blog
{
    public class UserData
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }
        
        public string Email { get; set;}
        
        public string Login { get; set; }
        
        public string PasswordHash { get; set; }
        
        public DateTime Birthday { get; set; }
        
        public List<Post> Posts { get; set; }
        
        public List<Group> Groups { get; set; }
    }
}