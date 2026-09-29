// SoundCore Engine v2.0 - TecNM Campus Monclova
// Authors: Rosembert Jared Ortiz Reyes - I25050406
// Date: 28/09/2026 | Version: 1.0

using System.Collections;

namespace SoundCore.EstructurasPropias
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class SimpleLinkedList<T> : IEnumerable<T>
    {
        public Node<T>? Head { get; private set; }
        public int Count { get; private set; }

        public bool IsEmpty => Head == null;

        public void AddToEnd(T value)
        {
            var newNode = new Node<T>(value);
            if (IsEmpty)
            {
                Head = newNode;
            }
            else
            {
                var current = Head!;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }
            Count++;
        }

        public void PlayNext(T value)
        {
            var newNode = new Node<T>(value);
            if (IsEmpty)
            {
                Head = newNode;
            }
            else
            {
                newNode.Next = Head!.Next;
                Head.Next = newNode;
            }
            Count++;
        }

        public T AdvanceTrack()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The playback queue is empty.");

            T value = Head!.Value;
            Head = Head.Next;
            Count--;
            return value;
        }

        public void Reverse()
        {
            Node<T>? previous = null;
            Node<T>? current = Head;
            Node<T>? next = null;

            while (current != null)
            {
                next = current.Next; //Guardar puntero al resto de la lista
                current.Next = previous; //invertir la referencia
                previous = current; //Dezplazar previo
                current = next; //Dezplazar el actual
            }

            Head = previous;
        }

        public void InsertSort(T value, Comparison<T> comparator)
        {
            var newNode = new Node<T>(value);

            if (IsEmpty || comparator(value, Head!.Value) < 0)
            {
                newNode.Next = Head;
                Head = newNode;
                Count++;
                return;
            }

            var current = Head;
            while (current.Next != null && comparator(value, current.Next.Value) >= 0)
                current = current.Next;

            newNode.Next = current.Next;
            current.Next = newNode;
            Count++;
        }

        public void DebugDuplicates(Func<T, T, bool> areEqual)
        {
            var current = Head;

            while (current != null)
            {
                var runner = current;
                while (runner.Next != null)
                {
                    if (areEqual(current.Value, runner.Next.Value))
                    {
                        runner.Next = runner.Next.Next;
                        Count--;
                    }
                    else
                    {
                        runner = runner.Next;
                    }
                }
                current = current.Next;
            }
        }

        public void Sort(Comparison<T> comparator)
        {
            Node<T>? sorted = null;
            var current = Head;

            while (current != null)
            {
                var next = current.Next;

                if (sorted == null || comparator(current.Value, sorted.Value) < 0)
                {
                    current.Next = sorted;
                    sorted = current;
                }
                else
                {
                    var p = sorted;
                    while (p.Next != null && comparator(current.Value, p.Next.Value) >= 0)
                        p = p.Next;
                    current.Next = p.Next;
                    p.Next = current;
                }
                current = next;
            }

            Head = sorted;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}