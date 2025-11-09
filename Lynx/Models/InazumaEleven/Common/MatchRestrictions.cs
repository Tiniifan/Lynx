using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lynx.Models.InazumaEleven.Common
{
    public static class MatchRestrictions
    {
        public static readonly ReadOnlyCollection<string> IEGO = new ReadOnlyCollection<string>(
            new List<string>
            {
                "No condition (Other)",
                "No condition (Match)",
                "Win with wind team",
                "Win with wood team",
                "Win with fire team",
                "Win with earth team",
                "Win with female team",
                "Win with male team",
                "Win without shot special moves",
                "Win without save special moves"
            }
        );
    }
}
