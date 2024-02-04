namespace ezSpec.keyword.step {
    public class Then : Step, ConcurrentGroup {

        public override string Name {
            get { return "Then"; }
        }

        public Then(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }

        public override Step Clone() {
            return new Then(description, continousAfterFailure, callback);
        }
    }
}
