using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ezSpec.keyword.table.Test {

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

            Assert.ThrowsException<SystemException>(() => {
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
        public void get_table_by_index() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            Row row = Row.New(header, new List<string>() {
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                            "/users/user1",
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                      });

            string except =
                    "|\tname\t|\tpath\t|\tparent\t|\n" +
                    "|\tusers\t|\t/users\t|\tnull\t|\n" +
                    "|\tuser1\t|\t/users/user1\t|\tusers\t|\n" +
                    "|\tuser2\t|\t/users/user2\t|\tusers\t|";

            Assert.AreEqual(except, row.GetTable(0).ToString());
        }

        [TestMethod]
        public void get_table_by_name() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            Row row = Row.New(header, new List<string>() {
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                            "/users/user1",
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                      });

            string except =
                    "|\tname\t|\tpath\t|\tparent\t|\n" +
                    "|\tusers\t|\t/users\t|\tnull\t|\n" +
                    "|\tuser1\t|\t/users/user1\t|\tusers\t|\n" +
                    "|\tuser2\t|\t/users/user2\t|\tusers\t|";

            Assert.AreEqual(except, row.GetTable("origin_struct").ToString());
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
        public void empty_row_to_string() {
            List<string> rowData = new List<string>() { };
            Row row = Row.New(header, rowData);

            Assert.AreEqual("||", row.ToString());
        }

        [TestMethod]
        public void row_contains_table_to_string() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            Row row = Row.New(header, new List<string>() {
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                            "/users/user1",
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                      });

            string except =
                    "|\t|\t/users/user1\t|\t|\n" +
                    "\t<origin_struct>\n" +
                    "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                    "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                    "\t|\tuser1\t|\t/users/user1\t|\tusers\t|\n" +
                    "\t|\tuser2\t|\t/users/user2\t|\tusers\t|\n" +
                    "\t<except_struct>\n" +
                    "\t|\tname\t|\tpath\t|\tparent\t|\n" +
                    "\t|\tusers\t|\t/users\t|\tnull\t|\n" +
                    "\t|\tuser2\t|\t/users/user2\t|\tusers\t|";

            Assert.AreEqual(except, row.ToString());
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
        public void row_contains_table_to_string_beautify() {
            Header header = Header.New(new List<string>() {
                "origin_struct", "delete_node", "except_struct"
            });
            Row row = Row.New(header, new List<string>() {
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user1 | /users/user1 | users  |\n| user2 | /users/user2 | users  |",
                            "/users/user1",
                            "| name  |     path     | parent |\n| users |    /users    |  null  |\n| user2 | /users/user2 | users  |"
                      });
            List<int> columnsLength = new List<int>() {
                5, 14, 5
            };

            string except =
                    "|     | /users/user1 |     |\n" +
                    "\t<origin_struct>\n" +
                    "\t| name  | path         | parent |\n" +
                    "\t| users | /users       | null   |\n" +
                    "\t| user1 | /users/user1 | users  |\n" +
                    "\t| user2 | /users/user2 | users  |\n" +
                    "\t<except_struct>\n" +
                    "\t| name  | path         | parent |\n" +
                    "\t| users | /users       | null   |\n" +
                    "\t| user2 | /users/user2 | users  |";

            Assert.AreEqual(except, row.ToString(columnsLength));
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

            Assert.ThrowsException<SystemException>(() => row.ToString(columnsLength));
        }
    }
}
