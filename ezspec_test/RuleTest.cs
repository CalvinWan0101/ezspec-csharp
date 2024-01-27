using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class RuleTest {

        [TestMethod]
        public void create_rule_with_name_and_description() {
            Rule rule = Rule.New("name", "description");

            Assert.AreEqual("name", rule.Name);
            Assert.AreEqual("description", rule.Description);
        }

        [TestMethod]
        public void create_rule_with_name() {
            Rule rule = Rule.New("name");

            Assert.AreEqual("name", rule.Name);
            Assert.AreEqual("", rule.Description);
        }

        [TestMethod]
        public void get_rule_string() {
            Rule rule = Rule.New("name", "description");

            Assert.AreEqual("Rule: name\ndescription", rule.ToString());
        }

        [TestMethod]
        public void get_rule_string_without_description() {
            Rule rule = Rule.New("name");

            Assert.AreEqual("Rule: name", rule.ToString());
        }

        [TestMethod]
        public void create_scenario_with_name_by_rule() {
            Rule rule = Rule.New("Rule's name");

            RuntimeScenario scenario = rule.NewScenario("Scenario's name");

            Assert.AreEqual("Scenario's name", scenario.Name);
            Assert.AreEqual(scenario, rule.Scenarios[0]);
        }

        [TestMethod]
        public void create_scenario_without_name_by_rule() {
            Rule rule = Rule.New("Rule's name");

            RuntimeScenario scenario = rule.NewScenario();

            Assert.AreEqual("create scenario without name by rule", scenario.Name);
            Assert.AreEqual(scenario, rule.Scenarios[0]);
        }

        [TestMethod]
        public void get_rule_with_scenario_without_name_string() {
            Rule rule = Rule.New("Rule's name");

            RuntimeScenario scenario = rule.NewScenario();

            string expect =
                "Rule: Rule's name\n" +
                "\n" +
                "Scenario: get rule with scenario without name string";
            Assert.AreEqual(expect, rule.ToString());
        }

        [TestMethod]
        public void get_rule_with_scenario_string() {
            Rule rule = Rule.New("name", "description");

            rule.NewScenario("scenario 1")
                .Given("a given step", env => { });

            string expect =
                "Rule: name\n" +
                "description\n" +
                "\n" +
                "Scenario: scenario 1\n" +
                "[Pending] Given a given step";
            Assert.AreEqual(expect, rule.ToString());
        }

        [TestMethod]
        public void get_rule_with_multiple_scenarios_string() {
            Rule rule = Rule.New("name", "description");

            rule.NewScenario("scenario 1")
                .Given("a given step", env => { });
            rule.NewScenario("scenario 2")
                .Given("a given step", env => { })
                .When("do nothing", env => { });

            string expect =
                "Rule: name\n" +
                "description\n" +
                "\n" +
                "Scenario: scenario 1\n" +
                "[Pending] Given a given step\n" +
                "\n" +
                "Scenario: scenario 2\n" +
                "[Pending] Given a given step\n" +
                "[Pending] When do nothing";
            Assert.AreEqual(expect, rule.ToString());
        }

    }

}
