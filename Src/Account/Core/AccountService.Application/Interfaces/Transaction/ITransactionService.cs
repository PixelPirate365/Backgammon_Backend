using System.Transactions;

namespace AccountService.Application.Interfaces.Transaction
{
    public interface ITransactionService {
        TransactionScope CreateAsyncTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
