using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Library__WPF
{
    public class Settlement : ICloneable, IComparable<Settlement>
    {
        public static int totalSettlements = 0;
        private int settlementNumber;
        private List<LandPlot> landPlots;

        public Settlement()
        {
            LandPlots = new List<LandPlot>();
        }

        public int SettlementNumber
        {
            get => settlementNumber;
            set => settlementNumber = value;
        }

        public List<LandPlot> LandPlots
        {
            get => landPlots;
            set => landPlots = value;
        }

        static Settlement()
        {
            totalSettlements = 0;
        }

        public Settlement(List<LandPlot> landPlots)
        {
            this.settlementNumber = ++totalSettlements;
            this.landPlots = landPlots;
        }


        public object Clone()
        {
            return new Settlement(new List<LandPlot>(this.landPlots));
        }

        public int CompareTo(Settlement other)
        {
            return this.settlementNumber.CompareTo(other.settlementNumber);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Settlement other = (Settlement)obj;
            return settlementNumber == other.settlementNumber && Equals(landPlots, other.landPlots);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(settlementNumber, landPlots);
        }

        public override string ToString()
        {
            return $"Settlement #{settlementNumber}, Total Land Plots: {landPlots.Count}";
        }

        public string GetShortInfo()
        {
            return $"Class: Settlement, Number: {settlementNumber}";
        }
    }
}
