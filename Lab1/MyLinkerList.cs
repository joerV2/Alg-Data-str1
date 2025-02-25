using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectVector
{
    public class MyLinkedList<T> : IEnumerable
    {
        
        public IEnumerator GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Content;
                current = current.Next;
            }
        }

        public class MyNode<T>
        {
            private T _content;
            private MyNode<T> _next;
            private MyLinkedList<T> _master;

            public T Content
            {
                get { return _content; }
                set { _content = value; }
            }

            public MyNode<T> Next
            {
                get
                {
                    _master._countOperations++;
                    return _next;
                }
                set
                {
                    _master._countOperations++;
                    _next = value;
                }
            }
            public MyNode(MyLinkedList<T> master, T value)
            {
                _master = master;
                Content = value;
                Next = null;
            }
        }

        protected int _countOperations;

        protected MyNode<T> _head;
        protected MyNode<T> _tail;

        private int _count;
        public int Count => _count;//Отслеживание кол-ва элементов в списке

        public int CountOperations
        {
            get { return _countOperations; }
        }

        public MyLinkedList()
        {
            _countOperations = 0;
            _head = _tail = null;
            _count = 0;
        }

        public void Add(T value)
        {
            var node = new MyNode<T>(this, value);

            if (_head == null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = node;
            }
            _count++;
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы допустимого диапазона.");

            var newNode = new MyNode<T>(this, value);

            if (index == 0)
            {
                newNode.Next = _head;
                _head = newNode;
                if (_count == 0)
                    _tail = newNode;
            }
            else
            {
                var current = _head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
                if (newNode.Next == null)
                    _tail = newNode;
            }
            _count++;
        }
    }
}
