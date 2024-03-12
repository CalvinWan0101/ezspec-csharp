using ezSpec.keyword.step;
using System.Text;

namespace ezSpec.keyword {
    public class Scenario : AbstractScenario {

        public static Scenario New(string name) {
            return New(name, null);
        }

        public static Scenario New(string name, Background? background) {
            return new Scenario(name, background);
        }

        internal static Scenario New(Background? background, IList<Step> steps, Example example) {
            return new Scenario(background, steps, example);
        }

        private Scenario(string name, Background? background) : base(name, background) {
        }

        private Scenario(Background? background, IList<Step> steps, Example example) : base("", null) {
            foreach (var step in steps) {
                this.steps.Add(step);
            }
            this.env.SetExample(example);
            this.background = background;
        }

        public new Scenario Given(string description, Step.StepCallback callback) {
            base.Given(description, callback);
            return this;
        }

        public new Scenario Given(string description, bool continous, Step.StepCallback callback) {
            base.Given(description, continous, callback);
            return this;
        }

        public new Scenario When(string description, Step.StepCallback callback) {
            base.When(description, callback);
            return this;
        }

        public new Scenario When(string description, bool continous, Step.StepCallback callback) {
            base.When(description, continous, callback);
            return this;
        }

        public new Scenario Then(string description, Step.StepCallback callback) {
            base.Then(description, callback);
            return this;
        }

        public new Scenario Then(string description, bool continous, Step.StepCallback callback) {
            base.Then(description, continous, callback);
            return this;
        }

        public new Scenario And(string description, Step.StepCallback callback) {
            base.And(description, callback);
            return this;
        }

        public new Scenario And(string description, bool continous, Step.StepCallback callback) {
            base.And(description, continous, callback);
            return this;
        }

        public new Scenario But(string description, Step.StepCallback callback) {
            base.But(description, callback);
            return this;
        }

        public new Scenario But(string description, bool continous, Step.StepCallback callback) {
            base.But(description, continous, callback);
            return this;
        }

        public new Scenario ThenSuccess(string description, Step.StepCallback callback) {
            base.ThenSuccess(description, callback);
            return this;
        }

        public new Scenario ThenSuccess(bool continous, Step.StepCallback callback) {
            base.ThenSuccess(continous, callback);
            return this;
        }

        public new Scenario ThenSuccess(Step.StepCallback callback) {
            base.ThenSuccess(callback);
            return this;
        }

        public new Scenario ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            base.ThenSuccess(description, continous, callback);
            return this;
        }

        public new Scenario ThenFailure(string description, Step.StepCallback callback) {
            base.ThenFailure(description, callback);
            return this;
        }

        public new Scenario ThenFailure(bool continous, Step.StepCallback callback) {
            base.ThenFailure(continous, callback);
            return this;
        }

        public new Scenario ThenFailure(Step.StepCallback callback) {
            base.ThenFailure(callback);
            return this;
        }

        public new Scenario ThenFailure(string description, bool continous, Step.StepCallback callback) {
            base.ThenFailure(description, continous, callback);
            return this;
        }

        public void Execute() {
            if (background != null) {
                background.Environment = env;
                background.Execute();
            }
            StepExecutor.Execute(steps, env);
        }

        public void ExecuteConcurrently() {
            if (background != null) {
                background.Environment = env;
                background.ExecuteConcurrently();
            }
            StepExecutor.ExecuteConcurrently(steps, env);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Scenario: ");
            result.Append(name);
            for (int i = 0; i < steps.Count; i++) {
                result.Append("\n\t");
                result.Append(steps[i].ToStringWithResult().Replace("\n", "\n\t"));
            }
            return result.ToString();
        }
    }
}
