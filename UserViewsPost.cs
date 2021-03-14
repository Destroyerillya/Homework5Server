namespace Blog
{
    public class UserViewsPost
    {
        public int Id { get; set; }
        
        public UserData Viewer { get; set; }
        
        public Post Post { get; set; }
    }
}