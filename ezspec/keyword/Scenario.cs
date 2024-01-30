using ezSpec.exception;
using ezSpec.keyword.step;
using ezSpec.keyword.table;
using System.Text;

namespace ezSpec.keyword {
    public class Scenario {
        protected string name;
        protected IList<Step> steps;
        private ScenarioEnvironment env;

        public string Name {
            get { return name; }
        }

        public IList<Step> Steps {
            get { return steps; }
        }

        protected Scenario(string name) {
            this.name = name;
            steps = new List<Step>();
            env = ScenarioEnvironment.New();
        }

        public static Scenario New(string name) {
            return new Scenario(name);
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

        public void Execute() {
            ExecuteSteps();
            ThrowExceptionIfFailed();
        }

        public void ExecuteConcurrently() {
            ExecuteStepsConcurrently();
            ThrowExceptionIfFailed();
        }

        private bool ExecuteStep(Step step) {
            try {
                env.SetArguments(step.Arguments);
                if (Table.ContainsTable(step.Description)) {
                    env.SetAnonymousTable(Table.New(step.Description));
                }
                step.Callback?.Invoke(env);
                step.Result = Result.Success();
                return false;
            }
            catch (PendingException e) {
                step.Result = Result.Pending(e);
                return false;
            }
            catch (Exception e) {
                step.Result = Result.Failure(e);
                return !step.IsContinuousAfterFailure;
            }
        }

        private void ExecuteSteps() {
            bool skip = false;
            foreach (Step step in steps) {
                if (skip) {
                    step.Result = Result.Skipped();
                    continue;
                }

                skip = ExecuteStep(step);
            }
        }

        private void ExecuteStepsConcurrently() {
            bool skip = false;
            for (int currentStep = 0; currentStep < Steps.Count;) {
                if (skip) {
                    steps[currentStep++].Result = Result.Skipped();
                }
                else if (Steps[currentStep] is ConcurrentGroup) {
                    List<Step> concurrentSteps = new List<Step>() {
                        steps[currentStep++]
                    };

                    for (; currentStep < Steps.Count && Steps[currentStep] is not ConcurrentGroup; currentStep++) {
                        concurrentSteps.Add(steps[currentStep]);
                    }

                    foreach (Step step in concurrentSteps) {
                        skip |= ExecuteStep(step);
                    }
                }
            }
        }

        private void ThrowExceptionIfFailed() {
            StringBuilder errorMessage = new StringBuilder();
            foreach (Step step in steps) {
                if (step.Result.IsFailure) {
                    errorMessage.Append(step.Name);
                    errorMessage.Append(" ");
                    errorMessage.Append(step.EraseReversedWords);
                    errorMessage.Append("\n");
                    errorMessage.Append(step.Result.ExceptionMessage);
                    errorMessage.Append("\n");
                }
            }

            if (errorMessage.Length > 0) {
                throw new EzSpecError(errorMessage.ToString());
            }
        }
    }
}
