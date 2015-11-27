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
using System.Text.RegularExpressions;

namespace TSDev
{
	/// <summary>
	/// Summary description for CAutoComplete.
	/// </summary>
    public class CAutoComplete
	{
		public CAutoComplete()
		{
			
		}

        public class ACRet {
			public string ObjectName;
			public string FunctionName;
			public string VariableName;
		}

		private Hashtable _ht = new Hashtable();
		private Hashtable _obj = new Hashtable();

        public class ACEntry {
			public ArrayList FormattedName;
			public ArrayList FormattedDescription;
			public ArrayList FormattedParameters;

			public ArrayList UnformattedName;
			public ArrayList UnformattedDescription;
			public ArrayList UnformattedParameters;
		}

        public class ClassEntry {
			public string ClassName;
			public string ClassInheritsFrom;

			public Hashtable func_list = new Hashtable();
			public Hashtable prop_list = new Hashtable();

			internal class FuncEntry {
				public string func_name;
				public string func_params;
				public string func_descr;
				public string func_ret;
			}

			internal class PropEntry {
				public string prop_type;
				public string prop_name;
				public string prop_descr;
			}
		}

		public ACEntry ReturnParamObject(string keyword) {
			if (!_ht.ContainsKey(keyword))
				return new ACEntry();
			else
				return (ACEntry)_ht[keyword];
		}

		public bool HasParamObject(string keyword) {
			if (!_ht.ContainsKey(keyword))
				return false;
			else
				return true;
		}

		public bool HasClassObject(string obj) {
			if (!this._obj.ContainsKey(obj))
				return false;
			else
				return true;
		}

		public ClassEntry ReturnClassObject(string obj) {
			if (!_obj.ContainsKey(obj))
				return null;
			else
				return (ClassEntry)_obj[obj];
		}

		public Hashtable ReturnAll() {
			return _ht;
		}

		public Hashtable ReturnAllClasses() {
			return this._obj;
		}

		public void LoadClassData(string filename) {
			g.LogDebug("CAUTOCOMPLETE::LoadClassData: Enter - " + filename);
			_obj = new Hashtable();

			StreamReader reader = new StreamReader(filename);
			string fulldata = reader.ReadToEnd();
			reader.Close();

			Regex classes = new Regex(@"\s*class\s*(\b[A-Z_][A-Z0-9_]*\b)\s*(\:\s*public\s*(\b[A-Z][A-Z0-9]*\b)\s*)?\s*\{\s*(.*?)\};", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);

			g.LogDebug("CAUTOCOMPLETE::LoadClassData: Loading classes");
			foreach(Match m in classes.Matches(fulldata)) {
				// Define the object and declare their functions
				if (this._obj.ContainsKey(m.Groups[1].Value.ToLower()))
					continue;

				ClassEntry cls = new ClassEntry();
				cls.ClassName = m.Groups[1].Value;
				cls.ClassInheritsFrom = m.Groups[3].Value;
				
				// Run a regex on the list of functions
				Regex funcs = new Regex(@"(^\s*\/\*\!\s*(.*)\s*\*\/\n)?^\s*virtual\s*(void|bool|string|int|float|double)\s*(\b[A-Z_][A-Z0-9_]*)\s*\((.*)\)\s*\{\}\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

				foreach(Match func_m in funcs.Matches(m.Groups[4].Value)) {
					// Insert function data into the arraylists
					if (cls.func_list.ContainsKey(func_m.Groups[4].Value.ToLower()))
						continue;

					ClassEntry.FuncEntry func = new ClassEntry.FuncEntry();
					func.func_descr = func_m.Groups[2].Value;
					func.func_name = func_m.Groups[4].Value;
					func.func_ret = func_m.Groups[3].Value;
					func.func_params = func_m.Groups[5].Value;

					cls.func_list.Add(func.func_name.ToLower(), func);
				}

				// ^\s*\/\*\!\s*\n\s*\*\/\n\s*(\b[A-Z_][A-Z0-9_]+\b)\s*(\b[A-Z_][A-Z0-9_]+\b)\s*\;\s*$

				// Pull the list of parameters next
				Regex param_list = new Regex(@"^\s*\/\*\!\s*(\n)?\s*\*\/(\n)?\s*(\b[A-Z_][A-Z0-9_]+\b)\s*(\b[A-Z_][A-Z0-9_]+\b)\s*\;\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

				foreach(Match param_m in param_list.Matches(m.Groups[4].Value)) {
					// Insert parameter data into the arraylists
					if (cls.prop_list.ContainsKey(param_m.Groups[4].Value.ToLower()))
						continue;

					ClassEntry.PropEntry prop = new ClassEntry.PropEntry();
					prop.prop_name = param_m.Groups[4].Value;
					prop.prop_type = param_m.Groups[3].Value;

					cls.prop_list.Add(prop.prop_name.ToLower(), prop);
				}
				
				this._obj.Add(m.Groups[1].Value.ToLower(), cls);
			}

			// Merge the inherited functions and properties into the list:
			g.LogDebug("CAUTOCOMPLETE::LoadClassData: Merging inheritence tree");
			foreach(CAutoComplete.ClassEntry cls in this._obj.Values) {
				if (cls.ClassInheritsFrom != "")
					MergeInherited(cls, cls.ClassInheritsFrom);
			}

			g.LogDebug("CAUTOCOMPLETE::LoadClassData: Done");
		}

		private void MergeInherited(ClassEntry base_cls, string class_name) {
			if (!this._obj.ContainsKey(class_name.ToLower()))
				return;

			CAutoComplete.ClassEntry cls = (ClassEntry)this._obj[class_name.ToLower()];
			
			if (cls.ClassInheritsFrom == cls.ClassName)
				return;

			if (cls.ClassInheritsFrom != "")
				MergeInherited(cls, cls.ClassInheritsFrom);
			
			foreach(ClassEntry.FuncEntry func in cls.func_list.Values) {
				if (base_cls.func_list.ContainsKey(func.func_name.ToLower()))
					//base_cls.func_list.Remove(base_cls.func_list[func.func_name.ToLower()]);
					continue;

				base_cls.func_list.Add(func.func_name.ToLower(), func);
			}

			foreach(ClassEntry.PropEntry prop in cls.prop_list.Values) {
				if (base_cls.prop_list.ContainsKey(prop.prop_name.ToLower()))
					//base_cls.prop_list.Remove(base_cls.prop_list[prop.prop_name.ToLower()]);
					continue;

				base_cls.prop_list.Add(prop.prop_name.ToLower(), prop);
			}
		}

		public void LoadAutoComplete(string filename) {
			
			g.LogDebug("CAUTOCOMPLETE::LoadAutoComplete: Enter - " + filename);

			g.LogDebug("CAUTOCOMPLETE::LoadAutoComplete: Opening file");
			StreamReader reader = new StreamReader(filename);
					

			string linedata;
			g.LogDebug("CAUTOCOMPLETE::LoadAutoComplete: Loading function listing");
			while((linedata = reader.ReadLine()) != null) {
				// Skip comments and newlines
				if (linedata == "")
					continue;
				else if (linedata.StartsWith("#"))
					continue;

				// Split the autocomplete line data
				string [] entry = linedata.Split('|');
				
				// Skip bad lines
				if (entry.Length != 4)
					continue;

				//IParameterInfo p;
				//IListMember m;
				ACEntry acentry;

				// Check if an existing entry already exists:
				if (!_ht.ContainsKey(entry[0].ToLower())) {
					// Create a new entry:
					//p = new ParameterInfo();
					//m = p.AddMember();
					acentry = new ACEntry();
					acentry.FormattedParameters = new ArrayList();
					acentry.FormattedDescription = new ArrayList();
					acentry.FormattedName = new ArrayList();

					acentry.UnformattedDescription = new ArrayList();
					acentry.UnformattedName = new ArrayList();
					acentry.UnformattedParameters = new ArrayList();
					
					// Add it to the hashtable
					_ht.Add(entry[0].ToLower(), acentry);
				} else {
					// Take previous entry and create a new member
					acentry = (ACEntry)_ht[entry[0].ToLower()];
					//m = p.AddMember();
				}

				acentry.FormattedName.Add("<b><u>" + entry[2] + "</u> " + entry[0] + "</b>");
				acentry.UnformattedName.Add(entry[0]);
				
				// Split the parameters, if there are any
				if (entry[1].Length > 0) {
					string [] paramlist = entry[1].Split(',');
					//m.Params = new ParamInfo[paramlist.Length];
					
					string s_paramlist = "";
					string s_uparamlist = "";
					// Enumerate through each parameter and add it to the list
					for(int i = 0; i < paramlist.Length; i++) {
						// Split the current parameter entry at the space:
						string[] paramentry = paramlist[i].Split(' ');

						// Skip bad parameter entries
						if (paramentry.Length != 2)
							continue;

						//ParamInfo pentry = new ParamInfo();
						//pentry.DataType = paramentry[0];
						//pentry.Name = paramentry[1];
						//pentry.Qualifier = "System.";

						//m.Params.Add(pentry);
						s_paramlist += "<u>" + paramentry[0] + "</u> <i>" + paramentry[1] + "</i>, ";
						s_uparamlist += paramentry[0] + " " + paramentry[1] + ", ";
					}

					s_paramlist = s_paramlist.Substring(0, s_paramlist.Length - 2);
					s_uparamlist = s_uparamlist.Substring(0, s_uparamlist.Length - 2);
					acentry.FormattedParameters.Add(s_paramlist);
					acentry.UnformattedParameters.Add(s_uparamlist);
				} else {
					// Just add a blank parameter listing
					acentry.FormattedParameters.Add("<i>void</i>");
					acentry.UnformattedParameters.Add("void");
				}

                acentry.FormattedDescription.Add(entry[3]);
				acentry.UnformattedDescription.Add(entry[3]);
				//m.DataType = entry[2];		// Return type
				//m.Description = entry[3];	// Description

			} // End while
			
			// Close auto-complete file
			g.LogDebug("CAUTOCOMPLETE::LoadAutoComplete: Closing file");
			reader.Close();
		} // End function
	}
}
