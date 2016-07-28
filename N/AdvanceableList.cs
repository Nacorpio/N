using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace N
{
    /// <summary>
    /// Defines functionality for a collection that can be advanced.
    /// </summary>
    public interface IAdvanceable
    {
        /// <summary>
        /// Gets the current position of the current instance.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Advances the current instance one step.
        /// </summary>
        /// <returns>True if the instance can be advanced further; otherwise false.</returns>
        bool Advance();

        /// <summary>
        /// Goes to a specific position within the current instance.
        /// </summary>
        /// <param name="position">The position within the current instance to go to.</param>
        /// <returns></returns>
        void GoTo(int position);
    }

    /// <summary>
    /// Defines functionality for a generic collection that can be advanced.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IAdvanceable<T> : IAdvanceable
    {
        /// <summary>
        /// Advances the current instance one step.<para />
        /// References the current element.
        /// </summary>
        /// <param name="current"></param>
        /// <returns>True if the instance can be advanced further; otherwise false.</returns>
        bool Advance(out T current);

        /// <summary>
        /// Gets the current element of the current instance.
        /// </summary>
        /// <returns>The current element.</returns>
        T GetCurrent();

        /// <summary>
        /// Gets an element a specific amount of steps forward, without advancing.
        /// </summary>
        /// <param name="n">The amount of steps to peek.</param>
        /// <returns>The element a specific amount of steps forward; otherwise null.</returns>
        T Peek(int n);
    }

    

    /// <summary>
    /// Represents a list that can be advanced throughout.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public class AdvanceableList<T> : IAdvanceable<T>
    {
        private readonly List<T> _elements;

        /// <summary>
        /// Initializes an instance of the <see cref="AdvanceableList{T}"/> class.
        /// </summary>
        public AdvanceableList() : this(new List<T>())
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="AdvanceableList{T}"/> class.
        /// </summary>
        /// <param name="capacity">The preset capacity of the collection.</param>
        public AdvanceableList(int capacity) : this(new List<T>(capacity))
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="AdvanceableList{T}"/> class.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        public AdvanceableList(IEnumerable<T> enumerable)
        {
            _elements = new List<T>(enumerable);
            Position = 1;
        }

        /// <summary>
        /// Gets the current position of the <see cref="AdvanceableList{T}"/>.
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Fired when the <see cref="AdvanceableList{T}"/> has advanced.
        /// </summary>
        public event EventHandler<AdvanceableList<T>> Advanced;

        /// <summary>
        /// Searches for the first occurrence of a specific element, starting at a specific position.
        /// </summary>
        /// <param name="element">The element to search for.</param>
        /// <param name="index">The zero-based index to start searching at.</param>
        /// <returns></returns>
        public int IndexOf(T element, int index)
        {
            return _elements.IndexOf(element, index);
        }

        /// <summary>
        /// Searches for the first occurrence of a specific element.
        /// </summary>
        /// <param name="element">The element to search for.</param>
        /// <returns></returns>
        public int IndexOf(T element)
        {
            return IndexOf(element, 0);
        }

        /// <summary>
        /// Gets a collection of elements within a specific range.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The amount of elements to add.</param>
        /// <returns></returns>
        public IEnumerable<T> GetRange(int startIndex, int count)
        {
            return _elements.GetRange(0, count);
        }

        /// <summary>
        /// Returns a collection of the remaining elements to advance through in the <see cref="AdvanceableList{T}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetRemaining()
        {
            return _elements.GetRange(Position, _elements.Count - Position);
        }

        /// <summary>
        /// Advances the <see cref="AdvanceableList{T}"/> one step.
        /// </summary>
        /// <returns></returns>
        public bool Advance()
        {
            if (Position > _elements.Count)
                return false;

            Position++;
            Advanced?.Invoke(this, this);

            return true;
        }

        /// <summary>
        /// Goes to a specific position within the <see cref="AdvanceableList{T}"/>.
        /// </summary>
        /// <param name="position">The position.</param>
        public void GoTo(int position)
        {
            if (Position < 0 || Position > _elements.Count)
                throw new ArgumentOutOfRangeException(nameof(position));

            Position = position;
        }

        /// <summary>
        /// Advances the <see cref="AdvanceableList{T}"/> one step.<para />
        /// References the current element.
        /// </summary>
        /// <param name="current">The current element.</param>
        /// <returns></returns>
        public bool Advance(out T current)
        {
            if (Advance())
            {
                current = GetCurrent();
                return true;
            }

            current = default(T);
            return false;
        }

        /// <summary>
        /// Advances the <see cref="AdvanceableList{T}"/> while a specific predicate is matching the current element.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns></returns>
        public IEnumerable<T> AdvanceWhile(Predicate<T> predicate)
        {
            var results = new Collection<T>();

            var current = GetCurrent();
            results.Add(current);

            while (Advance(out current))
            {
                results.Add(current);

                if (!predicate(GetCurrent()))
                    break;
            }

            return results;
        }

        /// <summary>
        /// Advances the <see cref="AdvanceableList{T}"/> until a specific predicate is matching the current element.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns></returns>
        public IEnumerable<T> AdvanceUntil(Predicate<T> predicate)
        {
            return AdvanceWhile(x => !predicate(GetCurrent()));
        }

        /// <summary>
        /// Advances the <see cref="AdvanceableList{T}"/> until the last element.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> AdvanceUntilEnd()
        {
            return AdvanceUntil(x => Position == _elements.Count);
        }

        /// <summary>
        /// Peeks the <see cref="AdvanceableList{T}"/> a specific amount of steps.
        /// </summary>
        /// <param name="n">The amount of steps to peek.</param>
        /// <returns></returns>
        public T Peek(int n)
        {
            if (n + Position < 0 || Position + n > _elements.Count)
                throw new ArgumentOutOfRangeException(nameof(n));

            return _elements[Position + n];
        }

        /// <summary>
        /// Gets the current element of the <see cref="AdvanceableList{T}"/>.
        /// </summary>
        /// <returns></returns>
        public T GetCurrent()
        {
            return _elements[Position - 1];
        }
    }
}
