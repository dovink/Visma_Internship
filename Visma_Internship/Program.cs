using System;
using System.ComponentModel;
using Visma_Internship;

namespace VismaResourceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var shortages = new ReadWrite();

            Console.WriteLine("Enter your username");
            var currentUserName = Console.ReadLine();
            var currentRole = GetUserRole();

            while (true)
            {
                Console.WriteLine("Enter command (add, delete, list) or 'exit' to quit:");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                var inputArgs = input.Split(' ');

                switch (inputArgs[0].ToLower())
                {
                    case "add":
                        TaskUtility.Add(currentUserName, shortages);
                        break;
                    case "delete":
                        Console.WriteLine("Provide the title that you would like to delete:");
                       string deleteTitle = Console.ReadLine();
                        shortages.DeleteShortage(deleteTitle, currentUserName, currentRole);
                        break;
                    case "list":
                        TaskUtility.Filter(currentUserName, currentRole, shortages);
                        break;
                    case "exit":
                        return;
                    default:
                        ShowHelp();
                        break;
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("Usage: add/delete/list");
        }
        public static UserRole GetUserRole()
        {
            Console.WriteLine("Enter your role (0 for Regular User, 1 for Administrator):");
            string UserLevel = Console.ReadLine();
            return UserLevel == "1" ? UserRole.Administrator : UserRole.RegularUser;
        }
    }
}
