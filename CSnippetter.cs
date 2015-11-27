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
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TSDev
{
	[Serializable]
	public class CSnippetter
	{
		public CSnippetter() {
			// Blank constructor
		}

		public static string ParseKeywords(ArrayList kw) {
			string final = "";
			foreach(string keyword in kw) {
				final += keyword + " ";
			}

			return final.Trim();
		}

		public static void Save(CSnippetter snippet, string filename) {
			XmlSerializer xml = new XmlSerializer(typeof(CSnippetter));
			FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);

			xml.Serialize(file, snippet);

			file.Close();
		}

		public static CSnippetter Load(string filename) {
			if (!File.Exists(filename))
				return new CSnippetter();

			XmlSerializer xml = new XmlSerializer(typeof(CSnippetter));
			FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);

			CSnippetter newsnip = null;

			try {
				newsnip = (CSnippetter)xml.Deserialize(file);
			} catch {
				newsnip = new CSnippetter();
			}

			file.Close();

			return newsnip;
		}

		[Serializable]
		public class CodeEntry {
			public CodeEntry(string title, string descr, string contents, string category) {
				this.CodeContents = contents;
				this.CodeDescr = descr;
				this.CodeTitle = title;

				if (category == "")
					category = "Code";

				this.CodeCategory = category;
			}

			public CodeEntry() {}

			public string CodeContents = "";
			public string CodeTitle = "";
			public string CodeDescr = "";
			public string CodeCategory = "";
			public ArrayList CodeKeywords = new ArrayList();
			
			public string CodeURL = "";
			public long CodeExpires = 0;
		}

		[Serializable]
		public class CodeEntryCollection : CollectionBase {
			public CodeEntryCollection () {}

			public virtual void Add(CodeEntry entry) {
				this.List.Add(entry);
			}

			public void Add(object entry) {
				this.List.Add(entry);
			}

			public virtual void Remove(CodeEntry entry) {
				this.List.Remove(entry);
			}

			public CodeEntryCollection Search(string[] keywords) {
				CodeEntryCollection ret = new CodeEntryCollection();

				foreach(CodeEntry entry in this.List) {
					foreach(string keyword in keywords) {
						if (entry.CodeKeywords.Contains(keyword.ToLower())) {
							ret.Add(entry);
							goto breakloop;
						}
					}

				breakloop:
					// Do nothing
					if (true) {}
				}

				return ret;
			}

			public int Count {
				get {
					return this.List.Count;
				}
			}

			public CodeEntry this[int Index] {
				get {
					return (CodeEntry)this.List[Index];
				}
			}
		}

		public CodeEntryCollection CodeSnippets = new CodeEntryCollection();
	}
}
