using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.table.test {

    [TestClass]
    public class TableTest
    {
        private static Header header;
        private static List<Row> rows;

        [ClassInitialize]
        public static void BeforeAll(TestContext context) {
            List<string> headerData = new List<string>() {
                "id", "name", "score"
            };
            header = Header.New(headerData);

            rows = new List<Row>();
            rows.Add(new Row(header, new List<string>() { "10001", "Joe", "60" }));
            rows.Add(new Row(header, new List<string>() { "10002", "Calvin", "80" }));
            rows.Add(new Row(header, new List<string>() { "10003", "Howard", "100" }));
        }

        [TestMethod]
        public void create_table() {
            Table table = new Table();

            Assert.AreEqual(0, table.Header.Size);
            Assert.AreEqual(0, table.Rows.Count);
        }

        [TestMethod]
        public void create_table_with_header() {
            Table table = new Table(header);

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual("id", table.Header.Get(0));
            Assert.AreEqual("name", table.Header.Get(1));
            Assert.AreEqual("score", table.Header.Get(2));
            Assert.AreEqual(0, table.Rows.Count);
        }

        [TestMethod]
        public void create_table_with_header_and_rows() {
            Table table = new Table(header, rows);

            List<int> beautify = new List<int>() {
                7, 8, 5
            };

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual(3, table.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60  |", table.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80  |", table.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100 |", table.Rows[2].ToString(beautify));
        }

        [TestMethod]
        public void create_table_from_raw_data() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Table table = new Table(rawData);

            List<int> beautify = new List<int>() {
                7, 8, 7
            };

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual("| id    | name   | score |", table.Header.ToString(beautify));
            Assert.AreEqual(3, table.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60    |", table.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80    |", table.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100   |", table.Rows[2].ToString(beautify));
        }
    }
}
