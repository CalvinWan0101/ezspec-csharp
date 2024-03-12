namespace ezSpec.keyword.step {
    public class ThenFailure : Step, BeginConcurrentStep {

        public override string Name {
            get { return "ThenFailure"; }
        }

        public ThenFailure(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        internal override Step CloneWithDifferentDescription(string description) {
            return new ThenFailure(description, this.continousAfterFailure, this.callback);
        }
    }
}
