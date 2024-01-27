using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword.table {
    public class Row {
        private Header header;
        private IList<string> columns;

        public ReadOnlyCollection<string> Columns {
            get { return new ReadOnlyCollection<string>(columns); }
        }

        protected Row(Header header, IList<string> columns) {
            this.header = header;
            this.columns = new List<string>(columns);
        }

        protected Row(Row row) {
            header = Header.New(row.header);
            columns = new List<string>(row.columns);
        }

        public static Row New(Header header, IList<string> columns) {
            return new Row(header, columns);
        }

        public static Row New(Row row) {
            return new Row(row);
        }

        public string Get(int index) {
            return columns[index];
        }

        public string Get(string columnName) {
            for (int i = 0; i < columns.Count; i++) {
                if (header.Get(i) == columnName) {
                    return columns[i];
                }
            }
            throw new SystemException($"Unable to get the \"{columnName}\"");
        }

        public string GetOrEmpty(string columnName) {
            for (int i = 0; i < columns.Count; i++) {
                if (header.Get(i) == columnName) {
                    return columns[i];
                }
            }
            return "";
        }

        public Table GetTable(int index) {
            return Table.New(columns[index]);
        }

        public Table GetTable(string columnName) {
            for (int i = 0; i < columns.Count; i++) {
                if (header.Get(i) == columnName) {
                    return Table.New(columns[i]);
                }
            }
            throw new SystemException($"Unable to get the \"{columnName}\"");
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder("|");
            foreach (string column in columns) {
                if (Table.ContainsTable(column)) {
                    sb.Append("\t|");
                }
                else {
                    sb.Append($"\t{column}\t|");
                }
            }

            for (int i = 0; i < columns.Count; i++) {
                if (Table.ContainsTable(columns[i])) {
                    sb.Append("\n");
                    sb.Append($"\t<{header.Get(i)}>\n");
                    sb.Append("\t");
                    sb.Append(Table.New(columns[i]).ToString().Replace("\n", "\n\t"));
                }
            }
            if (0 == columns.Count) {
                sb.Append("|");
            }
            return sb.ToString();
        }

        public string ToString(IList<int> columnsLength) {
            if (columnsLength.Count != columns.Count) {
                throw new SystemException("The count of columnsLength didn't match the count of row.");
            }

            StringBuilder sb = new StringBuilder("|");
            for (int i = 0; i < columns.Count; i++) {
                if (Table.ContainsTable(columns[i])) {
                    sb.Append(new string(' ', columnsLength[i]));
                    sb.Append("|");
                }
                else {
                    sb.Append(" ");
                    sb.Append(columns[i]);
                    sb.Append(new string(' ', columnsLength[i] - columns[i].Length - 1));
                    sb.Append("|");
                }
            }

            for (int i = 0; i < columns.Count; i++) {
                if (Table.ContainsTable(columns[i])) {
                    sb.Append("\n");
                    sb.Append($"\t<{header.Get(i)}>\n");
                    sb.Append("\t");
                    sb.Append(Table.New(columns[i]).ToStringBeautify().Replace("\n", "\n\t"));
                }
            }
            if (0 == columns.Count) {
                sb.Append("|");
            }
            return sb.ToString();
        }
    }
}
