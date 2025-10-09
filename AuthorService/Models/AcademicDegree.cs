namespace AuthorService.Models
{
    public class AcademicDegree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AcademicCenter { get; set; }
        public DateOnly? DateGrade { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string? AcademicDegreeGuid { get; set; }
    }
}
