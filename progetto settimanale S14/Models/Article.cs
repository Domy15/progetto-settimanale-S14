namespace progetto_settimanale_S14.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price {  get; set; }
        public string Description { get; set; }
        public string? Cover { get; set; }
        public List<string> Images { get; set; } = new List<string>();
    }
}
