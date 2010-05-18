using System;
using System.Text.RegularExpressions;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Intentions;
using JetBrains.ReSharper.Intentions.CSharp.DataProviders;
using JetBrains.TextControl;
using JetBrains.Util;

namespace BoxcarMethodNamePlugin
{
    [ContextAction(Group = "C#", Name = "BoxcarMethodName", Description = "Adds context action to convert from spaces to underscore in string literals")]
    public class BoxcarMethodName : BulbItemImpl, IContextAction
    {
        readonly ICSharpContextActionDataProvider _provider;
        readonly Regex literalRegex = new Regex("\"([^\"]+)\"");

        public BoxcarMethodName(ICSharpContextActionDataProvider provider)
        {
            _provider = provider;
        }

        protected override Action<ITextControl> ExecuteTransaction(ISolution solution, IProgressIndicator progress)
        {
            var document = _provider.Document;

            var caretOffset = _provider.CaretOffset.Offset;
            if (caretOffset > -1)
            {
                var lineMarker = document.GetCoordsByOffset(caretOffset);
                var lineStartOffset = document.GetLineStartOffset(lineMarker.Line);
                var lineEndOffset = document.GetLineEndOffsetNoLineBreak(lineMarker.Line);

                var lineContent = document.GetLineText(lineMarker.Line);
                var literalMatch = literalRegex.Match(lineContent);

                var literal = BoxcarAString(literalMatch.Value);
                var postfix = lineContent.Substring(literalMatch.Index + literal.Length + 2);

                lineContent = lineContent.Substring(0, literalMatch.Index) + literal + postfix;

                document.ReplaceText(new TextRange(lineStartOffset, lineEndOffset), lineContent);
            }
            return null;
        }

        private string BoxcarAString(string textSegmentToChange)
        {
            var segmentAfterTheChange = textSegmentToChange.Trim();
            segmentAfterTheChange = segmentAfterTheChange.Replace("\"", "");
            segmentAfterTheChange = segmentAfterTheChange.Replace(" ", "_");
            return segmentAfterTheChange;
        }

        public override string Text
        {
            get { return "Convert from spaces to underscore in string literals"; }
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            var caretOffset = _provider.CaretOffset.Offset;
            if (caretOffset > -1)
            {
                var lineMarker = _provider.Document.GetCoordsByOffset(caretOffset);
                var lineContent = _provider.Document.GetLineText(lineMarker.Line);

                var indents = GetLineIndents(lineContent);

                if (lineContent.Substring(indents).StartsWith("public") && literalRegex.IsMatch(lineContent))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetLineIndents(string lineContent)
        {
            var indents = 0;
            foreach (var character in lineContent)
            {
                if (character == ' ')
                    indents++;
                else break;
            }
            return indents;
        }
    }
}
