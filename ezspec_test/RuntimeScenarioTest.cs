using ezSpec.exception;
using ezSpec.keyword.step;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class RuntimeScenarioTest {

        [TestMethod]
        public void create_runtime_scenario_with_name_and_rule() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.AreEqual("name", runtimeScenario.Name);
        }

        [TestMethod]
        public void runtime_scenario_with_given() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.Given("given description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("Given", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("given description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_given_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.Given("given description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("Given", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("given description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_when() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.When("when description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("When", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("when description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_when_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.When("when description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("When", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("when description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.Then("then description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("Then", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.Then("then description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("Then", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_and() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.And("and description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("And", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("and description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_and_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.And("and description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("And", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("and description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_but() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.But("but description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("But", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("but description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);

        }

        [TestMethod]
        public void runtime_scenario_with_but_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.But("but description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("But", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("but description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_success() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenSuccess("then success description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenSuccess", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then success description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_success_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenSuccess("then success description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenSuccess", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then success description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_success_without_description() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenSuccess(Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenSuccess", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_success_without_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenSuccess(env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenSuccess", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_failure() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenFailure("then failure description", env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenFailure", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then failure description", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_failure_with_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenFailure("then failure description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenFailure", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("then failure description", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_failure_without_description() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenFailure(Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenFailure", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("", runtimeScenario.Steps[0].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_then_failure_without_continous() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario.ThenFailure(env => { });

            Assert.AreEqual(1, runtimeScenario.Steps.Count);
            Assert.AreEqual("ThenFailure", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("", runtimeScenario.Steps[0].Description);
            Assert.IsFalse(runtimeScenario.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_Scenario_with_steps() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given("given description", Step.ContinuousAfterFailure, env => { })
                .When("when description", Step.ContinuousAfterFailure, env => { })
                .Then("then description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(3, runtimeScenario.Steps.Count);
            Assert.AreEqual("Given", runtimeScenario.Steps[0].Name);
            Assert.AreEqual("When", runtimeScenario.Steps[1].Name);
            Assert.AreEqual("Then", runtimeScenario.Steps[2].Name);
            Assert.AreEqual("given description", runtimeScenario.Steps[0].Description);
            Assert.AreEqual("when description", runtimeScenario.Steps[1].Description);
            Assert.AreEqual("then description", runtimeScenario.Steps[2].Description);
            Assert.IsTrue(runtimeScenario.Steps[0].IsContinuousAfterFailure);
            Assert.IsTrue(runtimeScenario.Steps[1].IsContinuousAfterFailure);
            Assert.IsTrue(runtimeScenario.Steps[2].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void all_steps_use_the_same_scenario_environment() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given("two numbers ${100} and ${80}.", env => {
                    env.Put("number1", env.GetIntArg(0));
                    env.Put("number2", env.GetIntArg(1));
                })
                .When("I calculate the average of these two numbers.", env => {
                    int averange = (env.GetInt("number1") + env.GetInt("number2")) / 2;
                    env.Put("average", averange);
                })
                .Then("I get the answer equre to ${90}.", env => {
                    Assert.AreEqual(env.GetIntArg(0), env.GetInt("average"));
                })
                .Execute();
        }

        [TestMethod]
        public void steps_are_all_success() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given("two numbers ${100} and ${80}.", env => {
                    env.Put("number1", env.GetIntArg(0));
                    env.Put("number2", env.GetIntArg(1));
                })
                .When("I calculate the average of these two numbers.", env => {
                    int averange = (env.GetInt("number1") + env.GetInt("number2")) / 2;
                    env.Put("average", averange);
                })
                .Then("I get the answer equre to ${90}.", env => {
                    Assert.AreEqual(env.GetIntArg(0), env.GetInt("average"));
                })
                .Execute();

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
        }

        [TestMethod]
        public void step_terminate_after_failure() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step.", env => {
                    })
                    .When("I run when step and failed", Step.TerminateAfterFailure, env => {
                        Assert.AreEqual(1, 2);
                    })
                    .Then("this step should be skip", env => {
                    })
                    .Execute();
            });

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsFailure);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSkipped);
        }

        [TestMethod]
        public void step_continue_after_failure() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step.", env => {
                    })
                    .When("I run when step and failed", Step.ContinuousAfterFailure, env => {
                        Assert.AreEqual(1, 2);
                    })
                    .Then("this step should not be skip", env => {
                    })
                    .Execute();
            });

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsFailure);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
        }

        [TestMethod]
        public void step_pending() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given("a given step.", env => {
                })
                .When("I run when step and failed", env => {
                    PendingException.Pending("the step not done");
                })
                .Then("this step should not be skip", env => {
                })
                .Execute();

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsPending);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
        }

        [TestMethod]
        public void step_execute_concurrently_all_steps() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given("a given step.", env => {
                })
                .When("I run when step", env => {
                })
                .Then("this step should not be skip", env => {
                })
                .And("this step also should not be skip", env => {
                })
                .But("nothing", env => {
                })
                .ExecuteConcurrently();

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[3].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[4].Result.IsSuccess);
        }

        [TestMethod]
        public void step_execute_concurrently_and_terminate_after_failure() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");

            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step.", env => {
                    })
                    .When("I run when step and failed", Step.TerminateAfterFailure, env => {
                        Assert.AreEqual(1, 2);
                    })
                    .And("this step shoul not be skip", env => {
                    })
                    .Then("this step should be ship", env => {
                    })
                    .And("this step also should be skip", env => {
                    })
                    .ExecuteConcurrently();
            });

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsFailure);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[3].Result.IsSkipped);
            Assert.IsTrue(runtimeScenario.Steps[4].Result.IsSkipped);
        }

        [TestMethod]
        public void step_execute_concurrently_and_continue_after_failure() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");

            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step.", env => {
                    })
                    .When("I run when step and failed", Step.ContinuousAfterFailure, env => {
                        Assert.AreEqual(1, 2);
                    })
                    .And("this step shoul not be skip", env => {
                    })
                    .Then("this step should not be ship", env => {
                    })
                    .And("this step also should not be skip", env => {
                    })
                    .ExecuteConcurrently();
            });

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsFailure);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[3].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[4].Result.IsSuccess);
        }

        [TestMethod]
        public void step_execute_concurrently_with_pending() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");

            runtimeScenario
                .Given("a given step.", env => {
                })
                .When("I run when step and failed", env => {
                    PendingException.Pending("the step not done");
                })
                .And("this step shoul not be skip", env => {
                })
                .Then("this step should not be ship", env => {
                })
                .And("this step also should not be skip", env => {
                })
                .ExecuteConcurrently();

            Assert.IsTrue(runtimeScenario.Steps[0].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[1].Result.IsPending);
            Assert.IsTrue(runtimeScenario.Steps[2].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[3].Result.IsSuccess);
            Assert.IsTrue(runtimeScenario.Steps[4].Result.IsSuccess);
        }

        [TestMethod]
        public void step_description_contain_table() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given(@"
                    a table 
                    | name  |     path     | parent |
                    | users |    /users    |  null  |
                    | user1 | /users/user1 | users  |
                    | user2 | /users/user2 | users  |".AutoTrim(), env => {
                })
                .When("I try to get the argument", env => {
                    Assert.AreEqual("|\tusers\t|\t/users\t|\tnull\t|", env.Table.Rows[0].ToString());
                    Assert.AreEqual("|\tuser1\t|\t/users/user1\t|\tusers\t|", env.Table.Rows[1].ToString());
                    Assert.AreEqual("|\tuser2\t|\t/users/user2\t|\tusers\t|", env.Table.Rows[2].ToString());
                })
                .Then("done", env => {
                })
                .Execute();
        }

        [TestMethod]
        public void more_than_one_step_description_contain_table() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            runtimeScenario
                .Given(@"s
                    a table 
                    | name  |     path     | parent |
                    | users |    /users    |  null  |
                    | user1 | /users/user1 | users  |
                    | user2 | /users/user2 | users  |".AutoTrim(), env => {
                })
                .When("I try to get the argument", env => {
                    Assert.AreEqual("|\tusers\t|\t/users\t|\tnull\t|", env.Table.Rows[0].ToString());
                    Assert.AreEqual("|\tuser1\t|\t/users/user1\t|\tusers\t|", env.Table.Rows[1].ToString());
                    Assert.AreEqual("|\tuser2\t|\t/users/user2\t|\tusers\t|", env.Table.Rows[2].ToString());
                })
                .Then(@"
                    I given another table
                    |  id   |  name  | score |
                    | 10001 |  Joe   |  60   |
                    | 10002 | Calvin |  80   |
                    | 10003 | Howard |  100  |".AutoTrim(), env => {
                    Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", env.Table.Rows[0].ToString());
                    Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", env.Table.Rows[1].ToString());
                    Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", env.Table.Rows[2].ToString());
                })
                .Execute();
        }

        [TestMethod]
        public void runtime_scenario_without_steps_to_string() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");

            string except = "Scenario: name";
            Assert.AreEqual(except, runtimeScenario.ToString());
        }

        [TestMethod]
        public void runtime_scenario_to_string() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step", env => {
                    })
                    .When("I run when step", env => {
                        Assert.IsTrue(true);
                    })
                    .Then("this step failure", Step.ContinuousAfterFailure, env => {
                        Assert.IsTrue(false);
                    })
                    .And("this step success", env => {
                        Assert.IsTrue(true);
                    })
                    .But("nothing", env => {
                    })
                    .Execute();
            });

            string except =
                "Scenario: name\n" +
                "[Success] Given a given step\n" +
                "[Success] When I run when step\n" +
                "[Failure] Then this step failure\n" +
                "[Success] And this step success\n" +
                "[Success] But nothing";

            Assert.AreEqual(except, runtimeScenario.ToString());
        }

        [TestMethod]
        public void runtime_scenario_with_step_contain_more_than_one_line_description() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given("a given step line 1\na given step line 2", env => {
                    })
                    .When("I run when step line 1\nI run when step line 2", env => {
                        Assert.IsTrue(true);
                    })
                    .Then("this step failure line 1\nthis step failure line 2", Step.ContinuousAfterFailure, env => {
                        Assert.IsTrue(false);
                    })
                    .And("this step success line 1\nthis step success line 2", env => {
                        Assert.IsTrue(true);
                    })
                    .But("nothing line 1\nnothing line 2", env => {
                    })
                    .Execute();
            });

            string except =
                "Scenario: name\n" +
                "[Success] Given a given step line 1\n" +
                "          a given step line 2\n" +
                "[Success] When I run when step line 1\n" +
                "          I run when step line 2\n" +
                "[Failure] Then this step failure line 1\n" +
                "          this step failure line 2\n" +
                "[Success] And this step success line 1\n" +
                "          this step success line 2\n" +
                "[Success] But nothing line 1\n" +
                "          nothing line 2";

            Assert.AreEqual(except, runtimeScenario.ToString());
        }

        [TestMethod]
        public void runtime_scenario_with_step_contain_long_string_description() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.ThrowsException<EzSpecError>(() => {
                runtimeScenario
                    .Given(@"a given step line 1
                             a given step line 2".AutoTrim(), env => {
                    })
                    .When(@"I run when step line 1
                            I run when step line 2".AutoTrim(), env => {
                        Assert.IsTrue(true);
                    })
                    .Then(@"this step failure line 1
                            this step failure line 2".AutoTrim(), Step.ContinuousAfterFailure, env => {
                        Assert.IsTrue(false);
                    })
                    .And(@"this step success line 1
                            this step success line 2".AutoTrim(), env => {
                        Assert.IsTrue(true);
                    })
                    .But(@"nothing line 1
                            nothing line 2".AutoTrim(), env => {
                    })
                    .Execute();
            });

            string except =
                "Scenario: name\n" +
                "[Success] Given a given step line 1\n" +
                "          a given step line 2\n" +
                "[Success] When I run when step line 1\n" +
                "          I run when step line 2\n" +
                "[Failure] Then this step failure line 1\n" +
                "          this step failure line 2\n" +
                "[Success] And this step success line 1\n" +
                "          this step success line 2\n" +
                "[Success] But nothing line 1\n" +
                "          nothing line 2";

            Assert.AreEqual(except, runtimeScenario.ToString());
        }


    }
}
