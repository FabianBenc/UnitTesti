using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ZadatakGIT
{
    class Program
    {
        static void Main(string[] args)
        {
            Program meni = new Program();

            Console.Write("\n GIT implementacija\n");
            Console.Write("1-Singly linked lista.\n2-Doubly linked lista.\n3-Stack.\n4-Queue.\n5-Hash Table.\n6-Binary Tree.\n7-Izlaz.\n");
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
                    meni.BinaryTreePrimjer();
                    break;
                case "7":
                    break;

                default:
                    break;
            }

        }
        public void SLLPrimjer()
        {
            SinglyLinkedList SLL = new SinglyLinkedList();
            Console.WriteLine("====================================");
            SLL.PrintAllNodes();
            Console.WriteLine();
            Console.WriteLine("Dodavanje elemenata u listu (Add at last)");
            SLL.AddAtLast(12);
            SLL.AddAtLast(13);
            SLL.AddAtLast(14);
            SLL.AddAtLast(15);
            SLL.PrintAllNodes();
            Console.WriteLine();

            Console.WriteLine("Dodavanje elemenata u listu na pocetak (Add at start)");
            SLL.AddAtStart(11);
            SLL.PrintAllNodes();
            Console.WriteLine();
            Console.WriteLine("Brisanje elementa u listi sa pocetka");
            SLL.RemoveFromStart();
            SLL.PrintAllNodes();

            Console.ReadKey();
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
            Hashtable tablica = new Hashtable();

            tablica.Add(1, "Ivica");
            tablica.Add(2, "Marko");
            tablica.Add(3, "Ivan");
            tablica.Add(4, "Marija");

            Console.WriteLine("Broj elemenata u hash tablici:{0}", tablica.Count);
            Console.WriteLine("Ispis elementa pomocu kljuca:{0}", tablica[2]);
            Console.WriteLine("Ispis hash tablice");

            foreach (DictionaryEntry entry in tablica)
            {
                Console.WriteLine("{0} {1}", entry.Key, entry.Value);
            }

            Console.WriteLine("Postoji li kljuc:{0}", tablica.Contains(4));

        }
        public void BinaryTreePrimjer()
        {
            BinaryTree<int> BTree = new BinaryTree<int>();

            Console.WriteLine("========================================");
            BTree.Add(4);
            BTree.Add(2);
            BTree.Add(1);
            BTree.Add(3);
            BTree.Add(6);
            BTree.Add(5);
            BTree.Add(7);

            Console.WriteLine();
            Console.WriteLine("Dodani Elementi stabla: ");
            foreach (Object obj in BTree)
            {
                Console.WriteLine(obj);
            }
            
            BTree.Remove(4);
            BTree.Remove(7);
            Console.WriteLine("Stablo nakon brisanja elemenata: ");
            foreach (Object obj in BTree)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("Sadrzi li stablo element 6?: " + BTree.Contains(6));
        }
    }
}
