using ezSpec.exception;
using ezSpec.table;
using System.Text;

namespace ezSpec {
    public class RuntimeScenario {
        private string name;
        private IList<Step> steps;
        private ScenarioEnvironment env;

        public string Name {
            get { return name; }
        }

        public IList<Step> Steps {
            get { return steps; }
        }

        private RuntimeScenario(string name) {
            this.name = name;
            this.steps = new List<Step>();
            this.env = ScenarioEnvironment.New();
        }

        public static RuntimeScenario New(string name) {
            return new RuntimeScenario(name);
        }

        public RuntimeScenario Given(string description, Step.StepCallback callback) {
            return this.Given(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario Given(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Given(description, continous, callback));

            return this;
        }

        public RuntimeScenario When(string description, Step.StepCallback callback) {
            return this.When(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario When(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new When(description, continous, callback));

            return this;
        }

        public RuntimeScenario Then(string description, Step.StepCallback callback) {
            return this.Then(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario Then(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Then(description, continous, callback));

            return this;
        }

        public RuntimeScenario And(string description, Step.StepCallback callback) {
            return this.And(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario And(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new And(description, continous, callback));

            return this;
        }

        public RuntimeScenario But(string description, Step.StepCallback callback) {
            return this.But(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario But(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new But(description, continous, callback));

            return this;
        }

        public RuntimeScenario ThenSuccess(string description, Step.StepCallback callback) {
            return this.ThenSuccess(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario ThenSuccess(bool continous, Step.StepCallback callback) {
            return this.ThenSuccess("", continous, callback);
        }

        public RuntimeScenario ThenSuccess(Step.StepCallback callback) {
            return this.ThenSuccess("", Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenSuccess(description, continous, callback));

            return this;
        }

        public RuntimeScenario ThenFailure(string description, Step.StepCallback callback) {
            return this.ThenFailure(description, Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario ThenFailure(bool continous, Step.StepCallback callback) {
            return this.ThenFailure("", continous, callback);
        }

        public RuntimeScenario ThenFailure(Step.StepCallback callback) {
            return this.ThenFailure("", Step.TerminateAfterFailure, callback);
        }

        public RuntimeScenario ThenFailure(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenFailure(description, continous, callback));

            return this;
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < steps.Count; i++) {
                result.Append(steps[i].ToString());
                if (i != steps.Count - 1) {
                    result.Append("\n");
                }
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
