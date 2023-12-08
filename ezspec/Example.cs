using ezSpec.table;

namespace ezSpec {
    public class Example : Row {
        protected Example(Row row): base(row) {
        }

        public Example New(Row row) {
            return new Example(row);
        }
    }
}
