using System;

namespace BugTracker.Services
{
    public class TrelloRequestFailedException : Exception
    {
        public TrelloRequestFailedException()
        {
        }

        public TrelloRequestFailedException(string message) : base(message)
        {
        }

        public TrelloRequestFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}