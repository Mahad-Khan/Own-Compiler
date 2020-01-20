using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWord
{
    class OperatorList
    {
      public  LinkList OpList;

        public OperatorList()
        {
            OpList = new LinkList();
            OpList.addLink("ASSIGNOP", "=");
            OpList.addLink("PM","+");
            OpList.addLink("PM", "-");
            OpList.addLink("MDM", "*");
            OpList.addLink("MDM", "/");
            OpList.addLink("MDM", "%");
            OpList.addLink("ROP", "<");
            OpList.addLink("ROP", ">");
            OpList.addLink("ROP", "<=");
            OpList.addLink("ROP", ">=");
            OpList.addLink("ROP", "~=");
            OpList.addLink("ROP", "==");
            OpList.addLink("COMPOUND_ASSIGN", "+=");
            OpList.addLink("COMPOUND_ASSIGN", "-=");
            OpList.addLink("COMPOUND_ASSIGN", "/=");
            OpList.addLink("COMPOUND_ASSIGN", "*=");
            OpList.addLink("COMPOUND_ASSIGN", "%=");
            OpList.addLink("DOTOP",".");
        }
    }
}
