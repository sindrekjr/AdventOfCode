using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day23;

class Solution : SolutionBase
{
    public Solution() : base(23, 2020, "Crab Cups") { }

    protected override string SolvePartOne()
        => ProcessMoves(Input.ToIntArray().ToList(), 100).JoinAsStrings();

    protected override string SolvePartTwo()
    {
        // var cups = Input.ToIntArray().ToList();
        // for (int i = cups.Max() + 1; i <= 1000000; i++) cups.Add(i);
        // cups = ProcessMoves(cups, 10000000);
        // var target = cups.IndexOf(1);
        // return cups.Skip(target).Take(2).Aggregate(1, (product, i) => product * i).ToString();
        return "";
    }

    List<int> ProcessMoves(List<int> cups, int amount)
    {
        var current = 0;
        // var history = new Dictionary<(int, string), int>();
        for (int c = 0, currentCup = cups[current]; c < amount; c++, currentCup = cups[current])
        {
            // Console.WriteLine("-- move " + (c + 1) + " --");
            // Console.WriteLine("cups: " + cups.JoinAsStrings(" "));

            var picked = new List<int>();
            while (picked.Count < 3)
            {
                picked.Add(NextClockwise(cups, current));
            }

            // Console.WriteLine("pick up: " + picked.JoinAsStrings(" "));

            var destination = currentCup- 1;
            while (!cups.Contains(destination))
            {
                destination = destination < cups.Min()
                    ? cups.Max()
                    : destination - 1;
            }

            // Console.WriteLine("destination: " + destination);

            for (int i = 2, dest = cups.IndexOf(destination) + 1; i >= 0; i--) cups.Insert(dest, picked[i]);
            
            // var sequence = cups.JoinAsStrings();
            // if (history.ContainsKey((current, sequence)))
            // {
            //     var diff = c - history[(current, sequence)];
            //     c += diff * (int) Math.Floor((double) (amount - c) / diff);
            // }
            // else
            // {
            //     history.Add((current, sequence), c);
            // }
            
            current = cups.IndexOf(currentCup) + 1;
            if (current >= cups.Count) current = 0;

            // Console.WriteLine();
        }

        return cups;
    }

    int NextClockwise(List<int> cups, int i)
    {
        i = ++i < cups.Count ? i : 0;
        var next = cups[i];
        cups.RemoveAt(i);
        return next;
    }
}

// class LinkedList<T> : IList<T>
// {
//     T Head { get; set; }
//     LinkedList<T> Tail { get; set; }
//     int Size { get; set; } = 0;

//     public int Count => Size;

//     public bool IsReadOnly => false;

//     public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//     public LinkedList() { }

//     public LinkedList(T head) : this(head, null) { }

//     public LinkedList(IEnumerable<T> elements) : this(elements.First(), new LinkedList<T>(elements.Skip(1))) { }

//     LinkedList(T head, LinkedList<T> rest)
//     {
//         Add(head);
//         Tail = rest;

//         if (Tail != null) Size += Tail.Size;
//     }

//     public int IndexOf(T item) => Head.Equals(item) ? 0 : Tail.IndexOf(item) + 1;

//     public void Insert(int index, T item)
//     {
//         if (index == 0) 
//         {
//             Tail = new LinkedList<T>(Head, Tail);
//             Head = item;
//         }
//         else
//         {
//             if (Tail == null) Tail = new LinkedList<T>();
//             Tail.Insert(index - 1, item);
//         }

//         Size++;
//     }

//     public void RemoveAt(int index)
//     {
//         if (index == 0)
//         {
//             Head = Tail != default ? Tail.Head : default;
//             Tail = Tail != default ? Tail.Tail : default;
//         }
//         else
//         {
//             Tail.RemoveAt(index - 1);
//         }

//         Size--;
//     }

//     public void Add(T item)
//     {
//         if (Head == null)
//         {
//             Head = item;
//         }
//         else if (Tail == null)
//         {
//             Tail = new LinkedList<T>(item);
//         }
//         else
//         {
//             Tail.Add(item);
//         }
        
//         Size++;
//     }

//     public void Clear()
//     {
//         Head = default;
//         Tail = default;
//         Size = default;
//     }

//     public bool Contains(T item) => Head.Equals(item) || Tail != null && Tail.Contains(item);

//     public void CopyTo(T[] array, int arrayIndex)
//     {
//         throw new NotImplementedException();
//     }

//     public bool Remove(T item)
//     {
//         throw new NotImplementedException();
//     }

//     public IEnumerator<T> GetEnumerator()
//     {
//         throw new NotImplementedException();
//     }

//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         throw new NotImplementedException();
//     }
// }
