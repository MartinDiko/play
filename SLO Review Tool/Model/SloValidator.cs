using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    public class SloValidator
    {
        public static string GetString(string section, Dictionary<object, object> container, string property)
        {
            if (!container.ContainsKey(property)) throw new SloValidationException(ThreadContext<SloParsingContext>.ForThread(), section, container, property);
            return container[property] as string;
        }

        public static List<object> GetList(string section, Dictionary<object, object> container, string property)
        {
            if (!container.ContainsKey(property)) throw new SloValidationException(ThreadContext<SloParsingContext>.ForThread(), section, container, property);
            return container[property] as List<object>;
        }
    }
}
