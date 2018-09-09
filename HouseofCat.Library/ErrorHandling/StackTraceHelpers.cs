using HouseofCat.Models.General;
using System;
using System.Collections.Generic;

namespace HouseofCat.Library.ErrorHandling
{
    public static class StackTraceHelpers
    {
        public static Stacky PrettifyStackTrace(string stackTrace, Type exceptionType)
        {
            var stacky = new Stacky
            {
                ExceptionType = exceptionType.ToString()
            };

            if (!string.IsNullOrEmpty(stackTrace))
            {
                stacky.StackLines = new List<string>();

                var lines = stackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                var stackCount = 0;

                try // Try to Prettify
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i];

                        if (i == 0)
                        {
                            var subStrings = line.Split(new string[] { " in " }, StringSplitOptions.RemoveEmptyEntries);
                            var methodStrings = subStrings[0].Split(new string[] { "   at " }, StringSplitOptions.RemoveEmptyEntries);
                            var fileStrings = (subStrings.Length > 1) ? subStrings[1].Split(new string[] { ":line " }, StringSplitOptions.RemoveEmptyEntries) : new string[] { subStrings[0], string.Empty };
                            stacky.Method = methodStrings[methodStrings.Length - 1];
                            stacky.FileName = fileStrings[0].Contains(".cs") ? fileStrings[0] : "System/NET Exception";
                            stacky.Line = int.Parse(fileStrings[1]);

                            stacky.StackLines.Add(stacky.Method);
                        }
                        else if (line.StartsWith("---"))
                        {
                            stackCount++;
                            stacky.StackLines.Add($"=== Sub-stack {stackCount} ===");
                        }
                        else
                        {
                            stacky.StackLines.Add(line.Replace("   at ", " @ "));
                        }
                    }
                }
                catch // Else just print the lines as is.
                {
                    stackCount = 0;
                    stacky.StackLines.Clear();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i];

                        if (line.StartsWith("---"))
                        {
                            stackCount++;
                            stacky.StackLines.Add($"=== Sub-stack {stackCount} ===");
                        }
                        else
                        { stacky.StackLines.Add(line.Replace("   at ", " @ ")); }
                    }
                }
            }

            return stacky;
        }
    }
}
