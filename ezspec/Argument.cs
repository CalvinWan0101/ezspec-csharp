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

        public Argument(string expression) {
            if (IsKeyValuePair(expression)) {
                InitializeWithKeyValuePair(expression);
            } else {
                InitializeWithValue(expression);
            }
        }

        private bool IsKeyValuePair(string expression) {
            return expression.Trim().StartsWith("${") && expression.Trim().EndsWith("}");
        }

        private void InitializeWithKeyValuePair(string expression) {
            Regex reg = new Regex("^([^=:]+)[=:](.+)$");
            MatchCollection matchs = reg.Matches(expression);
            GroupCollection groups = matchs[0].Groups;

            key = groups[1].Value.Trim().Substring(2).Trim();
            value = groups[2].Value.Trim().Substring(0, groups[2].Value.Trim().Length - 1).Trim();
        }

        private void InitializeWithValue(string expression) {
            Regex reg = new Regex("\\$(\\S)+");
            MatchCollection matchs = reg.Matches(expression);
            GroupCollection groups = matchs[0].Groups;

            key = "";
            value = groups[0].Value.Trim().Substring(1);
        }

    }

}
