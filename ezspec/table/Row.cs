using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.table {
    public class Row {
        private Header header;
        private IList<string> columns;

        public IList<string> Columns {
            get { return new ReadOnlyCollection<string>(columns); }
        }

        public Row(Header header, IList<string> columns) {
            this.header = header;
            this.columns = new List<string>(columns);
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

        public override string ToString() {
            StringBuilder sb = new StringBuilder("|");
            foreach (string column in columns) {
                sb.Append($"\t{column}\t|");
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
                sb.Append(" ");
                sb.Append(columns[i]);
                sb.Append(new string(' ', columnsLength[i] - columns[i].Length - 1));
                sb.Append("|");
            }
            if (0 == columns.Count) {
                sb.Append("|");
            }
            return sb.ToString();
        }
    }
}
