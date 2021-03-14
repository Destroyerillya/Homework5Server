using System;
using System.Collections.Generic;

namespace Blog
{
    public class Post
    {
        public int Id { get; set; }
        
        public DateTime PublicationTime { get; set; }
        
        public string Title  { get; set; }
        
        public string Text { get; set; }
        
        public string ImagePath { get; set; }
        
        public UserData Author { get; set; } 
    }
}