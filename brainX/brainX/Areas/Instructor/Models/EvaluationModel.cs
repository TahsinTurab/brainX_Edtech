using brainX.Infrastructure.Domains;

namespace brainX.Areas.Instructor.Models
{
    public class EvaluationModel
    {
        public List<Solution> Solutions { get; set; }
        public Guid SolutionId { get; set; }
        public string Verdict { get; set; }
        public Solution Solution { get; set; }
        public Test Test { get; set; }
    }
}
