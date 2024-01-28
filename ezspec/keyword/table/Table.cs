using System.Text;

namespace ezSpec.keyword.table {
    public class Table {
        private string rawData;
        private Header header;
        private IList<Row> rows;

        public string RawData {
            get { return rawData; }
        }

        public Header Header {
            get { return header; }
        }

        public IList<Row> Rows {
            get { return rows; }
        }

        private Table(Header header, IList<Row> rows) {
            this.header = Header.New(header);
            this.rows = new List<Row>();
            foreach (Row row in rows) {
                this.rows.Add(Row.New(row));
            }
            rawData = "";
        }

        private Table(string rawData) {
            if (!ContainsTable(rawData)) {
                throw new SystemException("The raw data contains no table.");
            }

            this.rawData = rawData;
            rawData = rawData.Replace("\r\n", "\n");
            List<string> lines = rawData.Split('\n').ToList();
            lines = lines.FindAll(line => line.Contains("|"));

            header = Header.New(ConvertLineStringToRowData(lines[0]));

            rows = new List<Row>();
            for (int i = 1; i < lines.Count; i++) {
                List<string> columns = ConvertLineStringToRowData(lines[i]);
                rows.Add(Row.New(header, columns));
            }
        }

        private Table(Table table) {
            header = Header.New(table.header);
            rows = new List<Row>();
            foreach (Row row in table.rows) {
                rows.Add(Row.New(row));
            }
            rawData = table.rawData;
        }

        public static Table New(Header header) {
            return new Table(header, new List<Row>());
        }

        public static Table New(Header header, List<Row> rows) {
            return new Table(header, rows);
        }

        public static Table New(string rawData) {
            return new Table(rawData);
        }

        public static Table New(Table table) {
            return new Table(table);
        }

        private List<string> ConvertLineStringToRowData(string line) {
            line = line.Trim();
            return line
                .Substring(1, line.Length - 2)
                .Split('|')
                .ToList()
                .ConvertAll(str => str.Trim());
        }

        public Row GetRow(int index) {
            return rows[index];
        }

        public Row GetRow(string firstColumn) {
            foreach (Row row in rows) {
                if (row.Get(0) == firstColumn) {
                    return row;
                }
            }
            throw new SystemException($"Unable to get the row with the first column \"{firstColumn}\"");
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append(header.ToString());
            foreach (Row row in rows) {
                result.Append("\n");
                result.Append(row.ToString());
            }
            return result.ToString();
        }

        public string ToStringBeautify() {
            List<int> beautify = new List<int>();
            for (int i = 0; i < header.Size; i++) {
                beautify.Add(GetMaxColumnLength(i) + 2);
            }

            StringBuilder result = new StringBuilder();
            result.Append(header.ToString(beautify));
            foreach (Row row in rows) {
                result.Append("\n");
                result.Append(row.ToString(beautify));
            }
            return result.ToString();
        }

        private int GetMaxColumnLength(int columnIndex) {
            int max = header.Get(columnIndex).Length;
            foreach (Row row in rows) {
                if (ContainsTable(row.Get(columnIndex))) {
                    break;
                }
                if (row.Get(columnIndex).Length > max) {
                    max = row.Get(columnIndex).Length;
                }
            }
            return max;
        }

        public static bool ContainsTable(string description) {
            description = description.Replace("\r", "");
            List<string> lines = description.Split("\n").ToList();

            return lines.ConvertAll(x => x.Trim())
                        .Any(x => x.StartsWith('|'));
        }
    }
}
