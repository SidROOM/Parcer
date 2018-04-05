using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Parser
{

    class Program
    {
        static void Main(string[] args)
        {
            Node root = null;

            using (StreamReader sr = new StreamReader(@"C:\\Text.txt", Encoding.Default))
            {
                var str = sr.ReadToEnd().Replace(" ", "");
                str = str.Replace("\r", "");
                str = str.Replace("\\\"", "*");
                Parser.s = str.Replace("\n", "");
            }

            while (Parser.s != "")
            {
                if (root == null) root = new Node(Parser.id, 0, Parser.NameNode(), Parser.ValueNode());
                else
                    root = Parser.ParseChildrens(root);
            }

            try
            {
                StreamWriter sw = new StreamWriter(@"C:\\ParseText.txt");
                Parser.WriteToNote(root, sw);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadLine();
            }
            finally
            {
                Console.WriteLine("Well done parsing!");
                Console.ReadLine();
            }
        }
    }
}
