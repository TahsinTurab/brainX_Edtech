using brainX.Infrastructure.Domains;

namespace brainX.Areas.Instructor.Models
{
    public class TestCreateModel
    {
        public string Name { get; set; }
        public int TotalTime { get; set; }
        public int QuizTime { get; set; }
        public Guid CourseId { get; set; }
        public string PracticalTask1 { get; set; }
        public string PracticalTask2 { get; set; }
        public string PracticalTask3 { get; set; }
        public string StatusMessage { get; set; }
    }
}
