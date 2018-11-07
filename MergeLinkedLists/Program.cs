using ConcurrentPriorityQueue;
using System;

namespace MergeLinkedLists
{
  class Program
  {
    static void Main(string[] args)
    {
      ListNode list1 = new ListNode { Value = 1, Next = new ListNode { Value = 3, Next = new ListNode { Value = 5, Next = new ListNode { Value = 10 } } } };
      ListNode list2 = new ListNode { Value = 2, Next = new ListNode { Value = 4, Next = new ListNode { Value = 6, Next = new ListNode { Value = 11 } } } };
      ListNode list3 = new ListNode { Value = 1, Next = new ListNode { Value = 2, Next = new ListNode { Value = 7, Next = new ListNode { Value = 8 } } } };

      ListNode mergedList = Merge(new ListNode[] { list1, list2, list3 });

      PrintList(mergedList);

      Console.WriteLine("Press any key to finish...");
      Console.ReadKey();
    }

    private static void PrintList(ListNode mergedList)
    {
      ListNode currentNode = mergedList;

      while (currentNode != null)
      {
        Console.Write(currentNode.Value);
        currentNode = currentNode.Next;
        if (currentNode != null) Console.Write(" - ");
      }

      Console.WriteLine();
    }

    public static ListNode Merge(ListNode[] lists)
    {
      ListNode mergedList = null;
      ListNode mergedListTail = null;

      ConcurrentPriorityQueue<ListNode, int> queue = new ConcurrentPriorityQueue<ListNode, int>();
      for (int i = 0; i < lists.Length; i++)
      {
        queue.Enqueue(lists[i], -lists[i].Value);
      }

      mergedListTail = GetMinValue(queue);
      mergedList = mergedListTail;

      while (mergedListTail != null)
      {
        ListNode nodeWithMinValue = GetMinValue(queue);

        mergedListTail.Next = nodeWithMinValue;
        mergedListTail = nodeWithMinValue;
      }

      return mergedList;
    }

    private static ListNode GetMinValue(ConcurrentPriorityQueue<ListNode, int> queue)
    {
      if (queue.Count == 0) return null;

      ListNode minNode = queue.Dequeue();

      if (minNode.Next != null)
        queue.Enqueue(minNode.Next, -minNode.Next.Value);

      return minNode;
    }
  }

  public class ListNode
  {
    public int Value { get; set; }
    public ListNode Next { get; set; }
  }
}
