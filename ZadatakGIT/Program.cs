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

        }
        public void DLLPrimjer()
        {

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
