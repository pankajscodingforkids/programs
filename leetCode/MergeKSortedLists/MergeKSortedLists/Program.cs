using System;

namespace MergeKSortedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    
    //Definition for singly-linked list.
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    
    public class Solution
    {

        public int IndexOfListWithMinimum(ListNode[] lists)
        {
            int indexOfMin = -1;
            int minSoFar = 0;

            for (int index = 0; index < lists.Length; index++)
            {
                if (lists[index] == null) { continue; }
                if (indexOfMin == -1)
                {
                    indexOfMin = index;
                    minSoFar = lists[index].val;
                }

                if (lists[index].val < minSoFar)
                {
                    indexOfMin = index;
                    minSoFar = lists[index].val;
                }
            }

            return indexOfMin;

        }

        ListNode PluckAtIndex(ListNode[] lists, int Index)
        {
            ListNode pluckedNode = lists[Index];
            lists[Index] = lists[Index].next;
            pluckedNode.next = null;
            return pluckedNode;
        }


        public ListNode MergeKLists(ListNode[] lists)
        {
            ListNode Head = null;
            ListNode Tail = null;

            int indexOfMin = IndexOfListWithMinimum(lists);
            if (indexOfMin == -1) { return Head; }

            // Pluck First Entry
            Head = Tail = PluckAtIndex(lists, indexOfMin);

            indexOfMin = IndexOfListWithMinimum(lists);
            while (indexOfMin != -1)
            {

                // Pluck First Entry
                Tail.next = PluckAtIndex(lists, indexOfMin);
                Tail = Tail.next;
                indexOfMin = IndexOfListWithMinimum(lists);

            }

            return Head;
        }
    }
}
