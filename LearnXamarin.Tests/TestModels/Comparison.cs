namespace LearnXamarin.Tests.TestModels
{
    public class Comparison
    {
        public VisualCell Before { get; }

        public VisualCell After { get; }

        public Comparison(VisualCell before, VisualCell after)
        {
            Before = before;
            After = after;
        }
    }
}
