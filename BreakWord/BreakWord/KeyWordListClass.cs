using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWord
{
    class KeywordListClass
    {

        public LinkList l;
        public KeywordListClass()
        {
            l = new LinkList();
            l.addLink("DT","int");
            l.addLink("DT", "float");
            l.addLink("DT", "string");
            l.addLink("DT", "char");
            l.addLink("TYPE", "var");
            l.addLink("TYPE", "val");
            l.addLink("FOR", "for");
            l.addLink("WHILE", "while");
            l.addLink("DO", "do");
            l.addLink("FUN", "fun");
            l.addLink("MAIN", "main");
            l.addLink("WHEN","when");
            l.addLink("IF", "if");
            l.addLink("ELSE", "else");
            l.addLink("CONTINUE", "continue");
            l.addLink("BREAK", "break");
            l.addLink("INCDEC", "inc");
            l.addLink("INCDEC", "dec");
            l.addLink("NULL", "null");
            l.addLink("AND", "and");
            l.addLink("OR", "or");
            l.addLink("NOT", "not");
            l.addLink("RETURN", "return");
            l.addLink("BY", "by");
            l.addLink("ELSE", "else");
            l.addLink("ABSTRACT","abstract");
            l.addLink("OPEN", "open");
            l.addLink("NEW","new");
            l.addLink("AM","public");
            l.addLink("AM", "private");
            l.addLink("AM", "protected");
            l.addLink("CLASS", "class");
            l.addLink("ARR", "arr");
            l.addLink("OBJECT", "object");
            l.addLink("THIS","this");
            l.addLink("CONSTRUCTOR", "constructor");
            l.addLink("INTERFACE", "interface");
        }
       
    }
}
