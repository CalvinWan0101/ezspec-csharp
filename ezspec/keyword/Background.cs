using ezSpec.keyword.step;
using System.Text;

namespace ezSpec.keyword {
    public class Background : RuntimeScenario {
        protected Background(string name) : base(name) {
        }

        public static new Background New(string name) {
            return new Background(name);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Background: ");
            result.Append(name);
            foreach (Step step in steps) {
                result.Append("\n");
                result.Append(step.Name);
                result.Append(" ");
                result.Append(step.Description);
            }
            return result.ToString();
        }
    }
}
