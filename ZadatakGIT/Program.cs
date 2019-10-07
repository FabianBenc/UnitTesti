using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadatakGIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Program meni = new Program();

            Console.Write("\n GIT implementacija\n");
            Console.Write("1-Signly linked lista.\n2-Doubly linked lista.\n3-Stack.\n4-Queue.\n5-Hash Table.\n6-Izlaz.\n");
            Console.Write("Vas izbor: ");

            string IzborKorisnika = Console.ReadLine();

            switch (IzborKorisnika)
            {
                case "1":
                    //singly liinked lista
                    meni.SLLPrimjer();
                    break;
                case "2":
                    //doubly linked lista
                    meni.DLLPrimjer();
                    break;
                case "3":
                    //stack
                    meni.StackPrimjer();
                    break;
                case "4":
                    //queue
                    meni.QueuePrimjer();
                    break;
                case "5":
                    //Hash Table
                    meni.HashTablePrimjer();
                    break;
                case "6":
                    break;

                default:
                    break;
            }

        }
        public void SLLPrimjer()
        {

        }
        public void DLLPrimjer()
        {
          
        }
        public void StackPrimjer()
        {
            Stack myStack = new Stack();

            Console.WriteLine("======================================");
            myStack.Push(10);
            myStack.Push(20);
            myStack.Push(30);
            myStack.Push(40);
            myStack.PrintStack();
            myStack.Peek();
            Console.WriteLine("Element poppan iz Stacka: {0}", myStack.Pop());
            myStack.PrintStack();
        }
        public void QueuePrimjer()
        {

        }
        public void HashTablePrimjer()
        {

        }
    }
}
