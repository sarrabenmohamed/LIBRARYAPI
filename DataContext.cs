using System;
using System.Collections.Generic;

public class DataContext
{
    public List<Post> Posts { get; set; } = new List<Post>();
    public List<Author> Authors { get; set; } = new List<Author>();
    public List<Reader> Readers { get; set; } = new List<Reader>();

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }

    public void RemovePost(Post post)
    {
        Posts.Remove(post);
    }

    public void AddAuthor(Author author)
    {
        Authors.Add(author);
    }

    public void AddReader(Reader reader)
    {
        Readers.Add(reader);
    }
}
