namespace EzSpec.Keywords.Steps;

public class And(string description, bool continuous, Step.StepCallback callback) : Step(description, continuous, callback) {

    public override string Name {
        get { return "And"; }
    }

    internal override Step CloneWithDifferentDescription(string description) {
        return new And(description, this.continousAfterFailure, this.callback);
    }
}
