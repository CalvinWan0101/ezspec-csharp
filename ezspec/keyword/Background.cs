using System.Collections.ObjectModel;
using ezSpec.keyword.step;
using System.Text;

namespace ezSpec.keyword {
    public class Background {
        private readonly string name;
        private readonly IList<Step> steps;
        private ScenarioEnvironment env;

        public string Name {
            get { return name; }
        }
        
        public ReadOnlyCollection<Step> Steps {
            get { return new ReadOnlyCollection<Step>(steps); }
        }

        internal ScenarioEnvironment Environment {
            set { env = value; }
        }

        protected Background(string name) {
            this.name = name;
            this.steps = new List<Step>();
        }

        public static new Background New(string name) {
            return new Background(name);
        }

        public Background Given(string description, Step.StepCallback callback) {
            return Given(description, Step.TerminateAfterFailure, callback);
        }

        public Background Given(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Given(description, continous, callback));

            return this;
        }

        public Background And(string description, Step.StepCallback callback) {
            return And(description, Step.TerminateAfterFailure, callback);
        }

        public Background And(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new And(description, continous, callback));

            return this;
        }

        public Background But(string description, Step.StepCallback callback) {
            return But(description, Step.TerminateAfterFailure, callback);
        }

        public Background But(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new But(description, continous, callback));

            return this;
        }
        
        public void Execute() {
            StepExecutor.Execute(steps, env);
        }
        
        public void ExecuteConcurrently() {
            StepExecutor.ExecuteConcurrently(steps, env);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Background: ");
            result.Append(name);
            foreach (Step step in steps) {
                result.Append("\n\t");
                result.Append(step.ToString());
            }
            return result.ToString();
        }
    }
}
