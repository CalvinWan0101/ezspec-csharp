namespace EzSpec.Keywords.Steps;

public class When(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback), BeginConcurrentStep {

    public override string Name {
        get { return "When"; }
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new When(description, this.continousAfterFailure, this.callback);
    }
}
