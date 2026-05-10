using System.Security.Cryptography;
using System.Text;

namespace Otimizador_PC_Web.Dados
{
    public class Utilidades
    {
        public static string EncriptarSenha(string senha)
        {

            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(senha));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }

    }
}
