using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

}
