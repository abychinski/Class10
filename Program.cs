using Castle.Core.Internal;
using Class10.Models;

namespace Class10
{
    class Program
    {
        static void Main(string[] args)
        {
            var choice = "";
            do
            {
                Console.WriteLine("(1) Display Blogs  \n(2) Add Blog \n(3) Display Posts  \n(4) Add Post");
                choice = Console.ReadLine().ToLower();

                if (choice == "2")
                {
                    Console.WriteLine("Enter Blog name: ");
                    var blogName = Console.ReadLine();
                    if (string.IsNullOrEmpty(blogName))
                    {
                        Console.WriteLine("Blog name cannot be empty! Enter Blog name: ");
                        blogName = Console.ReadLine();
                    }

                    var blog = new Blog();
                    blog.Name = blogName;

                    using (var context = new BlogContext())
                    {
                        context.Add(blog);
                        context.SaveChanges();
                    }
                }

                else if (choice == "1")
                {
                    using (var context = new BlogContext())
                    {
                        Console.WriteLine("Here is the list of blogs");
                        foreach (var b in context.Blogs)
                        {
                            Console.WriteLine($"Blog: {b.BlogId}) {b.Name}");
                        }
                    }
                }

                else if (choice == "4")
                {
                    var post = new Post();
                    Console.WriteLine("Enter your Post title");
                    var postTitle = Console.ReadLine();
                    if (string.IsNullOrEmpty(postTitle))
                    {
                        Console.WriteLine("Post Title cannot be empty! Enter Post Title: ");
                        postTitle = Console.ReadLine();
                    }

                    post.Title = postTitle;

                    Console.WriteLine("Enter Post Content: ");
                    var postContent = Console.ReadLine();
                    if (string.IsNullOrEmpty(postContent))
                    {
                        Console.WriteLine("Content cannot be empty! Enter Post Content: ");
                        postContent = Console.ReadLine();
                    }
                    post.Content = postContent;

                    using (var context = new BlogContext())
                    {

                        Console.WriteLine("Enter BlogId of Post: ");
                    }
                    var blId = Console.ReadLine();
                    if (string.IsNullOrEmpty(blId))
                    {
                        Console.WriteLine("BlogId cannot be empty! Enter BlogId: ");
                        blId = Console.ReadLine();
                    }
                    int blogId;
                    while (!int.TryParse(blId, out blogId))
                    {
                        Console.WriteLine("BlogId must be a number! Enter valid BlogId: ");

                    }
                    post.BlogId = blogId;
                    using (var context = new BlogContext())
                    {
                        var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
                        if (blog == null)
                        {
                            Console.WriteLine("Blog does not exist!");
                        }
                        else
                        {
                            context.Posts.Add(post);
                            context.SaveChanges();
                        }
                    }
                }

                else if (choice == "3")
                {

                    Console.WriteLine("Which Blog number do you want to see posts?: ");
                    var blId = Console.ReadLine();
                    if (string.IsNullOrEmpty(blId))
                    {
                        Console.WriteLine("BlogId cannot be empty! Enter BlogId: ");
                        blId = Console.ReadLine();
                    }

                    int blogId;
                    while (!int.TryParse(blId, out blogId))
                    {
                        Console.WriteLine("BlogId must be a number! Enter valid BlogId: ");
                    }
                    using (var context = new BlogContext())
                    {
                        var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
                        if (blog == null)
                        {
                            Console.WriteLine("Blog does not exist!");

                        }
                        else if (blog.Posts.IsNullOrEmpty())
                        {
                            Console.WriteLine($"There are no posts in {blog.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"Posts for Blog: {blog.Name}");
                            foreach (var post in blog.Posts)
                            {
                                Console.WriteLine($"\tPost: {post.PostId}) {post.Title}: {post.Content}");
                            }
                        }

                    }
                }
            } while (choice != "x");

        }
    }
}
