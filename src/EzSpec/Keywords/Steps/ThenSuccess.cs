namespace EzSpec.Keywords.Steps;

public class ThenSuccess(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback), BeginConcurrentStep {

    public override string Name {
        get { return "ThenSuccess"; }
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new ThenSuccess(description, this.continousAfterFailure, this.callback);
    }
}
