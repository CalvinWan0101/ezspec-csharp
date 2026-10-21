using EzSpec.Exceptions;
using System.Text;
using System.Text.RegularExpressions;

namespace EzSpec.Keywords.Steps;

public abstract partial class Step(string description, bool continous, Step.StepCallback callback) {
    public delegate void StepCallback(ScenarioEnvironment env);

    public static readonly bool ContinuousAfterFailure = true;
    public static readonly bool TerminateAfterFailure = false;

    protected string description = description;
    protected StepCallback callback = callback;
    protected bool continousAfterFailure = continous;
    private Result stepResult = Result.Pending(PendingException.New());

    public abstract string Name { get; }

    public string Description {
        get { return description; }
        internal set { description = value; }
    }

    internal StepCallback Callback {
        get { return callback; }
    }

    public bool IsContinuousAfterFailure {
        get { return continousAfterFailure; }
    }

    public Result Result {
        get { return stepResult; }
        set { stepResult = value; }
    }

    public List<Argument> Arguments {
        get {
            List<Argument> arguments = new List<Argument>();

            Regex reg = new Regex("\\$\\{([^{}]+)\\}");
            MatchCollection matchs = reg.Matches(description);

            foreach (Match match in matchs) {
                arguments.Add(Argument.New(match.Groups[0].Value));
            }

            return arguments;
        }
    }

    public string EraseReversedWords {
        get {
            string result = description;

            Regex reg = new Regex("\\$\\{([^{}]+)\\}");
            MatchCollection matchs = reg.Matches(description);

            foreach (Match match in matchs) {
                Argument argument = Argument.New(match.Groups[0].Value);
                result = result.Replace(match.Groups[0].Value, argument.Value);
            }
            return result;
        }
    }

    internal abstract Step CloneWithDifferentDescription(string description);

    public override string ToString() {
        StringBuilder result = new StringBuilder();
        result.Append(Name);
        result.Append(" ");
        result.Append(EraseReversedWords);
        return result.ToString();
    }

    public string ToStringWithResult() {
        StringBuilder result = new StringBuilder();
        result.Append("[");
        result.Append(stepResult.ToString());
        result.Append("] ");
        result.Append(Name);
        result.Append(" ");
        result.Append(EraseReversedWords.Replace("\n", "\n          "));
        return result.ToString();
    }
}
