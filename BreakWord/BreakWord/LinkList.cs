using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakWord
{
    class LinkList
    {
        private Node Head;
        private Node last;

        public LinkList()
        {
            Head = null;
        }

    

        public void AddBreakPartInList(string v)
        {
            Node newNode = new Node(v);
            if (isEmpty())
            {
                newNode.NextValueAdd = Head;
                Head = newNode;
                last = newNode;
            }
            else
            {
                last.NextValueAdd = newNode;
                last = newNode;
                last.NextValueAdd = null;

            }

        }
        public void StackPush(int v)
        {
            Node newNode = new Node(v);
            newNode.NextValueAdd = Head;
            Head = newNode;

        }
        public int StackPop()
        {
            Node temp = Head; 
            Head = Head.NextValueAdd; 
            return temp.LineNo;     
        }

        public void addLink(string classp, string valuep)
        {
            Node newNode = new Node(classp, valuep);

            if (isEmpty())
            {
                newNode.NextValueAdd = Head;
                Head = newNode;
                last = newNode;

            }
            else
            {
                last.NextValueAdd = newNode;
                last = newNode;
                last.NextValueAdd = null;
              
            }
        }
        public void CreateToken(string cp,string vp,int Ln)
        {

            Node newNode = new Node(cp,vp,Ln);

            if (isEmpty())
            {
                newNode.NextValueAdd = Head;
                Head = newNode;
                last = newNode;

            }
            else
            {
                last.NextValueAdd = newNode;
                last = newNode;
                last.NextValueAdd = null;

            }
        }

        public string Lookup(string name)
        {
           
            Node current = Head;
            if (current == null)
                return null;

            while (current != null)
            {
                string n = null;
                n=current.Get_Name();
              
                if(name==n)
                {
                    string y = null;
                    y= current.Get_Type();
                  
                    return y;
                }

                current = current.NextValueAdd;

            }
            return null;
        }

        public void Insert(string n ,string t,string c,string p,LinkList r)
        {

            Node newNode = new Node(n, t, c,p,r);

            if (isEmpty())
            {
                newNode.NextValueAdd = Head;
                Head = newNode;
                last = newNode;

            }
            else
            {
                last.NextValueAdd = newNode;
                last = newNode;
                last.NextValueAdd = null;

            }

        }

        public void Insert_FT(string n,string t,int s)
        {
            Node newNode = new Node(n, t,s);

            if (isEmpty())
            {
                newNode.NextValueAdd = Head;
                Head = newNode;
                last = newNode;

            }
            else
            {
                last.NextValueAdd = newNode;
                last = newNode;
                last.NextValueAdd = null;

            }

        }
        public string Lookup_FT(string name,int scope)
        {

            Node current = Head;
            if (current == null)
                return null;
           

            while (current != null)
            {
                 
                string n = null;
                n = current.GetClassPart();

                if (name == n)
                {
                    int TScope = current.GetLineNo();
                    if (scope == TScope)
                    {
                        string y = null;
                        y = current.GetValuePart();

                        return y;
                    }
                }

                current = current.NextValueAdd;

            }
            return null;
        }



        private bool isEmpty()
        {
            return Head == null;
        }

        public string DisplayList()
        {
            string v = null;
            Node current = Head;
            while (current != null)
            {
                v = v + current.DisplayLink() + "\n";
                current = current.NextValueAdd;
            }
            return v;
        }


        public string DisplayTokens()
        {
            
            string v = null;
            Node current = Head;
            while (current != null)
            {
                v =v + current.DispalyToken() + "\n";
                current = current.NextValueAdd;
               
            }
            return v;
        }
        

        public string Dis()
        {

            string x = null;
            Node current = Head;
            while (current != null)
            {
                x = x + current.DisplayTypdefAttri() + "\n";
                current = current.NextValueAdd;

            }
            return x;
        }

           public Node GetHead()
        {
            return Head;
        }

        public bool IsEmpty()
        {
            if (Head == null)
              return  true;
            else
              return  false;
        }

      
        public TrueAndClassPartReturn IsMatch(string s)
        {
            Node current = Head;
            string v = null;
            string c = null;
            while (current != null)
            {
                v = current.ValuePart;

                if (v == s)
                {
                    c = current.ClassPart;

                    TrueAndClassPartReturn abc = new TrueAndClassPartReturn
                    {
                        cp = c,
                        isAvail = true
                    };
                    return abc;

                    //get classPart content fron node and return variable of classPart
                    //get valuepart content from node and reurn variable of valuePart
                }
                current = current.NextValueAdd;
            }
            TrueAndClassPartReturn def = new TrueAndClassPartReturn
            {
                cp = "inValid",
                isAvail = false
            };
            return def;

        }


    }
}
