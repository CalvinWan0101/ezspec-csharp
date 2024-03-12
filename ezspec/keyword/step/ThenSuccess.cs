namespace ezSpec.keyword.step {
    public class ThenSuccess : Step, BeginConcurrentStep {

        public override string Name {
            get { return "ThenSuccess"; }
        }

        public ThenSuccess(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        internal override Step CloneWithDifferentDescription(string description) {
            return new ThenSuccess(description, this.continousAfterFailure, this.callback);
        }
    }
}
