using ezSpec.keyword.table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class ScenarioEnvironmentTest {

        private static Example inputExample;
        private static List<Argument> arguments1;
        private static List<Argument> arguments2;
        private static Table anonymousTable;

        [ClassInitialize]
        public static void BeforeAll(TestContext context) {
            Header inputHeader = Header.New(new List<string>() {
                "id", "name", "score"
            });
            Row inputRow = Row.New(inputHeader, new List<string>() { "10001", "Joe", "60" });
            inputExample = Example.New(inputRow);

            arguments1 = new List<Argument>() {
                Argument.New("${id : 10001}"),
                Argument.New("${name : Joe}")
            };
            arguments2 = new List<Argument>() {
                Argument.New("${score : 60}")
            };

            Header anonymousHeader = Header.New(new List<string>() {
                "id", "name", "score"
            });

            List<Row> anonymousRows = new List<Row>() {
                Row.New(anonymousHeader, new List<string>() { "20001", "Anna", "70" })
            };

            anonymousTable = Table.New(anonymousHeader, anonymousRows);
        }


        [TestMethod]
        public void create_empty_scenario_enviroment() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            Assert.AreEqual(0, env.ExecutionCount);
            Assert.IsNull(env.Input);
            Assert.AreEqual(0, env.Arguments.Count);
            Assert.AreEqual(0, env.HistoricalArguments.Count);
            Assert.IsNull(env.Table);
        }

        [TestMethod]
        public void set_execution_count() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            var setExecutionCountMethod = env.GetType().GetMethod("SetExecutionCount", BindingFlags.Instance | BindingFlags.NonPublic);
            setExecutionCountMethod?.Invoke(env, new object[] { 1 });

            Assert.AreEqual(1, env.ExecutionCount);
        }

        [TestMethod]
        public void set_input() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            var setInputTableMethod = env.GetType().GetMethod("SetInput", BindingFlags.Instance | BindingFlags.NonPublic);
            setInputTableMethod?.Invoke(env, new object[] { inputExample });

            Assert.IsNotNull(env.Input);
            Assert.AreEqual(3, env.Input.Columns.Count);
            Assert.AreEqual("10001", env.Input.Get(0));
            Assert.AreEqual("Joe", env.Input.Get(1));
            Assert.AreEqual("60", env.Input.Get(2));
        }

        [TestMethod]
        public void set_arguments() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            var setArgumentsMethod = env.GetType().GetMethod("SetArguments", BindingFlags.Instance | BindingFlags.NonPublic);
            setArgumentsMethod?.Invoke(env, new object[] { arguments1 });

            Assert.AreEqual(2, env.Arguments.Count);
            Assert.AreEqual("id", env.Arguments[0].Key);
            Assert.AreEqual("10001", env.Arguments[0].Value);
            Assert.AreEqual("name", env.Arguments[1].Key);
            Assert.AreEqual("Joe", env.Arguments[1].Value);
            Assert.AreEqual(2, env.HistoricalArguments.Count);
            Assert.AreEqual("id", env.HistoricalArguments[0].Key);
            Assert.AreEqual("10001", env.HistoricalArguments[0].Value);
            Assert.AreEqual("name", env.HistoricalArguments[1].Key);
            Assert.AreEqual("Joe", env.HistoricalArguments[1].Value);
        }

        [TestMethod]
        public void set_twice_arguments() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            var setArgumentsMethod = env.GetType().GetMethod("SetArguments", BindingFlags.Instance | BindingFlags.NonPublic);
            setArgumentsMethod?.Invoke(env, new object[] { arguments1 });
            setArgumentsMethod?.Invoke(env, new object[] { arguments2 });

            Assert.AreEqual(1, env.Arguments.Count);
            Assert.AreEqual("score", env.Arguments[0].Key);
            Assert.AreEqual("60", env.Arguments[0].Value);
            Assert.AreEqual(3, env.HistoricalArguments.Count);
            Assert.AreEqual("id", env.HistoricalArguments[0].Key);
            Assert.AreEqual("10001", env.HistoricalArguments[0].Value);
            Assert.AreEqual("name", env.HistoricalArguments[1].Key);
            Assert.AreEqual("Joe", env.HistoricalArguments[1].Value);
            Assert.AreEqual("score", env.HistoricalArguments[2].Key);
            Assert.AreEqual("60", env.HistoricalArguments[2].Value);
        }

        [TestMethod]
        public void set_anonymous_table() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            var setInputTableMethod = env.GetType().GetMethod("SetAnonymousTable", BindingFlags.Instance | BindingFlags.NonPublic);
            setInputTableMethod?.Invoke(env, new object[] { anonymousTable });

            Assert.IsNotNull(env.Table);
            Assert.AreEqual(3, env.Table.Header.Size);
            Assert.AreEqual("id", env.Table.Header.Get(0));
            Assert.AreEqual("name", env.Table.Header.Get(1));
            Assert.AreEqual("score", env.Table.Header.Get(2));
            Assert.AreEqual(1, env.Table.Rows.Count);
            Assert.AreEqual("20001", env.Table.Rows[0].Get(0));
            Assert.AreEqual("Anna", env.Table.Rows[0].Get(1));
            Assert.AreEqual("70", env.Table.Rows[0].Get(2));
        }

        [TestMethod]
        public void get_argument_as_string_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);

            Assert.AreEqual("10001", env.GetStringArg(0));
            Assert.AreEqual("Joe", env.GetStringArg(1));
        }

        [TestMethod]
        public void get_argument_as_string_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);

            Assert.AreEqual("10001", env.GetStringArg("id"));
            Assert.AreEqual("Joe", env.GetStringArg("name"));
        }

        [TestMethod]
        public void get_argument_as_int_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(60, env.GetIntArg(0));
        }

        [TestMethod]
        public void get_argument_as_int_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(60, env.GetIntArg("score"));
        }

        [TestMethod]
        public void get_argument_as_double_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(60.0, env.GetDoubleArg(0));
        }

        [TestMethod]
        public void get_argument_as_double_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(60.0, env.GetDoubleArg("score"));
        }

        [TestMethod]
        public void get_historical_arguments_as_string_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual("10001", env.GetStringHistoricalArg(0));
            Assert.AreEqual("Joe", env.GetStringHistoricalArg(1));
            Assert.AreEqual("60", env.GetStringHistoricalArg(2));
        }

        [TestMethod]
        public void get_historical_arguments_as_string_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual("10001", env.GetStringHistoricalArg("id"));
            Assert.AreEqual("Joe", env.GetStringHistoricalArg("name"));
            Assert.AreEqual("60", env.GetStringHistoricalArg("score"));
        }

        [TestMethod]
        public void get_historical_arguments_as_int_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(10001, env.GetIntHistoricalArg(0));
            Assert.AreEqual(60, env.GetIntHistoricalArg(2));
        }

        [TestMethod]
        public void get_historical_arguments_as_int_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(10001, env.GetIntHistoricalArg("id"));
            Assert.AreEqual(60, env.GetIntHistoricalArg("score"));
        }

        [TestMethod]
        public void get_historical_arguments_as_double_by_index() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(10001.0, env.GetDoubleHistoricalArg(0));
            Assert.AreEqual(60.0, env.GetDoubleHistoricalArg(2));
        }

        [TestMethod]
        public void get_historical_arguments_as_double_by_key() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);

            Assert.AreEqual(10001.0, env.GetDoubleHistoricalArg("id"));
            Assert.AreEqual(60.0, env.GetDoubleHistoricalArg("score"));
        }

        [TestMethod]
        public void set_and_get_string_variable() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            env.Put("str_var", "this is a string");

            Assert.AreEqual("this is a string", env.GetString("str_var"));
        }

        [TestMethod]
        public void set_and_get_int_variable() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            env.Put("int_var", 3);

            Assert.AreEqual(3, env.GetInt("int_var"));
        }

        [TestMethod]
        public void set_and_get_double_variable() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            env.Put("double_var", 3.0);

            Assert.AreEqual(3.0, env.GetDouble("double_var"));
        }

        [TestMethod]
        public void set_and_get_boolean_variable() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            env.Put("boolean_var", true);

            Assert.AreEqual(true, env.GetBool("boolean_var"));
        }

        [TestMethod]
        public void set_and_get_object_variable() {
            ScenarioEnvironment env = ScenarioEnvironment.New();

            env.Put("object_var", new Tuple<int, int>(3, 5));

            var result = env.Get<Tuple<int, int>>("object_var");
            Assert.AreEqual(3, result.Item1);
            Assert.AreEqual(5, result.Item2);
        }

        [TestMethod]
        public void copy_environment() {
            ScenarioEnvironment env = ScenarioEnvironment.New();
            SetEnviormentArgument(env, arguments1);
            SetEnviormentArgument(env, arguments2);
            SetEnviormentInput(env, inputExample);
            SetEnviormentTable(env, anonymousTable);
            env.Put("str_var", "this is a string");
            env.Put("int_var", 3);
            env.Put("double_var", 3.0);
            env.Put("boolean_var", true);
            env.Put("object_var", new Tuple<int, int>(3, 5));

            ScenarioEnvironment copyEnv = ScenarioEnvironment.New(env);

            Assert.AreEqual(0, copyEnv.Arguments.Count);
            Assert.AreEqual(3, copyEnv.HistoricalArguments.Count);
            Assert.AreEqual("10001", copyEnv.GetStringHistoricalArg("id"));
            Assert.AreEqual("Joe", copyEnv.GetStringHistoricalArg("name"));
            Assert.AreEqual("60", copyEnv.GetStringHistoricalArg("score"));
            Assert.AreEqual(3, copyEnv.Input.Columns.Count);
            Assert.AreEqual(1, copyEnv.Table.Rows.Count);
            Assert.AreEqual("this is a string", copyEnv.GetString("str_var"));
            Assert.AreEqual(3, copyEnv.GetInt("int_var"));
            Assert.AreEqual(3.0, copyEnv.GetDouble("double_var"));
            Assert.AreEqual(true, copyEnv.GetBool("boolean_var"));
            Assert.AreEqual(3, copyEnv.Get<Tuple<int, int>>("object_var").Item1);
            Assert.AreEqual(5, copyEnv.Get<Tuple<int, int>>("object_var").Item2);
        }

        private void SetEnviormentArgument(ScenarioEnvironment env, IList<Argument> arguments) {
            var setArgumentsMethod = env.GetType().GetMethod("SetArguments", BindingFlags.Instance | BindingFlags.NonPublic);
            setArgumentsMethod?.Invoke(env, new object[] { arguments });
        }

        private void SetEnviormentInput(ScenarioEnvironment env, Example input) {
            var setArgumentsMethod = env.GetType().GetMethod("SetInput", BindingFlags.Instance | BindingFlags.NonPublic);
            setArgumentsMethod?.Invoke(env, new object[] { input });
        }

        private void SetEnviormentTable(ScenarioEnvironment env, Table table) {
            var setArgumentsMethod = env.GetType().GetMethod("SetAnonymousTable", BindingFlags.Instance | BindingFlags.NonPublic);
            setArgumentsMethod?.Invoke(env, new object[] { table });
        }
    }

}
