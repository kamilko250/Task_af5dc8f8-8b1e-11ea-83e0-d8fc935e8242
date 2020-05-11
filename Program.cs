using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;


namespace Dev23
{
    class Program
    {
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        static void Main(string[] args)
        {
            XmlTextReader data = new XmlTextReader("data.xml");
            
                    int b = 0;
            
                while (data.Read())
                {
                    if (data.NodeType == XmlNodeType.Element)
                    {
                    if (data.Name == "data")
                        b++;

                    if (data.HasAttributes)
                    {
                        string sha = "";
                        string guid = "";
                        sha = data.GetAttribute("sha256");
                        guid = data.GetAttribute("guid");
                        string value = data.ReadElementContentAsString();
                        //Console.WriteLine(value);
                        byte[] bytevalue = StringToByteArray(value);
                        byte[] byteshavalue = StringToByteArray(sha);
                        using (SHA256 mysha256 = SHA256.Create())
                        {
                            byte[] hasValue = mysha256.ComputeHash(bytevalue);
               
                            if (!byteshavalue.SequenceEqual(hasValue))
                            {
                                Console.WriteLine(guid);
                            }
                            //for (int i = 0; i < hasValue.Length; i++)
                            //{
                            //    Console.Write(hasValue[i]);
                            //}
                            //Console.WriteLine();
                            //for (int i = 0; i < hasValue.Length; i++)
                            //{
                            //    Console.Write(byteshavalue[i]);
                            //}
                            //Console.WriteLine();
                        if (b == 10)
                            return;
                        }

                    }

                    
                }
            }
        }
    }
}
