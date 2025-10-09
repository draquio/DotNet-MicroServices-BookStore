namespace AuthorService.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateOnly? Birthdate { get; set; }
        public ICollection<AcademicDegree> AcademicDegrees { get; set; } = new List<AcademicDegree>();
        public Guid? AuthorGuid { get; set; }
    }
}
