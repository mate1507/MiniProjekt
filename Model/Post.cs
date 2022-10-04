namespace Model
{
    public class Post
    {
        public int PostId {get; set;}
        public string PostName {get; set;}
        public List<Post> Posts {get; set; }= new List<Post>();
    }
}