using InitialProject.Context;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View.Guest;
using InitialProject.View.Owner;
using InitialProject.View.Tourist;
using InitialProject.View.Guide;
using InitialProject.View.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var userRepository = new UserRepository(db);
                var loginService = new LoginService(userRepository);

                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;

                int userId = loginService.FindUser(username, password, out int userType);

                if (userId != 0)
                {
                    switch (userType)
                    {
                        case 0:
                            // Owner
                            /*
                            RatingWindow newWindow = new RatingWindow(userId);
                            newWindow.Show();
                            Close();
                            */
                            ErrorMessageTextBlock.Text = "Owner";
                            break;
                        case 1:
                            // Guest
                            /*
                            GuestWindow guestWindow = new GuestWindow(userId);
                            guestWindow.Show();
                            Close();
                            break;
                            */
                        case 2:
                            // Tourist

                            SearchTourist newWindow = new SearchTourist(userId);
                            newWindow.Show();
                            Close();
                            
                            ErrorMessageTextBlock.Text = "Tourist";
                            break;
                        case 3:
                            case 3:
                            //GUIDE 
                            
                            HomePage newWindow = new HomePage(userId);
                            newWindow.Show();
                            Close();

                            ErrorMessageTextBlock.Text = "Guide";
                            break;
                        default:
                            ErrorMessageTextBlock.Text = "Unknow user type.";
                            break;
                    }
                }
                else
                {
                    ErrorMessageTextBlock.Text = "Invalid username or password.";
                }
            }
        }
    }
}
