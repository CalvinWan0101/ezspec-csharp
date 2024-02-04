using ezSpec.exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class ResultTest {

        [TestMethod]
        public void execute_success() {
            Result result = Result.Success();

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(StepExecutionOutcome.Success, result.Outcome);
            Assert.AreEqual("Success", result.ToString());
        }

        [TestMethod]
        public void execute_pending() {
            Exception exception = PendingException.New();
            Result result = Result.Pending(exception);

            Assert.IsTrue(result.IsPending);
            Assert.AreEqual(exception, result.Exception);
            Assert.AreEqual(StepExecutionOutcome.Pending, result.Outcome);
            Assert.AreEqual("Pending", result.ToString());
        }

        [TestMethod]
        public void execute_failure() {
            Exception exception = new Exception();
            Result result = Result.Failure(exception);

            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(exception, result.Exception);
            Assert.AreEqual(StepExecutionOutcome.Failure, result.Outcome);
            Assert.AreEqual("Failure", result.ToString());
        }

        [TestMethod]
        public void execute_skipped() {
            Result result = Result.Skipped();

            Assert.IsTrue(result.IsSkipped);
            Assert.IsNull(result.Exception);
            Assert.AreEqual(StepExecutionOutcome.Skipped, result.Outcome);
            Assert.AreEqual("Skipped", result.ToString());
        }

        [TestMethod]
        public void get_failure_exception_message() {
            Exception exception = new Exception();
            try {
                var list = new List<int>();
                int val = list[0];
            } catch(Exception e) {
                exception = e;
            }

            Result result = Result.Failure(exception);

            string except =
                "   at System.Collections.Generic.List`1.get_Item(Int32 index)\n" +
                "   at ezSpec.keyword.Test.ResultTest.get_failure_exception_message() in ezspec_test/ResultTest.cs:line 56";
            Assert.AreEqual(except, result.ExceptionMessage);
        }
    }
}
