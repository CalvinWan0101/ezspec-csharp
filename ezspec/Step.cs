using System.Text.RegularExpressions;

namespace ezSpec {
    public class Step {

        public delegate void StepCallback();

        static public bool ContinuousAfterFailure {
            get { return true; }
        }

        static public bool TerminateAfterFailure {
            get { return false; }
        }

        private string description;
        private StepCallback callback;
        private bool continousAfterFailure;

        public string Description { 
            get { return description; }
        }

        public StepCallback Callback { 
            get { return callback; }
        }

        public bool IsContinuousAfterFailure {
            get { return continousAfterFailure; }
        }

        public List<Argument> Arguments {
            get {
                List<Argument> arguments = new List<Argument>();

                Regex reg = new Regex("\\s\\$\\{([^}]+)\\}|\\s\\$([\\S^{]+)");
                MatchCollection matchs = reg.Matches(description);
                GroupCollection groups = matchs[0].Groups;

                foreach(Match match in matchs) {
                    arguments.Add(Argument.Create(match.Groups[0].Value));
                }

                return arguments;
            }
        }

        public Step(string description, StepCallback callback) { 
            this.description = description;
            this.continousAfterFailure = false;
            this.callback = callback;
        }

        public Step(string description, bool continous, StepCallback callback) {
            this.description = description;
            this.continousAfterFailure = continous;
            this.callback= callback;
        }
    }
}
