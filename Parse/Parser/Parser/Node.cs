using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Node
    {
        public int id;
        public int idparent;
        public string name;
        public string value;

        public bool flag = false;
        public List<Node> ListChildren = new List<Node>();

        public Node(int id, int idparent, string name, string value)
        {
            this.id = id;
            this.idparent = idparent;
            this.name = name;
            this.value = value;
        }
    }
}
