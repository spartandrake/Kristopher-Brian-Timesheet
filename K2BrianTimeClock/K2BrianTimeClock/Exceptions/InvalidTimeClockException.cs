using System;

namespace K2BrianTimeClock.DAL.Exceptions
{
    public class InvalidTimeClockException : Exception
    {
        public InvalidTimeClockException()
        {
        }

        public InvalidTimeClockException(string message) : base(message)
        {

        }

        public InvalidTimeClockException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}