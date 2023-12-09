using System.Text;

namespace ezSpec {
    public class Result {

        private StepExecutionOutcome executionOutcome;
        private Exception exception;

        public Exception Exception {
            get { return exception; }
        }

        public string ExceptionMessage {
            get {
                if (exception is null || exception.StackTrace is null) {
                    return "";
                }

                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory)!
                        .Parent!.Parent!.Parent!.FullName;
                return exception.StackTrace
                    .Replace($"in {projectDirectory}\\", "")
                    .Replace("\r", "");
            }
        }

        public bool IsSuccess {
            get { return executionOutcome == StepExecutionOutcome.Success; }
        }

        public bool IsPending {
            get { return executionOutcome == StepExecutionOutcome.Pending; }
        }

        public bool IsFailure {
            get { return executionOutcome == StepExecutionOutcome.Failure; }
        }

        public bool IsSkipped {
            get { return executionOutcome == StepExecutionOutcome.Skipped; }
        }

        public StepExecutionOutcome Outcome {
            get { return executionOutcome; }
        }

        protected Result(StepExecutionOutcome outcome, Exception exception) {
            this.executionOutcome = outcome;
            this.exception = exception;
        }

        public static Result Success() {
            return new Result(StepExecutionOutcome.Success, null);
        }

        public static Result Pending(Exception exception) {
            return new Result(StepExecutionOutcome.Pending, exception);
        }

        public static Result Failure(Exception exception) {
            return new Result(StepExecutionOutcome.Failure, exception);
        }

        public static Result Skipped() {
            return new Result(StepExecutionOutcome.Skipped, null);
        }

        public override string ToString() {
            return executionOutcome.ToString();
        }
    }
}
