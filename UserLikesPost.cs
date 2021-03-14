namespace Blog
{
    public class UserLikesPost
    {
        public int Id { get; set; }
        
        public UserData Liker { get; set; }
        
        public Post Post { get; set; }
    }
}