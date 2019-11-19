using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SloReviewTool.Model
{
    public class SloParsingContext
    {
        public SloRecord Record { get; set; }
        public string Yaml { get; set; }

        public SloParsingContext(string yaml)
        {
            Yaml = yaml;
        }

        public SloParsingContext(SloRecord record)
        {
            Record = record;
        }
    }
}
