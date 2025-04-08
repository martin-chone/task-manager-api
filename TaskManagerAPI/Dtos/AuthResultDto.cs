namespace TaskManagerAPI.Dtos
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
    }
}
