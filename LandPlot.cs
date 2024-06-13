using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Library__WPF
{
    public class LandPlot : ICloneable, IComparable<LandPlot>
    {
        private Owner owner;
        private Description description;
        private Purpose purpose;
        private double marketValue;

        public Owner Owner
        {
            get => owner;
            set => owner = value;
        }

        public Description Description
        {
            get => description;
            set => description = value;
        }

        public Purpose Purpose
        {
            get => purpose;
            set => purpose = value;
        }

        public double MarketValue
        {
            get => marketValue;
            set => marketValue = value;
        }

        public LandPlot() { }

        public LandPlot(Owner owner, Description description, Purpose purpose, double marketValue)
        {
            this.owner = owner;
            this.description = description;
            this.purpose = purpose;
            this.marketValue = marketValue;
        }

        public object Clone()
        {
            return new LandPlot((Owner)this.owner.Clone(), (Description)this.description.Clone(), this.purpose, this.marketValue);
        }

        public int CompareTo(LandPlot other)
        {
            return this.marketValue.CompareTo(other.marketValue);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            LandPlot other = (LandPlot)obj;
            return Owner.Equals(other.Owner) &&
                   Description.Equals(other.Description) &&
                   Purpose == other.Purpose &&
                   MarketValue == other.MarketValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Owner, Description, Purpose, MarketValue);
        }



        public override string ToString()
        {
            return $"Owner: {Owner.ToString()}, Purpose: {Purpose}, Market Value: {MarketValue} USD";
        }


        public string GetShortInfo()
        {
            return $"Owner's Last Name: {owner.LastName}, Market Value: {marketValue} USD";
        }
    }
}
