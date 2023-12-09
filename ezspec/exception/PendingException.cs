namespace ezSpec.exception {
    public class PendingException : Exception {
        public PendingException(): base() {
        }

        public PendingException(string message): base(message) {
        }
    }
}
