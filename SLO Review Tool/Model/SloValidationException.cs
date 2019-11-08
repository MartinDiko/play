using System;
using System.Collections.Generic;

namespace SloReviewTool.Model
{
    public class SloValidationException : Exception
    {
        public SloParsingContext Context { get; set; }
        public string Section { get; set; }
        public Dictionary<object, object> Container { get; set; }
        public string Property { get; set; }

        public SloValidationException(SloParsingContext context, string section, Dictionary<object, object> container, string property)
        {
            Context = context;
            Section = section;
            Container = container;
            Property = property;
        }

        public override string Message {
            get {
                return $"Failure parsing '{Context.Record.ServiceName}' in Section '{Section}' at Property '{Property}'";
            }
        }
}
}
