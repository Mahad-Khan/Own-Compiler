using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakWord
{
    class SyntaxAnalyzer
    {
        Node CurrentToken;
        string cp;
        public LinkList TypeDef;
        public LinkList FuncTable;
        public int HScope;
        public int Scope;
        public Stack FuncStack;
        public StackICG st;
        public bool FlagInsert;
        public int indexLabel;
        public int TempIndex;
        public Stack ICG;
        string lab1;
        string lab2;
        public string[] alpha = { "A", "B","C","D","E" ,"F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        int tag;
        Form1 nn;
        string te;
      public   string kisibhnamse;



        public SyntaxAnalyzer(LinkList CopyList)
        {

            CurrentToken = CopyList.GetHead();
            cp = CurrentToken.GetClassPart();
            TypeDef = new LinkList();
            st = new StackICG(50);
            FuncTable = new LinkList();
            Scope = 0;
            HScope = 0;
            FlagInsert = false;
            FuncStack = new Stack(50);
            FuncStack.Push(0);
            indexLabel = 1;
            TempIndex = 1;
            ICG = new Stack(50);
            tag = -1;
            lab1 = null;
            lab2 = null;
            nn = new Form1();
            te = null;

        }
        public Do ABS()
        {
            Do ndo = new Do();
            LinkList r = new LinkList();

            if (Start(ref r))
            {
                ndo.cppp = "Valid Syntax";
                ndo.cpline = CurrentToken.LineNo;
                return ndo;
            }

            else
            {
                ndo.cppp = "InValid Syntax";
                ndo.cpline = CurrentToken.LineNo;
                return ndo;
            }


        }
        public bool Start(ref LinkList r)
        {
            if (cp == "NEWLINE")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
            }

            if (Class_Null(ref  r))
            {
                if (cp == "NEWLINE")
                {
                    
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                }
                if (Main_Class(ref r))
                {
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                    }
                    if (Class_Null2(ref r))
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }
                        return true;
                    }
                }

            }

            return false;
        }

        public bool Class_Null2(ref LinkList r)
        {
            if (Class_St(ref r) || Interface_St(ref r) || Abs_Class_St(ref r) || Obj_Class(ref r))
            {
                if (Class_Null2(ref r))
                {
                    return true;

                }
            }

            else if (cp == "$") { return true; }

            return false;

        }

        public bool Class_Null(ref LinkList r)
        {
            if (Class_St(ref r) || Interface_St(ref r) || Abs_Class_St(ref r) || Obj_Class(ref r))
            {
                if (Class_Null(ref r))
                {
                    return true;

                }
            }

            else if (cp == "MAIN") { return true; }

            return false;
        }

        public bool Main_Class(ref LinkList r)
        {
            if (cp == "MAIN")
            {
                string name = null;
                string type = "Class";
                string cat = "Main Class";
                bool f = false;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "CLASS")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "ID")
                    {
                        name = CurrentToken.GetValuePart();
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }

                        if (Inher_Body(name,type,cat,ref r,f))
                        {
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                            }
                            
                            if (cp == "{")
                            {
                                FlagInsert = true;
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                }
                                if (Main_Class_Body(ref r))
                                {
                                    if (cp == "NEWLINE")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                    }
                                    if (cp == "}")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                        return true;

                                    }

                                }

                            }
                        }

                    }

                }

            }
            return false;
        }

        public bool Inher_Body(string name,string type,string cat,ref LinkList r,bool f)
        {
            if (cp == "::")
            {
                string parent = null;
                string check = null;
                bool flag = false;


                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    parent = CurrentToken.GetValuePart();       // yahan agr Not Decalare in this scope bh hoga tab bhi table m enter hojaiga

                    check = TypeDef.Lookup(parent);

                    if (f == false)
                    {

                        if (check != "Class" && check != "Interface")
                        {
                            MessageBox.Show(parent + " Not Decalare in this scope");     // yahan agr Not Decalare in this scope bh hoga tab bhi table m enter hojaiga 
                        }
                        if (check == "Class")
                        {
                            flag = true;
                        }
                    }

                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Multi(name, type, cat, parent, flag, ref r,f))
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }
                        return true;

                    }


                }

            }
            else if (cp == "{" || cp == "NEWLINE")
            {
                

                if (f == false)
                {
                    string parent = "-";
                    CreateReference(ref r);
                    TypeDef.Insert(name, type, cat, parent, r);
                }
                    return true;
                
            }

            return false;

        }

        public bool Multi(string name,string type,string cat,string parent,bool flag, ref LinkList r,bool f)
        {
            if (cp == ",")
            {
                string p = null;
                string check = null;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    p = CurrentToken.GetValuePart();
                    check= TypeDef.Lookup(p);

                    if (f == false)
                    {
                        if (check != "Class" && check != "Interface")
                        {
                            MessageBox.Show(p + " is Not Declare in this scope");

                        }
                    }

                    if (f == false)
                    {
                        if (check == "Class" && flag == true)
                        {
                            MessageBox.Show(p + " is class and multiple class inheritance is not allowed");
                        }
                    }

                    if (f == false)
                    {
                        if (check == "Class")
                        {
                            flag = true;
                        }
                    }
                    parent = parent+" , " + p;

                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Multi(name,type,cat,parent,flag,ref r,f))
                    {

                        return true;

                    }
                }
            }
            else if (cp == "{" || cp == "NEWLINE")
            {

                if (f == false)
                {
                    CreateReference(ref r);
                    TypeDef.Insert(name, type, cat, parent, r);
                }
                return true;
            }

            return false;
        }

        public bool Main_Class_Body(ref LinkList r)
        {
            if (cp == "AM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();


                if (Class_Mst(ref r))
                {
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                    }
                    if (Main_Func(ref r))
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }
                        if (Class_Mst11(ref r))
                        {
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                            }
                            return true;
                        }
                    }

                }
            }
            return false;
        }
        public bool Class_Mst11(ref LinkList r)
        {
            if (Class_Sst11(ref r))
            {
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (Class_Mst11(ref r))
                    {
                        return true;

                    }


                }

            }
            else if (cp == "}") return true;
            return false;
        }
        public bool Class_Sst11(ref LinkList r)
        {
            if (cp == "AM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Class_Sst(ref r))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Main_Func(ref LinkList r)
        {
            if (cp == "MAIN")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "FUN")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "ID")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        if (cp == "(")
                        {
                            FlagInsert = false;
                            CreateScope();
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (Para_List())
                            {
                                if (cp == ")")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                    if (cp == "->")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();

                                        if(Return_Type())
                                        {
                                            if (cp == "NEWLINE")
                                            {
                                                CurrentToken = CurrentToken.Increment();
                                                cp = CurrentToken.GetClassPart();

                                            }
                                            if (cp == "{")
                                            {
                                                
                                                CurrentToken = CurrentToken.Increment();
                                                cp = CurrentToken.GetClassPart();


                                                if (cp == "NEWLINE")
                                                {
                                                    CurrentToken = CurrentToken.Increment();
                                                    cp = CurrentToken.GetClassPart();



                                                }

                                                if (Mst())
                                                {
                                                    if (cp == "}")
                                                    {
                                                        DestroyScope();
                                                        CurrentToken = CurrentToken.Increment();
                                                        cp = CurrentToken.GetClassPart();


                                                        return true;




                                                    }

                                                }

                                            }

                                        }

                                    }

                                }

                            }

                        }
                    }
                }

            }

            return false;
        }

        public bool Return_Type()
        {
            if (Dt_Id())
            {
                return true;
            }
            if (cp == "VOID")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                return true;
            }
            return false;
        }
        public bool Para_List()
        {
            if (cp == "ID")
            {
                string name = CurrentToken.GetValuePart();
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Null_Arr())
                {
                    if (cp == ":")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (Dt_Id1(name))
                        {

                            if (Para_List2())
                            {
                                return true;

                            }

                        }

                    }
                }

            }
            else if (cp == ")")
            { return true; }

            return false;
        }

        public bool Para_List2()
        {
            if (cp == ",")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    string name = CurrentToken.GetValuePart();
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Null_Arr())
                    {
                        if (cp == ":")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (Dt_Id1(name))
                            {
                                if (Para_List2())
                                {
                                    return true;

                                }

                            }


                        }

                    }
                }
            }

            else if (cp == ")") return true;


            return false;
        }
        public bool Null_Arr()
        {
            if (cp == "[")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "]")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    return true;
                }

            }
            else if (cp == ":" || cp == "{" || cp == "NEWLINE" ) return true;

            return false;
        }

        public bool Dt_Id()
        {
            if (cp == "DT" || cp == "ID")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Null_Arr())
                {
                    return true;

                }

            }
            return false;
        }

        public bool Declaration()
        {
            if (cp == "TYPE")
            {
                string name;
               
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();


                if (cp == "ID")
                {
                    name = CurrentToken.GetValuePart();
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Dynamic_Static(name))
                    {
                        if (Init())
                        {
                            return true;

                        } 

                    }

                }
            }
            return false;
        }

        public bool Dynamic_Static(string name)
        {
            if (cp == ":")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Dt_Id1(name))
                {
                    return true;
                }
            }
            else if (cp == "ASSIGNOP" || cp == "NEWLINE") return true; 
            return false;
        }

        public bool Init()
        {
            if (cp == "ASSIGNOP")
            {
                string Op = CurrentToken.GetValuePart();
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Exp())
                {
              
                    return true;
                }
            }

            else if (cp == "NEWLINE" ) return true;
            return false;
        }

        public bool Func_Dec(ref LinkList r)
        {
            if (Virt_Over_Null())
            {
                if (cp == "FUN")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "ID")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        if (cp == "(")
                        {
                            
                            CreateScope();
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (Para_List())
                            {
                                if (cp == ")")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                    if (cp == "->")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                        if (Return_Type())
                                        {
                                            if (cp == "NEWLINE")
                                            {
                                                CurrentToken = CurrentToken.Increment();
                                                cp = CurrentToken.GetClassPart();
                                            }
                                            if (cp == "{")
                                            {
                                                CurrentToken = CurrentToken.Increment();
                                                cp = CurrentToken.GetClassPart();
                                                if (cp == "NEWLINE")
                                                {
                                                    CurrentToken = CurrentToken.Increment();
                                                    cp = CurrentToken.GetClassPart();
                                                }
                                                if (Mst())
                                                {

                                                    if (cp == "}")
                                                    {
                                                        DestroyScope();
                                                        CurrentToken = CurrentToken.Increment();
                                                        cp = CurrentToken.GetClassPart();

                                                        return true;
                                                    }

                                                }

                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return false;

        }
        public bool Virt_Over_Null()
        {
            if (cp == "VO")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                return true;

            }
            else if (cp == "FUN") { return true; }
            return false;
        }

        public bool Func_Call()
        {
            if (cp == "(")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Argument_Statement())
                {
                    if (cp == ")")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        return true;
                    }
                }
            }
            return false;

        }
        public bool Argument_Statement()
        {
            if (Exp())
            {
                if (Exp2())
                {
                    return true;
                }
            }
            else if (cp == ")" || cp=="}") return true;

            return false;
        }

        public bool Exp2()
        {
            if (cp == ",")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Exp())
                {
                    if (Exp2())
                    {
                        return true;
                    }
                }
            }
            else if (cp == ")" || cp=="}") return true;

            return false;
        }

        public bool Return_St()
        {
            if (cp == "RETURN")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Exp())
                {
                    return true;
                }

            }
            return false;
        }
        public bool Continue_St()
        {
            if (cp == "CONTINUE")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Exp())
                {
                    return true;
                }

            }
            return false;

        }

        public bool Break_St()
        {
            if (cp == "BREAK")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Exp())
                {
                    return true;
                }

            }
            return false;
        }

        public bool While_St()
        {
            if (cp == "WHILE")
            {
                tag++;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                lab1 = CreateLabel();
                string temp_create = CreateTemp();
                string pass ="\n"+alpha[tag] + "=" + temp_create;

                GenIC(pass + "\n");
                GenIC(lab1 + "\n");
                
                lab2 = CreateLabel();
                string par = "IF ( " + temp_create + " == False ) jmp " + lab2;

                GenIC(par );

                PushObj n = new PushObj(lab1 , lab2);
                st.Push(n);
                


                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                }
                CreateScope();
                if (cp == "(")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                    }
                    if (Exp())
                    {

                        if (cp == ")")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                            }
                            if (Body())
                            {
                                PushObj exx = st.Pop();
                                string last1 = "\nJMP " + exx.p1;

                                GenIC(last1 + "\n");

                                string last2 = exx.p2;

                                GenIC(last2 + ":");

                                return true;

                            }

                        }

                    }

                }
            }
            return false;

        }
        public bool Do_While_St()
        {
            if (cp == "DO")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                }
                CreateScope();
                if (cp == "{")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                    }
                    if (Mst())
                    {

                        if (cp == "}")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                            }
                            if (cp == "WHILE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                                if (cp == "(")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();

                                    if (Exp())
                                    {

                                        if (cp == ")")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();

                                            return true;

                                        }

                                    }

                                }

                            }

                        }

                    }

                }


            }

            return false;
        }

        public bool For_St()
        {
            if (cp == "FOR")
            {
                tag++;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                string lab1 = CreateLabel();
                FlagInsert = false;
                CreateScope();
                if (cp == "(")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (C1())
                    {
                        string tempCreate = CreateTemp();
                        string pass = alpha[tag] + "=" + tempCreate;
                        GenIC(pass + "\n");
                        GenIC(lab1 + ":"+"\n");


                        if (cp == "|")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (C2())
                            {
                                tag++;
                                tempCreate = CreateTemp();
                                pass = alpha[tag] + "=" + tempCreate;
                                GenIC(pass + "\n");
                                string lab2 = CreateLabel();
                                string par = "if (" + tempCreate + "==False ) jump " + lab2;
                                GenIC(par + "\n");
                                PushObj ooo = new PushObj(lab1, lab2);
                                st.Push(ooo);

                                

                                if (cp == "|")
                                {
                                    
                                    CurrentToken = CurrentToken.Increment();
                                    Node Current2 = CurrentToken;
                                    string op = Current2.GetValuePart();
                                    Current2 = Current2.Increment();
                                    string n = Current2.GetValuePart();
                                    Current2 = Current2.Increment();
                                    Current2 = Current2.Increment();
                                    string num = Current2.GetValuePart();
                                    cp = CurrentToken.GetClassPart();
                                    if(op=="inc")
                                    {
                                      kisibhnamse = n + "=" + n + "+" + num;
                                    }
                                    if(op=="dec")
                                    {
                                       kisibhnamse = n + "=" + n + "-" + num;

                                    }
                                   
 

                                    if (C3())
                                    {

                                        tag++;
                                        tempCreate = CreateTemp();
                                        pass = alpha[tag] + "=" + tempCreate;

                                        GenIC(pass + "\n");

                                        GenIC(kisibhnamse+"\n");

                                        if (cp == ")")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();
                                            if (cp == "NEWLINE")
                                            {
                                                CurrentToken = CurrentToken.Increment();
                                                cp = CurrentToken.GetClassPart();
                                            }
                                            if (Body())
                                            {

                                                

                                                PushObj exx = st.Pop();
                                                string last1 = "\nJMP " + exx.p1;

                                                GenIC(last1 + "\n");

                                                string last2 = exx.p2;

                                                GenIC(last2 + ":");

                                                return true;

                                            }

                                        }

                                    }

                                }

                            }

                        }


                    }

                }

            }
            return false;

        }
        public bool C1()
        {

            if (cp == "ID")
            {
                string name = CurrentToken.GetValuePart();
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (LeftFact(name))
                {
                    return true;

                }
            }
            else if (cp == "|" || cp==")") return true;

            return false;
        }

        public bool LeftFact(string name)
        {
            if (cp == ":")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Dt_Id1(name))
                {
                    if (cp== "ASSIGNOP")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        if (Exp())
                        {
                            return true;
                        }
                    }
                }
            }
            if (Assign() || Func_Call())
            {
                return true;


            }

            return false;

        }
        public bool C2()
        {
            if (Exp())
            {
                return true;

            }
            else if (cp == "|") return true;
            return false;

        }


        public bool C3()
        {
            if (Inc_Dec_St() || C1())
            {
                return true;

            }

            else if (cp == ")") return true;

            return false;


        }

        public bool When_St()
        {
            if (cp == "WHEN")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "(")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (Exp())
                    {
                        if (cp == ")")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                            }CreateScope();
                            if (cp == "{")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();

                                }
                                if (Case_Body())
                                {
                                    if (Else_Body())
                                    {
                                        if (cp == "}")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();
                                            return true;
                                        }
                                    }

                                }

                            }

                        }
                    }
                }
            }
            return false;
        }
        public bool Case_Body()
        {
            if (Exp())
            {
                if (cp == "->")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                    }
                    if (Body())
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();


                        }
                        if (Case_Body())
                        {
                            return true;

                        }

                    }

                }
            }
            else if (cp == "ELSE") return true;

            return false;

        }

        public bool Id_IntConst()
        {
            if (cp == "ID" || cp == "INT_CONST")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                return true;

            }
            return false;

        }
        public bool Else_Body()
        {
            if (cp == "ELSE")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "->")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                    }
                    if (Body())
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                        }
                        return true;
                    }

                }

            }
            return false;

        }

        public bool If_St()
        {
            if (cp == "IF")
            {

                tag++;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                lab1 = CreateLabel();
                string temp_create = CreateTemp();
                string pass = "\n" + alpha[tag] + "=" + temp_create;

                GenIC(pass + "\n");

                string lab2 = CreateLabel();

                string par = "IF (" + temp_create + "== False ) \n JMP" + lab1;
                GenIC(par + "\n");
                tag++;
                temp_create = CreateTemp();
                string par2 = alpha[tag] + "=" + temp_create;
                GenIC(par2 + "\n");

                PushObj ote = new PushObj(lab1,lab2);

                st.Push(ote);



                CreateScope();
                if (cp == "(")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Exp())
                    {
                        if (cp == ")")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();


                            }

                            if (Body())
                            {

                                PushObj exx = st.Pop();

                                string last1 =  exx.p1+":";
                                GenIC(last1 + "\n");
                                tag++;
                                temp_create = CreateTemp();
                                string par3 = alpha[tag] + "=" + temp_create;
                                GenIC(par3 + "\n");



                                string last2 = exx.p2+":";
                                GenIC(last2 + "\n");
                                tag++;
                                temp_create = CreateTemp();
                                string par4 = alpha[tag] + "=" + temp_create;
                                GenIC(par4 + "\n");


                                if (Else_St())
                                {
                                    return true;
                                }
                            }

                        }
                    }
                }
            }
            return false;
        }
        public bool Else_St()
        {
            if (cp == "ELSE")
            {
                CreateScope();
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();


                }
                if (Body())
                {
                    return true;
                }
            }
            else if (cp == "NEWLINE")
            {
                return true;
            }
            return false;


        }


        public bool Array_Dec()
        {
            if (cp == "ARR")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    string name = CurrentToken.GetValuePart();
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "[")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (cp == "]")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (cp == ":")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                                if (Dt_Id1(name))
                                {


                                    if (Array_Assign())
                                    {
                                        return true;
                                    }

                                }

                            }

                        }

                    }
                }
            }
            return false;
        }

        public bool Array_Assign()
        {
            if (cp == "ASSIGNOP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "[")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Exp())
                    {

                        if (cp == "]")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (Block())
                            {
                                return true;
                            }

                        }

                    }

                }

            }
            return false;
        }
        public bool Block()
        {
            if (cp == "{")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Argument_Statement())
                {
                    if (cp == "}")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        return true;
                    }
                }



            }
            else if (cp == "NEWLINE") 
            {
                return true;
            }
            return false;

        }

        public bool Const_List()
        {
            if (cp == "INT_CONST" || cp == "CHAR_CONST" || cp == "STRING_CONST" || cp == "FLOAT_CONST" || cp == "ID")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Const_List2())
                {
                    return true;

                }

            }
            return false;
        }
        public bool Const_List2()
        {
            if (cp == ",")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "INT_CONST" || cp == "CHAR_CONST" || cp == "STRING_CONST" || cp == "FLOAT_CONST" || cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (Const_List2())
                    {
                        return true;

                    }
                }

            }
            else if (cp == "}") return true;

            return false;
        }


        public bool Class_St(ref LinkList r)
        {
            if (Open_Null())
            {
                if (cp == "CLASS")
                {
                    string cat = "General";
                    string type = "Class";
                    bool f = false;
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "ID")
                    {
                        string name= CurrentToken.GetValuePart();
                        string t= TypeDef.Lookup(name);

                        if(t!=null)
                        {
                            MessageBox.Show("Redeclaration Error at "+CurrentToken.GetLineNo());
                            f = true;
                        }

                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }

                        if (Inher_Body(name,type,cat,ref r,f))
                        {
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                            }
                            if (cp == "{")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                }

                                if (Class_Mst11(ref r))
                                {
                                    if (cp == "}")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                        if (cp == "NEWLINE")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();
                                        }
                                        return true;

                                    }

                                }

                            }

                        }

                    }

                }

            }

            return false;
        }

        public bool Open_Null()
        {
            if (cp == "OPEN")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                return true;

            }
            else if (cp == "CLASS") { return true; }// sdfsdfsdfsdf

            return false;
        }
        public bool Constructor_St()
        {
            if (cp == "CONSTRUCTOR")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();


                }
                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "(")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (Para_List())
                        {
                            if (cp == ")")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();


                                }
                                if (cp == "{")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                    if (cp == "NEWLINE")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();


                                    }
                                    if (Mst())
                                    {
                                        if (cp == "}")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();

                                            return true;

                                        }

                                    }



                                }


                            }
                        }
                    }

                }

            }

            return false;

        }
        public bool Abs_Class_St(ref LinkList r)
        {
            if (cp == "ABSTRACT")
            {

                string name = null;
                string type = "Class";
                string cat = "Abstract";
                bool f = false;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                }

                if (cp == "CLASS")
                {

                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "ID")
                    {

                        name = CurrentToken.GetValuePart();
                        string t= TypeDef.Lookup(name);
                        if (t != null)
                        {
                            MessageBox.Show("Redeclaration Error at " + CurrentToken.GetLineNo());
                            f = true;
                        }
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }

                        if (Inher_Body(name,type,cat,ref r,f))
                        {
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                            }
                            if (cp == "{")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                }
                                if (Abs_Body(ref r))
                                {
                                    if (cp == "NEWLINE")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                    }
                                    if (cp == "}")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                        if (cp == "NEWLINE")
                                        {
                                            CurrentToken = CurrentToken.Increment();
                                            cp = CurrentToken.GetClassPart();

                                        }
                                        return true;

                                    }

                                }

                            }
                        }

                    }

                }

            }
            return false;

        }

        public bool Abs_Body(ref LinkList r)
        {
            if (cp == "AM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Normal(ref r))
                {
                    if (Abs_Func(ref r))
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (Abs_Normal(ref r))
                            {
                                return true;

                            }
                        }

                    }

                }
            }
            return false;


        }

        public bool Normal(ref LinkList r)
        {
            if (Normal2(ref r))
            {
                if (cp == "AM")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Normal(ref r))
                    {
                        return true;

                    }
                }
            }

            else if (cp == "ABSTRACT")
            {
                return true;
            }

            return false;

        }

        public bool Normal2(ref LinkList r)
        {
            if (Func_Dec(ref r) || Declaration() || Array_Dec())
            {
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    return true;
                }

            }
            return false;
        }
        public bool Abs_Normal(ref LinkList r)
        {
            if (cp == "AM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (A1(ref r))
                {
                    if (Abs_Normal(ref r))
                    {
                        return true;

                    }

                }

            }
            else if (cp == "}") { return true; }

            return false;
        }

        public bool A1(ref LinkList r)
        {
            if (Abs_Func(ref r) || Func_Dec(ref r) || Declaration() || Constructor_St() || Array_Dec())
            {
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    return true;

                }

            }
            return false;

        }
        public bool Abs_Func(ref LinkList r)
        {
            if (cp == "ABSTRACT")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "FUN")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "ID")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (cp == "(")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (Para_List())
                            {
                                if (cp == ")")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                    if (cp == "->")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                        if (Return_Type())
                                        {
                                            return true;
                                        }

                                    }
                                }

                            }

                        }

                    }

                }

            }
            return false;

        }
        public bool Interface_St(ref LinkList r)
        {
            if (cp == "INTERFACE")
            {
                string name = null;
                string type = "Interface";
                string cat = "General";
                bool f = false;
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    name = CurrentToken.GetValuePart();
                   string t=TypeDef.Lookup(name);
                    if (t != null)
                    {
                        MessageBox.Show("Redeclaration Error at " + CurrentToken.GetLineNo());
                        f = true;
                    }
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (Inher_Body(name,type,cat,ref r,f))
                    {
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                        }

                        if (cp == "{")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();
                            if (cp == "NEWLINE")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                            }

                            if (Interface_Body(ref r))
                            {
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                }
                                if (cp == "}")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                    if (cp == "NEWLINE")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();
                                    }
                                    return true;

                                }

                            }


                        }

                    }


                }
            }
            return false;

        }

        public bool Interface_Body(ref LinkList r)
        {
            if (cp == "AM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (Abs_Func(ref r))
                {
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (Interface_Body(ref r))
                        {
                            return true;

                        }
                    }
                }

            }
            else if (cp == "}" ) { return true; }
            return false;
        }

        public bool Obj_Class(ref LinkList r)
        {
            if (cp == "OBJECT")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "NEWLINE")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                    }
                    if (cp == "{")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        if (cp == "NEWLINE")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                        }
                        if (Class_Mst(ref r))
                        {
                            if (cp == "}")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();
                                if (cp == "NEWLINE")
                                {
                                    CurrentToken = CurrentToken.Increment();
                                    cp = CurrentToken.GetClassPart();
                                }
                                return true;
                            }

                        }

                    }

                }

            }

            return false;

        }
        public bool Obj_Dec()
        {
            if (cp == "ID")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ASSIGNOP")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "NEW")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (cp == "ID")
                        {
                            CurrentToken = CurrentToken.Increment();
                            cp = CurrentToken.GetClassPart();

                            if (cp == "(")
                            {
                                CurrentToken = CurrentToken.Increment();
                                cp = CurrentToken.GetClassPart();

                                if (Argument_Statement())
                                {
                                    if (cp == ")")
                                    {
                                        CurrentToken = CurrentToken.Increment();
                                        cp = CurrentToken.GetClassPart();

                                        return true;

                                    }

                                }

                            }

                        }

                    }

                }


            }
            return false;

        }
        public bool Obj_Access()
        {
            if (cp == "DOTOP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Fact())
                    {
                        if (Assign())
                        {
                            return true;
                        }

                    }

                }

            }
            return false;
        }
        public bool Fact()
        {
            if (cp == "DOTOP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Id3())
                    {
                        return true;

                    }

                }


            }

            else if (Func_Call() || Array_Access())
            {
                if (Id3())
                {
                    return true;
                }
            }
            else if (cp == "ASSIGNOP" || cp == "COMPOUND_ASSIGN" || cp == "MDM" || cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == "NEWLINE" || cp == "," || cp == ")" || cp == "|" || cp == "]" || cp=="->")
            {
                return true;
            }

            return false;
        }
        public bool Id3()
        {
            if (cp == "DOTOP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Abc())
                    {
                        if (Id3())
                        {
                            return true;

                        }

                    }

                }

            }
            else if (cp == "ASSIGNOP" || cp == "COMPOUND_ASSIGN" || cp == "MDM" || cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" ||cp=="->" ||cp=="]") { return true; }

            return false;
        }
        public bool Abc()
        {
            if (Func_Call() || Array_Access())
            {
                return true;

            }
            else if (cp == "DOTOP" || cp == "ASSIGNOP" || cp == "COMPOUND_ASSIGN" || cp == "MDM" || cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") { return true; }

            return false;
        }
        public bool Obj_Access2()
        {
            if (cp == "DOTOP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (Fact())
                    {
                        return true;
                    }

                }

            }
            return false;
        }

        public bool Array_Access()
        {
            if (cp == "[")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Exp())
                {
                    if (cp == "]")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        return true;

                    }
                }
            }
            return false;

        }

        public bool This_St2()
        {
            if (cp == "THIS")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "DOTOP")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "ID")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (This_Choice())
                        {
                            return true;

                        }

                    }
                }


            }
            return false;
        }

        public bool This_St()
        {
            if (cp == "THIS")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "DOTOP")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "ID")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (This_Choice())
                        {
                            if (Assign())
                            {
                                return true;

                            }

                        }

                    }
                }


            }
            return false;
        }


        public bool This_Choice()
        {
            if (Func_Call() || Array_Access())
            {
                return true;
            }
            else if (cp == "ASSIGNOP" || cp == "COMPOUND_ASSIGN" || cp == "MDM" || cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") { return true; }//sdfsdfsdfsdf
            return false;
        }
        public bool Body()
        {
            if (Sst())
            {
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    return true;
                }
            }
            else if (cp == "{")
            {

                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                }

                if (Mst())
                {
                    if (cp == "}")
                    {
                        DestroyScope();
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        return true;
                    }

                }

            }
            return false;
        }
        public bool Mst()
        {
            if (Sst())
            {


                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (Mst())
                    {
                        return true;
                    }
                }

            }
            else if (cp == "}") return true;

            return false;

        }

        public bool Sst()
        {
            if (cp == "ID")
            {
                string name = CurrentToken.GetValuePart();
                
                
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Sst2())
                {
                    return true;
                }
                return false;

            }

            if (Declaration())
            {
                return true;
            }

            if (Array_Dec())
            {
                return true;
            }


            if (Return_St())
            {
                return true;
            }


            if (Continue_St())
            {
                return true;
            }


            if (Break_St())
            {
                return true;
            }


            if (While_St())
            {
                return true;
            }


            if (Do_While_St())
            {
                return true;
            }


            if (For_St())
            {
                return true;
            }
            if (Inc_Dec_St())
            {
                return true;

            }

            if (When_St())
            {
                return true;

            }
            if (If_St())
            {
                return true;
            }
            if (This_St())
            {
                return true;
            }


            return false;

        }
        public bool Sst2()
        {
            if ((Func_Call() && Dot()) || Assign() || Obj_Access() || Obj_Dec() || (Array_Access() && Dot2()))
            {
                return true;
            }
            return false;

        }
        public bool Dot()
        {
            if (Obj_Access())
            {
                return true;

            }
            else if (cp == "NEWLINE") { return true; }

            return false;

        }
        public bool Dot2()
        {
            if (Obj_Access() || Assign())
            {
                return true;

            }
            return false;

        }

        public bool Assign()
        {
            if (Assign_Op())
            {
                if (Exp())
                {
                    return true;
                }

            }
            return false;

        }
        public bool Assign_Op()
        {
            if (cp == "ASSIGNOP" || cp == "COMPOUND_ASSIGN")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                return true;

            }
            return false;

        }

        public bool Inc_Dec_St()
        {
            if (cp == "INCDEC")
            {
              
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (cp == "ID")
                {
                    
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();

                    if (cp == "BY")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (Id_IntConst())
                        {

                            return true;

                        }

                    }
                }

            }
            return false;
        }

        public bool Class_Mst(ref LinkList r)
        {
            if (Class_Sst(ref r))
            {
                if (cp == "NEWLINE")
                {
                    CurrentToken = CurrentToken.Increment();
                    cp = CurrentToken.GetClassPart();
                    if (cp == "AM")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();

                        if (Class_Mst(ref r))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (cp == "}" || cp == "MAIN") { return true; }
            return false;

        }
        public bool Class_Sst(ref LinkList r)
        {
            if (Declaration() || Func_Dec(ref r) || Array_Dec() || Constructor_St())
            {
                return true;
            }
            return false;
        }

        



        public bool Exp()
        {
            if (T())
            {
                if (ExpComma())
                {
                    return true;
                }

            }
            return false;


        }
        public bool ExpComma()
        {
            if (cp == "OR")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                if (T())
                {
                    if (ExpComma())
                    {
                        return true;
                    }
                }

            }
            else if ( cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]")                 return true;

            return false;
        }

        public bool T()
        {
            if (S())
            {
                if (TComma())
                {
                    return true;
                }
            }
            return false;


        }
        public bool TComma()
        {
            if (cp == "AND")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (S())
                {
                    if (TComma())
                    {
                        return true;

                    }
                }

            }

            else if (cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") return true;

            return false;
 
        }
        public bool S()
        {
            if (V())
            {
                if (SComma())
                {
                    return true;
                }
            }
            return false;
        }

        public bool SComma()
        {
            if (cp == "ROP")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (V())
                {
                    if (SComma())
                    {
                        return true;
                    }
                }

            }
            else if ( cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") return true;
            return false;

        }
        public bool V()
        {
            if (A())
            {
                if (VComma())
                {
                    return true;
                }
            }
            return false;

        }
        public bool VComma()
        {
            if (cp == "PM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (A())
                {
                    if (VComma())
                    {
                        return true;
                    }

                }

            }
            else if (cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") return true;

            return false;

        }
        public bool A()
        {
            if (Z())
            {
                if (ACOmma())
                {
                    return true;
                }
            }
            return false;

        }
        public bool ACOmma()
        {
            if (cp == "MDM")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Z())
                {
                    if (ACOmma())
                    {
                        return true;

                    }
                }
            }
            else if (cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") return true; return false;

        }
        public bool Z()
        {
            if (Const())
            {
                return true;
            }
           else if (cp == "NOT")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Z())
                {
                    return true;
                }
            }

           else if (cp == "ID")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (ZZ())
                {
                    return true;
                }

            }

            else if (cp == "(")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                if (Exp())
                {
                    if (cp == ")")
                    {
                        CurrentToken = CurrentToken.Increment();
                        cp = CurrentToken.GetClassPart();
                        return true;

                    }

                }

            }
            else if(Inc_Dec_St())
            {
                return true;

            }
            else if(This_St2())
            {
                return true;

            }

            return false;
        }
        public bool ZZ()
        {
            if (Func_Call() || Obj_Access2() || (Array_Access()&& Obj_Access_Null()))
            {
                return true;
            }
            else if (cp == "MDM" || cp == "PM" || cp == "ROP" || cp == "AND" || cp == "OR" || cp == ")" || cp == "NEWLINE" || cp == "," || cp == "}" || cp == "|" || cp == "->" || cp == "]") return true;  //sadvsadvadf

            return false;
        }

        public bool Const()
        {
            if (cp == "INT_CONST" || cp == "STRING_CONST" || cp == "FLOAT_CONST" || cp == "CHAR_CONST" || cp == "BOOL_CONST")
            {
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();

                return true;


            }
            return false;
        }

        public bool Obj_Access_Null()
        {
            if (Obj_Access2())
            {
                return true;
            }
            else if (cp=="MDM"||cp=="PM"||cp=="ROP"||cp=="AND"||cp=="OR"||cp=="NEWLINE"||cp=="DOTOP" ||cp==","||cp==")" ||cp=="|"||cp=="]") { return true; }

            return false;

        }

        public bool Dt_Id1(string name)
        {
            if(cp=="DT" ||cp=="ID")
            {
                 string type = CurrentToken.GetValuePart();
                CurrentToken = CurrentToken.Increment();
                cp = CurrentToken.GetClassPart();
                string check=FuncTable.Lookup_FT(name,Scope);


                if(check==null)
                {
                    if (Scope>0)
                    {
                        FuncTable.Insert_FT(name, type, Scope);
                    }
                }
                else
                {
                    MessageBox.Show(name + " is already declare in this scope");
                }
                
                return true;

            }
            return false;
        }


        public  string Compatibility(string Lt,string Rt,string Op)
        {
            if(Op=="=" || Op=="+" || Op == "-" || Op == "/" || Op == "*" || Op == "%" || Op == "<" || Op == ">" || Op == "-" || Op == "<=" || Op == ">=" || Op == "==" || Op == "~="|| Op == "+=" || Op == "-=" || Op == "*=" || Op == "/=" || Op == "%=")
            {
                if (Lt == Rt)
                {
                    return null;
                }

                else
                    return "Compatibility Error";
            }
            else
                return "Compatibility Error";

        }

        public void CreateReference(ref LinkList r)
        {
            LinkList num = new LinkList();
            r = num;
        }
        public void CreateScope()
        {
            
            Scope = HScope;
            Scope += 1;
            HScope = Scope;
            FuncStack.Push(Scope);
        }
        public void DestroyScope()
        {
            
           Scope= FuncStack.Pop();
            Scope = FuncStack.GetTop();
         
        }
        public string CreateLabel()
        {
            string s = "L" + indexLabel++ ;
            return s;
        }

        public void GenIC(string s)
        {

            te += s;

        }
        public string RetTE()
        {
            return te;
        }
        public string CreateTemp()
        {
            return "t" + TempIndex++;
        }

    }
}