using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class ScenarioOutlineTest {
        
        [TestMethod]
        public void create_scenario_outline() {
            ScenarioOutline scenarioOutline = ScenarioOutline.New("ScenarioOutline's Name");

            Assert.AreEqual("ScenarioOutline's Name", scenarioOutline.Name);
        }

        [TestMethod]
        public void create_scenario_outline_without_name() {
            ScenarioOutline scenarioOutline = ScenarioOutline.New();

            Assert.AreEqual("", scenarioOutline.Name);
        }

        [TestMethod]
        public void create_scenario_outline_with_examples_by_raw_data() {
            ScenarioOutline scenarioOutline = ScenarioOutline.New("ScenarioOutline's Name");
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            scenarioOutline.WithExamples(rawData);

            var getScenarioOutlineField = scenarioOutline.GetType().GetField("multiExamples", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = getScenarioOutlineField?.GetValue(scenarioOutline);
            Assert.IsNotNull(value);

            List<Examples> listOfExamples = value as List<Examples>;

            Assert.AreEqual(1, listOfExamples.Count);
            Assert.AreEqual("", listOfExamples[0].Name);
            Assert.AreEqual("", listOfExamples[0].Description);
            Assert.AreEqual(3, listOfExamples[0].ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", listOfExamples[0].ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", listOfExamples[0].ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", listOfExamples[0].ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_scenario_outline_with_examples_by_examples() {
            ScenarioOutline scenarioOutline = ScenarioOutline.New("ScenarioOutline's Name");
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";
            Examples examples = Examples.New(rawData);

            scenarioOutline.WithExamples(examples);

            var getScenarioOutlineField = scenarioOutline.GetType().GetField("multiExamples", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = getScenarioOutlineField?.GetValue(scenarioOutline);
            Assert.IsNotNull(value);

            List<Examples> listOfExamples = value as List<Examples>;

            Assert.AreEqual(1, listOfExamples.Count);
            Assert.AreEqual("", listOfExamples[0].Name);
            Assert.AreEqual("", listOfExamples[0].Description);
            Assert.AreEqual(3, listOfExamples[0].ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", listOfExamples[0].ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", listOfExamples[0].ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", listOfExamples[0].ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_scenario_outline_with_examples_by_multiple_examples() {
            ScenarioOutline scenarioOutline = ScenarioOutline.New("ScenarioOutline's Name");
            string rawData1 = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";
            Examples examples1 = Examples.New(rawData1);

            string rawData2 = @"
                |  id   | name | score |
                | 10004 | Arno |  70   |
                | 10005 | Alan |  90   |";
            Examples examples2 = Examples.New(rawData2);

            scenarioOutline.WithExamples(examples1, examples2);

            var getScenarioOutlineField = scenarioOutline.GetType().GetField("multiExamples", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = getScenarioOutlineField?.GetValue(scenarioOutline);
            Assert.IsNotNull(value);

            List<Examples> listOfExamples = value as List<Examples>;

            Assert.AreEqual(2, listOfExamples.Count);
            Assert.AreEqual("", listOfExamples[0].Name);
            Assert.AreEqual("", listOfExamples[0].Description);
            Assert.AreEqual(3, listOfExamples[0].ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", listOfExamples[0].ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", listOfExamples[0].ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", listOfExamples[0].ExampleSet[2].ToString());
            Assert.AreEqual("", listOfExamples[1].Name);
            Assert.AreEqual("", listOfExamples[1].Description);
            Assert.AreEqual(2, listOfExamples[1].ExampleSet.Count);
            Assert.AreEqual("|\t10004\t|\tArno\t|\t70\t|", listOfExamples[1].ExampleSet[0].ToString());
            Assert.AreEqual("|\t10005\t|\tAlan\t|\t90\t|", listOfExamples[1].ExampleSet[1].ToString());
        }

        [TestMethod]
        public void execute_with_single_examples() {
            string rawData = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();

            var scenariosField = scenarioOutline.GetType().GetField("scenarios", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = scenariosField?.GetValue(scenarioOutline);
            Assert.IsNotNull(value);

            List<Scenario> scenarios = value as List<Scenario>;

            Assert.AreEqual(3, scenarios.Count);
            Assert.AreEqual(3, scenarios[0].Steps.Count);
            Assert.AreEqual("apple which cost $10", scenarios[0].Steps[0].Description);
            Assert.AreEqual("I bought 3 apple", scenarios[0].Steps[1].Description);
            Assert.AreEqual("I should pay $30", scenarios[0].Steps[2].Description);
            Assert.AreEqual(3, scenarios[1].Steps.Count);
            Assert.AreEqual("banana which cost $20", scenarios[1].Steps[0].Description);
            Assert.AreEqual("I bought 4 banana", scenarios[1].Steps[1].Description);
            Assert.AreEqual("I should pay $80", scenarios[1].Steps[2].Description);
            Assert.AreEqual(3, scenarios[2].Steps.Count);
            Assert.AreEqual("orange which cost $30", scenarios[2].Steps[0].Description);
            Assert.AreEqual("I bought 2 orange", scenarios[2].Steps[1].Description);
            Assert.AreEqual("I should pay $60", scenarios[2].Steps[2].Description);
        }

        [TestMethod]
        public void execute_with_multiple_examples() {
            string rawData1 = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";
            string rawData2 = @"
                | fruit | cost | quantity | pay |
                | peach |  15  |    6     | 90  |
                | grape |  25  |    5     | 125 |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData1)
                .WithExamples(rawData2)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();

            var scenariosField = scenarioOutline.GetType().GetField("scenarios", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = scenariosField?.GetValue(scenarioOutline);
            Assert.IsNotNull(value);

            List<Scenario> scenarios = value as List<Scenario>;

            Assert.AreEqual(5, scenarios.Count);
            Assert.AreEqual(3, scenarios[0].Steps.Count);
            Assert.AreEqual("apple which cost $10", scenarios[0].Steps[0].Description);
            Assert.AreEqual("I bought 3 apple", scenarios[0].Steps[1].Description);
            Assert.AreEqual("I should pay $30", scenarios[0].Steps[2].Description);
            Assert.AreEqual(3, scenarios[1].Steps.Count);
            Assert.AreEqual("banana which cost $20", scenarios[1].Steps[0].Description);
            Assert.AreEqual("I bought 4 banana", scenarios[1].Steps[1].Description);
            Assert.AreEqual("I should pay $80", scenarios[1].Steps[2].Description);
            Assert.AreEqual(3, scenarios[2].Steps.Count);
            Assert.AreEqual("orange which cost $30", scenarios[2].Steps[0].Description);
            Assert.AreEqual("I bought 2 orange", scenarios[2].Steps[1].Description);
            Assert.AreEqual("I should pay $60", scenarios[2].Steps[2].Description);
            Assert.AreEqual("peach which cost $15", scenarios[3].Steps[0].Description);
            Assert.AreEqual("I bought 6 peach", scenarios[3].Steps[1].Description);
            Assert.AreEqual("I should pay $90", scenarios[3].Steps[2].Description);
            Assert.AreEqual("grape which cost $25", scenarios[4].Steps[0].Description);
            Assert.AreEqual("I bought 5 grape", scenarios[4].Steps[1].Description);
            Assert.AreEqual("I should pay $125", scenarios[4].Steps[2].Description);
        }

        [TestMethod]
        public void get_scenario_outline_with_single_examples_string_before_execute() {
            string rawData = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                });

            string except =
                "Scenario Outline: buy fruit\n" +
                "Raw Steps:\n" +
                "Given <fruit> which cost $<cost>\n" +
                "When I bought <quantity> <fruit>\n" +
                "Then I should pay $<pay>\n" +
                "\n" +
                "Examples: \n" +
                "| fruit  | cost | quantity | pay |\n" +
                "| apple  | 10   | 3        | 30  |\n" +
                "| banana | 20   | 4        | 80  |\n" +
                "| orange | 30   | 2        | 60  |";
            Assert.AreEqual(except, scenarioOutline.ToString());
        }

        [TestMethod]
        public void get_scenario_outline_with_multiple_examples_string_before_execute() {
            string rawData1 = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";
            string rawData2 = @"
                | fruit | cost | quantity | pay |
                | peach |  15  |    6     | 90  |
                | grape |  25  |    5     | 125 |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData1)
                .WithExamples(rawData2)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                });

            string except =
                "Scenario Outline: buy fruit\n" +
                "Raw Steps:\n" +
                "Given <fruit> which cost $<cost>\n" +
                "When I bought <quantity> <fruit>\n" +
                "Then I should pay $<pay>\n" +
                "\n" +
                "Examples: \n" +
                "| fruit  | cost | quantity | pay |\n" +
                "| apple  | 10   | 3        | 30  |\n" +
                "| banana | 20   | 4        | 80  |\n" +
                "| orange | 30   | 2        | 60  |\n" +
                "\n" +
                "Examples: \n" +
                "| fruit | cost | quantity | pay |\n" +
                "| peach | 15   | 6        | 90  |\n" +
                "| grape | 25   | 5        | 125 |";
            Assert.AreEqual(except, scenarioOutline.ToString());
        }

        [TestMethod]
        public void get_scenario_outline_with_single_examples_string_after_execute() {
            string rawData = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();

            string except =
                "Scenario Outline: buy fruit\n" +
                "Raw Steps:\n" +
                "Given <fruit> which cost $<cost>\n" +
                "When I bought <quantity> <fruit>\n" +
                "Then I should pay $<pay>\n" +
                "\n" +
                "Examples: \n" +
                "| fruit  | cost | quantity | pay |\n" +
                "| apple  | 10   | 3        | 30  |\n" +
                "| banana | 20   | 4        | 80  |\n" +
                "| orange | 30   | 2        | 60  |\n" +
                "\n" +
                "[1]\n" +
                "[Success] Given apple which cost $10\n" +
                "[Success] When I bought 3 apple\n" +
                "[Success] Then I should pay $30\n" +
                "\n" +
                "[2]\n" +
                "[Success] Given banana which cost $20\n" +
                "[Success] When I bought 4 banana\n" +
                "[Success] Then I should pay $80\n" +
                "\n" +
                "[3]\n" +
                "[Success] Given orange which cost $30\n" +
                "[Success] When I bought 2 orange\n" +
                "[Success] Then I should pay $60";
            Assert.AreEqual(except, scenarioOutline.ToString());
        }

        [TestMethod]
        public void get_scenario_outline_with_multiple_examples_string_after_execute() {
            string rawData1 = @"
                | fruit  | cost | quantity | pay |
                | apple  |  10  |    3     | 30  |
                | banana |  20  |    4     | 80  |
                | orange |  30  |    2     | 60  |";
            string rawData2 = @"
                | fruit | cost | quantity | pay |
                | peach |  15  |    6     | 90  |
                | grape |  25  |    5     | 125 |";

            ScenarioOutline scenarioOutline = ScenarioOutline.New("buy fruit");
            scenarioOutline.WithExamples(rawData1)
                .WithExamples(rawData2)
                .Given("<fruit> which cost $<cost>", env => {
                    env.Put("cost", env.Example.Get("cost"));
                })
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt("cost") * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();

            string except =
                "Scenario Outline: buy fruit\n" +
                "Raw Steps:\n" +
                "Given <fruit> which cost $<cost>\n" +
                "When I bought <quantity> <fruit>\n" +
                "Then I should pay $<pay>\n" +
                "\n" +
                "Examples: \n" +
                "| fruit  | cost | quantity | pay |\n" +
                "| apple  | 10   | 3        | 30  |\n" +
                "| banana | 20   | 4        | 80  |\n" +
                "| orange | 30   | 2        | 60  |\n" +
                "\n" +
                "Examples: \n" +
                "| fruit | cost | quantity | pay |\n" +
                "| peach | 15   | 6        | 90  |\n" +
                "| grape | 25   | 5        | 125 |\n" +
                "\n" +
                "[1]\n" +
                "[Success] Given apple which cost $10\n" +
                "[Success] When I bought 3 apple\n" +
                "[Success] Then I should pay $30\n" +
                "\n" +
                "[2]\n" +
                "[Success] Given banana which cost $20\n" +
                "[Success] When I bought 4 banana\n" +
                "[Success] Then I should pay $80\n" +
                "\n" +
                "[3]\n" +
                "[Success] Given orange which cost $30\n" +
                "[Success] When I bought 2 orange\n" +
                "[Success] Then I should pay $60\n" +
                "\n" +
                "[4]\n" +
                "[Success] Given peach which cost $15\n" +
                "[Success] When I bought 6 peach\n" +
                "[Success] Then I should pay $90\n" +
                "\n" +
                "[5]\n" +
                "[Success] Given grape which cost $25\n" +
                "[Success] When I bought 5 grape\n" +
                "[Success] Then I should pay $125";
            Assert.AreEqual(except, scenarioOutline.ToString());
        }
    }
}