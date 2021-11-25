using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace CodeDomExample
{
    class Program
    {
        static void Main()
        {
            var model = CreateCodeDomModel();

            var options = new CodeGeneratorOptions
            {
                BlankLinesBetweenMembers = true,
                BracingStyle = "C"
            };

            GenerateCsharpModel(model, "Squirrel.cs", options);
        }

        private static CodeCompileUnit CreateCodeDomModel()
        {
            var codeNamespace = CreateNamespace();
            var codeClass = CreateClass();

            var compilationUnit = new CodeCompileUnit();

            codeNamespace.Types.Add(codeClass);
            compilationUnit.Namespaces.Add(codeNamespace);

            return compilationUnit;
        }

        private static CodeTypeDeclaration CreateClass()
        {
            var codeClass = new CodeTypeDeclaration("Squirrel")
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };

            AddNameField(codeClass);
            AddNameProperty(codeClass);
            AddHelloMethod(codeClass);

            return codeClass;
        }

        private static void AddNameField(CodeTypeDeclaration codeClass)
        {
            var nameField = new CodeMemberField(new CodeTypeReference(typeof(string)), "name")
            {
                Attributes = MemberAttributes.Private,
                InitExpression = new CodePrimitiveExpression("Chip")
            };
            codeClass.Members.Add(nameField);
        }

        private static void AddNameProperty(CodeTypeDeclaration codeClass)
        {
            var nameProperty = new CodeMemberProperty
            {
                Type = new CodeTypeReference(typeof(string)),
                Name = "Name",
                Attributes = MemberAttributes.Public,
                HasGet = true,
            };

            nameProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), "name")));

            codeClass.Members.Add(nameProperty);
        }

        private static void AddHelloMethod(CodeTypeDeclaration codeClass)
        {
            var helloMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public,
                Name = "SayHello"
            };
            
            var parameter = new CodeParameterDeclarationExpression(typeof(string), "greetee");
            helloMethod.Parameters.Add(parameter);

            var greetSnippet = new CodeSnippetExpression(
                "Console.WriteLine($\"Squirrel named '{Name}' is greeting '{greetee}'\")");
            var greetStatement = new CodeExpressionStatement(greetSnippet);
            helloMethod.Statements.Add(greetStatement);

            codeClass.Members.Add(helloMethod);
        }

        private static CodeNamespace CreateNamespace()
        {
            var codeNamespace = new CodeNamespace("CodeDom.Generated");
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            return codeNamespace;
        }

        private static void GenerateCsharpModel(
            CodeCompileUnit model,
            string fileName,
            CodeGeneratorOptions options)
        {
            var provider = new CSharpCodeProvider();
            using var sw = new StreamWriter(fileName);
            provider.GenerateCodeFromCompileUnit(model, sw, options);
        }
    }
    

}