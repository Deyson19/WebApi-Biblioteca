
namespace WebApi_Helpers
{
    public static class RandomIsbn
    {
        private static Random random = new Random();

        public static string IsbnNuevo(string currentIsbn)
        {
            int cantidad = 4;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var complemento = new string(Enumerable.Repeat(chars, cantidad)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return $"{complemento}-{currentIsbn}";
        }
    }
}