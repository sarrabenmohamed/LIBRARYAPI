using System;
using System.Collections.Generic;

public class Author
{
    public string Name { get; set; }

    public List<Post> Posts { get; set; }

    public Author(string name)
    {
        Name = name;
        Posts = new List<Post>();
    }
    public void DeletePost(Post post)
    {
        if (post.Status == Post.PostStatus.Draft)
        {
            Posts.Remove(post);
        }
        else
        {
            throw new InvalidOperationException("Cannot delete published posts.");
        }
    }
}
