using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace com.etak.core.operation.util
{
    /// <summary>
    /// Utility class to calculate MD5 hashes
    /// </summary>
    public static class MD5Utility
    {
        /// <summary>
        /// Calculates the MD5 Hash of a byte []
        /// </summary>
        /// <param name="original">the source of data to calculate the hash</param>
        /// <returns>the MD5 Hash</returns>
        public static string ComputeHash(byte[] original)
        {
            if (original == null)
                return string.Empty;

            if (original.Length == 0)
                return string.Empty;

            byte[] hash = new byte[0];

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                hash = md5.ComputeHash(original);
            }

            string hashStr = string.Empty;
            for (int i = 0; i < hash.Length; i++)
                hashStr += hash[i].ToString("x").PadLeft(2, '0');

            return hashStr;
        }

        /// <summary>
        /// Calculated the MD5 Hash of a file
        /// </summary>
        /// <param name="fileName">the path to the file</param>
        /// <returns>the MD5 Hash</returns>
        public static string ComputeHashOfFile(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException();

            if (fileName.Trim().Length == 0)
                throw new ArgumentNullException();

            if (!File.Exists(fileName))
                throw new ArgumentException("File isn't exist: " + fileName);

            FileStream fs = null;
            BinaryReader br = null;
            byte[] buf = new byte[0];
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                buf = br.ReadBytes((int)fs.Length);
            }
            finally
            {
                if (br != null)
                    br.Close();

                if (fs != null)
                    fs.Close();
            }

            return ComputeHash(buf);
        }

        /// <summary>
        /// Calculates the MD5 hash of an string
        /// </summary>
        /// <param name="original">the string to be hashed</param>
        /// <returns>the MD5 hash of the string</returns>
        public static string ComputeHash(string original)
        {
            if (original == null)
                throw new ArgumentNullException();

            byte[] b =
                Encoding.ASCII.GetBytes(original);

            return ComputeHash(b);
        }
    }
}

