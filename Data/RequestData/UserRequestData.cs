namespace CompleteDeveloperNetwork.Data.Response
{
    public class UserRequestData
    {
        public string Username { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Skillsets { get; set; }
        public string? Hobby { get; set; }
    }
}
