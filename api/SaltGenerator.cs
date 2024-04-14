namespace FinanceAPI.Controllers
{
    public class SaltGenerator
    {
        public static string GenerateSalt()
        {
            string salt = "";
            string saltString = "1234567890qwertyuiopasdfghjklzxcvbnm";
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                salt += saltString[random.Next(0, saltString.Length - 1)];
            }
            return salt;
        }
    }
}
