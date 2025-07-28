namespace EyeScenceApp.Domain.Entities
{ 
    public class Crew:Celebrity
    {
        public ICollection<WorksOn> WorksOn { get; set; } = new HashSet<WorksOn>();
    }
}
