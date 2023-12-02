using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.Test {

    [TestClass]
    public class RuntimeScenarioTest {

        [TestMethod]
        public void create_runtime_scenario_with_name_and_rule() {
            RuntimeScenario runtimeScenario = RuntimeScenario.New("name");
            Assert.AreEqual("name", runtimeScenario.Name);
        }

        //[TestMethod]
        //public void runtime_scenario_with_given() {
        //    RuntimeScenario runtimeScenario = new RuntimeScenario("name");
        //    runtimeScenario.Given("given description", () => { });
        //    Assert.AreEqual("given description", runtimeScenario.Steps.Description);
        //}
    }
}
