namespace ezSpec.keyword.step {
    public class And : Step {

        public override string Name {
            get { return "And"; }
        }

        public And(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
        }
    }
}
