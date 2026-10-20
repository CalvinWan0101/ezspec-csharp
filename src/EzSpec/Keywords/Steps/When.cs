namespace EzSpec.Keywords.Steps;

public class When : Step, BeginConcurrentStep {

    public override string Name {
        get { return "When"; }
    }

    public When(string description, bool continuous, StepCallback callback) : base(description, continuous, callback) {
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new When(description, this.continousAfterFailure, this.callback);
    }
}
