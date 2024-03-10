using ezSpec.keyword;
using ezSpec.keyword.table;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.Test {

    [TestClass]
    public class ExamplesTest {

        [TestMethod]
        public void create_examples() {
            Table table = Table.New(@"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |");

            Examples examples = Examples.New("examplesName", "examplesDescription", table);

            Assert.AreEqual("examplesName", examples.Name);
            Assert.AreEqual("examplesDescription", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_without_description() {
            Table table = Table.New(@"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |");

            Examples examples = Examples.New("examplesName", table);

            Assert.AreEqual("examplesName", examples.Name);
            Assert.AreEqual("", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_with_only_table() {
            Table table = Table.New(@"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |");

            Examples examples = Examples.New(table);

            Assert.AreEqual("", examples.Name);
            Assert.AreEqual("", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_with_raw_table() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("examplesName", "examplesDescription", rawData);

            Assert.AreEqual("examplesName", examples.Name);
            Assert.AreEqual("examplesDescription", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_with_raw_table_without_description() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("examplesName", rawData);

            Assert.AreEqual("examplesName", examples.Name);
            Assert.AreEqual("", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_with_only_raw_table() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New(rawData);

            Assert.AreEqual("", examples.Name);
            Assert.AreEqual("", examples.Description);
            Assert.AreEqual(3, examples.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examples.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examples.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examples.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void create_examples_with_examples() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("examplesName", "examplesDescription", rawData);
            Examples examplesCopy = Examples.New(examples);

            Assert.AreEqual("examplesName", examplesCopy.Name);
            Assert.AreEqual("examplesDescription", examplesCopy.Description);
            Assert.AreEqual(3, examplesCopy.ExampleSet.Count);
            Assert.AreEqual("|\t10001\t|\tJoe\t|\t60\t|", examplesCopy.ExampleSet[0].ToString());
            Assert.AreEqual("|\t10002\t|\tCalvin\t|\t80\t|", examplesCopy.ExampleSet[1].ToString());
            Assert.AreEqual("|\t10003\t|\tHoward\t|\t100\t|", examplesCopy.ExampleSet[2].ToString());
        }

        [TestMethod]
        public void examples_to_string() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("examplesName", "examplesDescription", rawData);

            string expect =
                "Examples: examplesName\n" +
                "\texamplesDescription\n" +
                "\t| id    | name   | score |\n" +
                "\t| 10001 | Joe    | 60    |\n" +
                "\t| 10002 | Calvin | 80    |\n" +
                "\t| 10003 | Howard | 100   |";

            Assert.AreEqual(expect, examples.ToString());
        }

        [TestMethod]
        public void examples_to_string_without_description() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("examplesName", "", rawData);

            string expect =
                "Examples: examplesName\n" +
                "\t| id    | name   | score |\n" +
                "\t| 10001 | Joe    | 60    |\n" +
                "\t| 10002 | Calvin | 80    |\n" +
                "\t| 10003 | Howard | 100   |";

            Assert.AreEqual(expect, examples.ToString());
        }

        [TestMethod]
        public void examples_to_string_with_only_raw_table() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Examples examples = Examples.New("", "", rawData);

            string expect =
                "Examples: \n" +
                "\t| id    | name   | score |\n" +
                "\t| 10001 | Joe    | 60    |\n" +
                "\t| 10002 | Calvin | 80    |\n" +
                "\t| 10003 | Howard | 100   |";

            Assert.AreEqual(expect, examples.ToString());
        }
    }
}
