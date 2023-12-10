namespace ezSpec {
    public class When : Step {

        public override string Name {
            get { return "When"; }
        }

        public When(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
