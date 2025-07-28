namespace EyeScenceApp.Domain.Entities
{
    public class DigitalContentAward:Award
    {
        public ICollection<DigitalContentAwards> DigitalContents { get; set; }
    }
}
