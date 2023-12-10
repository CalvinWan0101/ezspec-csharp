using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ezSpec.Test {

    [TestClass]
    public class StepTest {

        [TestMethod]
        public void create_step() {
            Step step = Step.New("description", env => { });
            Assert.AreEqual("description", step.Description);
            Assert.IsFalse(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void create_step_with_continous() {
            Step step = Step.New("description", Step.ContinuousAfterFailure, env => { });
            Assert.AreEqual("description", step.Description);
            Assert.IsTrue(step.IsContinuousAfterFailure);
        }

        [TestMethod]
        public void invoke_step_callback() {
            int notifyCount = 0;
            Step step = Step.New("description", env => {
                notifyCount++;
            });

            var callback = step.GetType().GetProperty("Callback", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(step, null) as Step.StepCallback;
            callback?.Invoke(null);

            Assert.AreEqual(1, notifyCount);
        }

        [TestMethod]
        public void description_with_dollar_sign_argument() {
            Step step = Step.New("With argument ${123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_equal_sign_argument_in_curly_brackets() {
            Step step = Step.New("With argument ${arg=123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_colon_argument_in_curly_brackets() {
            Step step = Step.New("With argument ${arg:123}", env => { });

            List<Argument> arguments = step.Arguments;

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("arg", arguments[0].Key);
            Assert.AreEqual("123", arguments[0].Value);
        }

        [TestMethod]
        public void description_with_many_arguments() {
            Step step = Step.New("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", env => { });

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
            Step step = Step.New("description", env => { });

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
            Step step = Step.New("With arrguments ${arg1:abc}, ${$210}, and ${arg2=123}", env => { });

            Assert.AreEqual("With arrguments abc, $210, and 123", step.EraseReversedWords);
        }
    }
}
