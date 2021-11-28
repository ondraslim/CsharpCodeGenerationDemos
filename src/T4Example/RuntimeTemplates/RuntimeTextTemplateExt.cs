using System.Collections.Generic;
using System.Linq;

namespace T4Example.RuntimeTemplates
{
    public partial class RuntimeTextTemplate
    {
        public ICollection<Data> DataCollection { get; }

        public RuntimeTextTemplate(IEnumerable<Data> data)
        {
            DataCollection = data.ToList();
        }
    }

    public class Data
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}