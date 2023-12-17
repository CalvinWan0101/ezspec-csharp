using ezSpec.exception;
using System.Text;
using System.Text.RegularExpressions;

namespace ezSpec {

    public abstract class Step {
        public delegate void StepCallback(ScenarioEnvironment env);

        public static readonly bool ContinuousAfterFailure = true;
        public static readonly bool TerminateAfterFailure = false;

        private string description;
        private StepCallback callback;
        private bool continousAfterFailure;
        private Result stepResult;

        public abstract string Name { get; }

        public string Description {
            get { return description; }
        }

        internal StepCallback Callback {
            get { return callback; }
        }

        public bool IsContinuousAfterFailure {
            get { return continousAfterFailure; }
        }

        public Result Result {
            get { return stepResult; }
            set { stepResult = value; }
        }

        public List<Argument> Arguments {
            get {
                List<Argument> arguments = new List<Argument>();

                Regex reg = new Regex("\\$\\{([^{}]+)\\}");
                MatchCollection matchs = reg.Matches(description);

                foreach (Match match in matchs) {
                    arguments.Add(Argument.New(match.Groups[0].Value));
                }

                return arguments;
            }
        }

        public string EraseReversedWords {
            get {
                string result = description;

                Regex reg = new Regex("\\$\\{([^{}]+)\\}");
                MatchCollection matchs = reg.Matches(description);

                foreach (Match match in matchs) {
                    Argument argument = Argument.New(match.Groups[0].Value);
                    result = result.Replace(match.Groups[0].Value, argument.Value);
                }
                return result;
            }
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("[");
            result.Append(stepResult.ToString());
            result.Append("] ");
            result.Append(Name);
            result.Append(" ");
            result.Append(description.Replace("\n", "\n          "));
            return result.ToString();
        }

        protected Step(string description, bool continous, StepCallback callback) {
            this.description = description;
            this.continousAfterFailure = continous;
            this.callback = callback;
            this.stepResult = Result.Pending(PendingException.New());
        }
    }
}
