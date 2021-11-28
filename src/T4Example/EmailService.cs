using System;

namespace T4Example
{
    public interface IEmailService
    {
        public void SendEmail(string text, string to);
    } 

    public class EmailService : IEmailService
    {
        public void SendEmail(string text, string to)
        {
            Console.WriteLine($"Email to: {to}");
            Console.WriteLine($"Email body:{text}");
        }
    }
}