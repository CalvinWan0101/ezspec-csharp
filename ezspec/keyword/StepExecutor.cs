using ezSpec.exception;
using ezSpec.keyword.step;
using ezSpec.keyword.table;
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
                SetEnvironmentByStep(step, env);
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

        private static void SetEnvironmentByStep(Step step, ScenarioEnvironment env) {
            env.SetArguments(step.Arguments);
            if (Table.ContainsTable(step.Description)) {
                env.SetAnonymousTable(Table.New(step.Description));
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

        private static void ExecuteStepsConcurrently(IList<Step> steps, ScenarioEnvironment env){
            ConcurrentResults concurrentResults = new ConcurrentResults();
            List<ConcurrentGroup> concurrentGroups = ConcurrentGroup.SliceConcurrentGroups(steps);
            foreach (ConcurrentGroup concurrentGroup in concurrentGroups) {
                ExecuteConcurrentGroup(concurrentGroup, env, concurrentResults);
            }
        }

        private static void ExecuteConcurrentGroup(ConcurrentGroup concurrentGroup, ScenarioEnvironment env, ConcurrentResults concurrentResults) {
            if (concurrentResults.IsSkip()) {
                foreach (Step step in concurrentGroup.Steps) {
                    step.Result = Result.Skipped();
                }
            } else {
                concurrentResults.Clear();
                Parallel.ForEach(concurrentGroup.Steps, step => {
                    concurrentResults.AddResultSkipStatus(ExecuteStep(step, env));
                });
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

        private class ConcurrentResults {
            private bool isSkip;
            
            public bool IsSkip() {
                return isSkip;
            }

            public void AddResultSkipStatus(bool result) {
                isSkip |= result;
            }
            
            public void Clear() {
                isSkip = false;
            }
        }
    }
}