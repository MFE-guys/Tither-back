namespace Tither.Shared.ViewModels
{
    public class MemberViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MemberType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public DateTime IncludeDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Historic { get; set; }
    }
}
