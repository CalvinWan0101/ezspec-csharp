namespace ezSpec.table
{
    public class Table
    {
        private Header header;
        private IList<Row> rows;

        public Header Header {
            get { return header; }
        }

        public IList<Row> Rows {
            get { return rows; }
        }

        public Table() {
            this.header = Header.New();
            this.rows = new List<Row>();
        }

        public Table(Header header) {
            this.header = Header.New(header);
            this.rows = new List<Row>();
        }

        public Table(Header header, IList<Row> rows) {
            this.header = Header.New(header);
            this.rows = new List<Row>();
            foreach (Row row in rows) {
                this.rows.Add(new Row(row));
            }
        }

        public Table(string rawData) {
            rawData = rawData.Replace("\r\n", "\n");
            List<string> lines = rawData.Split('\n').ToList();
            lines = lines.FindAll(line => line.Contains("|"));

            this.header = Header.New(ConvertLineStringToRowData(lines[0]));
            
            this.rows = new List<Row>();
            for (int i = 1; i < lines.Count; i++) {
                List<string> columns = ConvertLineStringToRowData(lines[i]);
                this.rows.Add(new Row(this.header, columns));
            }
        }

        private List<string> ConvertLineStringToRowData(string line) {
            line = line.Trim();
            return line
                .Substring(1, line.Length - 2)
                .Split('|')
                .ToList()
                .ConvertAll(str => str.Trim());
        }
    }
}
