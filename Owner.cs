using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Library__WPF
{
    public class Owner : ICloneable, IComparable<Owner>
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set => birthDate = value;
        }

        public Owner(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public Owner() { }

        public object Clone()
        {
            return new Owner(this.firstName, this.lastName, this.birthDate);
        }

        public int CompareTo(Owner other)
        {
            return string.Compare(this.lastName, other.lastName, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Owner other = (Owner)obj;
            return firstName == other.firstName && lastName == other.lastName && birthDate == other.birthDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(firstName, lastName, birthDate);
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}, born on {birthDate.ToShortDateString()}";
        }
    }
}
