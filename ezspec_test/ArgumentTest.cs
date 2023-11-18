using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.Test {

    [TestClass]
    public class ArgumentTest {

        [TestMethod]
        public void value_argument_start_with_dollar_sign() {
            Assert.AreEqual("1", Argument.Create("$1").Value);
            Assert.AreEqual("", Argument.Create("$1").Key);
            Assert.AreEqual("80", Argument.Create("$80").Value);
            Assert.AreEqual("\\$5", Argument.Create("$\\$5").Value);
            Assert.AreEqual("$5", Argument.Create("$$5").Value);
            Assert.AreEqual("$$5", Argument.Create("$$$5").Value);
            Assert.AreEqual("$$20$20", Argument.Create("$$$20$20").Value);
            Assert.AreEqual("vat:100", Argument.Create("$vat:100").Value);
            Assert.AreEqual("vat=100", Argument.Create("$vat=100").Value);
        }

        [TestMethod]
        public void key_value_argument_place_in_curly_brackets_with_equal_sign() {
            Assert.AreEqual("price", Argument.Create("${price=21,000}").Key);
            Assert.AreEqual("21,000", Argument.Create("${price=21,000}").Value);
            Assert.AreEqual("price", Argument.Create("${price=  21,000     }").Key);
            Assert.AreEqual("21,000", Argument.Create("${price=  21,000     }").Value);
            Assert.AreEqual("price", Argument.Create("${    price =21,000}").Key);
            Assert.AreEqual("21,000", Argument.Create("${    price =21,000}").Value);
            Assert.AreEqual("price", Argument.Create("${ price   =   21,000 }").Key);
            Assert.AreEqual("21,000", Argument.Create("${ price   =   21,000 }").Value);
            Assert.AreEqual("price", Argument.Create("${price=$21,000}").Key);
            Assert.AreEqual("$21,000", Argument.Create("${price=$21,000}").Value);
        }

        [TestMethod]
        public void key_value_argument_place_in_curly_brackets_with_colon () {
            Assert.AreEqual("price", Argument.Create("${price:21,000}").Key);
            Assert.AreEqual("21,000", Argument.Create("${price:21,000}").Value);
            Assert.AreEqual("price", Argument.Create("${price:  21,000     }").Key);
            Assert.AreEqual("21,000", Argument.Create("${price:  21,000     }").Value);
            Assert.AreEqual("price", Argument.Create("${    price :21,000}").Key);
            Assert.AreEqual("21,000", Argument.Create("${    price :21,000}").Value);
            Assert.AreEqual("price", Argument.Create("${ price   :   21,000 }").Key);
            Assert.AreEqual("21,000", Argument.Create("${ price   :   21,000 }").Value);
            Assert.AreEqual("price", Argument.Create("${price:$21,000}").Key);
            Assert.AreEqual("$21,000", Argument.Create("${price:$21,000}").Value);
        }

    }

}
