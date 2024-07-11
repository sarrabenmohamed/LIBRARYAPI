using System;
using System.Collections.Generic;

public class Post
{
    public enum PostStatus { Draft, Published }

    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? PublishDate { get; set; }
    public PostStatus Status { get; set; }
    public List<string> Tags { get; set; }

    public Post(string content)
    {
        Content = content;
        CreationDate = DateTime.Now;
        Status = PostStatus.Draft;
        Tags = new List<string>();
    }

    public void Publish()
    {
        if (Status == PostStatus.Draft)
        {
            Status = PostStatus.Published;
            PublishDate = DateTime.Now;
        }
        else
        {
            throw new InvalidOperationException("Only draft posts can be published.");
        }
    }
    public void AssignTag(string tag)
    {
        if (!Tags.Contains(tag))
        {
            Tags.Add(tag);
        }
    }
}
