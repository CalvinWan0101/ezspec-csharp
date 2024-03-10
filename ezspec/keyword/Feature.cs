using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword {

    public class Feature {

        private readonly string name;
        private readonly string description;
        private readonly IList<Rule> rules;
        private Rule defaultRule;

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        public ReadOnlyCollection<Rule> Rules {
            get { return new ReadOnlyCollection<Rule>(rules); }
        }

        private Feature(string name, string description) {
            this.name = name;
            this.description = description;
            this.defaultRule = Rule.New("", "");
            this.rules = new List<Rule>();
        }

        public static Feature New(string name) {
            return new Feature(name, "");
        }

        public static Feature New(string name, string description) {
            return new Feature(name, description);
        }

        public void Initialize() {
            defaultRule = Rule.New("", "");
            rules.Clear();
        }

        public void NewRule(string name) {
            NewRule(name, "");
        }

        public void NewRule(string name, string description) {
            if (rules.Any(rule => rule.Name == name)) {
                throw new ArgumentException("Rule name cannot be duplicated: " + name);
            }
            Rule rule = Rule.New(name, description);
            rules.Add(rule);
        }

        public Rule WithDefaultRule() {
            return defaultRule;
        }

        public Rule WithRule(string name) {
            return rules.First(rule => rule.Name == name);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Feature: ");
            result.Append(Name);
            if ("" != Description) {
                result.Append("\n\n")
                    .Append(Description);
            }
            if (0 != defaultRule.Scenarios.Count) {
                result.Append("\n\n");
                result.Append(defaultRule.ToString());
            }
            foreach (Rule rule in rules) {
                result.Append("\n\n");
                result.Append(rule.ToString());
            }
            return result.ToString();
        }
    }
}