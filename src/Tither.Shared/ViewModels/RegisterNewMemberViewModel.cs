namespace Tither.Shared.ViewModels
{
    public class RegisterNewMemberViewModel
    {
        public RegisterNewMemberViewModel(string message) 
            => Message = message;

        public string Message { get; set;}
    }
}
