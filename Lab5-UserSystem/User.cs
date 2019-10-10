
namespace Lab5_UserSystem
{
    public class User
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdmin { get; set; }
        public User( string userName, string userEmail )
        {
            UserName = userName;
            UserEmail = userEmail;
            IsAdmin = false;
        }
    }
}
