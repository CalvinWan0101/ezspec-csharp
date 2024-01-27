using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.table.Test {

    [TestClass]
    public class TableTest {
        private static Header header;
        private static List<Row> rows;

        [ClassInitialize]
        public static void BeforeAll(TestContext context) {
            List<string> headerData = new List<string>() {
                "id", "name", "score"
            };
            header = Header.New(headerData);

            rows = new List<Row>();
            rows.Add(Row.New(header, new List<string>() { "10001", "Joe", "60" }));
            rows.Add(Row.New(header, new List<string>() { "10002", "Calvin", "80" }));
            rows.Add(Row.New(header, new List<string>() { "10003", "Howard", "100" }));
        }

        [TestMethod]
        public void create_table() {
            Table table = Table.New();

            Assert.AreEqual(0, table.Header.Size);
            Assert.AreEqual(0, table.Rows.Count);
            Assert.AreEqual("", table.RawData);
        }

        [TestMethod]
        public void create_table_with_header() {
            Table table = Table.New(header);

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual("id", table.Header.Get(0));
            Assert.AreEqual("name", table.Header.Get(1));
            Assert.AreEqual("score", table.Header.Get(2));
            Assert.AreEqual(0, table.Rows.Count);
            Assert.AreEqual("", table.RawData);
        }

        [TestMethod]
        public void create_table_with_header_and_rows() {
            Table table = Table.New(header, rows);

            List<int> beautify = new List<int>() {
                7, 8, 5
            };

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual(3, table.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60  |", table.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80  |", table.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100 |", table.Rows[2].ToString(beautify));
            Assert.AreEqual("", table.RawData);
        }

        [TestMethod]
        public void create_table_from_raw_data() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Table table = Table.New(rawData);

            List<int> beautify = new List<int>() {
                7, 8, 7
            };

            Assert.AreEqual(3, table.Header.Size);
            Assert.AreEqual("| id    | name   | score |", table.Header.ToString(beautify));
            Assert.AreEqual(3, table.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60    |", table.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80    |", table.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100   |", table.Rows[2].ToString(beautify));
            Assert.AreEqual(rawData, table.RawData);
        }

        [TestMethod]
        public void create_table_from_illegal_raw_data() {
            string rawData = "RFTYGUHIJOUYTGH*(&^123";

            Assert.ThrowsException<SystemException>(() => Table.New(rawData));
        }

        [TestMethod]
        public void create_table_from_table() {
            Table table = Table.New(header, rows);

            Table newTable = Table.New(table);

            List<int> beautify = new List<int>() {
                7, 8, 7
            };

            Assert.AreEqual(3, newTable.Header.Size);
            Assert.AreEqual("| id    | name   | score |", newTable.Header.ToString(beautify));
            Assert.AreEqual(3, newTable.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60    |", newTable.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80    |", newTable.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100   |", newTable.Rows[2].ToString(beautify));
            Assert.AreEqual("", newTable.RawData);
        }

        [TestMethod]
        public void create_table_from_raw_data_table() {
            string rawData = @"
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";
            Table table = Table.New(rawData);

            Table newTable = Table.New(table);

            List<int> beautify = new List<int>() {
                7, 8, 7
            };

            Assert.AreEqual(3, newTable.Header.Size);
            Assert.AreEqual("| id    | name   | score |", newTable.Header.ToString(beautify));
            Assert.AreEqual(3, newTable.Rows.Count);
            Assert.AreEqual("| 10001 | Joe    | 60    |", newTable.Rows[0].ToString(beautify));
            Assert.AreEqual("| 10002 | Calvin | 80    |", newTable.Rows[1].ToString(beautify));
            Assert.AreEqual("| 10003 | Howard | 100   |", newTable.Rows[2].ToString(beautify));
            Assert.AreEqual(rawData, newTable.RawData);
        }

        [TestMethod]
        public void get_row_by_index() {
            Table table = Table.New(header, rows);

            Assert.AreEqual(rows[0].ToString(), table.GetRow(0).ToString());
            Assert.AreEqual(rows[1].ToString(), table.GetRow(1).ToString());
            Assert.AreEqual(rows[2].ToString(), table.GetRow(2).ToString());
        }

        [TestMethod]
        public void get_row_by_first_column() {
            Table table = Table.New(header, rows);

            Assert.AreEqual(rows[0].ToString(), table.GetRow("10001").ToString());
            Assert.AreEqual(rows[1].ToString(), table.GetRow("10002").ToString());
            Assert.AreEqual(rows[2].ToString(), table.GetRow("10003").ToString());
        }

        [TestMethod]
        public void get_row_by_first_column_with_illegal_column() {
            Table table = Table.New(header, rows);

            Assert.ThrowsException<SystemException>(() => table.GetRow("10004"));
        }

        //[TestMethod]
        //public void add_new_row_to_table() {
        //    Table table = Table.New(header, rows);
        //    Row newRow = new Row(header, new List<string>() { "10004", "Annie", "40" });
        //    table.AddRow(newRow);

        //    Assert.AreEqual(4, table.Rows.Count);
        //    Assert.AreEqual(newRow.ToString(), table.GetRow(3).ToString());
        //}

        //[TestMethod]
        //public void add_new_row_to_table_by_list_of_string() {
        //    Table table = Table.New(header, rows);

        //    List<string> columns = new List<string>();
        //    columns.Add("10004");
        //    columns.Add("Annie");
        //    columns.Add("40");
        //    table.AddRow(columns);

        //    Assert.AreEqual(4, table.Rows.Count);
        //    Assert.AreEqual(columns[0], table.GetRow(3).Columns[0]);
        //    Assert.AreEqual(columns[1], table.GetRow(3).Columns[1]);
        //    Assert.AreEqual(columns[2], table.GetRow(3).Columns[2]);
        //}

        //[TestMethod]
        //public void clear_table() {
        //    Table table = Table.New(header, rows);
        //    table.Clear();

        //    Assert.AreEqual(0, table.Rows.Count);
        //}

        [TestMethod]
        public void table_to_string() {
            Table table = Table.New(header, rows);

            string expected =
                "|\tid\t|\tname\t|\tscore\t|\n" +
                "|\t10001\t|\tJoe\t|\t60\t|\n" +
                "|\t10002\t|\tCalvin\t|\t80\t|\n" +
                "|\t10003\t|\tHoward\t|\t100\t|";

            Assert.AreEqual(expected, table.ToString());
        }

        [TestMethod]
        public void table_to_string_with_beautify() {
            Table table = Table.New(header, rows);

            string expected =
                "| id    | name   | score |\n" +
                "| 10001 | Joe    | 60    |\n" +
                "| 10002 | Calvin | 80    |\n" +
                "| 10003 | Howard | 100   |";

            Assert.AreEqual(expected, table.ToStringBeautify());
        }


        [TestMethod]
        public void table_in_table_to_string() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            List<Row> rows = new List<Row>() {
                Row.New(header, new List<string>(){
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                    "/users/user1",
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                }),
                Row.New(header, new List<string>(){
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                    "/users/user2",
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |"
                })
            };
            Table table = Table.New(header, rows);

            string except =
                "|\torigin_struct\t|\tdelete_node\t|\texcept_struct\t|\n" +
                "|\t|\t/users/user1\t|\t|\n" +
                "\t<origin_struct>\n" +
                "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                "\t|\tuser1\t|\t/users/user1\t|\tusers\t|\n" +
                "\t|\tuser2\t|\t/users/user2\t|\tusers\t|\n" +
                "\t<except_struct>\n" +
                "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                "\t|\tuser2\t|\t/users/user2\t|\tusers\t|\n" +
                "|\t|\t/users/user2\t|\t|\n" +
                "\t<origin_struct>\n" +
                "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                "\t|\tuser1\t|\t/users/user1\t|\tusers\t|\n" +
                "\t|\tuser2\t|\t/users/user2\t|\tusers\t|\n" +
                "\t<except_struct>\n" +
                "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                "\t|\tuser1\t|\t/users/user1\t|\tusers\t|";
            Assert.AreEqual(except, table.ToString());
        }

        [TestMethod]
        public void table_in_table_to_string_beautify() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            List<Row> rows = new List<Row>() {
                Row.New(header, new List<string>(){
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                    "/users/user1",
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                }),
                Row.New(header, new List<string>(){
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                    "/users/user2",
                    "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |"
                })
            };
            Table table = Table.New(header, rows);

            string except =
                "| origin_struct | delete_node  | except_struct |\n" +
                "|               | /users/user1 |               |\n" +
                "\t<origin_struct>\n" +
                "\t| name  | path         | parent |\n" +
                "\t| users | /users       | null   |\n" +
                "\t| user1 | /users/user1 | users  |\n" +
                "\t| user2 | /users/user2 | users  |\n" +
                "\t<except_struct>\n" +
                "\t| name  | path         | parent |\n" +
                "\t| users | /users       | null   |\n" +
                "\t| user2 | /users/user2 | users  |\n" +
                "|               | /users/user2 |               |\n" +
                "\t<origin_struct>\n" +
                "\t| name  | path         | parent |\n" +
                "\t| users | /users       | null   |\n" +
                "\t| user1 | /users/user1 | users  |\n" +
                "\t| user2 | /users/user2 | users  |\n" +
                "\t<except_struct>\n" +
                "\t| name  | path         | parent |\n" +
                "\t| users | /users       | null   |\n" +
                "\t| user1 | /users/user1 | users  |";
            Assert.AreEqual(except, table.ToStringBeautify());
        }

        [TestMethod]
        public void string_contains_table() {
            string description = @"
                this is a table:
                |  id   |  name  | score |
                | 10001 |  Joe   |  60   |
                | 10002 | Calvin |  80   |
                | 10003 | Howard |  100  |";

            Assert.IsTrue(Table.ContainsTable(description));
        }

        [TestMethod]
        public void string_does_not_contain_table() {
            string description = "this is a description";

            Assert.IsFalse(Table.ContainsTable(description));
        }
    }
}
