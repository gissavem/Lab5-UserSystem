﻿using System;
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
            string userName = userNameBox.Text;
            string userEmail = userEmailBox.Text;
            User user = new User(userName, userEmail);
            users.Add(user);
        }

       
        private void UserListBoxInitialized(object sender, EventArgs e)
        {
            userListBox.ItemsSource = users;
            userListBox.DisplayMemberPath = "UserName";
        }

      
        private void AdminListBoxInitialized(object sender, EventArgs e)
        {
            adminListBox.ItemsSource = admins;
            userListBox.DisplayMemberPath = "UserName";
        }
    }
}
