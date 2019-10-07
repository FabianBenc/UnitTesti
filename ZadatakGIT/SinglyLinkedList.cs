using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakGIT
{
    public class SinglyLinkedList
    {
        private Node head;
        private Node current;//zadnji ili tail node
        public int Count;

        public SinglyLinkedList()
        {
            head = new Node();
            current = head;
        }

        public void AddAtLast(object data)
        {
            Node newNode = new Node();
            newNode.Value = data;
            current.Next = newNode;
            current = newNode;
            Count++;
        }

        public void AddAtStart(object data)
        {
            Node newNode = new Node() { Value = data };
            newNode.Next = head.Next;
            head.Next = newNode;
            Count++;
        }

        public void RemoveFromStart()
        {
            if (Count > 0)
            {
                head.Next = head.Next.Next;
                Count--;
            }
            else 
            {
                Console.WriteLine("Nema elemenata u ovoj linked listi.");
            }
        }
        public void PrintAllNodes()
        {
            Console.Write("Head->");
            Node curr = head;
            while (curr.Next != null)
            {
                curr = curr.Next;
                Console.Write(curr.Value);
                Console.Write("->");
            }
            Console.Write("NULL");
        }
    }

    
}
