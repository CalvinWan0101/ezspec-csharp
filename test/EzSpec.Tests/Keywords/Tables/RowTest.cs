using EzSpec.Keywords.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzSpec.Tests.Keywords.Tables;

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
        Row row = Row.New(header, rowData);
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
        Row row = Row.New(header, rowData);
        Row rowCopy = Row.New(row);

        Assert.AreEqual(2, rowCopy.Columns.Count);
        Assert.AreEqual(row.Columns[0], rowCopy.Columns[0]);
        Assert.AreEqual(row.Columns[1], rowCopy.Columns[1]);
    }

    [TestMethod]
    public void get_column_by_index() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("data1", row.Get(0));
        Assert.AreEqual("data2", row.Get(1));
    }

    [TestMethod]
    public void get_column_by_name() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("data1", row.Get("column1"));
        Assert.AreEqual("data2", row.Get("column2"));
    }

    [TestMethod]
    public void get_column_by_name_not_exist() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.ThrowsExactly<SystemException>(() => {
            row.Get("columnNotExist");
        });
    }

    [TestMethod]
    public void get_column_or_empty_by_name() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("data1", row.GetOrEmpty("column1"));
        Assert.AreEqual("data2", row.GetOrEmpty("column2"));
    }

    [TestMethod]
    public void get_column_or_empty_by_name_not_exist() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("", row.GetOrEmpty("columnNotExist"));
    }


    [TestMethod]
    public void row_to_string() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("|\tdata1\t|\tdata2\t|", row.ToString());
    }

    [TestMethod]
    [DataRow("|")]
    [DataRow("| name |\n| Joe |")]
    public void table_like_cell_is_plain_text(string value) {
        Row row = Row.New(header, new List<string>() { value, "data2" });

        Assert.AreEqual(value, row.Get(0));
        Assert.AreEqual(value, row.Get("column1"));
        Assert.AreEqual($"|\t{value}\t|\tdata2\t|", row.ToString());
        Assert.AreEqual($"| {value} | data2 |", row.ToString(new List<int>() { value.Length + 2, 7 }));
    }

    [TestMethod]
    public void empty_row_to_string() {
        List<string> rowData = new List<string>() { };
        Row row = Row.New(header, rowData);

        Assert.AreEqual("||", row.ToString());
    }

    [TestMethod]
    public void row_to_string_beautify() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        List<int> columnsLength = new List<int>() {
            10, 15
        };

        Assert.AreEqual("| data1    | data2         |", row.ToString(columnsLength));
    }

    [TestMethod]
    public void empty_row_to_string_beautify() {
        List<string> rowData = new List<string>() { };
        Row row = Row.New(header, rowData);

        List<int> columnsLength = new List<int>() { };

        Assert.AreEqual("||", row.ToString(columnsLength));
    }

    [TestMethod]
    public void row_to_string_with_wrong_amount_of_column() {
        List<string> rowData = new List<string>() {
            "data1", "data2"
        };
        Row row = Row.New(header, rowData);

        List<int> columnsLength = new List<int>() {
            10
        };

        Assert.ThrowsExactly<SystemException>(() => row.ToString(columnsLength));
    }
}
