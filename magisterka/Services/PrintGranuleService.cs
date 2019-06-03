using System.Collections.Generic;
using System.Linq;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class PrintGranuleService : IPrintGranuleService
    {
        private readonly char _separator = ';';

        public List<string> Print(GranuleSet granuleSet)
        {
            var content = new List<string> { _printHeader(granuleSet) };

            if (granuleSet.Count > 0)
            {
                content.AddRange(_printContent(granuleSet));
            }

            return content;
        }

        private List<string> _printContent(GranuleSet granuleSet)
        {
            var granulesInPreviousOrder = granuleSet.OrderBy(x => x.ObjectNumber).ToList();
            var content = new List<string>();

            var length = granulesInPreviousOrder[0].Count();
            for (var i = 0; i < length; i++)
            {
                var line = $"u{i + 1}";

                foreach (var granule in granulesInPreviousOrder)
                {
                    line += _separator + granule[i].ToString();
                }

                content.Add(line);
            }

            return content;
        }

        private string _printHeader(GranuleSet granuleSet)
        {
            var header = "" + _separator;

            for (var i = 0; i < granuleSet.Count; i++)
            {
                if (i > 0)
                {
                    header += _separator;
                }

                header += $"g(u{i + 1})";
            }

            return header;
        }
    }
}
