using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.Clear();
        Console.WriteLine("Hello World! This is the YouTubeVideos Project.\n");

        // Create comments for video 1
        List<Comment> video1Comments = new List<Comment>
        {
            new Comment("TechFan123", "Great explanation! This really helped me understand the concept."),
            new Comment("CodeNewbie", "Thanks for the tutorial, very clear and easy to follow."),
            new Comment("DevMaster", "Could you make a follow-up video on advanced topics?"),
        };

        // Create comments for video 2
        List<Comment> video2Comments = new List<Comment>
        {
            new Comment("FitnessGuru", "Amazing workout! Felt the burn in all the right places."),
            new Comment("HealthyLiving", "Perfect for beginners. Love the modifications you showed."),
            new Comment("GymRat2024", "Been doing this routine for a week, already seeing results!"),
            new Comment("WellnessJourney", "Your form tips are so helpful. Thanks for the detailed explanations.")
        };

        // Create comments for video 3
        List<Comment> video3Comments = new List<Comment>
        {
            new Comment("Foodie4Life", "This recipe looks incredible! Definitely trying this weekend."),
            new Comment("ChefInTraining", "Love how you explain each step. Makes cooking less intimidating."),
            new Comment("KitchenNovice", "Finally, a recipe I can actually follow. Thank you!"),
            new Comment("CulinaryArtist", "Your knife skills are amazing. Any tips for improving technique?")
        };

        // Create comments for video 4
        List<Comment> video4Comments = new List<Comment>
        {
            new Comment("Wanderlust2024", "Your travel tips are always so practical. Thanks for sharing!"),
            new Comment("AdventureSeeker", "How much did this trip cost approximately?"),
            new Comment("ExploreMore", "The cinematography in this video is absolutely beautiful.")
        };

        // Create the videos
        Video video1 = new Video("Learn C# in 30 Minutes", "CodeAcademy Pro", 1932, video1Comments);
        Video video2 = new Video("Full Body HIIT Workout", "FitLife Channel", 5453, video2Comments);
        Video video3 = new Video("Perfect Homemade Pizza Recipe", "Chef's Kitchen", 749, video3Comments);
        Video video4 = new Video("Exploring Hidden Gems in Iceland", "Travel Diaries", 1354, video4Comments);

        // Put all videos in a list
        List<Video> videoList = new List<Video> { video1, video2, video3, video4 };

        // Iterate through each video in the list and present its information and its comments info
        foreach (Video video in videoList)
        {
            Console.WriteLine(video.GetTitle());
            Console.WriteLine($"Author: {video.GetAuthor()} | Length: {video.GetFormattedLength()}");
            Console.WriteLine($"The video has {video.CountComment()} comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"{comment.GetCommenterName()}\n{comment.GetCommentText()}");
            }
            Console.WriteLine();
        }
    }
}