using System.Collections.ObjectModel;
using ezSpec.keyword.step;
using System.Text;

namespace ezSpec.keyword {
    public class Scenario {
        protected readonly string name;
        protected Background? background;
        protected readonly IList<Step> steps;
        protected readonly ScenarioEnvironment env;

        public string Name {
            get { return name; }
        }
        
        public ReadOnlyCollection<Step> Steps {
            get { return new ReadOnlyCollection<Step>(steps); }
        }
        
        public static Scenario New(string name) {
            return New(name, null);
        }

        public static Scenario New(string name, Background? background) {
            return new Scenario(name, background);
        }

        internal static Scenario New(Background? background, IList<Step> steps, Example example) {
            return new Scenario(background, steps, example);
        }

        protected Scenario(string name, Background? background) {
            this.name = name;
            this.background = background;
            this.steps = new List<Step>();
            this.env = ScenarioEnvironment.New();
        }

        protected Scenario(Background? background, IList<Step> steps, Example example) {
            this.name = "";
            this.background = background;
            this.steps = new List<Step>();
            this.env = ScenarioEnvironment.New();
            foreach (var step in steps) {
                this.steps.Add(step);
            }
            this.env.SetExample(example);
            this.background = background;
        }

        public Scenario Given(string description, Step.StepCallback callback) {
            Given(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario Given(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Given(description, continous, callback));
            return this;
        }

        public Scenario When(string description, Step.StepCallback callback) {
            When(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario When(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new When(description, continous, callback));
            return this;
        }

        public Scenario Then(string description, Step.StepCallback callback) {
            Then(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario Then(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new Then(description, continous, callback));
            return this;
        }

        public Scenario And(string description, Step.StepCallback callback) {
            And(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario And(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new And(description, continous, callback));
            return this;
        }

        public Scenario But(string description, Step.StepCallback callback) {
            But(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario But(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new But(description, continous, callback));
            return this;
        }

        public Scenario ThenSuccess(string description, Step.StepCallback callback) {
            ThenSuccess(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario ThenSuccess(bool continous, Step.StepCallback callback) {
            ThenSuccess("", continous, callback);
            return this;
        }

        public Scenario ThenSuccess(Step.StepCallback callback) {
            ThenSuccess("", Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenSuccess(description, continous, callback));
            return this;
        }

        public Scenario ThenFailure(string description, Step.StepCallback callback) {
            ThenFailure(description, Step.TerminateAfterFailure, callback);
            return this;
        }

        public Scenario ThenFailure(bool continous, Step.StepCallback callback) {
            ThenFailure("", continous, callback);
            return this;
        }

        public Scenario ThenFailure(Step.StepCallback callback) {
            ThenFailure("", Step.TerminateAfterFailure, callback);
            return this;
        }

        public new Scenario ThenFailure(string description, bool continous, Step.StepCallback callback) {
            steps.Add(new ThenFailure(description, continous, callback));
            return this;
        }

        public void Execute() {
            if (background != null) {
                background.Environment = env;
                background.Execute();
                if (!background.IsExecuteSuccess) {
                    SkipAllSteps();
                    return;
                }
            }
            StepExecutor.Execute(steps, env);
        }

        public void ExecuteConcurrently() {
            if (background != null) {
                background.Environment = env;
                background.ExecuteConcurrently();
                if (!background.IsExecuteSuccess) {
                    SkipAllSteps();
                    return;
                }
            }
            StepExecutor.ExecuteConcurrently(steps, env);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Scenario: ");
            result.Append(name);
            for (int i = 0; i < steps.Count; i++) {
                result.Append("\n\t");
                result.Append(steps[i].ToStringWithResult().Replace("\n", "\n\t"));
            }
            return result.ToString();
        }
        
        private void SkipAllSteps() {
            foreach (Step step in steps) {
                step.Result = Result.Skipped();
            }
        }
    }
}
