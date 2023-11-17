using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezspec_csharp_test {

    [TestClass]
    public class FeatureTest {

        [TestMethod]
        public void create_feature_with_name_and_description() {
            Feature feature = Feature.New("name", "description");

            Assert.AreEqual("name", feature.Name);
            Assert.AreEqual("description", feature.Description);
        }

        [TestMethod]
        public void create_feature_with_name() {
            Feature feature = Feature.New("name");

            Assert.AreEqual("name", feature.Name);
            Assert.AreEqual("", feature.Description);
        }

        [TestMethod]
        public void get_feature_text() {
            Feature feature = Feature.New("name", "description");
            Assert.AreEqual("Feature: name\n\ndescription", feature.FeatureText);
        }

        [TestMethod]
        public void get_feature_text_without_description() {
            Feature feature = Feature.New("name");
            Assert.AreEqual("Feature: name", feature.FeatureText);
        }

        [TestMethod]
        public void get_feature_string() {
            Feature feature = Feature.New("name", "description");
            Assert.AreEqual("Feature: name\n\ndescription", feature.ToString());
        }
    }

}
