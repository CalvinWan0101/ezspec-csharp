using System.Diagnostics;
using System.Text;

namespace ezSpec.keyword {

    public class Rule {

        private string name;
        private string description;
        private IList<RuntimeScenario> scenarios;
        private Background background;

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        public IList<RuntimeScenario> Scenarios {
            get { return scenarios; }
        }
        
        public Background Background {
            get { return background; }
        }

        private Rule(string name, string description) {
            this.name = name;
            this.description = description;
            this.scenarios = new List<RuntimeScenario>();
            this.background = null;
        }

        public static Rule New(string name) {
            return new Rule(name, "");
        }

        public static Rule New(string name, string description) {
            return new Rule(name, description);
        }

        public Background NewBackground(string name) {
            background = Background.New(name);
            return background;
        }

        public RuntimeScenario NewScenario(string name) {
            scenarios.Add(RuntimeScenario.New(name));
            return scenarios.Last();
        }

        public RuntimeScenario NewScenario() {
            scenarios.Add(RuntimeScenario.New(GetNewScenarioCallerFunctionName()));
            return scenarios.Last();
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            if("" != name) {
                result.Append("Rule: ");
                result.Append(name);
            }
            if ("" != description) {
                result.Append("\n");
                result.Append(description);
            }
            foreach (RuntimeScenario scenario in scenarios) {
                if(0 != result.Length) {
                    result.Append("\n\n");
                }
                result.Append(scenario.ToString());
            }
            return result.ToString();
        }

        private string GetNewScenarioCallerFunctionName() {
            StackTrace stackTrace = new StackTrace();
            return stackTrace.GetFrame(2)!.GetMethod()!.Name.Replace("_", " ");
        }

    }

}
