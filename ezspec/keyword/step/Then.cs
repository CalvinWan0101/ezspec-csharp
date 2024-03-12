namespace ezSpec.keyword.step {
    public class Then : Step, BeginConcurrentStep {

        public override string Name {
            get { return "Then"; }
        }

        public Then(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        internal override Step CloneWithDifferentDescription(string description) {
            return new Then(description, this.continousAfterFailure, this.callback);
        }
    }
}
