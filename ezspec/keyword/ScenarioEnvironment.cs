using ezSpec.keyword.table;

namespace ezSpec.keyword {
    public class ScenarioEnvironment {

        private int executionCount;
        private Example input;
        private IList<Argument> arguments;
        private IList<Argument> historicalArguments;
        private Table anonymousTable;
        private IDictionary<string, object> context;

        public int ExecutionCount {
            get { return executionCount; }
        }

        public Example Input {
            get { return input; }
        }

        public IList<Argument> Arguments {
            get { return arguments; }
        }

        public IList<Argument> HistoricalArguments {
            get { return historicalArguments; }
        }

        public Table Table {
            get { return anonymousTable; }
        }

        protected ScenarioEnvironment() {
            executionCount = 0;
            arguments = new List<Argument>();
            historicalArguments = new List<Argument>();
            context = new Dictionary<string, object>();
        }

        protected ScenarioEnvironment(ScenarioEnvironment env) {
            executionCount = 0;
            arguments = new List<Argument>();
            historicalArguments = new List<Argument>();
            foreach (Argument argument in env.historicalArguments) {
                historicalArguments.Add(Argument.New(argument));
            }
            input = Example.New(env.input);
            anonymousTable = Table.New(env.anonymousTable);
            context = new Dictionary<string, object>(env.context);
        }

        public static ScenarioEnvironment New() {
            return new ScenarioEnvironment();
        }

        public static ScenarioEnvironment New(ScenarioEnvironment env) {
            return new ScenarioEnvironment(env);
        }

        internal void SetExecutionCount(int count) {
            executionCount = count;
        }

        internal void SetInput(Example example) {
            input = Example.New(example);
        }

        internal void SetArguments(IList<Argument> arguments) {
            this.arguments.Clear();
            foreach (Argument argument in arguments) {
                this.arguments.Add(Argument.New(argument));
                historicalArguments.Add(Argument.New(argument));
            }
        }

        internal void SetAnonymousTable(Table table) {
            anonymousTable = Table.New(table);
        }

        public string GetStringArg(int index) {
            return arguments[index].Value;
        }

        public string GetStringArg(string key) {
            return FindInArgumentsByKey(key);
        }

        public int GetIntArg(int index) {
            return int.Parse(arguments[index].Value);
        }

        public int GetIntArg(string key) {
            return int.Parse(FindInArgumentsByKey(key));
        }

        public double GetDoubleArg(int index) {
            return double.Parse(arguments[index].Value);
        }

        public double GetDoubleArg(string key) {
            return double.Parse(FindInArgumentsByKey(key));
        }

        public string GetStringHistoricalArg(int index) {
            return historicalArguments[index].Value;
        }

        public string GetStringHistoricalArg(string key) {
            return FindInHistoricalArgumentsByKey(key);
        }

        public int GetIntHistoricalArg(int index) {
            return int.Parse(historicalArguments[index].Value);
        }

        public int GetIntHistoricalArg(string key) {
            return int.Parse(FindInHistoricalArgumentsByKey(key));
        }

        public double GetDoubleHistoricalArg(int index) {
            return double.Parse(historicalArguments[index].Value);
        }

        public double GetDoubleHistoricalArg(string key) {
            return double.Parse(FindInHistoricalArgumentsByKey(key));
        }

        public void Put(string key, object value) {
            context.Add(key, value);
        }

        public string GetString(string key) {
            return (string)FindInContextByKey(key);
        }

        public int GetInt(string key) {
            return (int)FindInContextByKey(key);
        }

        public double GetDouble(string key) {
            return (double)FindInContextByKey(key);
        }

        public bool GetBool(string key) {
            return (bool)FindInContextByKey(key);
        }

        public T Get<T>(string key) {
            return (T)FindInContextByKey(key);
        }

        private string FindInArgumentsByKey(string key) {
            return arguments.First((argument) => argument.Key == key).Value;
        }

        private string FindInHistoricalArgumentsByKey(string key) {
            return historicalArguments.First((argument) => argument.Key == key).Value;
        }

        private object FindInContextByKey(string key) {
            return context.First((pair) => pair.Key == key).Value;
        }
    }
}
