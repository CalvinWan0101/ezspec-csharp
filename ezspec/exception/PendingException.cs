namespace ezSpec.exception {
    public class PendingException : Exception {
        private PendingException(): base() {
        }

        private PendingException(string message): base(message) {
        }

        static public PendingException New() {
            return new PendingException();
        }

        static public PendingException New(string message) {
            return new PendingException(message);
        }

        public static void Pending() {
            throw new PendingException();
        }

        public static void Pending(string message) {
            throw new PendingException(message);
        }
    }
}
