

namespace SzkolaWebApp.Models
{
    public class LoginViewModel
    {
        public string ErrorMessage { get; set; }
        public UserCredentials Credentials { get; set; }
        public bool RegistrationComplete { get; set; }
    }
}