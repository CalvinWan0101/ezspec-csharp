using System.Collections.ObjectModel;

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
            return "";
        }

    }
}
