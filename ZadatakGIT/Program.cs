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
            LinkedList<int> DLL = new LinkedList<int>();

            Console.WriteLine("=======================================");
            DLL.AddFirst(4);
            DLL.AddFirst(6);
            DLL.AddFirst(2);
            DLL.AddLast(17);
            DLL.AddLast(5);
            Console.WriteLine("Elementi Doubly Linked Liste: ");
            foreach (Object obj in DLL)
            {
                Console.WriteLine(obj);
            }
            DLL.RemoveLast();
            DLL.RemoveFirst();
            Console.WriteLine("Elementi Doubly Linked Liste nakon brisanja prvog i zadnjeg: ");
            foreach (Object obj in DLL)
            {
                Console.WriteLine(obj);
            }
            DLL.Remove(17);
            Console.WriteLine("Brisanje elementa 17 sa liste: ");
            foreach (Object obj in DLL)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("Sadrzi li dvostruka lista 4?" + DLL.Contains(4));
            DLL.Clear();

        }
        public void StackPrimjer()
        {

        }
        public void QueuePrimjer()
        {

        }
        public void HashTablePrimjer()
        {

        }
    }
}
