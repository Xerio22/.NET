

namespace SzkolaWebApp.Models
{
    public class ContentEditViewModel : IAuthentication
    {
        public string Content { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsUserAuthenticated { get; set; }
    }
}