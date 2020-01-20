using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWord
{
    class PunctListClass
    {

        public LinkList PunctList;
        public PunctListClass()
        {
            PunctList = new LinkList();
            PunctList.addLink("(","(");
            PunctList.addLink(")", ")");
            PunctList.addLink("{", "{");
            PunctList.addLink("}", "}");
            PunctList.addLink("[", "[");
            PunctList.addLink("]", "]");
           
            PunctList.addLink(":", ":");
            PunctList.addLink(",", ",");
   
            PunctList.addLink("|","|");
           
            PunctList.addLink("->", "->");
            PunctList.addLink("::", "::");
        }
    }
}
