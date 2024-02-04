namespace ezSpec.keyword.step {
    public class But : Step {

        public override string Name {
            get { return "But"; }
        }

        public But(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        public override Step Clone() {
            return new But(description, continousAfterFailure, callback);
        }
    }
}
