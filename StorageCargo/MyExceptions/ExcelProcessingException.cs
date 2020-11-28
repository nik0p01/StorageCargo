using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageCargo.Exceptions
{
    class ExcelProcessingException : ApplicationException
    {
        public ExcelProcessingException() { }

        public ExcelProcessingException(string message) : base(message) { }

        public ExcelProcessingException(string message, Exception inner) : base(message, inner) { }

    }
}
