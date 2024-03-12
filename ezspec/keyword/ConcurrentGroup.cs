using System.Collections.ObjectModel;
using ezSpec.keyword.step;

namespace ezSpec.keyword;

public class ConcurrentGroup {
    private readonly IList<Step> steps;
    
    public ReadOnlyCollection<Step> Steps {
        get { return new ReadOnlyCollection<Step>(steps); }
    }
    
    private ConcurrentGroup(IList<Step> steps) {
        this.steps = steps;
    }

    public static List<ConcurrentGroup> SliceConcurrentGroups(IList<Step> steps) {
        List<ConcurrentGroup> concurrentGroups = new List<ConcurrentGroup>();
        List<Step> concurrentSteps = new List<Step>();
        for (int currentStep = 0; currentStep < steps.Count; currentStep++) {
            if (steps[currentStep] is BeginConcurrentStep) {
                if (concurrentSteps.Count > 0) {
                    concurrentGroups.Add(new ConcurrentGroup(concurrentSteps));
                    concurrentSteps = new List<Step>();
                }
            }
            concurrentSteps.Add(steps[currentStep]);
        }
        if (concurrentSteps.Count > 0) {
            concurrentGroups.Add(new ConcurrentGroup(concurrentSteps));
        }
        return concurrentGroups;
    }
}