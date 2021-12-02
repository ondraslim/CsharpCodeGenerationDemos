using System;

namespace ReflectionExample
{
    public class SampleClass : ISampleInterface
    {
        private int myField = 42;

        public string MyProperty { get; set; } = "myPropertyValue";

        public SampleClass()
        {
        }

        public SampleClass(int myField, string myProperty)
        {
            this.myField = myField;
            MyProperty = myProperty;
        }

        public void MyMethod(string text)
        {
            Console.WriteLine(text);
        }

    }
}
