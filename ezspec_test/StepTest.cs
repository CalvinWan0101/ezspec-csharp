using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ezSpec.keyword.step.Test {

    [TestClass]
    public class StepTest {

        [TestMethod]
        public void create_step() {
            Given given = new Given("description", false, env => { });
            Assert.AreEqual("description", given.Description);
            Assert.IsFalse(given.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void create_step_with_continous() {
            Given given = new Given("description", Step.ContinuousAfterFailure, env => { });
            Assert.AreEqual("description", given.Description);
            Assert.IsTrue(given.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void invoke_step_callback() {
            int notifyCount = 0;
            Given given = new Given("description", false, env => {
                notifyCount++;
            });

            var callback = given.GetType().GetProperty("Callback", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(given, null) as Step.StepCallback;
            callback?.Invoke(null);

            Assert.AreEqual(1, notifyCount);
        }

        [TestMethod]
        public void description_with_dollar_sign_argument() {
            Given given = new Given("With argument ${123}", false, env => { });

            List<Argument> arguments = given.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_equal_sign_argument_in_curly_brackets() {
            Given given = new Given("With argument ${arg=123}", false, env => { });

            List<Argument> arguments = given.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_colon_argument_in_curly_brackets() {
            Given given = new Given("With argument ${arg:123}", false, env => { });

            List<Argument> arguments = given.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_many_arguments() {
            Given given = new Given("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", false, env => { });

            List<Argument> arguments = given.Arguments;

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
            Given given = new Given("description", false, env => { });

            Result successExpect = Result.Success();

            given.Result = successExpect;
            Assert.AreEqual(successExpect, given.Result);

            Exception e = new Exception();
            Result pendingExpect = Result.Pending(e);
            given.Result = pendingExpect;
            Assert.AreEqual(pendingExpect, given.Result);
        }

        [TestMethod]
        public void erase_reversed_words() {
            Given given = new Given("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", false, env => { });

            Assert.AreEqual("With arrguments abc, $210, and 123", given.EraseReversedWords);
        }

        [TestMethod]
        public void get_name() {
            Given given = new Given("description", false, env => { });

            Assert.AreEqual("Given", given.Name);
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
    }
}
