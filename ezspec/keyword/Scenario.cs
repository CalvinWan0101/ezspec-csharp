using ezSpec.keyword.step;
using System.Text;

namespace ezSpec.keyword {
    public class Scenario : StepExecutor {
        private string name;
        private Background? background;

        public string Name {
            get { return name; }
        }

        private Scenario(string name, Background? background) : base() {
            this.name = name;
            this.background = background;
        }

        public static Scenario New(string name) {
            return New(name, null);
        }

        public static Scenario New(string name, Background? background) {
            return new Scenario(name, background);
        }

        public Scenario Given(string description, Step.StepCallback callback) {
            return Given(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario Given(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Given(description, continous, callback));

            return this;
        }

        public Scenario When(string description, Step.StepCallback callback) {
            return When(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario When(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new When(description, continous, callback));

            return this;
        }

        public Scenario Then(string description, Step.StepCallback callback) {
            return Then(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario Then(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Then(description, continous, callback));

            return this;
        }

        public Scenario And(string description, Step.StepCallback callback) {
            return And(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario And(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new And(description, continous, callback));

            return this;
        }

        public Scenario But(string description, Step.StepCallback callback) {
            return But(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario But(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new But(description, continous, callback));

            return this;
        }

        public Scenario ThenSuccess(string description, Step.StepCallback callback) {
            return ThenSuccess(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario ThenSuccess(bool continous, Step.StepCallback callback) {
            return ThenSuccess("", continous, callback);
        }

        public Scenario ThenSuccess(Step.StepCallback callback) {
            return ThenSuccess("", Step.TerminateAfterFailure, callback);
        }

        public Scenario ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenSuccess(description, continous, callback));

            return this;
        }

        public Scenario ThenFailure(string description, Step.StepCallback callback) {
            return ThenFailure(description, Step.TerminateAfterFailure, callback);
        }

        public Scenario ThenFailure(bool continous, Step.StepCallback callback) {
            return ThenFailure("", continous, callback);
        }

        public Scenario ThenFailure(Step.StepCallback callback) {
            return ThenFailure("", Step.TerminateAfterFailure, callback);
        }

        public Scenario ThenFailure(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenFailure(description, continous, callback));

            return this;
        }

        public override void Execute() {
            if (background != null) {
                background.Environment = env;
                background.Execute();
            }
            base.Execute();
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Scenario: ");
            result.Append(name);
            for (int i = 0; i < steps.Count; i++) {
                result.Append("\n");
                result.Append(steps[i].ToString());
            }
            return result.ToString();
        }
    }
}
