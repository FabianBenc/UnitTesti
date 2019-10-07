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
            
        }
        public void QueuePrimjer()
        {
            Queue<int> queue = new Queue<int>();

            Console.WriteLine("===================================");
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            Console.WriteLine("Queue elementi:");

            foreach (Object obj in queue)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("Najvisi element u queue je {0}\n", queue.Peek());

            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            Console.WriteLine("Queue elementi poslije 3 Dequeue-a:");

            foreach (Object obj in queue)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("Najvisi element u queue nakon dequeue-a je {0}\n", queue.Peek());

        }
        public void HashTablePrimjer()
        {

        }
    }
}
