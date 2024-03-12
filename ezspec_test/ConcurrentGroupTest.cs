using ezSpec.keyword.step;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {
    [TestClass]
    public class ConcurrentGroupTest {
        [TestMethod]
        public void slice_concurrent_group() {
            List<Step> steps = new List<Step>();
            steps.Add(new Given("given1", Step.TerminateAfterFailure, env => { }));
            steps.Add(new And("and1-1", Step.TerminateAfterFailure, env => { }));
            steps.Add(new Given("given2", Step.TerminateAfterFailure, env => { }));
            steps.Add(new And("and2-1", Step.TerminateAfterFailure, env => { }));
            steps.Add(new And("and2-2", Step.TerminateAfterFailure, env => { }));
            
            List<ConcurrentGroup> concurrentGroups = ConcurrentGroup.SliceConcurrentGroups(steps);
            
            Assert.AreEqual(2, concurrentGroups.Count);
            Assert.AreEqual(2, concurrentGroups[0].Steps.Count);
            Assert.AreEqual("given1", concurrentGroups[0].Steps[0].Description);
            Assert.AreEqual("and1-1", concurrentGroups[0].Steps[1].Description);
            Assert.AreEqual(3, concurrentGroups[1].Steps.Count);
            Assert.AreEqual("given2", concurrentGroups[1].Steps[0].Description);
            Assert.AreEqual("and2-1", concurrentGroups[1].Steps[1].Description);
            Assert.AreEqual("and2-2", concurrentGroups[1].Steps[2].Description);
        }
    }
}