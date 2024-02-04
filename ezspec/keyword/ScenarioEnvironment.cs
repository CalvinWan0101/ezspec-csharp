using ezSpec.keyword.table;
using System.Collections.ObjectModel;

namespace ezSpec.keyword {
    public class ScenarioEnvironment {

        private int executionCount;
        private Example example;
        private IList<Argument> arguments;
        private IList<Argument> historicalArguments;
        private Table anonymousTable;
        private IDictionary<string, object> context;

        public int ExecutionCount {
            get { return executionCount; }
        }

        public Example Example {
            get { return example; }
        }

        public ReadOnlyCollection<Argument> Arguments {
            get { return new ReadOnlyCollection<Argument>(arguments); }
        }

        public ReadOnlyCollection<Argument> HistoricalArguments {
            get { return new ReadOnlyCollection<Argument>(historicalArguments); }
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
            if (env.example != null) {
                example = Example.New(env.example);
            }
            if (env.anonymousTable != null) {
                anonymousTable = Table.New(env.anonymousTable);
            }
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

        internal void SetExample(Example example) {
            this.example = Example.New(example);
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
            object value = FindInContextByKey(key);
            if (value is string) {
                return int.Parse((string)value);
            }
            return (int)value;
        }

        public double GetDouble(string key) {
            object value = FindInContextByKey(key);
            if (value is string) {
                return double.Parse((string)value);
            }
            return (double)value;
        }

        public bool GetBool(string key) {
            object value = FindInContextByKey(key);
            if (value is string) {
                return bool.Parse((string)value);
            }
            return (bool)value;
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
