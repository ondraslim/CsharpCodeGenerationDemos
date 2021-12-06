using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExample
{
    class Program
    {
        static void Main()
        {
            RunIntrospectionDemo();
            RunMethodDemo();
            RunAssemblyScanDemo();
            RunObjectCreationDemo();

            Console.ReadKey();
        }

        private static void RunIntrospectionDemo()
        {
            Console.WriteLine(nameof(RunIntrospectionDemo));
            var sampleClass = new SampleClass();

            // obtain type instance
            Type type = sampleClass.GetType();
            Console.WriteLine($"Type: {type.Name}");

            // obtain method infos of the type
            // can be done similarly for fields, properties, etc.
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (var methodIn in methods)
            {
                Console.WriteLine($"Method: {methodIn.Name}");
            }
        }

        private static void RunMethodDemo()
        {
            Console.WriteLine(nameof(RunMethodDemo));
            var sampleClass = new SampleClass();

            // obtain type instance
            Type type = sampleClass.GetType();

            // obtain information about a specific method
            MethodInfo? method = type.GetMethod(nameof(SampleClass.MyMethod));
            Console.WriteLine($"Method name: {method.Name}, return type: {method.ReturnType.Name}");
            
            // obtain information about it's parameters
            var parameters = method.GetParameters().Select(p => $"{p.ParameterType.Name}:{p.Name}");
            Console.WriteLine($"Parameters: {string.Join(", ", parameters)}");

            // invoke the method
            method.Invoke(sampleClass, new object[] { "Hello World!" });
        }

        private static void RunAssemblyScanDemo()
        {
            Console.WriteLine(nameof(RunAssemblyScanDemo));

            // get types in executing assembly
            List<Type> assemblyClassTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass)
                .ToList();

            // get classes implementing interface ISampleInterface
            var classesOfSampleInterface = assemblyClassTypes
                .Where(t => t.GetInterfaces().Contains(typeof(ISampleInterface)));

            Console.WriteLine($"Classes implementing {nameof(ISampleInterface)}: " +
                              $"{string.Join(", ", classesOfSampleInterface)}");

            // get classes not implementing interface ISampleInterface
            var classesNotOfSampleInterface = assemblyClassTypes
                .Where(t => !t.GetInterfaces().Contains(typeof(ISampleInterface)));

            Console.WriteLine($"Classes not implementing {nameof(ISampleInterface)}: " +
                              $"{string.Join(", ", classesNotOfSampleInterface)}");
        }

        private static void RunObjectCreationDemo()
        {
            Console.WriteLine(nameof(RunObjectCreationDemo));

            // create an instance of EmptyClass by invoking a parameterless constructor
            object emptyClass = Activator.CreateInstance(typeof(EmptyClass));
            
            Console.WriteLine(emptyClass.ToString());

            // create an instance of SampleClass with constructor matching the provided arguments
            // int: 1, string: "myText"
            SampleClass sampleClass = (SampleClass) Activator.CreateInstance(
                typeof(SampleClass), 1, "myText");
            
            Console.WriteLine(sampleClass.ToString());
            Console.WriteLine(sampleClass.MyProperty);
            sampleClass.MyMethod("Hello World!");
        }

    }
}