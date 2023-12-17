namespace ezSpec {
    public class ThenFailure : Step, ConcurrentGroup {

        public override string Name {
            get { return "ThenFailure"; }
        }

        public ThenFailure(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
