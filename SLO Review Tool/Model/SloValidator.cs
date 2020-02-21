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
        public static string GetString(string section, Dictionary<object, object> container, string property, bool required = true)
        {
            if (!container.ContainsKey(property))
            {
                if (required)
                {
                    throw new SloValidationException(ThreadContext<SloParsingContext>.ForThread(), section, container, property);
                } else
                {
                    return "";
                }
            }
            return container[property] as string;
        }

        public static List<object> GetList(string section, Dictionary<object, object> container, string property)
        {
            if (!container.ContainsKey(property)) throw new SloValidationException(ThreadContext<SloParsingContext>.ForThread(), section, container, property);

            List<object> result;
            result = container[property] as List<object>;
            if (result == null) throw new SloValidationException(ThreadContext<SloParsingContext>.ForThread(), section, container, property);

            return result;
        }
    }
}
