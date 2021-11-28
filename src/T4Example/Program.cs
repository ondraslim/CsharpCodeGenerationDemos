﻿using System;
using System.Collections.Generic;
using T4Example.RuntimeTemplates;

namespace T4Example
{
    class Program
    {
        static void Main()
        {
            RunRuntimeTemplateDemo();
            RunEmailRuntimeTemplateDemo();
            RunDesignTimeTemplateDemo();
        }

        private static void RunRuntimeTemplateDemo()
        {
            // prepare data
            var data = new List<Data>()
            {
                new() { Name = "Name1", Value = "Value1" },
                new() { Name = "Name2", Value = "Value2" },
                new() { Name = "Name3", Value = "Value3" },
            };

            // use the template and data to produce text
            var basicTemplate = new RuntimeTextTemplate(data);
            var text = basicTemplate.TransformText();

            Console.WriteLine(text);
        }

        private static void RunEmailRuntimeTemplateDemo()
        {
            IEmailService emailService = new EmailService();

            // use the template and data to produce email text
            var emailTemplate = new EmailRuntimeTextTemplate("Joe", new DateTime(2010, 11, 22));
            var emailText = emailTemplate.TransformText();

            emailService.SendEmail(emailText, "me@me.me");
        }

        private static void RunDesignTimeTemplateDemo()
        {
            var generatedClass = new GeneratedClass()
            {
                Id = Guid.NewGuid(),
                Name = "Joe",
                Age = 42
            };

            Console.WriteLine(generatedClass.ToString());
        }
    }
}