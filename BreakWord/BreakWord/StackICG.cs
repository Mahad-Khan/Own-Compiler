using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWord
{
    class StackICG
    {
        private int Maxs;
        private PushObj[] StackArray;
        private int top;

        public StackICG(int s)
        {

            Maxs = s;
            top = -1;
            StackArray = new PushObj[Maxs];

        }

        public void Push(PushObj j)
        {
            StackArray[++top] = j;
        }

        public PushObj Pop()
        {

            return StackArray[top--];
        }
        public PushObj GetTop()
        {
            return StackArray[top];
        }
        
    }
    class PushObj
    {
      public string p1;
      public string p2;

        public  PushObj(string p11,string p22)
        {
            p1 = p11;
            p2 = p22;

        }

    }



}
