using Microsoft.VisualStudio.TestTools.UnitTesting;
using ezspec_csharp;

namespace ezspec_csharp_test {

    [TestClass]
    public class RuleTest {

        [TestMethod]
        public void create_rule_with_name() {
            Rule rule = Rule.New("name");

            Assert.AreEqual("name", rule.Name);
            Assert.AreEqual("", rule.Description);
        }

        [TestMethod]
        public void create_rule_with_name_and_description() {
            Rule rule = Rule.New("name", "description");

            Assert.AreEqual("name", rule.Name);
            Assert.AreEqual("description", rule.Description);
        }

        [TestMethod]
        public void get_rule_string() {
            Rule rule = Rule.New("name", "description");

            Assert.AreEqual("Rule: name\ndescription", rule.ToString());
        }

        [TestMethod]
        public void get_rule_string_without_description() {
            Rule rule = Rule.New("name");

            Assert.AreEqual("Rule: name", rule.ToString());
        }

    }

}
