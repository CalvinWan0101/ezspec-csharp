namespace ezSpec {
    public class ThenSuccess : Step {

        public override string Name {
            get { return "ThenSuccess"; }
        }

        public ThenSuccess(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
