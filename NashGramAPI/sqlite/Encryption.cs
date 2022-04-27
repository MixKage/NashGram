using System.Security.Cryptography;

namespace NashGramAPI.sqlite
{
    public static class Encryption
    {
        public static string Encr(string input) 
        {
            byte[] byteIsh = new System.Text.UTF8Encoding().GetBytes(input);
            SHA256 shaMan = new SHA256Managed();
            byte[] byteshaMan = shaMan.ComputeHash(byteIsh);
            string result = BitConverter.ToString(byteshaMan);
            result = result.ToLower().Replace("-", string.Empty); 
            return result;
        }


    }
}
