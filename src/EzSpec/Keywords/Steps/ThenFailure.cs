namespace EzSpec.Keywords.Steps;

public class ThenFailure(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback), BeginConcurrentStep {

    public override string Name => nameof(ThenFailure);

    internal override Step CloneWithDifferentDescription(string description) {
        return new ThenFailure(description, this.continousAfterFailure, this.callback);
    }
}
