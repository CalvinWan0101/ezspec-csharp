using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class FeatureTest {

        [TestMethod]
        public void create_feature_with_name_and_description() {
            Feature feature = Feature.New("name", "description");

            Assert.AreEqual("name", feature.Name);
            Assert.AreEqual("description", feature.Description);
        }

        [TestMethod]
        public void create_feature_with_name() {
            Feature feature = Feature.New("name");

            Assert.AreEqual("name", feature.Name);
            Assert.AreEqual("", feature.Description);
        }

        [TestMethod]
        public void get_feature_text() {
            Feature feature = Feature.New("name", "description");
            Assert.AreEqual("Feature: name\n\ndescription", feature.ToString());
        }

        [TestMethod]
        public void get_feature_text_without_description() {
            Feature feature = Feature.New("name");
            Assert.AreEqual("Feature: name", feature.ToString());
        }

        [TestMethod]
        public void get_feature_string() {
            Feature feature = Feature.New("name", "description");
            Assert.AreEqual("Feature: name\n\ndescription", feature.ToString());
        }

        [TestMethod]
        public void default_rule_created_when_new_feature() {
            Feature feature = Feature.New("Feature's name", "Feature's description");

            Assert.AreEqual("", feature.WithDefaultRule().Name);
            Assert.AreEqual("", feature.WithDefaultRule().Description);
        }

        [TestMethod]
        public void create_rule_with_name_and_description_by_feature() {
            Feature feature = Feature.New("Feature's name", "Feature's description");

            feature.NewRule("Rule's name", "Rule's description");

            Assert.AreEqual("Rule's name", feature.WithRule("Rule's name").Name);
            Assert.AreEqual("Rule's description", feature.WithRule("Rule's name").Description);
        }

        [TestMethod]
        public void create_rule_with_name_by_feature() {
            Feature feature = Feature.New("Feature's name", "Feature's description");

            feature.NewRule("Rule's name");

            Assert.AreEqual("Rule's name", feature.WithRule("Rule's name").Name);
            Assert.AreEqual("", feature.WithRule("Rule's name").Description);
        }

        [TestMethod]
        public void create_rule_by_feature_with_same_name() {
            Feature feature = Feature.New("Feature's name", "Feature's description");

            feature.NewRule("Rule's name");

            Assert.ThrowsException<ArgumentException>(() => { feature.NewRule("Rule's name"); });
        }

        [TestMethod]
        public void find_non_exist_rule() {
            Feature feature = Feature.New("Feature's name", "Feature's description");

            Assert.ThrowsException<InvalidOperationException>(() => feature.WithRule("rule non-existent"));
        }

        [TestMethod]
        public void feature_initialization() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.WithDefaultRule().NewScenario();
            feature.NewRule("Rule's name");
            feature.WithRule("Rule's name").NewScenario();

            feature.Initialize();

            Assert.AreEqual(0, feature.WithDefaultRule().Scenarios.Count);
            Assert.AreEqual(0, feature.Rules.Count);
        }

        [TestMethod]
        public void create_scenario_without_name() { 
            Feature feature = Feature.New("Feature's name", "Feature's description");
            RuntimeScenario scenario = feature.WithDefaultRule().NewScenario();

            Assert.AreEqual("create scenario without name", scenario.Name);
        }

        [TestMethod]
        public void get_feature_with_default_rule_with_scenario_string() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.WithDefaultRule()
                .NewScenario("Scenario's name")
                .Given("given description", step.Step.ContinuousAfterFailure, env => { })
                .When("when description", step.Step.ContinuousAfterFailure, env => { })
                .Then("then description", step.Step.ContinuousAfterFailure, env => { });

            string except =
                "Feature: Feature's name\n" +
                "\n" +
                "Feature's description\n" +
                "\n" +
                "Scenario: Scenario's name\n" +
                "[Pending] Given given description\n" +
                "[Pending] When when description\n" +
                "[Pending] Then then description";

            Assert.AreEqual(except, feature.ToString());
        }

        [TestMethod]
        public void get_feature_with_rule_with_scenario_string() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.NewRule("Rule's name", "Rule's description");

            feature.WithRule("Rule's name")
                .NewScenario("Scenario's name")
                .Given("given description", step.Step.ContinuousAfterFailure, env => { })
                .When("when description", step.Step.ContinuousAfterFailure, env => { })
                .Then("then description", step.Step.ContinuousAfterFailure, env => { });

            string except =
                "Feature: Feature's name\n" +
                "\n" +
                "Feature's description\n" +
                "\n" +
                "Rule: Rule's name\n" +
                "Rule's description\n" +
                "\n" +
                "Scenario: Scenario's name\n" +
                "[Pending] Given given description\n" +
                "[Pending] When when description\n" +
                "[Pending] Then then description";

            Assert.AreEqual(except, feature.ToString());
        }

        [TestMethod]
        public void get_feature_with_rule_without_scenario_string() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.NewRule("Rule's name", "Rule's description");

            string except =
                "Feature: Feature's name\n" +
                "\n" +
                "Feature's description\n" +
                "\n" +
                "Rule: Rule's name\n" +
                "Rule's description";

            Assert.AreEqual(except, feature.ToString());
        }

        [TestMethod]
        public void get_feature_with_multiple_rules_string() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.NewRule("Rule1's name", "Rule1's description");
            feature.NewRule("Rule2's name", "Rule2's description");

            feature.WithRule("Rule2's name")
                .NewScenario("Scenario's name")
                .Given("given description", step.Step.ContinuousAfterFailure, env => { })
                .When("when description", step.Step.ContinuousAfterFailure, env => { })
                .Then("then description", step.Step.ContinuousAfterFailure, env => { });

            string except =
                "Feature: Feature's name\n" +
                "\n" +
                "Feature's description\n" +
                "\n" +
                "Rule: Rule1's name\n" +
                "Rule1's description\n" +
                "\n" +
                "Rule: Rule2's name\n" +
                "Rule2's description\n" +
                "\n" +
                "Scenario: Scenario's name\n" +
                "[Pending] Given given description\n" +
                "[Pending] When when description\n" +
                "[Pending] Then then description";

            Assert.AreEqual(except, feature.ToString());
        }

        [TestMethod]
        public void get_feature_with_both_default_rule_and_rules_string() {
            Feature feature = Feature.New("Feature's name", "Feature's description");
            feature.NewRule("Rule's name", "Rule's description");

            feature.WithDefaultRule()
                .NewScenario("Scenario's name")
                .Given("given description", step.Step.ContinuousAfterFailure, env => { })
                .When("when description", step.Step.ContinuousAfterFailure, env => { })
                .Then("then description", step.Step.ContinuousAfterFailure, env => { });
           
            feature.WithRule("Rule's name")
                .NewScenario("Scenario's name")
                .Given("given description", step.Step.ContinuousAfterFailure, env => { })
                .When("when description", step.Step.ContinuousAfterFailure, env => { })
                .Then("then description", step.Step.ContinuousAfterFailure, env => { });

            string except =
                "Feature: Feature's name\n" +
                "\n" +
                "Feature's description\n" +
                "\n" +
                "Scenario: Scenario's name\n" +
                "[Pending] Given given description\n" +
                "[Pending] When when description\n" +
                "[Pending] Then then description\n" +
                "\n" +
                "Rule: Rule's name\n" +
                "Rule's description\n" +
                "\n" +
                "Scenario: Scenario's name\n" +
                "[Pending] Given given description\n" +
                "[Pending] When when description\n" +
                "[Pending] Then then description";

            Assert.AreEqual(except, feature.ToString());
        }
    }

}
