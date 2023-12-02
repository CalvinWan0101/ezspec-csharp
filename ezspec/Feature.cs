namespace ezSpec {

    public class Feature {

        private string name;
        private string description;

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        public string FeatureText {
            get {
                string featureText = "Feature: " + Name;
                if ("" != Description) {
                    featureText += "\n\n" + Description;
                }
                return featureText;
            }
        }

        private Feature(string name, string description) {
            this.name = name;
            this.description = description;
        }

        public static Feature New(string name) {
            return new Feature(name, "");
        }

        public static Feature New(string name, string description) {
            return new Feature(name, description);
        }

        public override string ToString() {
            return FeatureText;
        }
    }
}