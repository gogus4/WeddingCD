namespace WeddingCD.Common
{
    using System;

    /// <summary>
    /// Defines a generic event argument
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public EventArgs(T item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public T Item { get; set; }
    }
}