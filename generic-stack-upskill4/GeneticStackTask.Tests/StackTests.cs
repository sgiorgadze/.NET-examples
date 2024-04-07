﻿using System;
using System.Linq;
using NUnit.Framework;

namespace GenericStackTask.Tests
{
    [TestFixture(new[] { 12, 3, 4, int.MaxValue, int.MinValue, -12, 45, 12 }, 67, 8, TypeArgs = new Type[] { typeof(int) })]
    [TestFixture(new[] { 0.362, 3.0987, -198.4, -1008245.78, 9012.0001 }, 0.8911, 5, TypeArgs = new Type[] { typeof(double) })]
    [TestFixture(new[] { "12", "Zero", "Test", "Hello" }, "Hello, world!", 4, TypeArgs = new Type[] { typeof(string) })]
    [TestFixture(new[] { "12", "Zero", null, "Test", "Hello", null }, null, 6, TypeArgs = new Type[] { typeof(string) })]
    [TestFixture(new[] { 'a', '\n', '4', '5' }, '\t', 4, TypeArgs = new Type[] { typeof(char) })]
    [TestFixture(new[] { true, false, true, true }, false, 4, TypeArgs = new Type[] { typeof(bool) })]
    public class StackTests<T>
    {
        private const int someValue = 3;
        private readonly Stack<T> stack;
        private readonly T[] array;
        private readonly T value;
        private readonly int initCount;

        public StackTests(T[] source, T value, int count)
        {
            this.stack = new Stack<T>(source);
            this.value = value;
            this.initCount = count;
            this.array = source;
        }

        [Test]
        [Order(0)]
        public void Ctor_BasedOnEnumerableSource()
        {
            Assert.That(this.stack.Count == this.initCount);
        }

        [Test]
        [Order(1)]
        public void Iterator_Test()
        {
            int i = this.initCount;
            foreach (var item in this.stack)
            {
                Assert.AreEqual(item, this.array[--i]);
            }
        }

        [Test]
        [Order(2)]
        public void ToArray_Test()
        {
            T[] copy = this.stack.ToArray();
            Assert.Multiple(() =>
            {
                CollectionAssert.AreEqual(this.array.Reverse(), copy);
                Assert.AreNotSame(this.array, copy);
            });
        }

        [Test]
        [Order(3)]
        public void Push_OneElement()
        {
            int count = this.stack.Count;
            this.stack.Push(this.value);
            Assert.Multiple(() =>
            {
                CollectionAssert.Contains(this.stack, this.value);
                Assert.That(this.stack.Count == count + 1);
            });
        }

        [Test]
        [Order(4)]
        public void Contains_Test()
        {
            Assert.IsTrue(this.stack.Contains(this.value));
        }

        [Test]
        [Order(4)]
        public void Peek_Test()
        {
            int count = this.stack.Count;
            T actual = this.stack.Peek();
            Assert.Multiple(() =>
            {
                Assert.That(this.stack.Count == count);
                Assert.AreEqual(this.value, actual);
            });
        }

        [Test]
        [Order(5)]
        public void Push_SomeElement()
        {
            int count = this.stack.Count;
            for (int i = 1; i <= someValue; i++)
            {
                this.stack.Push(this.value);
            }

            Assert.That(this.stack.Count == count + someValue);
        }

        [Test]
        [Order(6)]
        public void Pop_SomeElement()
        {
            int count = this.stack.Count;
            for (int i = 1; i <= someValue; i++)
            {
                _ = this.stack.Pop();
            }

            Assert.That(this.stack.Count == count - someValue);
        }

        [Test]
        [Order(7)]
        public void Iterator_PopFromStack_Throw_InvalidOperationException()
        {
            _ = Assert.Throws<InvalidOperationException>(
                () =>
            {
                foreach (var item in this.stack)
                {
                    this.stack.Pop();
                }
            }, "Stack cannot be changed during iteration.");
        }

        [Test]
        [Order(8)]
        public void Iterator_PushIntoStack_Throw_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
            {
                foreach (var item in this.stack)
                {
                    this.stack.Push(this.value);
                }
            }, "Stack cannot be changed during iteration.");
        }

        [Test]
        [Order(9)]
        public void Clear_Test()
        {
            this.stack.Clear();
            Assert.That(this.stack.Count == 0);
        }

        [Test]
        [Order(10)]
        public void Pop_StackIsEmpty_ThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => this.stack.Pop(), "Invalid operation pop, stack is empty.");
        }
    }
}
