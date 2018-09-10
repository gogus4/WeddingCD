using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Extensions
{
    /// <summary>
    /// Extension class for Task.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Gets a task that has already completed successfully.
        /// </summary>
        /// <remarks>
        /// This should be removed when using .Net 4.6
        /// See https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.completedtask.aspx
        /// </remarks>
        public static readonly Task CompletedTask = Task.FromResult(false);
    }
}