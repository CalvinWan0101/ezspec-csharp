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

            header = Header.New(data);
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
        public void create_row_with_row() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);
            Row rowCopy = new Row(row);

            Assert.AreEqual(2, rowCopy.Columns.Count);
            Assert.AreEqual(row.Columns[0], rowCopy.Columns[0]);
            Assert.AreEqual(row.Columns[1], rowCopy.Columns[1]);
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
        public void row_to_string() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            Assert.AreEqual("|\tdata1\t|\tdata2\t|", row.ToString());
        }

        [TestMethod]
        public void empty_row_to_string() {
            List<string> rowData = new List<string>() {};
            Row row = new Row(header, rowData);

            Assert.AreEqual("||", row.ToString());
        }

        [TestMethod]
        public void row_to_string_beautify() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            List<int> columnsLength = new List<int>() {
                10, 15
            };

            Assert.AreEqual("| data1    | data2         |", row.ToString(columnsLength));
        }

        [TestMethod]
        public void empty_row_to_string_beautify() {
            List<string> rowData = new List<string>() {};
            Row row = new Row(header, rowData);

            List<int> columnsLength = new List<int>() {};

            Assert.AreEqual("||", row.ToString(columnsLength));
        }

        [TestMethod]
        public void row_to_string_with_wrong_amount_of_column() {
            List<string> rowData = new List<string>() {
                "data1", "data2"
            };
            Row row = new Row(header, rowData);

            List<int> columnsLength = new List<int>() {
                10
            };

            Assert.ThrowsException<SystemException>(() => row.ToString(columnsLength));
        }
    }
}
