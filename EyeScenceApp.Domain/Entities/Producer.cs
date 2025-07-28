using EyeScenceApp.Domain.Enums;
namespace EyeScenceApp.Domain.Entities
{
    public class Producer : Crew
    {
        public int ProducedProjectsCount { get; set; }

        public decimal TotalBoxOfficeRevenue { get; set; }

    }

}
