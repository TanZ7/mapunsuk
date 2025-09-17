using System.Collections.Generic;

namespace mapunsuk.Models
{
    public class PostManagementViewModel
    {
        public int TotalPosts { get; set; }
        public int UserPostsCount { get; set; }
        public int OtherPostsCount => TotalPosts - UserPostsCount;
        public IEnumerable<Post> UserPosts { get; set; }
    }
}