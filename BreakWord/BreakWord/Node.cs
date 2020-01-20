using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakWord
{ 
    class Node
    {
        public string ClassPart;
        public string ValuePart;
        public int LineNo;
        public string Name;
        public string Type;
        public string category;
        public string parent;
        public LinkList reff;
        public Node NextValueAdd;

        public Node(string a)           
        {
   
            ValuePart = a;
        }
        public Node(int a)
        {
           LineNo = a;
        }

        public Node(string a, string b)        /*constructor Overload*/
        {
            ClassPart = a;
            ValuePart = b;
        }

        public Node(string a,string b,int c)  //constructor Overload
        {
            ClassPart = a;
            ValuePart = b;
            LineNo = c;
        }

        public Node(string a,string b, string c ,string d,LinkList r) //    node for type definition
        {
            Name = a;
            Type = b;
            category = c;
            parent = d;
            reff = r;
        }

        public string GetValuePart()
        {
            return ValuePart;
        }
        public String GetClassPart()
        {
            return ClassPart;
        }
        public int GetLineNo()
        {
            return LineNo;
        }
        public Node GetNextValAdd()
        {
            return NextValueAdd;
        }
        public Node Increment()
        {
            return NextValueAdd;
        }
        public string Get_Type()
        {
            return Type;
        }
        public string Get_Name()
        {
            return Name;
        }
        public string DispalyToken()
        {
            string t = "  (  " +ClassPart+ "  |  " + ValuePart +"  |  " + LineNo+ "  )  ";
            return t;
        }

        public string DisplayLink()
        {   
            string v= ValuePart;
            return v;
        }
        public string DisplayTypdefAttri()
        {
            string a = Name+ " , " + Type+ " , " + category +" , " + parent;
            return a;
        }
     
    }
}
