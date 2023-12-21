using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using static VismaResourceManagement.Program;

namespace Visma_Internship
{
    public class ReadWrite
    {
        private const string FileName = "shortages.json";
        private List<Shortage> shortages;
        public ReadWrite()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };

            if (File.Exists(FileName))
            {
                var jsonData = File.ReadAllText(FileName);
                shortages = System.Text.Json.JsonSerializer.Deserialize<List<Shortage>>(jsonData, options) ?? new List<Shortage>();
            }
            else
            {
                shortages = new List<Shortage>();
            }
        }
        public bool AddShortage(Shortage newShortage)
        {
            var existingShortage = shortages.FirstOrDefault(s =>
                s.Title.Equals(newShortage.Title, StringComparison.OrdinalIgnoreCase)
                && s.Room == newShortage.Room);

            if (existingShortage != null)
            {
                if (existingShortage.Priority < newShortage.Priority)
                {
                    existingShortage.CreatorName = newShortage.CreatorName;
                    existingShortage.Name = newShortage.Name;
                    existingShortage.Category = newShortage.Category;
                    existingShortage.Priority = newShortage.Priority;
                    existingShortage.CreatedOn = newShortage.CreatedOn;
                    SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("A similar shortage already exists with equal or higher priority.");
                    return false;

                }
            }
            else
            {
                shortages.Add(newShortage);
                SaveChanges();
                return true;
            }
        }
        public void DeleteShortage(string title, string currentUserName, UserRole currentUserRole)
        {
            var shortage = shortages.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (shortage == null)
            {
                Console.WriteLine("Shortage not found.");
                return;
            }

            if (shortage.CreatorName.Equals(currentUserName, StringComparison.OrdinalIgnoreCase) || currentUserRole == UserRole.Administrator)
            {
                shortages.Remove(shortage);
                SaveChanges();
                Console.WriteLine("Shortage deleted successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to delete this shortage.");
            }
        }
        public IEnumerable<Shortage> ListShortages(string currentUser, UserRole role,
                                            string filterTitle = null,
                                            DateTime? startDate = null, DateTime? endDate = null,
                                            CategoryType? filterCategory = null,
                                            RoomType? filterRoom = null)
        {
            var query = shortages.AsEnumerable();


            if (role != UserRole.Administrator)
            {
                query = query.Where(s => s.CreatorName.Equals(currentUser, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filterTitle))
            {
                query = query.Where(s => s.Title.IndexOf(filterTitle, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(s => s.CreatedOn >= startDate.Value && s.CreatedOn <= endDate.Value);
            }

            if (filterCategory.HasValue)
            {
                query = query.Where(s => s.Category == filterCategory.Value);
            }

            if (filterRoom.HasValue)
            {
                query = query.Where(s => s.Room == filterRoom.Value);
            }
            return query.OrderByDescending(s => s.Priority).ToList();
        }

        private void SaveChanges()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var jsonData = System.Text.Json.JsonSerializer.Serialize(shortages, options);
            File.WriteAllText(FileName, jsonData);
        }
    }
}
