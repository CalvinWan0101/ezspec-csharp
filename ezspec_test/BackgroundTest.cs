using System.Reflection;
using ezSpec.exception;
using ezSpec.keyword.step;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {
    [TestClass]
    public class BackgroundTest {
        [TestMethod]
        public void runtime_scenario_with_given() {
            Background background = Background.New("background name");
            background.Given("given description", env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("Given", background.Steps[0].Name);
            Assert.AreEqual("given description", background.Steps[0].Description);
            Assert.IsFalse(background.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_given_with_continous() {
            Background background = Background.New("background name");
            background.Given("given description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("Given", background.Steps[0].Name);
            Assert.AreEqual("given description", background.Steps[0].Description);
            Assert.IsTrue(background.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_and() {
            Background background = Background.New("background name");
            background.And("and description", env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("And", background.Steps[0].Name);
            Assert.AreEqual("and description", background.Steps[0].Description);
            Assert.IsFalse(background.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_and_with_continous() {
            Background background = Background.New("background name");
            background.And("and description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("And", background.Steps[0].Name);
            Assert.AreEqual("and description", background.Steps[0].Description);
            Assert.IsTrue(background.Steps[0].IsContinuousAfterFailure);
        }

        [TestMethod]
        public void runtime_scenario_with_but() {
            Background background = Background.New("background name");
            background.But("but description", env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("But", background.Steps[0].Name);
            Assert.AreEqual("but description", background.Steps[0].Description);
            Assert.IsFalse(background.Steps[0].IsContinuousAfterFailure);

        }

        [TestMethod]
        public void runtime_scenario_with_but_with_continous() {
            Background background = Background.New("background name");
            background.But("but description", Step.ContinuousAfterFailure, env => { });

            Assert.AreEqual(1, background.Steps.Count);
            Assert.AreEqual("But", background.Steps[0].Name);
            Assert.AreEqual("but description", background.Steps[0].Description);
            Assert.IsTrue(background.Steps[0].IsContinuousAfterFailure);
        }
        
        [TestMethod]
        public void get_background_executed_success_status() {
            Background background = Background.New("background name");
            
            var environmentProperty = background.GetType().GetProperty("Environment", BindingFlags.Instance | BindingFlags.NonPublic);
            environmentProperty?.SetValue(background,  ScenarioEnvironment.New());
            
            background.Given("given step", env => { })
                .And("and step", env => { })
                .Execute();

            Assert.IsTrue(background.IsExecuteSuccess);
        }
        
        [TestMethod]
        public void get_background_executed_failure_status() {
            Background background = Background.New("background name");
            
            var environmentProperty = background.GetType().GetProperty("Environment", BindingFlags.Instance | BindingFlags.NonPublic);
            environmentProperty?.SetValue(background,  ScenarioEnvironment.New());

            Assert.ThrowsException<EzSpecError>(() => {
                background.Given("given step", env => { })
                    .And("and a failure step", env => {
                        Assert.IsTrue(false);
                    })
                    .Execute();
            });
            Assert.IsFalse(background.IsExecuteSuccess);
            
        }

        [TestMethod]
        public void get_background_string() {
            Background background = Background.New("background name");
            
            var environmentProperty = background.GetType().GetProperty("Environment", BindingFlags.Instance | BindingFlags.NonPublic);
            environmentProperty?.SetValue(background,  ScenarioEnvironment.New());
            
            Assert.ThrowsException<EzSpecError>(() => {
                background.Given("given step", env => { })
                    .And("and a success step", env => { })
                    .And("and a failure step", env => {
                        Assert.IsTrue(false);
                    })
                    .And("this step should be skipped", env => { })
                    .Execute();
            });
            
            string expect =
                "Background: background name\n" +
                "\t[Success] Given given step\n" +
                "\t[Success] And and a success step\n" +
                "\t[Failure] And and a failure step\n" +
                "\t[Skipped] And this step should be skipped";
            Assert.AreEqual(expect, background.ToString());
        }
    }
}
