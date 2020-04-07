using System;
using System.Text;
using System.Collections.Generic;

namespace Coding.Exercise
{
    public class Field
    {
        public string Name;
        public string Type;

        public Field(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(this.Type) && !string.IsNullOrWhiteSpace(this.Name))
            {
                return $"  public {Type} {Name};\n";
            }

            return string.Empty;
        }
    }
    public class CodeBuilder
    {
        private readonly string className;
        public List<Field> fields = new List<Field>();

        public CodeBuilder(string className)
        {
            this.className = className;
        }

        public CodeBuilder AddField(string fieldName, string fieldType)
        {
            this.fields.Add(new Field(fieldName, fieldType));
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"public class {className}\n{{\n");
            foreach (var f in this.fields)
                sb.Append(f.ToString());
            sb.Append("}");
            return sb.ToString();
        }
    }

    public class Demo
    {
        public static void Mainn(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
