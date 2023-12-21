using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_Internship
{
      public enum RoomType
    {
        MeetingRoom,
        Kitchen,
        Bathroom
    }
    public enum UserRole
    {
        RegularUser,
        Administrator
    }
    public enum CategoryType
    {
        Electronics,
        Food,
        Other
    }
    public class Shortage
    {
        public string CreatorName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public RoomType Room { get; set; }
        public CategoryType Category { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
