using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;


namespace Dev23
{
    class Program
    {
       
            

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
                    if (b == 10)
                        return;

                    if (data.HasAttributes)
                    {
                        string sha = "";
                        string guid = "";
                        sha = data.GetAttribute("sha256");
                        guid = data.GetAttribute("guid");
                        string value = data.Value;
                        byte[] bytevalue = Encoding.ASCII.GetBytes(value);
                        byte[] byteshavalue = Encoding.ASCII.GetBytes(sha);
                        using (SHA256 mysha256 = SHA256.Create())
                        {
                            byte[] hasValue = mysha256.ComputeHash(bytevalue);
               
               
                            if (byteshavalue.SequenceEqual(hasValue))
                            {
                                Console.WriteLine(guid);
                            }
               
                        }

                    }

                    
                }
            }
        }
    }
}
