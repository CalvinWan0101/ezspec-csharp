using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.table
{
    public class Header {
        private IList<string> header;

        public int Size {
            get { return header.Count; }
        }

        public IList<string> ColumnNames {
            get { return new ReadOnlyCollection<string>(header); }
        }

        private Header() {
            header = new List<string>();
        }

        private Header(IList<string> data) {
            header = new List<string>(data);
        }

        static public Header Create() {
            return new Header();
        }

        static public Header Create(IList<string> data) {
            return new Header(data);
        }

        public string Get(int index) {
            return header[index];
        }

        public void Clear() {
            header.Clear();
        }

        public void Reset(IList<string> newData) {
            header = new List<string>(newData);
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder("|");
            foreach (string columnName in header) {
                result.Append('\t');
                result.Append(columnName);
                result.Append('\t');
                result.Append('|');
            }
            return result.ToString();
        }

        public string ToString(IList<int> columnsLength) {
            StringBuilder result = new StringBuilder("|");
            for (int i = 0; i < header.Count; i++) {
                result.Append(" ");
                result.Append(header[i]);
                result.Append(new string(' ', columnsLength[i] - header[i].Length - 1));
                result.Append("|");
            }
            return result.ToString();
        }

    }

}
