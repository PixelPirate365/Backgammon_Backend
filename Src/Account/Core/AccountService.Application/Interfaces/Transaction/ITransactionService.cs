using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountService.Application.Interfaces.Transaction {
    public interface ITransactionService {
        TransactionScope CreateAsyncTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
