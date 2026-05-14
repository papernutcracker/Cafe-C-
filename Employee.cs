using System;

namespace CafeApp
{
    public class Employee
    {
        public string Name { get; private set; }
        public string Position { get; private set; }

        public Employee(string name, string position)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2)
                throw new ArgumentException("Employee name must be at least 2 characters.");

            if (string.IsNullOrWhiteSpace(position) || position.Trim().Length < 2)
                throw new ArgumentException("Employee position must be at least 2 characters.");

            Name = name.Trim();
            Position = position.Trim();
        }

        public override string ToString()
        {
            return $"[{Position}] {Name}";
        }
    }
}
