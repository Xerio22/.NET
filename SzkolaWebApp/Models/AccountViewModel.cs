

namespace SzkolaWebApp.Models
{
    public class AccountViewModel
    {
        public string ErrorMessage { get; set; }
        public string UsernameErrorMessage { get; set; }
        public string PasswordErrorMessage { get; set; }
        public UserCredentials Credentials { get; set; }
        public bool RegistrationComplete { get; set; }
    }
}