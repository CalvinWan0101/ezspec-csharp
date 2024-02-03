using ezSpec.exception;
using ezSpec.keyword.step;
using ezSpec.keyword.table;
using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword {
    public abstract class StepExecutor {
        protected IList<Step> steps;
        protected ScenarioEnvironment env;

        public ReadOnlyCollection<Step> Steps {
            get { return new ReadOnlyCollection<Step>(steps); }
        }

        protected StepExecutor() {
            steps = new List<Step>();
            env = ScenarioEnvironment.New();
        }

        public virtual void Execute() {
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
            } catch (PendingException e) {
                step.Result = Result.Pending(e);
                return false;
            } catch (Exception e) {
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
                } else if (Steps[currentStep] is ConcurrentGroup) {
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
