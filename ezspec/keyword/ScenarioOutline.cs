using ezSpec.keyword.step;
using System.Text;
using System.Text.RegularExpressions;

namespace ezSpec.keyword {
    public class ScenarioOutline : AbstractScenario {

        private readonly IList<Examples> multiExamples;
        private readonly IList<Scenario> scenarios;

        public static ScenarioOutline New(string name, Background? background) {
            return new ScenarioOutline(name, background);
        }

        public static ScenarioOutline New(string name) {
            return new ScenarioOutline(name, null);
        }

        public static ScenarioOutline New() {
            return new ScenarioOutline("", null);
        }

        private ScenarioOutline(string name, Background? background) : base(name, null) {
            this.multiExamples = new List<Examples>();
            this.scenarios = new List<Scenario>();
            this.background = background;
        }

        public ScenarioOutline WithExamples(string rawData) {
            Examples examples = Examples.New(rawData);
            WithExamples(examples);
            return this;
        }

        public ScenarioOutline WithExamples(params Examples[] examples) {
            foreach (var item in examples) {
                this.multiExamples.Add(item);
            }
            return this;
        }

        public new ScenarioOutline Given(string description, Step.StepCallback callback) {
            base.Given(description, callback);
            return this;
        }

        public new ScenarioOutline Given(string description, bool continous, Step.StepCallback callback) {
            base.Given(description, continous, callback);
            return this;
        }

        public new ScenarioOutline When(string description, Step.StepCallback callback) {
            base.When(description, callback);
            return this;
        }

        public new ScenarioOutline When(string description, bool continous, Step.StepCallback callback) {
            base.When(description, continous, callback);
            return this;
        }

        public new ScenarioOutline Then(string description, Step.StepCallback callback) {
            base.Then(description, callback);
            return this;
        }

        public new ScenarioOutline Then(string description, bool continous, Step.StepCallback callback) {
            base.Then(description, continous, callback);
            return this;
        }

        public new ScenarioOutline And(string description, Step.StepCallback callback) {
            base.And(description, callback);
            return this;
        }

        public new ScenarioOutline And(string description, bool continous, Step.StepCallback callback) {
            base.And(description, continous, callback);
            return this;
        }

        public new ScenarioOutline But(string description, Step.StepCallback callback) {
            base.But(description, callback);
            return this;
        }

        public new ScenarioOutline But(string description, bool continous, Step.StepCallback callback) {
            base.But(description, continous, callback);
            return this;
        }

        public new ScenarioOutline ThenSuccess(string description, Step.StepCallback callback) {
            base.ThenSuccess(description, callback);
            return this;
        }

        public new ScenarioOutline ThenSuccess(bool continous, Step.StepCallback callback) {
            base.ThenSuccess(continous, callback);
            return this;
        }

        public new ScenarioOutline ThenSuccess(Step.StepCallback callback) {
            base.ThenSuccess(callback);
            return this;
        }

        public new ScenarioOutline ThenSuccess(string description, bool continous, Step.StepCallback callback) {
            base.ThenSuccess(description, continous, callback);
            return this;
        }

        public new ScenarioOutline ThenFailure(string description, Step.StepCallback callback) {
            base.ThenFailure(description, callback);
            return this;
        }

        public new ScenarioOutline ThenFailure(bool continous, Step.StepCallback callback) {
            base.ThenFailure(continous, callback);
            return this;
        }

        public new ScenarioOutline ThenFailure(Step.StepCallback callback) {
            base.ThenFailure(callback);
            return this;
        }

        public new ScenarioOutline ThenFailure(string description, bool continous, Step.StepCallback callback) {
            base.ThenFailure(description, continous, callback);
            return this;
        }

        public void Execute() {
            foreach (Examples examples in multiExamples) {
                foreach (Example example in examples.ExampleSet) {
                    Scenario scenario = Scenario.New(background, GetReplacedSteps(steps, example), example);
                    scenario.Execute();
                    scenarios.Add(scenario);
                }
            }
        }

        public void ExecuteConcurrently() {
            foreach (Examples examples in multiExamples) {
                foreach (Example example in examples.ExampleSet) {
                    Scenario scenario = Scenario.New(background, GetReplacedSteps(steps, example), example);
                    scenario.ExecuteConcurrently();
                    scenarios.Add(scenario);
                }
            }
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Scenario Outline: ");
            result.Append(name);
            result.Append("\n\n\t");
            result.Append("Raw Steps:");
            foreach (Step step in steps) {
                result.Append("\n\t\t");
                result.Append(step.Name);
                result.Append(" ");
                result.Append(step.Description);
            }
            foreach (Examples examples in multiExamples) {
                result.Append("\n\n\t");
                result.Append(examples.ToString().Replace("\n", "\n\t"));
            }
            for (int i = 0; i < scenarios.Count; i++) {
                result.Append("\n\n\t[");
                result.Append(i + 1);
                result.Append("]");
                foreach (Step step in scenarios[i].Steps) {
                    result.Append("\n\t\t");
                    result.Append(step.ToStringWithResult());
                }
            }
            return result.ToString();
        }

        private IList<Step> GetReplacedSteps(IList<Step> steps, Example example) {
            IList<Step> replacedSteps = new List<Step>();
            foreach (Step step in steps) {
                Step clone = step.CloneWithDifferentDescription(ReplaceStepDescription(step.Description, example));
                replacedSteps.Add(clone);
            }
            return replacedSteps;
        }

        private string ReplaceStepDescription(string description, Example example) {
            string replacedDescription = description;

            Regex reg = new Regex("<([^<>]+)>");
            MatchCollection matchs = reg.Matches(replacedDescription);

            foreach (Match match in matchs) {
                string keyword = match.Groups[1].Value.Trim();
                replacedDescription = replacedDescription.Replace(match.Groups[0].Value, example.Get(keyword));
            }

            return replacedDescription;
        }
    }
}