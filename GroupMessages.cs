using System;
using System.Collections.Generic;


namespace Blog
{
    public class GroupMessages
    {
        public int Id { get; set; }
        
        public Group GroupId { get; set; }
        
        public UserData Source { get; set; }
        
        public DateTime PublicationTime { get; set; }
        
        public string Text { get; set; }
        
        public string ImagePath { get; set; }
    }
}