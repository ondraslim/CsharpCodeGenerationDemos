using System;

namespace T4Example.RuntimeTemplates
{
    // Extension of the EmailRuntimeTextTemplate
    // adds Name and SentDate properties,
    // which can be accessed by the .tt template
    public partial class EmailRuntimeTextTemplate
    {
        public string Name { get; }
        public DateTime SentDate { get; }

        public EmailRuntimeTextTemplate(string name, DateTime sentDate)
        {
            Name = name;
            SentDate = sentDate;
        }
    }
}