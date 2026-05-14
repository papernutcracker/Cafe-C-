using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Cafe myCafe = new Cafe("Code & Coffee");

            bool keepRunning = true;

            List<string> mainMenuOptions = new List<string>
            {
                "Add a new employee",
                "Remove an employee",
                "Show all employees",
                "Exit"
            };

            while (keepRunning)
            {
                int selectedIndex = RunInteractiveMenu($"=== {myCafe.Name} Management System ===", mainMenuOptions);

                Console.Clear();
                Console.CursorVisible = true;

                switch (selectedIndex)
                {
                    case 0:
                        AddNewEmployeeUI(myCafe);
                        break;
                    case 1:
                        RemoveEmployeeUI(myCafe);
                        break;
                    case 2:
                        ShowAllEmployeesUI(myCafe);
                        break;
                    case 3:
                        Console.WriteLine("\nExiting the system. Have a great day!");
                        keepRunning = false;
                        break;
                }

                if (keepRunning)
                {
                    Console.CursorVisible = false;
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey(true);
                }
            }
        }

        static int RunInteractiveMenu(string title, List<string> options)
        {
            int selectedIndex = 0;
            ConsoleKey keyPressed;

            do
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine("Use Up/Down arrows to navigate and Enter to select.\n");

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($" > {options[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"   {options[i]} ");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = options.Count - 1;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= options.Count) selectedIndex = 0;
                }

            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }

        static void AddNewEmployeeUI(Cafe cafe)
        {
            Console.WriteLine("--- HIRE NEW EMPLOYEE ---");
            string empName = GetValidInput("Enter employee name: ");
            string empPos = GetValidInput("Enter employee position (e.g., Waiter, Chef): ");

            try
            {
                Employee newEmployee = new Employee(empName, empPos);
                cafe.AddEmployee(newEmployee);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[SUCCESS] {newEmployee.Name} added as {newEmployee.Position}.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR] {ex.Message}");
                Console.ResetColor();
            }
        }

        static void RemoveEmployeeUI(Cafe cafe)
        {
            if (!cafe.HasEmployees())
            {
                Console.WriteLine("The cafe currently has no employees to remove.");
                return;
            }

            List<Employee> currentEmployees = cafe.ToList();
            List<string> options = currentEmployees.Select(e => e.ToString()).ToList();
            options.Add("[Cancel]");

            Console.CursorVisible = false;
            int selectedIndex = RunInteractiveMenu("--- FIRE AN EMPLOYEE ---", options);
            Console.CursorVisible = true;

            if (selectedIndex < currentEmployees.Count)
            {
                Employee employeeToRemove = currentEmployees[selectedIndex];
                cafe.RemoveEmployee(employeeToRemove);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SUCCESS] {employeeToRemove.Name} has been removed from the system.");
                Console.ResetColor();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Operation cancelled.");
            }
        }

        static void ShowAllEmployeesUI(Cafe cafe)
        {
            Console.WriteLine($"--- {cafe.Name.ToUpper()} STAFF LIST ---");

            if (!cafe.HasEmployees())
            {
                Console.WriteLine("The cafe currently has no employees.");
                return;
            }

            foreach (Employee staffMember in cafe)
            {
                Console.WriteLine(staffMember);
            }
        }

        static string GetValidInput(string prompt)
        {
            string input = string.Empty;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input) && input.Length >= 2)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Invalid input. Must be at least 2 characters long. Try again.\n");
                Console.ResetColor();
            }
            return input;
        }
    }
}
