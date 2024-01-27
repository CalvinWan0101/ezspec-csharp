using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword.table {
    public class Header {
        private IList<string> header;

        public int Size {
            get { return header.Count; }
        }

        public ReadOnlyCollection<string> ColumnNames {
            get { return new ReadOnlyCollection<string>(header); }
        }

        private Header() {
            header = new List<string>();
        }

        private Header(IList<string> data) {
            header = new List<string>(data);
        }

        private Header(Header header) {
            this.header = new List<string>(header.header);
        }

        static public Header New() {
            return new Header();
        }

        static public Header New(IList<string> data) {
            return new Header(data);
        }

        static public Header New(Header header) {
            return new Header(header);
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
                result.Append($"\t{columnName}\t|");
            }
            if (0 == header.Count) {
                result.Append("|");
            }
            return result.ToString();
        }

        public string ToString(IList<int> columnsLength) {
            if (columnsLength.Count != header.Count) {
                throw new SystemException("The count of columnsLength didn't match the count of header.");
            }

            StringBuilder result = new StringBuilder("|");
            for (int i = 0; i < header.Count; i++) {
                result.Append(" ");
                result.Append(header[i]);
                result.Append(new string(' ', columnsLength[i] - header[i].Length - 1));
                result.Append("|");
            }
            if (0 == header.Count) {
                result.Append("|");
            }
            return result.ToString();
        }

    }

}
