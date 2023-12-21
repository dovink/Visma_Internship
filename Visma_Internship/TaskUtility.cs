using System;
using Visma_Internship;

namespace Visma_Internship
{
    public class TaskUtility
    {
        public static void Add(string userName, ReadWrite dataStorage)
        {
            try
            {
                Console.WriteLine("Title: ");
                string title = Console.ReadLine();

                Console.WriteLine("Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Room (Choose a number: 1 - MeetingRoom, 2 - Kitchen, 3 - Bathroom)");
                RoomType room = ParseRoomType(Console.ReadLine());

                Console.WriteLine("Category (Choose a number: 1 - Electronics, 2 - Food, 3 - Other)");
                CategoryType category = ParseCategoryType(Console.ReadLine());

                Console.WriteLine("Priority (1-10):");
                string priorityInput = Console.ReadLine();

                if (!int.TryParse(priorityInput, out int priority) || priority < 1 || priority > 10)
                {
                    Console.WriteLine("Invalid format for priority. Please enter a number between 1 and 10.");
                    return;
                }

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime date = DateTime.Parse(currentDate);

                var shortage = new Shortage
                {
                    CreatorName = userName,
                    Title = title,
                    Name = name,
                    Room = room,
                    Category = category,
                    Priority = priority,
                    CreatedOn = date
            };

                bool isAdded = dataStorage.AddShortage(shortage);

                if (isAdded)
                {
                    Console.WriteLine("Shortage added successfully.");
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format for input. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void Filter(string currentUserName, UserRole currentRole, ReadWrite dataStorage)
        {
            try
            {
                Console.WriteLine("Filter by Title (leave blank if not needed):");
                string listTitle = Console.ReadLine();

                Console.WriteLine("Enter start date for filter (format: yyyy-MM-dd) or leave blank:");
                string startDateInput = Console.ReadLine();
                DateTime? startDate = string.IsNullOrEmpty(startDateInput) ? null : DateTime.Parse(startDateInput);

                Console.WriteLine("Enter end date for filter (format: yyyy-MM-dd) or leave blank:");
                string endDateInput = Console.ReadLine();
                DateTime? endDate = string.IsNullOrEmpty(endDateInput) ? null : DateTime.Parse(endDateInput);

                Console.WriteLine("Select Category (1 - Electronics, 2 - Food, 3 - Other, or leave blank):");
                CategoryType? filterCategory = ParseCategoryFilter(Console.ReadLine());

                Console.WriteLine("Select Room (1 - MeetingRoom, 2 - Kitchen, 3 - Bathroom, or leave blank):");
                RoomType? filterRoom = ParseRoomFilter(Console.ReadLine());

                var shortages = dataStorage.ListShortages(currentUserName, currentRole,
                                                              filterTitle: listTitle,
                                                              startDate: startDate,
                                                              endDate: endDate,
                                                              filterCategory: filterCategory,
                                                              filterRoom: filterRoom);


                foreach (var shortage in shortages)
                {
                    Console.WriteLine($"Title: {shortage.Title}");
                    Console.WriteLine($"Name: {shortage.Name}");
                    Console.WriteLine($"Room: {shortage.Room}");
                    Console.WriteLine($"Category: {shortage.Category}");
                    Console.WriteLine($"Priority: {shortage.Priority}");
                    Console.WriteLine($"Created On: {shortage.CreatedOn.ToShortDateString()}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static RoomType ParseRoomType(string input)
        {
            return input switch
            {
                "1" => RoomType.MeetingRoom,
                "2" => RoomType.Kitchen,
                "3" => RoomType.Bathroom,
                _ => throw new FormatException("Invalid room type.")
            };
        }

        private static CategoryType ParseCategoryType(string input)
        {
            return input switch
            {
                "1" => CategoryType.Electronics,
                "2" => CategoryType.Food,
                "3" => CategoryType.Other,
                _ => throw new FormatException("Invalid category type.")
            };
        }
        private static CategoryType? ParseCategoryFilter(string input)
        {
            return input switch
            {
                "1" => CategoryType.Electronics,
                "2" => CategoryType.Food,
                "3" => CategoryType.Other,
                _ => null
            };
        }
        private static RoomType? ParseRoomFilter(string input)
        {
            return input switch
            {
                "1" => RoomType.MeetingRoom,
                "2" => RoomType.Kitchen,
                "3" => RoomType.Bathroom,
                _ => null
            };
        }

    }
}
