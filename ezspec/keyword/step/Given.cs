namespace ezSpec.keyword.step {
    public class Given : Step, BeginConcurrentStep {

        public override string Name {
            get { return "Given"; }
        }

        public Given(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        internal override Step CloneWithDifferentDescription(string description) {
            return new Given(description, this.continousAfterFailure, this.callback);
        }
    }
}
