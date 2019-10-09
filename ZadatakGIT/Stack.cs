using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakGIT
{
    class Stack
    {
        static readonly int MAX = 1000;
        int top;
        int[] stack = new int[MAX];

        bool IsEmpty()
        {
            return (top < 0);
        }

        public Stack()
        {
            top = -1;
        }

        public bool Push(int data)
        {
            if (top >= MAX)
            {
                Console.WriteLine("Stack Overflow");
                return false;
            }
            else 
            {
                stack[++top] = data;
                return true;
            }
        }

        public int Pop()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack underflow");
                return 0;

            }
            else 
            {
                int value = stack[top--];
                return value;
            }
        }

        public void Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack Underflow");
                return;
            }
            else
                Console.WriteLine("Element na vrhu stacka je  : {0}", stack[top]);
        }

        public void PrintStack()
        {
            if (top < 0)
            {
                Console.WriteLine("Stack underflow");
                return;
            }
            else 
            {
                Console.WriteLine("Elementi u stacku su: ");
                for (int i = top; i >= 0; i--)
                {
                    Console.WriteLine(stack[i]);
                }
            }
        }
    }
}
