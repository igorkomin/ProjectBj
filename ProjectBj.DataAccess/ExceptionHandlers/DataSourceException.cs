using System;

namespace ProjectBj.DataAccess.ExceptionHandlers
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
