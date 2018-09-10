namespace WeddingCD.Common
{
    using System;

    /// <summary>
    /// Defines a  generic event argument.
    /// </summary>
    /// <typeparam name="T1">The first generic event arg.</typeparam>
    /// <typeparam name="T2">The second generic event arg.</typeparam>
    public class EventArgs<T1, T2> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="item1">The first generic item.</param>
        /// <param name="item2">The second generic item.</param>
        public EventArgs(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        /// <summary>
        /// Gets or sets the second generic item.
        /// </summary>
        /// <value>
        /// The second generic item.
        /// </value>
        public T2 Item2 { get; set; }

        /// <summary>
        /// Gets or sets the first generic item.
        /// </summary>
        /// <value>
        /// The first generic item.
        /// </value>
        public T1 Item1 { get; set; }
    }
}