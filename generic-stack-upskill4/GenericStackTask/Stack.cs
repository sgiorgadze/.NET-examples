using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericStackTask
{
    /// <summary>
    /// Represents extendable last-in-first-out (LIFO) collection of the specified type T.
    /// Internally it is implemented as an array, so Push can be O(n). Pop is O(1).
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private T[] items;

        private int count;
        private int version;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class class that is empty and has the default initial capacity.
        /// </summary>
        public Stack()
        {
            this.items = Array.Empty<T>();
            this.count = 0;
            this.version = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements of stack.</param>
        public Stack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative.");
            }

            this.items = new T[capacity];
            this.count = 0;
            this.version = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack{T}"/> class that contains elements copied
        /// from the specified collection and has sufficient capacity to accommodate the  number of elements copied.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public Stack(IEnumerable<T>? collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is ICollection<T> col)
            {
                int size = col.Count;
                this.items = new T[size];

                this.count = size;
                this.version = 0;
                col.CopyTo(this.items, 0);
            }
            else
            {
                this.items = new T[4];
                this.count = 0;
                this.version = 0;

                foreach (var item in collection)
                {
                    this.Push(item);
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the stack.
        /// </summary>
        public int Count => this.count;

        /// <summary>
        /// Removes and returns the object at the top of the stack.
        /// </summary>
        /// <returns>The object removed from the top of the stack.</returns>
        public T Pop()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            this.version++;
            T poppedItem = this.items[--this.count];

            return poppedItem;
        }

        /// <summary>
        /// Returns the object at the top of the stack without removing it.
        /// </summary>
        /// <returns>The object at the top of the stack.</returns>
        public T Peek()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return this.items[this.count - 1];
        }

        /// <summary>
        /// Inserts an object at the top of the stack.
        /// </summary>
        /// <param name="item">The object to push onto the stack.
        /// The value can be null for reference types.</param>
        public void Push(T item)
        {
            if (this.count == this.items.Length)
            {
                int newCapacity = (this.count == 0) ? 4 : this.count * 2;
                Array.Resize(ref this.items, newCapacity);
            }

            this.items[this.count++] = item;
            this.version++;
        }

        /// <summary>
        /// Copies the elements of stack to a new array.
        /// </summary>
        /// <returns>A new array containing copies of the elements of the stack.</returns>
        public T[] ToArray()
        {
            T[] newArray = new T[this.count];
            Array.Reverse(this.items);
            Array.Copy(this.items, newArray, this.count);
            return newArray;
        }

        /// <summary>
        /// Determines whether an element is in the stack.
        /// </summary>
        /// <param name="item">The object to locate in the stack. The value can be null for reference types.</param>
        /// <returns>Return true if item is found in the stack; otherwise, false.</returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < this.count; i++)
            {
                if (comparer.Equals(this.items[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all objects from the stack.
        /// </summary>
        public void Clear()
        {
            Array.Clear(this.items, 0, this.count);
            this.count = 0;
            this.version++;
        }

        /// <summary>
        /// Returns an enumerator for the stack.
        /// </summary>
        /// <returns>Return Enumerator object for the stack.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int currentVersion = this.version;

            for (int i = this.count - 1; i >= 0; i--)
            {
                if (this.version != currentVersion)
                {
                    throw new InvalidOperationException("Stack cannot be changed during iteration.");
                }

                yield return this.items[i];
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
