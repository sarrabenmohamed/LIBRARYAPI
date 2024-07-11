using System;
using System.Collections.Generic;

public class Reader
{
    public string Name { get; set; }
    public List<string> FollowedTags { get; set; }
    public List<Author> FollowedAuthors { get; set; }

    public Reader(string name)
    {
        Name = name;
        FollowedTags = new List<string>();
        FollowedAuthors = new List<Author>();
    }

    public void FollowTag(string tag)
    {
        FollowedTags.Add(tag);
    }

    public void FollowAuthor(Author author)
    {
        FollowedAuthors.Add(author);
    }
}
