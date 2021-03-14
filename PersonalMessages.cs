using System;
using System.Collections.Generic;

namespace Blog
{
    public class PersonalMessages
    {
        public int Id { get; set; }
        
        public DateTime PublicationTime { get; set; }
        
        public UserData Source { get; set; }
        
        public UserData Destination { get; set; }
        
        public string Text { get; set; }
        
        public string ImagePath { get; set; }
    }
}