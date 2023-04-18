using InitialProject.Serializer;
using System;

namespace InitialProject.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }

        public Comment() 
        { 
        }

        public Comment(DateTime creationTime, string text)
        {
            CreationTime = creationTime;
            Text = text;
        }
    }
}
