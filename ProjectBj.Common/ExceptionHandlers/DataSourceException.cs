using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Common.ExceptionHandlers
{
    public class DataSourceException : Exception
    {
        public DataSourceException()
        {
        }

        public DataSourceException(string message) :
            base(message)
        {
        }

        public DataSourceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
