using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YandS.DAL
{
    public static class ExtensionMethods
    {
        public static Int16? ToInt16(this object value)
        {
            if (value == null) return 0;
            return Convert.ToInt16(value);
        }

        public static Int32? ToInt32(this object value)
        {
            if (value == null) return 0;
            return Convert.ToInt32(value);
        }

        public static Int64? ToInt64(this object value)
        {
            if (value == null) return 0;
            return Convert.ToInt64(value);
        }

        public static decimal? ToDecimal(this object value)
        {
            if (value == null) return 0;
            return Convert.ToDecimal(value);
        }

        public static double? ToDouble(this object value)
        {
            if (value == null) return 0;
            return Convert.ToDouble(value);
        }

        public static DateTime? ToDateTime(this object value)
        {
            if (value == null) return null;
            return Convert.ToDateTime(value);
        }

        public static int GetQuarter(this DateTime date)
        {
            //date = date.AddDays( - 10);
            if (date.Month >= 4 && date.Month <= 6)
                return 2;
            else if (date.Month >= 7 && date.Month <= 9)
                return 3;
            else if (date.Month >= 10 && date.Month <= 12)
                return 4;
            else
                return 1;
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static List<T> DataTableToList<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    if (properties.PropertyType.FullName.Contains("System.Int32"))
                    {
                        properties.SetValue(instanceOfT, dataRow[properties.Name].ToInt32(), null);
                    }
                    else if (properties.PropertyType.FullName.Contains("System.Double"))
                    {
                        properties.SetValue(instanceOfT, dataRow[properties.Name].ToDouble(), null);
                    }
                    else if (properties.PropertyType.FullName.Contains("System.Boolean"))
                    {
                        properties.SetValue(instanceOfT, Convert.ToBoolean(dataRow[properties.Name]), null);
                    }
                    else if (properties.PropertyType.FullName.Contains("System.Byte[]"))
                    {
                        properties.SetValue(instanceOfT, HttpServerUtility.UrlTokenDecode(dataRow[properties.Name].ToString()), null);
                    }
                    else if (properties.PropertyType.FullName.Contains("System.Decimal"))
                    {
                        properties.SetValue(instanceOfT, dataRow[properties.Name].ToDecimal(), null);
                    }
                    else if (properties.PropertyType.FullName.Contains("System.DateTime"))
                    {
                        properties.SetValue(instanceOfT, dataRow[properties.Name].ToDateTime(), null);
                    }
                    else
                    {
                        if (properties.Name == "UIDENT")
                            properties.SetValue(instanceOfT, HttpServerUtility.UrlTokenEncode((byte[])dataRow[properties.Name]), null);
                        else
                            properties.SetValue(instanceOfT, dataRow[properties.Name].ToString(), null);
                    }

                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }
        public static T ToObject<T>(this DataTable dt)
        {
            var list = dt.DataTableToList<T>();
            if (list.Any())
                return list.FirstOrDefault();
            else
                return Activator.CreateInstance<T>();
        }
        public static IEnumerable<Dictionary<string, object>> ToDictionary(this DataTable table)
        {
            string[] columns = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            IEnumerable<Dictionary<string, object>> result = table.Rows.Cast<DataRow>()
                    .Select(dr => columns.ToDictionary(c => c, c => dr[c]));
            return result;
        }
        public static bool IsUrl(this string str)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(str, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        public static string IntMonthToString(int Month)
        {
            switch (Month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "";
            }
        }
        public static bool In(this string @this, params string[] strings)
        {
            return strings.Contains(@this);
        }
        public static bool IsTranslated(this string str)
        {
            foreach (char ch in str)
            {
                if (!(ch >= 'A' && ch <= 'Z') && !(ch >= 'a' && ch <= 'z'))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Uses regex '\b' as suggested in https://stackoverflow.com/questions/6143642/way-to-have-string-replace-only-hit-whole-words
        /// </summary>
        /// <param name="original"></param>
        /// <param name="wordToFind"></param>
        /// <param name="replacement"></param>
        /// <param name="regexOptions"></param>
        /// <returns></returns>
        public static string ReplaceWholeWord(this string original, string wordToFind, string replacement, System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.None)
        {
            string pattern = string.Format(@"\b{0}\b", wordToFind);
            string ret = System.Text.RegularExpressions.Regex.Replace(original, pattern, replacement, regexOptions);
            return ret;
        }

        #region "-----------------------------------------------------------Methods-----------------------------------------------------------------------------------------------------------------"
        /// <summary>
        /// The key
        /// </summary>
        private static byte[] key = { };
        public static string ENCRYPT_KEY = "EMS";

        /// <summary>
        /// The iv
        /// </summary>

        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        /// <summary>
        /// Decrypts the specified string automatic decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string automatic decrypt.</param>
        /// <param name="sEncryptionKey">The arguments encryption key.</param>
        /// <returns></returns>
        public static string Decrypt(string stringToDecrypt)
        {
            string sEncryptionKey = ENCRYPT_KEY;
            if (sEncryptionKey.Length < 8)
            {
                int length = sEncryptionKey.Length + (8 - sEncryptionKey.Length);
                sEncryptionKey = sEncryptionKey.PadLeft(length, 'O');
            }

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                    stringToDecrypt = stringToDecrypt.Replace('_', '+');
                    stringToDecrypt = stringToDecrypt.Replace('-', '/');
                    stringToDecrypt = stringToDecrypt.Replace('$', '=');

                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        return encoding.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Encrypts the specified string automatic encrypt.
        /// </summary>
        /// <param name="stringToEncrypt">The string automatic encrypt.</param>
        /// <param name="SEncryptionKey">The arguments encryption key.</param>
        /// <returns></returns>
        public static string Encrypt(string stringToEncrypt)
        {
            string SEncryptionKey = ENCRYPT_KEY;
            string XYZ = string.Empty;
            try
            {
                if (SEncryptionKey.Length < 8)
                {
                    int length = SEncryptionKey.Length + (8 - SEncryptionKey.Length);
                    SEncryptionKey = SEncryptionKey.PadLeft(length, 'O');
                }
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();

                        string x = Convert.ToBase64String(ms.ToArray());

                        x = x.Replace('+', '_');
                        x = x.Replace('/', '-');
                        x = x.Replace('=', '$');

                        XYZ = x.ToString();
                    }
                }
                return XYZ;



            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Creates the random key.
        /// </summary>
        /// <param name="PasswordLength">Length of the password.</param>
        /// <returns></returns>
        public static string CreateRandomKey(int PasswordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public static T IsNull<T>(this T DefaultValue, T InsteadValue)
        {

            object obj = "kk";

            if ((object)DefaultValue == DBNull.Value)
            {
                obj = null;
            }

            if (obj == null || DefaultValue == null || DefaultValue.ToString() == "")
            {
                return InsteadValue;
            }
            else
            {
                return DefaultValue;
            }

        }

        #endregion


    }

}
