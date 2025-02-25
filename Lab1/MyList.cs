using System;

namespace ProjectVector
{
    public class MyList<T> : ArrayDecorator<T>
    {
        protected int _count;

        public int Count
        {
            get { return _count; }
        }

        public int Capacity
        {
            get { return _array.Length; }
        }

        protected void Resize()
        {
            var wrapper = new ArrayDecorator<T>(new T[Capacity*2]);

            for (int i = 0; i < this.Length; i++)
            {
                wrapper[i] = this[i];
            }

            this.WrapFor(wrapper);
        }

        public void Add(T value)
        {
            this[_count] = value;
            _count++;
            if (_count >= Capacity) { Resize(); }
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс выходит за пределы допустимого диапазона.");

            if (_count >= Capacity)
            {
                Resize();
            }

            for (int i = _count; i > index; i--)
            {
                this[i] = this[i - 1];
            }

            this[index] = value;
            _count++;
        }

        public MyList(int capacity = 4)
            :base(new T[capacity]) { }

    }
}