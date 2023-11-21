namespace ezSpec {
    public class RuntimeScenario {
        private string name;

        public string Name { 
            get { return name; }
        }

        public RuntimeScenario(string name) {
            this.name = name;
        }

        //public void Given(string v, Action value) {
        //    throw new NotImplementedException();
        //}
    }
}
