/*

This file is part of TorqueDev.

TorqueDev is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by the 
Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

TorqueDev is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with TorqueDev.  If not, see <http://www.gnu.org/licenses>

EXCEPTIONS TO THE GPL: TorqueDev links in a number of third party libraries,
which are exempt from the license.  If you want to write closed-source
third party modules that you are going to link into TorqueDev, you may do so
without restriction.  I acknowledge that this technically allows for
one to bypass the open source provisions of the GPL, but the real goal
is to keep the core of TorqueDev free and open.  The rest is up to you.

*/

using System;
using System.Collections;
using System.Diagnostics;
using ActiproSoftware.SyntaxEditor;

namespace TSDev
{
	/// <summary>
	/// Summary description for CSemantecParser.
	/// </summary>
	internal class CSemanticParser : ActiproSoftware.SyntaxEditor.SemanticDefaultParser {
		public CSemanticParser() {
		}
		
		public override AutomaticOutliningSupportType AutomaticOutliningSupportType {
			get {
				return AutomaticOutliningSupportType.SingleLanguageOnly;
			}
		}

		public override void GetTokenOutliningAction(TokenStream tokenStream, ref string outliningKey, ref OutliningNodeAction tokenAction) {
			Token token = tokenStream.Peek();

			if ((token.Key == "OpenCurlyBraceToken" || token.Key == "CloseCurlyBraceToken") && g.Config.b_Ed_CodeFold == false)
				return;
			
			switch (token.Key) {
				case "OpenCurlyBraceToken":
					outliningKey = "CodeBlock";
					tokenAction = OutliningNodeAction.Start;
					break;
				case "CloseCurlyBraceToken":
					outliningKey = "CodeBlock";
					tokenAction = OutliningNodeAction.End;
					break;
				case "RegionStartToken":
					outliningKey = "CodeRegion";
					tokenAction = OutliningNodeAction.Start;
					break;
				case "RegionEndToken":
					outliningKey = "CodeRegion";
					tokenAction = OutliningNodeAction.End;
					break;
			}
		}

		public override void PostParse(Document document, DocumentModification modification) {
			if (modification.HasFlag(DocumentModificationFlags.ProgrammaticTextParse)) {
				if (g.Config.bAutoCollapse)
					document.Outlining.RootNode.CollapseDescendants("CodeRegion");
			}
		}

		public override void SetOutliningNodeCollapsedText(OutliningNode node) {
			TokenCollection tokens = node.Document.Tokens;
			int tokenIndex = tokens.IndexOf(node.StartOffset);
			
			switch(tokens[tokenIndex].Key) {
				case "RegionStartToken":
					string collapsedText = "";
					while(++tokenIndex < tokens.Count) {
						if (tokens[tokenIndex].Key == "RegionTokenEnd")
							break;

						collapsedText += tokens.Document.GetTokenText(tokens[tokenIndex]);
					}

					if (collapsedText != "")
						collapsedText = "   " + collapsedText.Trim() + "   ";
					else
						collapsedText = "   ...   ";

					node.CollapsedText = collapsedText;
					break;
			}
		}
	}
}
