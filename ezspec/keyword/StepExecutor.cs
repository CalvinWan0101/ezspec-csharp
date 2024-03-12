using ezSpec.exception;
using ezSpec.keyword.step;
using ezSpec.keyword.table;
using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword {
    public class StepExecutor {
        public static void Execute(IList<Step> steps, ScenarioEnvironment env) {
            ExecuteSteps(steps, env);
            ThrowExceptionIfFailed(steps);
        }

        public static void ExecuteConcurrently(IList<Step> steps, ScenarioEnvironment env) {
            ExecuteStepsConcurrently(steps, env);
            ThrowExceptionIfFailed(steps);
        }

        private static bool ExecuteStep(Step step, ScenarioEnvironment env) {
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

        private static void ExecuteSteps(IList<Step> steps, ScenarioEnvironment env) {
            bool skip = false;
            foreach (Step step in steps) {
                if (skip) {
                    step.Result = Result.Skipped();
                    continue;
                }

                skip = ExecuteStep(step, env);
            }
        }

        private static void ExecuteStepsConcurrently(IList<Step> steps, ScenarioEnvironment env) {
            bool skip = false;
            for (int currentStep = 0; currentStep < steps.Count;) {
                if (skip) {
                    steps[currentStep++].Result = Result.Skipped();
                } else if (steps[currentStep] is ConcurrentGroup) {
                    List<Step> concurrentSteps = new List<Step>() {
                        steps[currentStep++]
                    };

                    for (; currentStep < steps.Count && steps[currentStep] is not ConcurrentGroup; currentStep++) {
                        concurrentSteps.Add(steps[currentStep]);
                    }

                    foreach (Step step in concurrentSteps) {
                        skip |= ExecuteStep(step, env);
                    }
                }
            }
        }

        private static void ThrowExceptionIfFailed(IList<Step> steps) {
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
