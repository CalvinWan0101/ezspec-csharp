public static class Extensions {
    public static string AutoTrim(this string code) {
        List<string> lines = SplitByNewLine(code);
        int trimLen = lines.Skip(1).Min(s => s.Length - s.TrimStart().Length);

        if (lines.Count == 0) {
            return "";
        }
        else if (lines.Count == 1) {
            return lines[0];
        }
        return lines[0] + "\n" + string.Join("\n",
            lines
            .GetRange(1, lines.Count - 1)
            .ConvertAll(line => line.Substring(Math.Min(line.Length, trimLen))));
    }

    private static List<string> SplitByNewLine(string code) {
        if (code.Contains("\r\n")) {
            return code.Split("\r\n").ToList();
        }
        else if (code.Contains("\r")) {
            return code.Split("\r").ToList();
        }
        else {
            return code.Split("\n").ToList();
        }
    }
}