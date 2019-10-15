using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
            InitializeUserListBox();
            InitializeAdminListBox();            
            addUserButton.Click += OnAddUserButtonClick;
            changeUserDetailsButton.Click += OnChangeUserDetailsButtonClick;
            promoteUserToAdminButton.Click += OnPromoteUserButtonClick;
            demoteAdminToUserButton.Click += OnDemoteAdminButtonClick;
            removeUserButton.Click += OnRemoveUserButtonClick;
            adminListBox.SelectionChanged += AdminListBoxSelectionChanged;
            userListBox.SelectionChanged += UserListBoxSelectionChanged;
        }
        private void ClearTextBoxes()
        {
            userNameBox.Text = "";
            userEmailBox.Text = "";
            userInfoLabel.Content = "";
            userListBox.UnselectAll();
            adminListBox.UnselectAll();
        }
        private void EnableButtons(bool enableButton)
        {
            if (!enableButton)
            {
                demoteAdminToUserButton.IsEnabled = enableButton;
                promoteUserToAdminButton.IsEnabled = enableButton;
                removeUserButton.IsEnabled = enableButton;
                changeUserDetailsButton.IsEnabled = enableButton;
                return;
            }
            if (userListBox.SelectedItem != null)
            {
                promoteUserToAdminButton.IsEnabled = enableButton;
            }
            else if (adminListBox.SelectedItem != null)
            {
                demoteAdminToUserButton.IsEnabled = enableButton;
            }            
            removeUserButton.IsEnabled = enableButton;
            changeUserDetailsButton.IsEnabled = enableButton;
        }
        private void PrintUserInfo(User selectedUser)
        {
            string userName = selectedUser.UserName;
            string userEmail = selectedUser.UserEmail;
            bool isAdmin = selectedUser.IsAdmin;
            userInfoLabel.Content = $"Username: {userName}\nUser email: {userEmail}\nIs an admin: {isAdmin}";
        }
        private void InitializeUserListBox()
        {
            userListBox.ItemsSource = users;
            userListBox.DisplayMemberPath = "UserName";
        }
        private void InitializeAdminListBox()
        {
            adminListBox.ItemsSource = admins;
            adminListBox.DisplayMemberPath = "UserName";
        }
        private void OnAddUserButtonClick(object sender, RoutedEventArgs e)
        {
            string userName = userNameBox.Text.Trim();
            string userEmail = userEmailBox.Text.Trim();
            if (userName == null || userName == "" || userEmail == null || userEmail == "")
                return;
            User user = new User(userName, userEmail);
            users.Add(user);
            ClearTextBoxes();
        }      
        private void OnPromoteUserButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (User user in users)
            {
                if (user.Equals(userListBox.SelectedItem))
                {
                    user.IsAdmin = true;
                    users.Remove(user);
                    admins.Add(user);
                    ClearTextBoxes();
                    return;
                }
            }
        }
        private void OnDemoteAdminButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (User user in admins)
            {
                if (user.Equals(adminListBox.SelectedItem))
                {
                    user.IsAdmin = false;
                    admins.Remove(user);
                    users.Add(user);
                    ClearTextBoxes();
                    return;
                }
            }
        }
        private void OnChangeUserDetailsButtonClick(object sender, RoutedEventArgs e)
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
            ClearTextBoxes();
        }
        private void OnRemoveUserButtonClick(object sender, RoutedEventArgs e)
        {
            if (userListBox.SelectedItem != null)
            {
                var selectedUser = (User)userListBox.SelectedItem;
                users.Remove(selectedUser);
                ClearTextBoxes();
            }
            if (adminListBox.SelectedItem != null)
            {
                var selectedUser = (User)adminListBox.SelectedItem;
                admins.Remove(selectedUser);
                ClearTextBoxes();
            }
        }
        private void UserListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userListBox.SelectedItem == null)
            {
                EnableButtons(false);
            }
            else
            {
                adminListBox.UnselectAll();
                EnableButtons(true);
                User selectedUser = (User)userListBox.SelectedItem;
                PrintUserInfo(selectedUser);
            }
        }
        private void AdminListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (adminListBox.SelectedItem == null)
            {
                EnableButtons(false);               
            }
            else
            {
                userListBox.UnselectAll();
                EnableButtons(true);
                User selectedUser = (User)adminListBox.SelectedItem;
                PrintUserInfo(selectedUser);
            }
        }
    }
}
