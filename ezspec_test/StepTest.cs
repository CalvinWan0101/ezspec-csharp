using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.Test {

    [TestClass]
    public class StepTest {

        [TestMethod]
        public void create_step() {
            Step step = new Step("description", () => {});
            Assert.AreEqual("description", step.Description);
            Assert.IsFalse(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void create_step_with_continous() {
            Step step = new Step("description", Step.ContinuousAfterFailure, () => {});
            Assert.AreEqual("description", step.Description);
            Assert.IsTrue(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void invoke_step_callback() {
            int notifyCount = 0;
            Step step = new Step("description", () => {
                notifyCount++;
            });

            step.Callback();

            Assert.AreEqual(1, notifyCount);
        }

        [TestMethod]
        public void description_with_dollar_sign_argument() {
            Step step = new Step("With argument $123", () => {});

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_equal_sign_argument_in_curly_brackets() {
            Step step = new Step("With argument ${arg=123}", () => {});

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_colon_argument_in_curly_brackets() {
            Step step = new Step("With argument ${arg:123}", () => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_many_arguments() {
            Step step = new Step("With arrguments ${arg1:abc}, $$210 , and ${arg2=123}", () => {});

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(3, arguments.Count);
            Assert.AreEqual("arg1", arguments[0].Key);
            Assert.AreEqual("abc", arguments[0].Value);
            Assert.AreEqual("", arguments[1].Key);
            Assert.AreEqual("$210", arguments[1].Value);
            Assert.AreEqual("arg2", arguments[2].Key);
            Assert.AreEqual("123", arguments[2].Value);
        }
    }
}
