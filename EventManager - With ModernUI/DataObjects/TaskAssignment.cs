using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TaskAssignment
    {
        public int TaskAssignmentID { get; set; }
        public DateTime DateAssigned { get; set; }
        public int TaskID { get; set; }
        public string RoleID { get; set; }
        public int UserID { get; set; }
    }

    public class TaskAssignmentVM : TaskAssignment
    {
        public String Name { get; set; }
    }
}
