using System.ComponentModel.DataAnnotations;

namespace ProjectGuard.Models.Requests
{
    public class AuthModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
