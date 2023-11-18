namespace ezSpec {

    public class Rule {

        private string name;
        private string description;

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        private Rule(string name, string description) {
            this.name = name;
            this.description = description;
        }

        static public Rule New(string name) {
            return new Rule(name, "");
        }

        static public Rule New(string name, string description) {
            return new Rule(name, description);
        }

        public override string ToString() {
            string result = "Rule: " + name;
            if ("" != description) {
                result += "\n" + description;
            }
            return result;
        }

    }

}
