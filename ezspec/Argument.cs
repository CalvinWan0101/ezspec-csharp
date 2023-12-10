using System.Text.RegularExpressions;

namespace ezSpec {

    public class Argument {

        private string key;
        private string value;

        public string Key {
            get { return key; }
        }

        public string Value {
            get { return value; }
        }

        static public Argument New(string expression) {
            return new Argument(expression);
        }

        static public Argument New(Argument argument) {
            return new Argument(argument);
        }

        private Argument(string expression) {
            if (IsKeyValuePair(expression)) {
                InitializeWithKeyValuePair(expression);
            }
            else {
                InitializeWithValue(expression);
            }
        }

        private Argument(Argument argument) {
            this.key = argument.Key;
            this.value = argument.Value;
        }

        private bool IsKeyValuePair(string expression) {
            return expression.Trim().StartsWith("${")
                && expression.Trim().EndsWith("}")
                && (expression.Contains(":") || expression.Contains("="));
        }

        private void InitializeWithKeyValuePair(string expression) {
            Regex reg = new Regex("\\${([^=:{}]*)[=:]([^{}]+)}");
            MatchCollection matchs = reg.Matches(expression);
            GroupCollection groups = matchs[0].Groups;

            key = groups[1].Value.Trim();
            value = groups[2].Value.Trim();
        }

        private void InitializeWithValue(string expression) {
            Regex reg = new Regex("\\${(([^{}=:])+)}");
            MatchCollection matchs = reg.Matches(expression);
            GroupCollection groups = matchs[0].Groups;

            key = "";
            value = groups[1].Value.Trim();
            //value = value.Substring(2, value.Length - 3).Trim();
        }

    }

}
