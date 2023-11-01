using Hart_Check_Official.Models;

namespace WebApp_Doctor.Models
{
    public class ScheduleViewModel
    {
        public List<WorkOrder>? WorkOrders { get; set; }
        public List<Resource>? Resources { get; set; }
    }
}
