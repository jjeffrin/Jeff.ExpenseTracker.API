using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeff.ExpenseTracker.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Error { get; set; }
    }
}
