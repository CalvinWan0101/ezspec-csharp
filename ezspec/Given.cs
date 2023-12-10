namespace ezSpec {
    public class Given : Step {

        public override string Name {
            get { return "Given"; }
        }

        public Given(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
