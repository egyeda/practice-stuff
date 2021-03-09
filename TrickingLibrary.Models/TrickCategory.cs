namespace TrickingLibrary.Models
{
    public class TrickCategory
    {
        public string TrickId { get; set; }
        public Trick Trick { get; set; }

        public Category Category { get; set; }
        public string CategoryId { get; set; }
    }
}