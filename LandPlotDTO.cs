using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Library__WPF
{
    public class LandPlotDTO
    {
        public OwnerDTO Owner { get; set; }
        public DescriptionDTO Description { get; set; }
        public Purpose Purpose { get; set; }
        public double MarketValue { get; set; }
    }
}
