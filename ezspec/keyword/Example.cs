using ezSpec.keyword.table;

namespace ezSpec.keyword {
    public class Example : Row {
        protected Example(Row row) : base(row) {
        }

        public static Example New(Row row) {
            return new Example(row);
        }
    }
}
