using System;

namespace CodebridgeTestTask.Bll.Extension
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
