using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace CourseWork
{
    public class Hwid {

        // Cоль для MD5 хеша для идентификаторов железа
        private const string SALT = "T5'@[Den<Z\\y8f,D";

        // Ниже 2 метода, которые получают ID процессора и Материнской платы соответственно  

        public static byte[] getCpuId ()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            var mbsList = mbs.Get();
            var id = "";
            foreach (var mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            id += SALT;
            
            return MD5.Create().ComputeHash(new UTF8Encoding().GetBytes(id));
        }

        public static byte[] getBoardId ()
        {
            var mbs = new ManagementObjectSearcher("Select SerialNumber From Win32_BaseBoard");
            var mbsList = mbs.Get();
            var id = "";
            foreach (var mo in mbsList)
            {
                id = mo["SerialNumber"].ToString();
                break;
            }

            id += SALT;
            
            return MD5.Create().ComputeHash(new UTF8Encoding().GetBytes(id));
        }
    }
}