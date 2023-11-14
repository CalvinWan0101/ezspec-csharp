namespace ezspec_csharp_test {
    
    public class Feature {

        private string name;
        private string description;

        public string Name {
            get { return name; }
        }

        public string Description { 
            get { return description; }
        }

        private Feature(string name, string description) {
            this.name = name;
            this.description = description;
        }

        static public Feature New(string name) {
            return new Feature(name, "");
        }

        static public Feature New(string name, string description) {
            return new Feature(name, description);
        }

    }

}