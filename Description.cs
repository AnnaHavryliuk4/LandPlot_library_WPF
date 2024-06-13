using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Library__WPF
{
    public class Description : ICloneable, IComparable<Description>
    {
        private int groundwaterLevel;
        private string soilType;
        private List<(double, double)> geodeticReference;

        public int GroundwaterLevel
        {
            get => groundwaterLevel;
            set => groundwaterLevel = value;
        }

        public string SoilType
        {
            get => soilType;
            set => soilType = value;
        }

        public List<(double, double)> GeodeticReference
        {
            get => geodeticReference;
            set => geodeticReference = value;
        }

        public Description(int groundwaterLevel, string soilType, List<(double, double)> geodeticReference)
        {
            this.groundwaterLevel = groundwaterLevel;
            this.soilType = soilType;
            this.geodeticReference = geodeticReference;
        }

        public Description() { }

        public object Clone()
        {
            return new Description(this.groundwaterLevel, this.soilType, new List<(double, double)>(this.geodeticReference));
        }

        public int CompareTo(Description other)
        {
            return this.groundwaterLevel.CompareTo(other.groundwaterLevel);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Description other = (Description)obj;
            return groundwaterLevel == other.groundwaterLevel && soilType == other.soilType && Equals(geodeticReference, other.geodeticReference);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(groundwaterLevel, soilType, geodeticReference);
        }

        public override string ToString()
        {
            return $"Soil type: {soilType}, Groundwater level: {groundwaterLevel}m, Geodetic references: {string.Join(", ", geodeticReference)}";
        }
    }
}
