using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {
    [TestClass]
    public class BackgroundTest {
        [TestMethod]
        public void get_background_string() {
            Background background = Background.New("background name");
            background.Given("given step", env => { })
                .And("and step", env => { });

            string expect =
                "Background: background name\n" +
                "Given given step\n" +
                "And and step";
            Assert.AreEqual(expect, background.ToString());
        }
    }
}
