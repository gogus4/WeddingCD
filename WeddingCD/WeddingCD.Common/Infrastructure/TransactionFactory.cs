using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WeddingCD.Common.Infrastructure
{
    /// <summary>
    /// Factory for transaction scopes.
    /// </summary>
    public static class TransactionFactory
    {
        /// <summary>
        /// Starts a suppressed transaction for asynchronous usage.
        /// </summary>
        /// <returns>The transaction scope.</returns>
        public static Transaction StartNewSuppressedTransactionForAsync()
        {
            return new Transaction(true, scopeOptions: TransactionScopeOption.Suppress);
        }
    }
}