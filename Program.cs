using System;

namespace LIBRARYAPI
{
    using System;
    using System.Linq;

    class Program
    {
        static DataContext dataContext = new DataContext();
        static UserEngine userEngine = new UserEngine(dataContext);
        static ContentEngine contentEngine = new ContentEngine(dataContext);

        static void Main(string[] args)
        {
            Console.Title = typeof(Program).Name;
            DisplayUsage();
            Run();
        }

        static void DisplayUsage()
        {
            string usage = @"
Usage:
    createauthor <authorName>
    createreader <readerName>
    createpost <authorName> <content>
    publishpost <authorName> <postIndex>
    deletepost <authorName> <postIndex>
    followtag <readerName> <tag>
    followauthor <readerName> <authorName>
    getfollowedposts <readerName>
    showauthors
    showposts <authorName>
    showreaders
";
            WriteToConsole(usage);
        }

        static void Run()
        {
            while (true)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;

                try
                {
                    // Execute the command:
                    string result = Execute(consoleInput);

                    // Write out the result:
                    WriteToConsole(result);
                }
                catch (Exception ex)
                {
                    // OOPS! Something went wrong - Write out the problem:
                    WriteToConsole(ex.Message);
                }
            }
        }

        static string Execute(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length == 0) return "Invalid command";

            switch (parts[0].ToLower())
            {
                case "createauthor":
                    if (parts.Length < 2) return "Usage: createauthor <authorName>";
                    userEngine.RegisterAuthor(parts[1]);
                    return $"Author {parts[1]} created.";

                case "createreader":
                    if (parts.Length < 2) return "Usage: createreader <readerName>";
                    userEngine.RegisterReader(parts[1]);
                    return $"Reader {parts[1]} created.";

                case "createpost":
                    if (parts.Length < 3) return "Usage: createpost <authorName> <content>";
                    var author = userEngine.GetAuthor(parts[1]);
                    if (author == null) return $"Author {parts[1]} not found.";
                    contentEngine.CreatePost(author, string.Join(" ", parts.Skip(2)));
                    return $"Post created by {parts[1]}.";

                case "publishpost":
                    if (parts.Length < 3) return "Usage: publishpost <authorName> <postIndex>";
                    author = userEngine.GetAuthor(parts[1]);
                    if (author == null) return $"Author {parts[1]} not found.";
                    if (!int.TryParse(parts[2], out int postIndex) || postIndex < 0 || postIndex >= author.Posts.Count)
                        return "Invalid post index.";
                    var postToPublish = author.Posts[postIndex];
                    contentEngine.PublishPost(author, postToPublish);
                    return $"Post {postIndex} published by {parts[1]}.";

                case "deletepost":
                    if (parts.Length < 3) return "Usage: deletepost <authorName> <postIndex>";
                    author = userEngine.GetAuthor(parts[1]);
                    if (author == null) return $"Author {parts[1]} not found.";
                    if (!int.TryParse(parts[2], out postIndex) || postIndex < 0 || postIndex >= author.Posts.Count)
                        return "Invalid post index.";
                    var postToDelete = author.Posts[postIndex];
                    contentEngine.DeletePost(author, postToDelete);
                    return $"Post {postIndex} deleted by {parts[1]}.";

                case "followtag":
                    if (parts.Length < 3) return "Usage: followtag <readerName> <tag>";
                    var reader = userEngine.GetReader(parts[1]);
                    if (reader == null) return $"Reader {parts[1]} not found.";
                    reader.FollowTag(parts[2]);
                    return $"Reader {parts[1]} followed tag {parts[2]}.";

                case "followauthor":
                    if (parts.Length < 3) return "Usage: followauthor <readerName> <authorName>";
                    reader = userEngine.GetReader(parts[1]);
                    author = userEngine.GetAuthor(parts[2]);
                    if (reader == null) return $"Reader {parts[1]} not found.";
                    if (author == null) return $"Author {parts[2]} not found.";
                    reader.FollowAuthor(author);
                    return $"Reader {parts[1]} followed author {parts[2]}.";

                case "getfollowedposts":
                    if (parts.Length < 2) return "Usage: getfollowedposts <readerName>";
                    reader = userEngine.GetReader(parts[1]);
                    if (reader == null) return $"Reader {parts[1]} not found.";
                    var followedPosts = contentEngine.GetFollowedPosts(reader);
                    return followedPosts.Any()
                        ? "Followed Posts:\n" + string.Join("\n", followedPosts.Select(p => $"- {p.Content}"))
                        : "No followed posts found.";

                case "showauthors":
                    var authors = dataContext.Authors;
                    return authors.Any()
                        ? "Authors:\n" + string.Join("\n", authors.Select(a => $"- {a.Name}"))
                        : "No authors found.";

                case "showposts":
                    if (parts.Length < 2) return "Usage: showposts <authorName>";
                    author = userEngine.GetAuthor(parts[1]);
                    if (author == null) return $"Author {parts[1]} not found.";
                    return author.Posts.Any()
                        ? $"Posts by {author.Name}:\n" + string.Join("\n", author.Posts.Select((p, i) => $"{i}: {p.Content}"))
                        : $"No posts found for author {parts[1]}.";

                case "showreaders":
                    var readers = dataContext.Readers;
                    return readers.Any()
                        ? "Readers:\n" + string.Join("\n", readers.Select(r => $"- {r.Name}"))
                        : "No readers found.";
               
                case "assigntag":
                    if (parts.Length < 4) return "Usage: assigntag <authorName> <postIndex> <tag>";
                    author = userEngine.GetAuthor(parts[1]);
                    if (author == null) return $"Author {parts[1]} not found.";
                    if (!int.TryParse(parts[2], out postIndex) || postIndex < 0 || postIndex >= author.Posts.Count)
                        return "Invalid post index.";
                    var postToAssignTag = author.Posts[postIndex];
                    contentEngine.AssignTagToPost(postToAssignTag, parts[3]);
                    return $"Tag '{parts[3]}' assigned to post {postIndex}.";

                default:
                    return "Unknown command";
            }
        }

        public static void WriteToConsole(string message = "")
        {
            if (message.Length > 0)
            {
                Console.WriteLine(message);
            }
        }

        const string _readPrompt = "console> ";
        public static string ReadFromConsole(string promptMessage = "")
        {
            // Show a prompt, and get input:
            Console.Write(_readPrompt + promptMessage);
            return Console.ReadLine();
        }
    }

}
