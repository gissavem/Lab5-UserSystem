using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5_UserSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        ObservableCollection<User> admins = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void AddUserButtonClick(object sender, RoutedEventArgs e)
        {
            string userName = userNameBox.Text.Trim();
            string userEmail = userEmailBox.Text.Trim();
            if (userName == null || userName == "" || userEmail == null || userEmail == "")
                return;
            User user = new User(userName, userEmail);
            users.Add(user);
            userNameBox.Text = "";
            userEmailBox.Text = "";
        }

       
        private void UserListBoxInitialized(object sender, EventArgs e)
        {
            userListBox.ItemsSource = users;
            userListBox.DisplayMemberPath = "UserName";
        }

      
        private void AdminListBoxInitialized(object sender, EventArgs e)
        {
            adminListBox.ItemsSource = admins;
            adminListBox.DisplayMemberPath = "UserName";
        }
        private void PromoteUserToAdminButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (User user in users)
            {
                if (user.Equals(userListBox.SelectedItem))
                {
                    user.IsAdmin = true;
                    users.Remove(user);
                    admins.Add(user);
                    userInfoLabel.Content = "";

                    return;
                }
            }
        }

        private void DemoteAdminToUserButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (User user in admins)
            {
                if (user.Equals(adminListBox.SelectedItem))
                {
                    user.IsAdmin = false;
                    admins.Remove(user);
                    users.Add(user);
                    userInfoLabel.Content = "";

                    return;
                }
            }
        }

        private void UserListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userListBox.SelectedItem == null)
            {
                promoteUserToAdminButton.IsEnabled = false;
                removeUserButton.IsEnabled = false;
                changeUserDetailsButton.IsEnabled = false;

            }
            else
            {
                adminListBox.UnselectAll();
                promoteUserToAdminButton.IsEnabled = true;
                removeUserButton.IsEnabled = true;
                changeUserDetailsButton.IsEnabled = true;
                User selectedUser = (User)userListBox.SelectedItem;
                string userName = selectedUser.UserName;
                string userEmail = selectedUser.UserEmail;
                bool isAdmin = selectedUser.IsAdmin;
                userInfoLabel.Content = $"Username: {userName}\nUser email: {userEmail}\nIs an admin: {isAdmin}";

            }
        }

        private void AdminListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (adminListBox.SelectedItem == null)
            {
                demoteAdminToUserButton.IsEnabled = false;
                removeUserButton.IsEnabled = false;
                changeUserDetailsButton.IsEnabled = false;

            }
            else
            {
                userListBox.UnselectAll();
                demoteAdminToUserButton.IsEnabled = true;
                removeUserButton.IsEnabled = true;
                changeUserDetailsButton.IsEnabled = true;
                User selectedUser = (User)adminListBox.SelectedItem;
                string userName = selectedUser.UserName;
                string userEmail = selectedUser.UserEmail;
                bool isAdmin = selectedUser.IsAdmin;
                userInfoLabel.Content = $"Username: {userName}\nUser email: {userEmail}\nIs an admin: {isAdmin}";
            }
        }

        private void ChangeUserDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            string userName = userNameBox.Text.Trim();
            string userEmail = userEmailBox.Text.Trim();
            if (userName == null || userName == "" || userEmail == null || userEmail == "")
                return;

            if (userListBox.SelectedItem != null)
            {
                var selectedUser = (User)userListBox.SelectedItem;
                selectedUser.UserName = userName;
                selectedUser.UserEmail = userEmail;
                users[users.IndexOf((User)userListBox.SelectedItem)] = selectedUser;
                userListBox.Items.Refresh();                
            }
            else if (adminListBox.SelectedItem != null)
            {
                var selectedUser = (User)adminListBox.SelectedItem;
                selectedUser.UserName = userName;
                selectedUser.UserEmail = userEmail;
                admins[admins.IndexOf((User)adminListBox.SelectedItem)] = selectedUser;
                adminListBox.Items.Refresh();
            }
            userNameBox.Text = "";
            userEmailBox.Text = "";
            userInfoLabel.Content = "";
            userListBox.UnselectAll();
            adminListBox.UnselectAll();

        }
        private void RemoveUserButtonClick(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                var selectedUser = (User)userListBox.SelectedItem;
                users.Remove(selectedUser);
                userInfoLabel.Content = "";
            }
            if (adminListBox.SelectedItem != null)
            {
                var selectedUser = (User)adminListBox.SelectedItem;
                admins.Remove(selectedUser);
                userInfoLabel.Content = "";
            }
        }
    }
}
