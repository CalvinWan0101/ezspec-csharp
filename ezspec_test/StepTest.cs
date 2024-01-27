using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ezSpec.keyword.step.Test {

    [TestClass]
    public class StepTest {

        [TestMethod]
        public void create_step() {
            Step step = TestStep.New("description", env => { });
            Assert.AreEqual("description", step.Description);
            Assert.IsFalse(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void create_step_with_continous() {
            Step step = TestStep.New("description", Step.ContinuousAfterFailure, env => { });
            Assert.AreEqual("description", step.Description);
            Assert.IsTrue(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void invoke_step_callback() {
            int notifyCount = 0;
            Step step = TestStep.New("description", env => {
                notifyCount++;
            });

            var callback = step.GetType().GetProperty("Callback", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(step, null) as Step.StepCallback;
            callback?.Invoke(null);

            Assert.AreEqual(1, notifyCount);
        }

        [TestMethod]
        public void description_with_dollar_sign_argument() {
            Step step = TestStep.New("With argument ${123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_equal_sign_argument_in_curly_brackets() {
            Step step = TestStep.New("With argument ${arg=123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_colon_argument_in_curly_brackets() {
            Step step = TestStep.New("With argument ${arg:123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_many_arguments() {
            Step step = TestStep.New("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(3, arguments.Count);
            Assert.AreEqual("arg1", arguments[0].Key);
            Assert.AreEqual("abc", arguments[0].Value);
            Assert.AreEqual("", arguments[1].Key);
            Assert.AreEqual("$210", arguments[1].Value);
            Assert.AreEqual("arg2", arguments[2].Key);
            Assert.AreEqual("123", arguments[2].Value);
        }

        [TestMethod]
        public void set_and_get_result() {
            Step step = TestStep.New("description", env => { });

            Result successExpect = Result.Success();

            step.Result = successExpect;
            Assert.AreEqual(successExpect, step.Result);

            Exception e = new Exception();
            Result pendingExpect = Result.Pending(e);
            step.Result = pendingExpect;
            Assert.AreEqual(pendingExpect, step.Result);
        }

        [TestMethod]
        public void erase_reversed_words() {
            Step step = TestStep.New("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", env => { });

            Assert.AreEqual("With arrguments abc, $210, and 123", step.EraseReversedWords);
        }

        [TestMethod]
        public void get_name() {
            Step step = TestStep.New("description", env => { });

            Assert.AreEqual("TestStep", step.Name);
        }

        [TestMethod]
        public void step_keywords() {
            Step given = new Given("description", false, env => { });
            Step when = new When("description", false, env => { });
            Step then = new Then("description", false, env => { });
            Step and = new And("description", false, env => { });
            Step but = new But("description", false, env => { });
            Step thenSuccess = new ThenSuccess("description", false, env => { });
            Step thenFailure = new ThenFailure("description", false, env => { });

            Assert.AreEqual(given.Name, "Given");
            Assert.AreEqual(when.Name, "When");
            Assert.AreEqual(then.Name, "Then");
            Assert.AreEqual(and.Name, "And");
            Assert.AreEqual(but.Name, "But");
            Assert.AreEqual(thenSuccess.Name, "ThenSuccess");
            Assert.AreEqual(thenFailure.Name, "ThenFailure");
        }

        [TestMethod]
        public void step_to_string() {
            Step given = new Given("give description", false, env => { });
            Step when = new When("when description", false, env => { });
            Step then = new Then("then description", false, env => { });
            Step and = new And("and description", false, env => { });
            Step but = new But("but description", false, env => { });
            Step thenSuccess = new ThenSuccess("then success description", false, env => { });
            Step thenFailure = new ThenFailure("then failure description", false, env => { });

            Assert.AreEqual("[Pending] Given give description", given.ToString());
            Assert.AreEqual("[Pending] When when description", when.ToString());
            Assert.AreEqual("[Pending] Then then description", then.ToString());
            Assert.AreEqual("[Pending] And and description", and.ToString());
            Assert.AreEqual("[Pending] But but description", but.ToString());
            Assert.AreEqual("[Pending] ThenSuccess then success description", thenSuccess.ToString());
            Assert.AreEqual("[Pending] ThenFailure then failure description", thenFailure.ToString());
        }

        [TestMethod]
        public void step_contains_more_than_one_line_description() {
            Step given = new Given("give description line 1\ngive description line 2", false, env => { });
            Step when = new When("when description line 1\nwhen description line 2", false, env => { });
            Step then = new Then("then description line 1\nthen description line 2", false, env => { });
            Step and = new And("and description line 1\nand description line 2", false, env => { });
            Step but = new But("but description line 1\nbut description line 2", false, env => { });
            Step thenSuccess = new ThenSuccess("then success description line 1\nthen success description line 1", false, env => { });
            Step thenFailure = new ThenFailure("then failure description line 1\nthen failure description line 2", false, env => { });

            Assert.AreEqual("[Pending] Given give description line 1\n          give description line 2", given.ToString());
            Assert.AreEqual("[Pending] When when description line 1\n          when description line 2", when.ToString());
            Assert.AreEqual("[Pending] Then then description line 1\n          then description line 2", then.ToString());
            Assert.AreEqual("[Pending] And and description line 1\n          and description line 2", and.ToString());
            Assert.AreEqual("[Pending] But but description line 1\n          but description line 2", but.ToString());
            Assert.AreEqual("[Pending] ThenSuccess then success description line 1\n          then success description line 1", thenSuccess.ToString());
            Assert.AreEqual("[Pending] ThenFailure then failure description line 1\n          then failure description line 2", thenFailure.ToString());
        }

        private class TestStep : Step {
            public override string Name {
                get { return "TestStep"; }
            }

            public TestStep(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
            }

            public static TestStep New(string description, StepCallback callback) {
                return new TestStep(description, TerminateAfterFailure, callback);
            }

            public static TestStep New(string description, bool continuous, StepCallback callback) {
                return new TestStep(description, continuous, callback);
            }
        }
    }
}
