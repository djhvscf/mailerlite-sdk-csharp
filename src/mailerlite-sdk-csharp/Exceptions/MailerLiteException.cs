using System;

namespace mailerlite_sdk_csharp.Exceptions
{
    public class MailerLiteException : Exception
    {
        public MailerLiteException() : base() { }

        public MailerLiteException(string message) : base(message) { }
    }
}
