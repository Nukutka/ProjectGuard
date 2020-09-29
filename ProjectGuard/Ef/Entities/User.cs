namespace ProjectGuard.Ef.Entities
{
    public class User : BaseEntity
    {
        public User(string login, string hashPassword)
        {
            Login = login;
            HashPassword = hashPassword;
        }

        public User()
        {

        }

        public string Login { get; set; }
        public string HashPassword { get; set; }
    }
}
