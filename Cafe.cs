using System;
using System.Collections;
using System.Collections.Generic;

namespace CafeApp
{
    public class Cafe : IEnumerable<Employee>
    {
        public string Name { get; set; }
        private List<Employee> _employees = new List<Employee>();

        public Cafe(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cafe name cannot be empty.");
            Name = name.Trim();
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            _employees.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            if (employee != null && _employees.Contains(employee))
            {
                _employees.Remove(employee);
            }
        }

        public bool HasEmployees()
        {
            return _employees.Count > 0;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                yield return _employees[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
