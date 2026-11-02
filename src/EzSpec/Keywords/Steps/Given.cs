namespace EzSpec.Keywords.Steps;

public class Given(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback), BeginConcurrentStep {

    public override string Name => nameof(Given);

    internal override Step CloneWithDifferentDescription(string description) {
        return new Given(description, this.continousAfterFailure, this.callback);
    }
}
