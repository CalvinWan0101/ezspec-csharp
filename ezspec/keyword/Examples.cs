using ezSpec.keyword.table;
using System.Collections.ObjectModel;
using System.Text;

namespace ezSpec.keyword {
    public class Examples {
        private string name;
        private string description;
        private Table table;

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        public ReadOnlyCollection<Example> ExampleSet {
            get {
                // TODO: Use convertAll after updating Table
                List<Example> examples = new List<Example>();
                foreach (Row row in table.Rows) {
                    examples.Add(Example.New(row));
                }
                return new ReadOnlyCollection<Example>(examples);
            }
        }

        public static Examples New(string name, string description, Table table) {
            return new Examples(name, description, table);
        }

        public static Examples New(string name, string description, string rawTable) {
            return new Examples(name, description, Table.New(rawTable));
        }

        public static Examples New(string name, Table table) {
            return new Examples(name, "", table);
        }

        public static Examples New(string name, string rawTable) {
            return new Examples(name, "", Table.New(rawTable));
        }

        public static Examples New(Table table) {
            return new Examples("", "", table);
        }

        public static Examples New(string rawTable) {
            return new Examples("", "", Table.New(rawTable));
        }

        public static Examples New(Examples examples) {
            return new Examples(examples.Name, examples.Description, examples.table);
        }

        private Examples(string name, string description, Table table) {
            this.name = name;
            this.description = description;
            this.table = table;
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append("Examples: ");
            result.Append(name);
            result.Append("\n");
            if ("" != description) {
                result.Append(description);
                result.Append("\n");
            }
            result.Append(table.ToString());

            return result.ToString();
        }

        public string ToStringBeautify() {
            StringBuilder result = new StringBuilder();
            result.Append("Examples: ");
            result.Append(name);
            result.Append("\n");
            if ("" != description) {
                result.Append(description);
                result.Append("\n");
            }
            result.Append(table.ToStringBeautify());

            return result.ToString();
        }
    }
}
