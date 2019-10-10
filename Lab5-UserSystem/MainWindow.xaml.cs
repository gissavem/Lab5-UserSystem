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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5_UserSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> userList = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddUserButtonClick(object sender, RoutedEventArgs e)
        {
            string userName = userNameBox.Text;
            string userEmail = userEmailBox.Text;
            userList.Add(new User(userName, userEmail));
            userListBox.ItemsSource = userList;
            userListBox.Items.Refresh();
        }
    }
}
