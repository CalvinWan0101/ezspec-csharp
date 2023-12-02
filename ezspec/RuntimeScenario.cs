namespace ezSpec {
    public class RuntimeScenario {
        private string name;

        public string Name { 
            get { return name; }
        }

        private RuntimeScenario(string name) {
            this.name = name;
        }

        public static RuntimeScenario New(string name) {
            return new RuntimeScenario(name);
        }

        //public void Given(string v, Action value) {
        //    throw new NotImplementedException();
        //}
    }
}
