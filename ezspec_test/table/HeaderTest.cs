using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.table.Test {

    [TestClass]
    public class HeaderTest {

        [TestMethod]
        public void create_empty_header() {
            Header header = Header.New();

            Assert.AreEqual(0, header.Size);
        }

        [TestMethod]
        public void create_header_with_data() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };

            Header header = Header.New(data);

            Assert.AreEqual(2, header.Size);
            Assert.AreEqual("column1", header.Get(0));
            Assert.AreEqual("column2", header.Get(1));
        }

        [TestMethod]
        public void create_header_with_header() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);
            Header headerCopy = Header.New(header);

            data[0] = "column";
            Assert.AreEqual("column1", headerCopy.Get(0));
        }

        [TestMethod]
        public void get_header() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);

            IList<string> headerData = header.ColumnNames;

            Assert.AreEqual(2, headerData.Count);
            Assert.AreEqual("column1", headerData[0]);
            Assert.AreEqual("column2", headerData[1]);
        }

        [TestMethod]
        public void clear_header() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);
            Assert.AreEqual(2, header.Size);

            header.Clear();

            Assert.AreEqual(0, header.Size);
        }

        [TestMethod]
        public void reset_header() {
            Header header = Header.New();
            Assert.AreEqual(0, header.Size);

            List<string> data = new List<string>() {
                "column1", "column2"
            };
            header.Reset(data);

            Assert.AreEqual(2, header.Size);
            Assert.AreEqual("column1", header.Get(0));
            Assert.AreEqual("column2", header.Get(1));
        }

        [TestMethod]
        public void header_to_string() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);

            string headerStr = header.ToString();

            Assert.AreEqual("|\tcolumn1\t|\tcolumn2\t|", headerStr);
        }

        [TestMethod]
        public void empty_header_to_string() {
            List<string> data = new List<string>() { };
            Header header = Header.New(data);

            string headerStr = header.ToString();

            Assert.AreEqual("||", headerStr);
        }

        [TestMethod]
        public void header_to_string_beautify() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);

            List<int> columnsLength = new List<int>() {
                10, 15
            };
            string headerStr = header.ToString(columnsLength);

            Assert.AreEqual("| column1  | column2       |", headerStr);
        }

        [TestMethod]
        public void empty_header_to_string_beautify() {
            List<string> data = new List<string>() { };
            Header header = Header.New(data);

            List<int> columnsLength = new List<int> { };
            string headerStr = header.ToString(columnsLength);

            Assert.AreEqual("||", headerStr);
        }

        [TestMethod]
        public void header_to_string_with_wrong_amount_of_column() {
            List<string> data = new List<string>() {
                "column1", "column2"
            };
            Header header = Header.New(data);

            List<int> columnsLength = new List<int>() {
                10
            };
            Assert.ThrowsException<SystemException>(() => header.ToString(columnsLength));
        }
    }
}
