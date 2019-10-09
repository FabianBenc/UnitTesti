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

        }
    }
}
