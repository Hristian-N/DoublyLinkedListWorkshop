using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DoublyLinkedListWorkshop
{
    public class LinkedList<T> : IList<T>
    {
        private Node head;
        private Node tail;
        private int count;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public T Head
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException();

                return this.head.Value;
            }
        }

        public T Tail
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException();

                return this.tail.Value;
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
            private set
            {
                this.count = value;
            }
        }

        public void AddWhenEmpty(T value)
        {
            Node tmp = new Node(value);
            this.head = tmp;
            this.tail = tmp;
        }

        public void AddFirst(T value)
        {
            if (Count == 0)
                AddWhenEmpty(value);
            else
            {
                Node tmp = new Node(value);
                tmp.Next = head;
                head.Prev = tmp;
                head = tmp;
            }

            count++;
        }

        public void AddLast(T value)
        {
            if (Count == 0)
                AddWhenEmpty(value);
            else
            {
                Node tmp = new Node(value);

                this.tail.Next = tmp;
                tmp.Prev = tail;
                this.tail = tmp;
            }

            count++;
        }

        public void Add(int index, T value)
        {
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException();
            else if (index == Count)
            {
                AddLast(value);
            }
            else
            {
                Node trav = head;
                for (int i = 0; i < index; i++)
                {
                    trav = trav.Next;
                }

                Node tmp = new Node(value);
                tmp.Next = trav;
                tmp.Prev = trav.Prev;

                trav.Prev = tmp;
                tmp.Prev.Next = tmp;

                count++;
            }

        }

        public T Get(int index)
        {
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException();

            Node trav = head;

            for (int i = 0; i < index; i++)
            {
                trav = trav.Next;
            }

            return trav.Value;
        }

        public int IndexOf(T value)
        {
            int index = 0;
            Node trav = head;

            while (trav != null)
            {
                if (trav.Value.Equals(value))
                    return index;

                trav = trav.Next;
                index++;
            }

            return -1;
        }

        public T RemoveFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            else if (Count == 1)
            {
                T tmp = head.Value;
                head = tail = null;

                count--;

                return tmp;
            }
            else
            {
                T tmp = head.Value;
                head = head.Next;
                head.Prev = null;
                count--;

                return tmp;
            }
        }

        public T RemoveLast()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            else if (Count == 1)
            {
                T tmp = head.Value;
                head = tail = null;

                count--;

                return tmp;
            }
            else
            {
                T tmp = tail.Value;
                tail = tail.Prev;
                tail.Next = null;

                count--;

                return tmp;
            }
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            return new ListEnumerator(this.head);
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<T>)this).GetEnumerator();
        }

        // Use private nested class so that LinkedList users
        // don't know about the LinkedList internal structure
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value
            {
                get;
                private set;
            }

            public Node Next
            {
                get;
                set;
            }

            public Node Prev
            {
                get;
                set;
            }
        }

        // List enumerator that enables traversing all nodes of a list in a foreach loop
        private class ListEnumerator : System.Collections.Generic.IEnumerator<T>
        {
            private Node start;
            private Node current;

            internal ListEnumerator(Node head)
            {
                this.start = head;
                this.current = null;
            }

            public T Current
            {
                get
                {
                    if (this.current == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return this.current.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (current == null)
                {
                    current = this.start;
                    return true;
                }

                if (current.Next == null)
                {
                    return false;
                }

                current = current.Next;
                return true;
            }

            public void Reset()
            {
                this.current = null;
            }
        }
    }
}