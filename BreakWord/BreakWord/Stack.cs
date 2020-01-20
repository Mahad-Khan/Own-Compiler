using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakWord
{
    class Stack
    {
        private int Maxs;
        private int[] StackArray;
        private int top;
        
        public Stack(int s)
        {
            
            Maxs = s;
            top = -1;
            StackArray=new int[Maxs];
            
        }

        public void Push(int j)
        {
            StackArray[++top] = j;
        }

        public int Pop()
        {
            
           return StackArray[top--];
        }
        public int GetTop()
        {
            return StackArray[top];
        }
        
    }

    
}
