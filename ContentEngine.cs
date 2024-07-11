using System;
using System.Collections.Generic;
using System.Linq;

public class ContentEngine
{
    private readonly DataContext _dataContext;

    public ContentEngine(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Post CreatePost(Author author, string content)
    {
        var post = new Post(content);
        author.Posts.Add(post);
        _dataContext.AddPost(post);
        return post;
    }

    public void PublishPost(Author author, Post post)
    {
        if (author.Posts.Contains(post))
        {
            post.Publish();
        }
        else
        {
            throw new InvalidOperationException("Post does not belong to Author.");
        }
    }
    public void AssignTagToPost(Post post, string tag)
    {
        post.AssignTag(tag);
    }

    public void DeletePost(Author author, Post post)
    {
        if (author.Posts.Contains(post))
        {
            if (post.Status == Post.PostStatus.Draft)
            {
                author.DeletePost(post);
                _dataContext.RemovePost(post);
            }
            else
            {
                throw new InvalidOperationException("Only draft posts can be deleted.");
            }
        }
        else
        {
            throw new InvalidOperationException("Post does not belong to Author.");
        }
    }

    public List<Post> GetPostsByTag(string tag)
    {
        return _dataContext.Posts.Where(p => p.Tags.Contains(tag)).ToList(); //does the post have to be published?
    }

    public List<Post> GetPostsByAuthor(Author author)
    {
        return author.Posts.Where(p => p.Status == Post.PostStatus.Published).ToList();
    }

    public List<Post> GetFollowedPosts(Reader reader)
    {
        var followedAuthorPosts = _dataContext.Posts
            .Where(p => reader.FollowedAuthors.Any(author => author.Posts.Contains(p)) && p.Status == Post.PostStatus.Published)
            .ToList();

        var followedTagPosts = _dataContext.Posts
            .Where(p => p.Tags.Any(tag => reader.FollowedTags.Contains(tag)) && p.Status == Post.PostStatus.Published)
            .ToList();

        return followedAuthorPosts.Union(followedTagPosts).ToList();
    }

}
