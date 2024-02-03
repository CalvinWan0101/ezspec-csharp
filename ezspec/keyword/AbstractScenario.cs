using ezSpec.keyword.step;

namespace ezSpec.keyword {
    public abstract class AbstractScenario : StepExecutor {
        protected string name;
        protected Background? background;

        public string Name {
            get { return name; }
        }

        protected AbstractScenario(string name, Background? background) : base() {
            this.name = name;
            this.background = background;
        }

        public AbstractScenario Given(string description, Step.StepCallback callback) {
            return Given(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario Given(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Given(description, continous, callback));
            return this;
        }

        public AbstractScenario When(string description, Step.StepCallback callback) {
            return When(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario When(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new When(description, continous, callback));
            return this;
        }

        public AbstractScenario Then(string description, Step.StepCallback callback) {
            return Then(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario Then(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Then(description, continous, callback));
            return this;
        }

        public AbstractScenario And(string description, Step.StepCallback callback) {
            return And(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario And(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new And(description, continous, callback));
            return this;
        }

        public AbstractScenario But(string description, Step.StepCallback callback) {
            return But(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario But(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new But(description, continous, callback));
            return this;
        }

        public AbstractScenario ThenSuccess(string description, Step.StepCallback callback) {
            return ThenSuccess(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario ThenSuccess(bool continous, Step.StepCallback callback) {
            return ThenSuccess("", continous, callback);
        }

        public AbstractScenario ThenSuccess(Step.StepCallback callback) {
            return ThenSuccess("", Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenSuccess(description, continous, callback));
            return this;
        }

        public AbstractScenario ThenFailure(string description, Step.StepCallback callback) {
            return ThenFailure(description, Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario ThenFailure(bool continous, Step.StepCallback callback) {
            return ThenFailure("", continous, callback);
        }

        public AbstractScenario ThenFailure(Step.StepCallback callback) {
            return ThenFailure("", Step.TerminateAfterFailure, callback);
        }

        public AbstractScenario ThenFailure(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenFailure(description, continous, callback));
            return this;
        }
    }
}
