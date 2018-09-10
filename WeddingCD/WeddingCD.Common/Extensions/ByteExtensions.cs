using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Extensions
{
    /// <summary>
    /// Extension class for Byte
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Gets an hexadecimal string from a byte array.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>
        /// The hexadecimal string, for instance AABB00
        /// </returns>
        public static string GetHexadecimalStringFromBytes(this byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString().ToUpper();
        }

        /// <summary>
        /// Gets a byte array from an hexadecimal string.
        /// </summary>
        /// <param name="hexaData">The hexadecimal string.</param>
        /// <param name="expectedArraySize">Expected size of the array.</param>
        /// <returns>The byte array</returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid hexadecimal string;hexaData
        /// or
        /// Invalid hexadecimal string length;hexaData
        /// </exception>
        public static byte[] GetFromHexadecimalString(string hexaData, int expectedArraySize)
        {
            if (string.IsNullOrEmpty(hexaData))
            {
                if (expectedArraySize > 0)
                {
                    throw new ArgumentException("Invalid hexadecimal string", "hexaData");
                }

                return new byte[0];
            }

            if (hexaData.Length % 2 != 0 || hexaData.Length / 2 != expectedArraySize)
            {
                throw new ArgumentException("Invalid hexadecimal string length", "hexaData");
            }

            byte[] arr = new byte[expectedArraySize];
            for (int i = 0; i < expectedArraySize; i++)
            {
                arr[i] = (byte)((GetHexVal(hexaData[i << 1]) << 4) + GetHexVal(hexaData[(i << 1) + 1]));
            }

            return arr;
        }

        /// <summary>
        /// Returns the hexadecimal value associated with the specified char.
        /// </summary>
        /// <param name="hex">The specificied hexadecimal char.</param>
        /// <returns>The corresponding value from 0 to 15</returns>
        /// <exception cref="System.ArgumentException">The specified value is invalid</exception>
        private static int GetHexVal(char hex)
        {
            switch (hex)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;

                case 'a':
                case 'A':
                    return 10;

                case 'b':
                case 'B':
                    return 11;

                case 'c':
                case 'C':
                    return 12;

                case 'd':
                case 'D':
                    return 13;

                case 'e':
                case 'E':
                    return 14;

                case 'f':
                case 'F':
                    return 15;

                default:
                    throw new ArgumentException(string.Format("Invalid hexadecimal character found: {0}", hex), "hex");
            }
        }
    }
}