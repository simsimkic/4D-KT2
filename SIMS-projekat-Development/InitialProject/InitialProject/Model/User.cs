using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace InitialProject.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public int Age { get; set; }

        public User() { }

        public User(string username, string password, UserType userType)
        {
            Username = username;
            Password = password;
            UserType = userType;
        }

    }
}
