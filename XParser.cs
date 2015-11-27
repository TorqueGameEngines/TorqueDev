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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace TSDev
{


	internal class XParser 
	{


		public CAutoComplete.ACRet CodeCompletion(string linetext, int x_pos, int cctype) {
			if (cctype == 0) {
				Regex asdf = new Regex(@"\s*(\b[A-Z_][A-Z0-9_]*\b)\s*\(", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
				Match mt = asdf.Match(linetext, x_pos);
				
				//return (mt.Value.Split('(')[0].ToString());
			} else if (cctype == 1) {
				
				Regex asdf = new Regex(@"\s*?(%|\$\b[A-Z_][A-Z0-9_]*\b)?(\.)?(\b[A-Z_][A-Z0-9_]*\b)?(\b::\b)?(\b[A-Z_][A-Z0-9_]*\b).*?(?>[^()]+|\)(?<DEPTH>)|\((?<-DEPTH>))*(?(DEPTH)(?!))", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
				Match mt = asdf.Match(linetext, x_pos);

				if (mt.Success == false) {
					asdf = new Regex(@"\s*?(%|\$\b[A-Z_][A-Z0-9_]*\b)?(\.)?(\b[A-Z_][A-Z0-9_]*\b)\s*\(", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					mt = asdf.Match(linetext, x_pos);
				}
				
				//return (mt.Value.Split('(')[0].ToString());
				CAutoComplete.ACRet ret = new CAutoComplete.ACRet();
				ret.FunctionName = mt.Groups[5].Value;
				ret.ObjectName = mt.Groups[3].Value;
				ret.VariableName = mt.Groups[1].Value;

				return ret;
			} else {
				//return "";
			}

			return null;
		}

		public Hashtable MakeTokenListing(string code) {
			//System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;

			//Regex rx = new Regex(@"^(\s*//\s*(.*)\n)*?^\s*function\s*(\b[A-Z_][A-Z0-9_]*\b)\s*\((.*?)\)(\s*//\s*(.*)\s*\n)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);\
			Regex rx = new Regex(@"(^(\s*///\s*(.*?)\n){0,5})?^\s*function\s*(\b[A-Z_][A-Z0-9_]+\b)\s*\((.*?)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

			Hashtable ht = new Hashtable();

			foreach(Match mt in rx.Matches(code)) {
				if (ht.ContainsKey(mt.Groups[4].Value.ToLower()))
					continue;

				CProject.TokenKey token = new CProject.TokenKey();
                token.FuncName = mt.Groups[4].Value;
				token.LineNumber = mt.Groups[4].Index;

				if (mt.Groups[4].Value != "") {
					string[] token_params = mt.Groups[5].Value.Replace(" ", "").Split(',');
					token.FuncParams = token_params;
				} else {
					token.FuncParams = null;
				}

				//token.FuncDescr = mt.Groups[4].Value;
				string descr = mt.Groups[1].Value.Replace("///", "").Replace("\t", "").Replace("\r\n", "").Replace("\r", "").TrimStart('\n');
				
				foreach(string line in descr.Split('\n')) {
					if (line.Trim() == "")
						continue;

					token.FuncDescr += line.Trim() + "<br />";
				}

				if (token.FuncDescr != null)
					token.FuncDescr = token.FuncDescr.Substring(0, token.FuncDescr.Length - 6);


				ht.Add(token.FuncName.ToLower(), token);
			}

			//System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
			return ht;
		}

		public Hashtable MakeObjectListing(string code, CProject.File filedecl, Hashtable ht) {
			// Construct listing of objects and their functions
			Regex rx = new Regex(@"(^(\s*///\s*(.*?)\n){0,5})?^\s*\bfunction\b\s*(\b[A-Z_][A-Z0-9_]*\b)::(\b[A-Z][A-Z0-9]*\b)\s*\((.*?)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			
			//Hashtable ht = new Hashtable();

			foreach(Match mt in rx.Matches(code)) {
				CProject.TokenObject tokobj;
				if (ht.ContainsKey(mt.Groups[4].Value.ToLower()))
					tokobj = (CProject.TokenObject)ht[mt.Groups[4].Value.ToLower()];
				else {
					tokobj = new CProject.TokenObject();
					tokobj.ObjectName = mt.Groups[4].Value;
					tokobj.isInternal = false;
					ht.Add(mt.Groups[4].Value.ToLower(), tokobj);
				}

				if (!tokobj.ObjectFunctions.ContainsKey(mt.Groups[5].Value.ToLower())) {
					// Create a function definition if none exists already
					CProject.TokenObject.ObjectDescr objdescr = new CProject.TokenObject.ObjectDescr();
					objdescr.FuncName = mt.Groups[5].Value;
					objdescr.FuncOffset = mt.Groups[5].Index;
					objdescr.FuncFile = filedecl;
					
					// Set description
					string descr = mt.Groups[1].Value.Replace("///", "").Replace("\t", "").Replace("\r", "").TrimStart('\n');
					foreach(string line in descr.Split('\n')) {
						if (line.Trim() == "")
							continue;

						objdescr.FuncDescr += line.Trim() + "<br />";
					}

					// Grab all the function parameters by splitting it at the comma
					// and stick them in the appropriate array
					if (mt.Groups[4].Value != "")
						objdescr.FuncParams = mt.Groups[6].Value.Replace(" ", "").Split(',');
					else
						objdescr.FuncParams = null;

					// Add it to the hashtable
					tokobj.ObjectFunctions.Add(mt.Groups[5].Value.ToLower(), objdescr);
				}
			}

			return MakeObjectDeclarations(code, filedecl, ht);;
		}

		private Hashtable MakeObjectDeclarations(string code, CProject.File file, Hashtable curlist) {
			// Private function for enumerating all object declarations
			// which are declared like "new TypeOfObject(ObjectName) { }" ...
			// Skip objects that don't have a name.

			/*Regex rx = new Regex(@"^\s*\bnew\b\s*(\b[A-Z][A-Z0-9]*\b)\s*\((\b[A-Z_][A-Z0-9_]*\b)\)(\s*\n)?" + 
				@"(" + 
				@"(?<inner>" +
				@"(?>" + 
				@"\{(?<LEVEL>)" + 
				@"|" + 
				@"\};(?<-LEVEL>)" +
				@"|" +
				@"(?!\{|\};)." + 
				@")*" + 
				@"(?(LEVEL)(?!))" +
				@"))?"
				, RegexOptions.IgnoreCase | RegexOptions.Multiline);*/
			
			Regex rx = new Regex(@"^\s*\bnew\b\s*(\b[A-Z][A-Z0-9]*\b)\s*\((\b[A-Z_][A-Z0-9_]*\b)\)(\s*\n)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			
			foreach(Match mt in rx.Matches(code)) {
				// Enumerate each match and try to compare it to an active object

				//System.Windows.Forms.MessageBox.Show(mt.Groups["inner"].Value.ToLower());

				CProject.TokenObject tokobj;
				if (curlist.ContainsKey(mt.Groups[2].Value.ToLower()))
					tokobj = (CProject.TokenObject)curlist[mt.Groups[2].Value.ToLower()];
				else {
					tokobj = new CProject.TokenObject();
					tokobj.ObjectName = mt.Groups[2].Value;
					tokobj.isInternal = false;
					curlist.Add(mt.Groups[2].Value.ToLower(), tokobj);
				}

				tokobj.ObjectType = mt.Groups[1].Value;
				tokobj.ObjectFileDecl = file;
				tokobj.ObjectDeclOffset = mt.Groups[2].Index;
			}

			return curlist;
		}

		public Hashtable MakeTokenListingFromFile(string filename) {
			try {
				StreamReader reader = new StreamReader(filename);
				string file_contents = reader.ReadToEnd();

				reader.Close();

				return MakeTokenListing(file_contents);
			} catch (Exception exc) { System.Windows.Forms.MessageBox.Show(exc.Message); return new Hashtable(); };
		}

		public Hashtable MakeObjectListingFromFile(string filename, CProject.File file, Hashtable ht) {
			try {
				StreamReader reader = new StreamReader(filename);
				string file_contents = reader.ReadToEnd();

				reader.Close();

				return this.MakeObjectListing(file_contents, file, ht);
			} catch /*(Exception exc)*/ { /*System.Windows.Forms.MessageBox.Show(exc.Message);*/ return ht; };
		}

		public int FindToken(string word, string prevtok, out CProject.File out_file) {
			out_file = null;

			// Find it in the file listing first
			foreach(CProject.File file in g.Project.FileList) {
				if (file.TokenList.ContainsKey(word.ToLower())) {
					// Got it!
					out_file = file;
					return ((CProject.TokenKey)file.TokenList[word.ToLower()]).LineNumber;
				}
			}

			// Now search active objects, seeing if this is an object
			// we can jump to.
			if (g.Project.TokenObjList.ContainsKey(word.ToLower())) {
				// Yes, it appears this is a valid object...
				CProject.TokenObject tokobj = (CProject.TokenObject)g.Project.TokenObjList[word.ToLower()];
				if (tokobj.ObjectFileDecl != null) {
					out_file = tokobj.ObjectFileDecl;
					return tokobj.ObjectDeclOffset;
				} else {
					return -1;
				}
			}


			// Check to see if the *previous* token wasn't an object...
			// ... which would mean that the *current* token is a function
			// definition inside an object
			if (g.Project.TokenObjList.ContainsKey(prevtok.ToLower())) {
				// Yes, it appears this is so... see if the thingy is a valid function
				CProject.TokenObject tokobj = (CProject.TokenObject)g.Project.TokenObjList[prevtok.ToLower()];

				if (!tokobj.ObjectFunctions.ContainsKey(word.ToLower()))
					return -1;

				// Retrieve the function file information
				CProject.TokenObject.ObjectDescr func = (CProject.TokenObject.ObjectDescr)tokobj.ObjectFunctions[word.ToLower()];
				out_file = func.FuncFile;
				return func.FuncOffset;
			}

			return -1;
		}

	}
}
