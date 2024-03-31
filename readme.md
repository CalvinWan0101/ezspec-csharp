# ezSpec

**ezSpec** is referenced from [**ezSpec (Java)**](https://gitlab.com/TeddyChen/ezspec), which is a developer-facing Behavior-Driven Development (BDD) tool implemented in C#. It adopts Gherkin syntax to specify requirements in a Specification by Example (SBE) manner. The goal of ezSpec is to provide a BDD/SBE tool that is more developer-friendly than current tools.

Common BDD/SBE tools such as Cucumber, SpecFlow, and JBehave are external Domain-Specific Languages (DSL). Developers first write plain text feature files that are parsed by the tools to generate step definitions for particular programming languages such as Ruby, C#, and Java. Developers then implement the step definitions to write executable specifications.

Unlike common BDD/SBE tools, ezSpec is an internal DSL. Both feature files and step definitions are written in C#. The former is a test case and the latter is a C# lambda expression. Due to the internal DSL approach taken by ezSpec, the context switch between feature files written and step definition generation as well as written are removed.

## How to use

### Requirement

- .NET core 6.0 or later

### NuGet

- Will be available soon

### Usage

#### Feature

The Feature class represents the feature keyword in Gherkin which provides a high-level description of a software feature.
A feature contains one or more scenarios, which belong to a Rule.

A Feature is defined as a data member of a test class and initiated in the set up method before all tests.

#### Rule

A `Rule` represents the business logic of a feature. A feature can be broken down into multiple rules to aid in scenario illustration. Each rule, in turn, encompasses a collection of related scenarios that pertain to the feature's business logic. 

When your scenario doesn't need to be categorized under any rule, you can use the default rule, which is predefined in `Feature`. A rule has the capability to define a Background that can be shared among scenarios associated with the same rule.

- To create a rule in a feature, invoke `Feature::NewRule(String ruleName)`.
- To use the default rule, invoke `Feature::WithDefaultRule()`.
- To use the specific rule, invoke `Feature::WithRule(String ruleName)`.

```csharp
[TestClass]
public class RuleExample {
    private static Feature feature;

    [ClassInitialize]
    public static void Init(TestContext context) {
        feature = Feature.New("Point");
        feature.NewRule("Create point");
    }

    [TestMethod]
    public void scenario_example_with_user_created_rule() {
        feature.WithRule("Create point")
            .NewScenario()
            .Given("x(${10.0}) and y(${20.0})", env => { })
            .When("I create a point with x and y", env => { })
            .ThenSuccess(env => { })
            .Execute();
    }

    [TestMethod]
    public void scenario_example_with_default_rule() {
        feature.WithDefaultRule()
            .NewScenario()
            .Given("x(${10.0}) and y(${20.0})", env => { })
            .When("I create a point with x and y", env => { })
            .ThenSuccess(env => { })
            .Execute();
    }
}
```


#### Scenario

`Scenario` represents a concrete example that illustrates a business rule of a feature. It consists of a list of steps.


To create a scenario, invoke `Rule::NewScenario()`.

```csharp
[TestClass]
public class ScenarioExample {
    private static Feature feature;

    [ClassInitialize]
    public static void Init(TestContext context) {
        feature = Feature.New("Point");
        feature.NewRule("Create point");
    }

    [TestMethod]
    public void create_point_with_full_coordinate() {
        feature.WithRule("Create point")
            .NewScenario()
            .Given("x(${10.0}) and y(${20.0})", env => {
                env.Put("x", env.GetDoubleArg(0));
                env.Put("y", env.GetDoubleArg(1));
            })
            .When("I create a point with x and y", env => {
                Point point = new Point(env.GetDouble("x"), env.GetDouble("y"));
                env.Put("point", point);
            })
            .ThenSuccess(env => {
                Point point = env.Get<Point>("point");
                Assert.AreEqual(10.0, point.X);
                Assert.AreEqual(20.0, point.Y);
            })
            .Execute();
    }
}
```      


#### ScenarioOutline

`ScenarioOutline` is an alternative to `Scenario` that accepts multiple examples with the same steps.


To create a scenario outline, invoke `Rule::NewScenarioOutline()`.

```csharp
[TestClass]
public class ScenarioOutlineExample {
    private static Feature feature;

    [ClassInitialize]
    public static void Init(TestContext context) {
        feature = Feature.New("Point");
        feature.NewRule("Point addition");
    }

    [TestMethod]
    public void add_two_point() {
        string examples = @"
                | x1 | y1 | x2 | y2 | result_x | result_y |
                | 10 | 20 | 30 | 40 | 40       | 60       |
                | 5  | 10 | 15 | 20 | 20       | 30       |
            ";

        feature.WithRule("Point addition")
            .NewScenarioOutline()
            .WithExamples(examples)
            .Given("two points at (<x1>, <y1>) and (<x2>, <y2>)", env => {
                Point point1 = new Point(double.Parse(env.Example.Get(0)), double.Parse(env.Example.Get(1)));
                Point point2 = new Point(double.Parse(env.Example.Get(2)), double.Parse(env.Example.Get(3)));
                env.Put("point1", point1);
                env.Put("point2", point2);
            })
            .When("add the two point", env => {
                Point point1 = env.Get<Point>("point1");
                Point point2 = env.Get<Point>("point2");
                Point result = point1 + point2;
                env.Put("result", result);
            })
            .Then("the result should be (<result_x>, <result_y>)", env => {
                Point result = env.Get<Point>("result");
                Assert.AreEqual(double.Parse(env.Example.Get(4)), result.X);
                Assert.AreEqual(double.Parse(env.Example.Get(5)), result.Y);
            })
            .Execute();
    }
}
```

##### Examples

An `Examples` class which is a set of test data represents the Examples keyword of Gherkin. A Scenario Outline has one or more Examples. The Scenario Outline is executed once for each row in the Examples section, excluding the initial header row.

To use `Examples`, invoke:
- `Examples::New(string name, string description, Table table)`
- `Examples::New(string name, string description, string rawTable)`
- `Examples::New(string name, Table table)`
- `Examples::New(string name, string rawTable)`
- `Examples::New(Table table)`
- `Examples::New(string rawTable)`

#### Steps

The `Step` abstract class represents an action taken by a `Scenario`. A Step has a description, a lambda for step definition, and a result of executing the lambda.

The description is a sentence to describe a step and contains arguments for step definitions to write executable specification. The lambda defines the executable code of a step definition, verifying if the production code is compliant with the specification. All the lambdas in a Scenario and the Scenario as a whole form executable specification. The result has four values: Success, Failure, Skip, and Pending. It indicates the executed status of a step.

A Scenario contains a sequence of steps. When the Scenario is executed, each of its steps is executed sequentially. If a step fails, the Scenario is terminated and as a result the remaining steps are not executed. This behavior can be altered by setting the `ContinuousAfterFailure` attribute of Step to true. By so doing, the Scenario continuous its execution even a failed step is encountered.

The Steps have five concrete classes Given, When, Then, And, and But to represent the corresponding Gherkin Keywords. EzSpec also supports ThenSuccess and ThenFailure to express the two common execution results of the When.

The lambda accepts a parameter `env`, which is a [ScenarioEnvironment](#scenarioenvironment) for getting arguments from description, sharing data among steps, and getting example data.

Step Keywords provide fluent interface for users to concatenate each step and invoke `Scenario::Execute()` after the last step to execute the test.
Please refer [Usage Example](#usage-example)

ezSpec supports concurrent execution of steps via a mechanism called Concurrent Groups. Steps including Given, When, and Then define a concurrent group and act as the first step in the group. Steps such as And and But inside a concurrent group are executed concurrently with the first step. Each concurrent group is executed sequentially. To enable concurrent execution, invoke `Scenario::ExecuteConcurrently()` instead of `Scenario::Execute()`.

Any step failure in a concurrent group terminates the subsequent concurrent groups execution. To continue executing the subsequent concurrent groups while failure, set the `ContinuousAfterFailure` attribute for the step.

```csharp
[TestMethod]
public void create_steps_to_execute_concurrently() {
    feature.WithDefaultRule()
            .NewScenario()
            .Given("concurrent group A", env -> {
            })
            .When("concurrent group B", env -> {
            })
            .Then("concurrent group C", env -> {
            })
            .And("concurrent group C", env -> {
            })
            .And("concurrent group C", env -> {
            })
            .Then("concurrent group D", env -> {
            })
            .ExecuteConcurrently();
}
```

##### ScenarioEnvironment

The ScenarioEnvironment is the parameter passed to the lambda of the step and is used for three purposes: getting arguments from description, sharing data among steps, and getting example data.


**Single value arguments**: In step description, a word matches the pattern "\${value}" is an anonymous argument that can be accessed with `ScenarioEnvironment::Arguments` or `ScenarioEnvironment::GetStringArg(int index)` in its step definition. In addition, a word matches the pattern "${key=value}" or "${key:value}" is a named argument and can be accessed with the key by invoking `ScenarioEnvironment::GetStringArg(String key)` in its step definition. 

Instead of getting the argument in string format, you can have multiple methods to parse the argument to different types, here are the methods:
- `GetIntArg(int index)`
- `GetIntArg(string key)`
- `GetDoubleArg(int index)`
- `GetDoubleArg(string key)`

```csharp
[TestMethod]
public void get_single_value_arguments_from_step_descriptions() {
    feature.WithDefaultRule()
        .NewScenario()
        .Given("the tax excluded price of a computer is ${20,000}", env => {
        Assert.AreEqual(1, env.Arguments.Count);
        Assert.AreEqual("20,000", env.Arguments[0].Value);
        Assert.AreEqual("20,000", env.GetStringArg(0));
    })
    .And("the VAT rate is ${VAT=5%}", env => {
        Assert.AreEqual(1, env.Arguments.Count);
        Assert.AreEqual("5%", env.GetStringArg("VAT"));
        Assert.AreEqual("VAT", env.Arguments[0].Key);
    })
    .When("I buy the computer", env =>{})
    .Then("I need to pay ${total_price:21,000}", env =>{
        Assert.AreEqual("21,000", env.GetStringArg("total_price"));
    })
    .Execute();
}
```

**Table arguments**: Users define a table in the step description to provide arguments for the step definition. The arguments are retrieved by invoking `ScenarioEnvironment::Table` to get the table object, and then accessing the table object by invoking `Table::Rows` to get the `Row` of the table, finally invoking `Row::Get(string columnName)` to get the column.

```csharp
[TestMethod]
public void step_description_contain_table() {
    feature.WithDefaultRule()
        .NewScenario()
        .Given(@"
                a table 
                | name  |     path     | parent |
                | users |    /users    |  null  |
                | user1 | /users/user1 | users  |
                | user2 | /users/user2 | users  |".AutoTrim(), env => { })
        .When("I try to get the argument", env => {
            Assert.AreEqual("users", env.Table.Rows[0].Get("name"));
            Assert.AreEqual("/users", env.Table.Rows[0].Get("path"));
            Assert.AreEqual("null", env.Table.Rows[0].Get("parent"));
        })
        .Then("done", env => { })
        .Execute();
}
```

- **Sharing data among steps**: Since step definitions are implemented using C# lambdas, local variables inside the lambda cannot be accessed outside of it. In BDD, a situation may arise where one step definition needs to utilize variables generated in its previous steps. To share data among steps, user can put a value to ScenarioEnvironment with `ScenarioEnvironment::Put(string key, object value)` and access it in later steps with `ScenarioEnvironment::Get<T>(string key)`. 

  ScenarioEnvironment provides following methods to share variables among steps:
    - to define a variable, invoke `env.Put(string key, object value)`
    - to get a specific type object, invoke `env.Get<T>(string key)`
    - to get a string object, invoke `env.GetString(string key)`
    - to get a integer object, invoke `env.GetInt(string key)`
    - to get a double object, invoke `env.GetDouble(string key)`
    - to get a boolean object, invoke `env.GetBool(string key)`

- **Getting example data**: In scenario outline, the test data are provided from the Examples. 

To get the example data in a step, user can invoke `ScenarioEnvironment::Example` to get an example, and then access the example data by invoking `Example::Get(int index)` or `Example::Get(string columnName)`.
