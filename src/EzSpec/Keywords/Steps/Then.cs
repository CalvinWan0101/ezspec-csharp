namespace EzSpec.Keywords.Steps;

public class Then(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback), BeginConcurrentStep {

    public override string Name {
        get { return "Then"; }
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new Then(description, this.continousAfterFailure, this.callback);
    }
}
