using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.table.test {

    [TestClass]
    public class RowTest {

        private static Header header;

        [ClassInitialize]
        public static void BeforeAll(TestContext context) {
            List<string> data = new List<string>() {
                "column1", "column2"
            };

            header = Header.Create(data);
        }

        [TestMethod]
        public void create_row() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);
            IList<string> columns = row.Columns;

            Assert.AreEqual(2, columns.Count);
            Assert.AreEqual("data1", columns[0]);
            Assert.AreEqual("data2", columns[1]);
        }

        [TestMethod]
        public void get_column_by_index() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("data1", row.Get(0));
            Assert.AreEqual("data2", row.Get(1));
        }

        [TestMethod]
        public void get_column_by_name() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("data1", row.Get("column1"));
            Assert.AreEqual("data2", row.Get("column2"));
        }

        [TestMethod]
        public void get_column_by_name_not_exist() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.ThrowsException<SystemException>(() => {
                row.Get("columnNotExist");
            });
        }

        [TestMethod]
        public void get_column_or_empty_by_name() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("data1", row.GetOrEmpty("column1"));
            Assert.AreEqual("data2", row.GetOrEmpty("column2"));
        }

        [TestMethod]
        public void get_column_or_empty_by_name_not_exist() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("", row.GetOrEmpty("columnNotExist"));
        }

        [TestMethod]
        public void to_string() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("|\tdata1\t|\tdata2\t|", row.ToString());
        }

        [TestMethod]
        public void to_string_beautify() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            List<int> columnsLength = new List<int>() {
                10, 15
            };

            Assert.AreEqual("| data1    | data2         |", row.ToString(columnsLength));

        }
    }
}
