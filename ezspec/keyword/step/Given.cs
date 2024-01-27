namespace ezSpec.keyword.step {
    public class Given : Step, ConcurrentGroup {

        public override string Name {
            get { return "Given"; }
        }

        public Given(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
