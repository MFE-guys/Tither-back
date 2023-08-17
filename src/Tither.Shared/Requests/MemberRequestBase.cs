namespace Tither.Shared.Requests
{
    public abstract class MemberRequestBase
    {
        public MemberRequestBase(string name, string memberType, string email, string phone, bool status, string historic)
        {
            Name = name;
            MemberType = memberType;
            Email = email;
            Phone = phone;
            Status = status;
            Historic = historic;
        }

        public string Name { get; set; }
        public string MemberType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string Historic { get; set; }
    }
}
