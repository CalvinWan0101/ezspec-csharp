namespace EzSpec.Keywords.Steps;

public class But(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback) {

    public override string Name {
        get { return "But"; }
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new But(description, this.continousAfterFailure, this.callback);
    }
}
