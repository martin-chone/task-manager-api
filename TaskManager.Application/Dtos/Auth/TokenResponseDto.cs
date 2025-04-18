namespace TaskManager.Application.Dtos.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
    }
}
