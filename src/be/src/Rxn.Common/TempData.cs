using System.Collections.Generic;

namespace Rxn.Common
{
    public class TempData
    {
        public static readonly Dictionary<int, string> Locations = new Dictionary<int, string>
        {
            { 1, "CET" },
            { 2, "SEA" },
        };

        public static readonly Dictionary<int, string> PrePackTypes = new Dictionary<int, string>
        {
            { 1, "Solid Styles" },
            { 2, "Gray" },
            { 3, "Other" },
        };
    }
}