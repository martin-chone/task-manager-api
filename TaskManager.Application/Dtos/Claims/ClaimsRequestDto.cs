namespace TaskManager.Application.Dtos.Claims
{
    public class ClaimsRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
