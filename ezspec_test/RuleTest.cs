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
        public void get_rule_string_with_empty_name() {
            Rule rule = Rule.New("");

            Assert.AreEqual("", rule.ToString());
        }

        [TestMethod]
        public void create_scenario_with_name_by_rule() {
            Rule rule = Rule.New("Rule's name");

            Scenario scenario = rule.NewScenario("Scenario's name");

            Assert.AreEqual("Scenario's name", scenario.Name);
            Assert.AreEqual(scenario, rule.Scenarios[0]);
        }

        [TestMethod]
        public void create_scenario_without_name_by_rule() {
            Rule rule = Rule.New("Rule's name");

            Scenario scenario = rule.NewScenario();

            Assert.AreEqual("create scenario without name by rule", scenario.Name);
            Assert.AreEqual(scenario, rule.Scenarios[0]);
        }

        [TestMethod]
        public void get_rule_with_scenario_without_name_string() {
            Rule rule = Rule.New("Rule's name");

            Scenario scenario = rule.NewScenario();

            string expect =
                "Rule: Rule's name\n" +
                "\n" +
                "\tScenario: get rule with scenario without name string";
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
                "\tScenario: scenario 1\n" +
                "\t\t[Pending] Given a given step";
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
                "\tScenario: scenario 1\n" +
                "\t\t[Pending] Given a given step\n" +
                "\n" +
                "\tScenario: scenario 2\n" +
                "\t\t[Pending] Given a given step\n" +
                "\t\t[Pending] When do nothing";
            Assert.AreEqual(expect, rule.ToString());
        }

        [TestMethod]
        public void get_rule_without_name_contain_scenario() {
            Rule rule = Rule.New("");
            rule.NewScenario("Scenario's name")
                .Given("a given step", env => { })
                .When("do nothing", env => { });

            string except =
                "\tScenario: Scenario's name\n" +
                "\t\t[Pending] Given a given step\n" +
                "\t\t[Pending] When do nothing";

            Assert.AreEqual(except, rule.ToString());
        }

        [TestMethod]
        public void rule_has_no_background_as_default() {
            Rule rule = Rule.New("rule name");
            Assert.IsNull(rule.Background);
        }

        [TestMethod]
        public void rule_with_background() {
            Rule rule = Rule.New("rule name");
            Background background = rule.NewBackground("background name")
                .Given("give step", env => { })
                .And("and step", env => { });

            Assert.AreEqual(background, rule.Background);
            Assert.AreEqual(2, rule.Background.Steps.Count);
        }

        [TestMethod]
        public void get_rule_string_with_background() {
            Rule rule = Rule.New("rule name");
            rule.NewBackground("background name")
                .Given("give step", env => { })
                .And("and step", env => { });

            string except =
                "Rule: rule name\n" +
                "\n" +
                "\tBackground: background name\n" +
                "\t\t[Pending] Given give step\n" +
                "\t\t[Pending] And and step";

            Assert.AreEqual(except, rule.ToString());
        }

        [TestMethod]
        public void get_rule_string_with_empty_name_with_background() {
            Rule rule = Rule.New("");
            rule.NewBackground("background name")
                .Given("give step", env => { })
                .And("and step", env => { });

            string except =
                "\tBackground: background name\n" +
                "\t\t[Pending] Given give step\n" +
                "\t\t[Pending] And and step";

            Assert.AreEqual(except, rule.ToString());
        }

        [TestMethod]
        public void rule_with_background_and_scenario() {
            Rule rule = Rule.New("buy fruit");
            rule.NewBackground("the price of fruits")
                .Given("give the price of apple, banana and orange", env => {
                    env.Put("applePrice", 20);
                    env.Put("bananaPrice", 10);
                    env.Put("orangePrice", 5);
                });

            rule.NewScenario("buy three kinds of fruits")
                .When("I buy ${5} apples, ${6} bananas and ${7} oranges", env => {
                    int totalPrice = 0;
                    totalPrice += env.GetInt("applePrice") * env.GetIntArg(0);
                    totalPrice += env.GetInt("bananaPrice") * env.GetIntArg(1);
                    totalPrice += env.GetInt("orangePrice") * env.GetIntArg(2);
                    env.Put("totalPrice", totalPrice);
                })
                .Then("I should pay ${195}", env => {
                    Assert.AreEqual(env.GetInt("totalPrice"), env.GetIntArg(0));
                })
                .Execute();
        }

        [TestMethod]
        public void rule_with_background_and_two_scenarios() {
            Rule rule = Rule.New("buy fruit");
            rule.NewBackground("the price of fruits")
                .Given("give the price of apple, banana and orange", env => {
                    env.Put("applePrice", 20);
                    env.Put("bananaPrice", 10);
                    env.Put("orangePrice", 5);
                });

            rule.NewScenario("buy apple and banana")
                .When("I buy ${5} apples and ${6} bananas", env => {
                    int totalPrice = 0;
                    totalPrice += env.GetInt("applePrice") * env.GetIntArg(0);
                    totalPrice += env.GetInt("bananaPrice") * env.GetIntArg(1);
                    env.Put("totalPrice", totalPrice);
                })
                .Then("I should pay ${160}", env => {
                    Assert.AreEqual(env.GetInt("totalPrice"), env.GetIntArg(0));
                })
                .Execute();

            rule.NewScenario("buy banana and orange")
                .When("I buy ${6} bananas and ${7} oranges", env => {
                    int totalPrice = 0;
                    totalPrice += env.GetInt("bananaPrice") * env.GetIntArg(0);
                    totalPrice += env.GetInt("orangePrice") * env.GetIntArg(1);
                    env.Put("totalPrice", totalPrice);
                })
                .Then("I should pay ${95}", env => {
                    Assert.AreEqual(env.GetInt("totalPrice"), env.GetIntArg(0));
                })
                .Execute();
        }

        [TestMethod]
        public void rule_with_background_with_scenario_outline() {
            Rule rule = Rule.New("buy fruit");
            rule.NewBackground("the price of fruits")
                .Given("give the price of apple, banana and orange", env => {
                    env.Put("apple", 20);
                    env.Put("banana", 10);
                    env.Put("orange", 5);
                });

            rule.NewScenarioOutline("buy different fruit")
                .WithExamples(@"
                    | fruit  | quantity | pay |
                    | apple  | 10       | 200 |
                    | orange | 7        | 35  |")
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("fruit", env.Example.Get("fruit"));
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt(env.GetString("fruit")) * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();
        }

        [TestMethod]
        public void rule_with_background_with_scenario_outline_without_name() {
            Rule rule = Rule.New("buy fruit");
            rule.NewBackground("the price of fruits")
                .Given("give the price of apple, banana and orange", env => {
                    env.Put("apple", 20);
                    env.Put("banana", 10);
                    env.Put("orange", 5);
                });

            rule.NewScenarioOutline()
                .WithExamples(@"
                    | fruit  | quantity | pay |
                    | apple  | 10       | 200 |
                    | orange | 7        | 35  |")
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("fruit", env.Example.Get("fruit"));
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt(env.GetString("fruit")) * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();
        }

        [TestMethod]
        public void rule_with_background_with_multiple_scenario_outline() {
            Rule rule = Rule.New("buy fruit");
            rule.NewBackground("the price of fruits")
                .Given("give the price of apple, banana and orange", env => {
                    env.Put("apple", 20);
                    env.Put("banana", 10);
                    env.Put("orange", 5);
                });

            rule.NewScenarioOutline("buy different fruit")
                .WithExamples(@"
                    | fruit  | quantity | pay |
                    | apple  | 10       | 200 |
                    | orange | 7        | 35  |")
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("fruit", env.Example.Get("fruit"));
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt(env.GetString("fruit")) * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();

            rule.NewScenarioOutline("buy different fruit")
                .WithExamples(@"
                    | fruit  | quantity | pay |
                    | banana | 4        | 40  |
                    | orange | 8        | 40  |")
                .When("I bought <quantity> <fruit>", env => {
                    env.Put("fruit", env.Example.Get("fruit"));
                    env.Put("quantity", env.Example.Get("quantity"));
                })
                .Then("I should pay $<pay>", env => {
                    int totalPay = env.GetInt(env.GetString("fruit")) * env.GetInt("quantity");
                    Assert.AreEqual(int.Parse(env.Example.Get("pay")), totalPay);
                })
                .Execute();
        }
    }

}
