using System.ComponentModel.DataAnnotations;

namespace SzkolaWebApp.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}