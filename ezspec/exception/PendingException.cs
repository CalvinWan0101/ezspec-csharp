namespace ezSpec.exception;

public class PendingException : Exception {
    private PendingException() : base() {
    }

    private PendingException(string message) : base(message) {
    }

    public static PendingException New() {
        return new PendingException();
    }

    public static PendingException New(string message) {
        return new PendingException(message);
    }

    public static void Pending() {
        throw new PendingException();
    }

    public static void Pending(string message) {
        throw new PendingException(message);
    }
}