using System;
using System.Collections.Generic;
using System.Text;

namespace K2BrianTimeClock.DAL.Exceptions
{
    public class InvalidClockInException : Exception
    {
        public InvalidClockInException() { }
        public InvalidClockInException(string message) : base(message) { }
        public InvalidClockInException(string message, Exception innerException) : base(message, innerException) { }
    }
}
