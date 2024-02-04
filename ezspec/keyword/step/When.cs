namespace ezSpec.keyword.step {
    public class When : Step, ConcurrentGroup {

        public override string Name {
            get { return "When"; }
        }

        public When(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        public override Step Clone() {
            return new When(description, continousAfterFailure, callback);
        }
    }
}
