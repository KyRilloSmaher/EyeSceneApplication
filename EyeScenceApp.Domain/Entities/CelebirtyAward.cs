namespace EyeScenceApp.Domain.Entities
{
    public class CelebirtyAward:Award
    {
        public ICollection<CelebirtyAwards> celebirties { get; set; }
    }
}
