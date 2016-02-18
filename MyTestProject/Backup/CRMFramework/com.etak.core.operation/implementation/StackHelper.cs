using System.Collections.Generic;
using System.Threading;
using com.etak.core.model.operation;

namespace com.etak.core.operation.implementation
{
    class StackHelper
    {
       
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static ThreadLocal<Stack<BusinessOperationExecution>> _currentOperationStack = new ThreadLocal<Stack<BusinessOperationExecution>>(
         () => new Stack<BusinessOperationExecution>());

        public static Stack<BusinessOperationExecution> Stack
        {
            get { return _currentOperationStack.Value; }
        }

    }
}
