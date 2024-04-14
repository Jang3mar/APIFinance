namespace api.Models
{
    public class UserToken
    {
        public User user { get; set; }
        public string token { get; set; }

        public UserToken(User user, string token)
        {
            this.user = user;
            this.token = token;
        }
    }
}
