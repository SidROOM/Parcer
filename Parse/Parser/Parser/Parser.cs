using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Parser
{
    class Parser
    {
        public static string s;

        public static int id = 1;

        public static void NameCheck(string str)
        {
            if (Char.IsDigit(str[0])) throw new Exception("Неверный формат данных");
        }

        public static string ValueCheck(string str)
        {
            str = str.Remove(0, 1);

            str = str.Remove(str.Length - 1, 1);

            foreach (var c in str) if ((c == '\n') || (c == '*')) throw new Exception("Неверный формат данных");
            
            return str;

        }

        public static string NameNode()
        {
            var str = new StringBuilder();

            foreach (var c in s) if (c == '=') break; else str.Append(c);

            NameCheck(str.ToString());

            s = s.Remove(0, str.Length + 1);

            return str.ToString();
        }

        public static string ValueNode()
        {
            var str = new StringBuilder();

            if (s[0] == '{') return " ";
            else
            {
                bool flag = true;

                foreach (var c in s)
                {
                    if ((c == '\"') && (!flag))
                    {
                        str.Append(c);
                        break;
                    }
                    else str.Append(c);

                    if (c == '\"') flag = !flag;
                }
                s = s.Remove(0, str.Length);

                return ValueCheck(str.ToString());
            }
        }

        public static Node Childrens(int idparent)
        {
            var ChildrenNode = new Node(++id, idparent, NameNode(), ValueNode());

            if (ChildrenNode.value != " ")
            {
                if (s[0] != '}') ChildrenNode.flag = !ChildrenNode.flag; else s = s.Remove(0, 1);

                return ChildrenNode;
            }
            else
            {
                var nodes = ParseChildrens(ChildrenNode);

                if (s[0] != '}') ChildrenNode.flag = !ChildrenNode.flag; else s = s.Remove(0, 1);

                return nodes;
            }
        }

        public static Node ParseChildrens(Node node)
        {
            bool flag;
            do
            {
                if (s[0] == '{') s = s.Remove(0, 1);

                if (s[0] == '}')
                {
                    s = s.Remove(0, 1);
                    break;
                }

                var ch = Childrens(node.id);

                flag = ch.flag;

                node.ListChildren.Add(ch);

            } while (flag);

            return node;
        }

        public static void WriteToNote(Node node, StreamWriter sw)
        {
            sw.WriteLine("(" + node.id.ToString() + "," + " " + node.idparent + "," + " " + node.name + "," + " " + node.value + ")");
            foreach (var n in node.ListChildren) WriteToNote(n, sw);
        }

    }
}
