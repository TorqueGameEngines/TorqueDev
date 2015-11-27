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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TSDev
{
	public delegate void d_OnExplorerModified();

	[Serializable]
	public class CProject : ISerializable, IDisposable
	{
		public string ProjectName = "";
		public string ProjectPath = "";

		[DllImport("shlwapi.dll", CharSet=CharSet.Auto)]
		private static extern bool PathRelativePathTo(
			[Out] string pszPath,
			[In] string pszFrom,
			[In] uint dwAttrFrom,
			[In] string pszTo,
			[In] uint dwAttrTo
			);

		[Serializable]
		public class DirectoryCollection : CollectionBase, ICloneable {
			public virtual void Add(Directory dir) {
				if (!this.List.Contains(dir))
					this.List.Add(dir);
			}

			public virtual void Remove(Directory dir) {
				if (this.List.Contains(dir))
					this.List.Remove(dir);
			}

			public void Sort() {
				this.InnerList.Sort();
			}

			public Directory this[int index] {
				get { return (Directory)this.List[index]; }
				set { this.List[index] = value; }
			}

			public bool Contains(Directory dir) {
				return this.List.Contains(dir);
			}

			#region ICloneable Members

			public object Clone() {
				g.LogDebug("CPROJECT::DirectoryCollection::Clone: Enter");
				DirectoryCollection clonedCollection = new DirectoryCollection();
				clonedCollection.InnerList.AddRange((ICollection)this.InnerList.Clone());
				return clonedCollection;
			}

			#endregion
		}

		[Serializable]
        public class Directory : IComparable, ICloneable {
			public Directory() {
				// Do nothing for default constructor
			}

			public Directory(string name, Directory parent, bool isExpanded) {
				this._name = name;
				this.parent = parent;
				this.isExpanded = isExpanded;
			}

			public Directory(string name, Directory parent) {
				this._name = name;
				this.parent = parent;
				this.isExpanded = true;
			}

			public string _name;
			public Directory parent;
			public bool isExpanded;

			[NonSerialized]
			public bool isForeign = false;

			public string name {
				get {
					return _name;
				}

				set {
					_name = value;
					/*// Automatically rename the directory if it's being renamed
					if (System.IO.Directory.Exists(this.RelativePath + "\\" + value)) {
						MessageBox.Show("The directory cannot be renamed: Another directory with this name already exists.", "Rename Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}

					try {
						System.IO.Directory.SetCurrentDirectory(g.Project.ProjectPath);
						System.IO.Directory.Move(Path.GetFullPath(this.RelativePath), this.RelativePath + "\\" + value);
						_name = value;
					} catch { }*/
				}
			}

			public string RelativePath {
				get {
					// A recursive function to determining the relative
					// path of a directory, based from the project root
					if (parent != null) {
						return parent.RelativePath + @"\" + name;
					} else {
						return @".\\";
					}
				}
			}

			public Plugins.CWDirectory ToCWDir() {
				return new TSDev.Plugins.CWDirectory(
					this.GetHashCode(),
					name,
					((parent == null) ? null : parent.ToCWDir())
				);
			}
			

			#region IComparable Members

			public int CompareTo(object obj) {
				if (obj is CProject.Directory)
					return String.Compare((obj as CProject.Directory).name, this.name, true) * -1;
				else
					return 0;
			}

			#endregion

			#region ICloneable Members

			public object Clone() {
				return this.MemberwiseClone();				
			}

			#endregion
		}

		[Serializable]
		public class FileCollection : CollectionBase, IEnumerable, ICloneable {
			public virtual void Add(File file) {
				if (!this.List.Contains(file))
					this.List.Add(file);
			}

			public virtual void Remove(File file) {
				if (this.List.Contains(file))
					this.List.Remove(file);
			}

			public void Sort() {
				this.InnerList.Sort();
			}

			public File this[int index] {
				get { return (File)this.List[index]; }
				set { this.List[index] = value; }
			}

			public bool Contains(File file) {
				return this.List.Contains(file);
			}

			public bool ContainsFile(string FileName) {
				foreach (CProject.File file in this.InnerList) {
					if (file.RelativePath.ToLower() == FileName.ToLower())
						return true;
				}

				return false;
			}

			public File GetFile(string FileName) {
				foreach (CProject.File file in this.InnerList) {
					if (file.RelativePath.ToLower() == FileName.ToLower())
						return file;
				}

				return null;
			}

			public void CleanUpForeigns() {
				// If there are any files marked "foreign", this
				// will clean them out of the listing
				ArrayList todelete = new ArrayList();

				foreach (File file in this.InnerList) {
					if (file.isForeign)
						todelete.Add(file);
				}

				foreach (File file in todelete)
					this.List.Remove(file);
			}

			#region IEnumerable Members
			IEnumerator IEnumerable.GetEnumerator() {
				foreach (File file in this.List) {
					if (file.isForeign) {
						continue;
					} else {
						yield return file;
					}
				}
			}
			#endregion

			#region ICloneable Members

			public object Clone() {
				g.LogDebug("CPROJECT::FileCollection::Clone: Enter");
				FileCollection clonedCollection = new FileCollection();
				clonedCollection.InnerList.AddRange((ICollection)this.InnerList.Clone());
				return clonedCollection;
			}

			#endregion
		}

		[Serializable]
        public class File : IComparable, ICloneable {
			public File() {
				// Nothing for default constructor
			}

			public File(string simplename, string relativepath, Directory parent) {
				this._simplename = simplename;
				this.RelativePath = relativepath;
				this.ParentDir = parent;
			}

			public File(string simplename, string relativepath, bool foreign) {
				this._simplename = simplename;
				this.RelativePath = relativepath;
				this.ParentDir = null;
				this.isForeign = foreign;
			}

			public File(string simplename, string relativepath, bool foreign, bool text, Directory parent) {
				this._simplename = simplename;
				this.RelativePath = relativepath;
				this.ParentDir = null;
				this.isForeign = foreign;
				this.isText = text;
				this.ParentDir = parent;
			}

			private string _simplename;

			public string SimpleName {
				// Automatically rename the file when setting
				get {
					if (_simplename == null || _simplename == "")
						_simplename = Path.GetFileName(RelativePath);

					return _simplename; 
				}
				set {
					// Attempt to rename
					g.LogDebug("CPROJECT::File::FileRename: Enter - " + _simplename);
					string fullpath = Path.GetFullPath(RelativePath);

					if (System.IO.File.Exists(Path.GetDirectoryName(fullpath) + "\\" + value)) {
						g.LogDebug("CPROJECT::File::FileRename: Can't rename; conflicts");
						MessageBox.Show("Unable to rename file: Another file with that name already exists.", "Rename Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}

					try {
						g.LogDebug("CPROJECT::File::FileRename: Exec rename");
						System.IO.Directory.SetCurrentDirectory(g.Project.ProjectPath);

						System.IO.File.Move(fullpath, Path.GetDirectoryName(fullpath) + "\\" + value);
						this._simplename = value;
						this.RelativePath = CProject.PathGetRelative(Path.GetDirectoryName(fullpath) + "\\" + value, g.Project.ProjectPath);
					} catch { }
				}
			}

			public string RelativePath;
			public Directory ParentDir = new Directory();
			public bool isDirty = false;
			public bool isPendingReload = false;

            [NonSerialized]
			public bool isForeign = false;

			[NonSerialized]
			public bool isForcedReload = false;

			[NonSerialized]
			public int FileIcon = 2;

			[NonSerialized]
			public bool isFileReadonly = false;

			public bool isText = false;

			public ArrayList IndicatorList = new ArrayList();
			public Hashtable TokenList = new Hashtable();

			public virtual bool Save() { return false; }
			public virtual void SaveCommit() {}
			
			public bool isInProject {
				get {
					if (g.Project == null)
						return false;
					else
						return g.Project.FileList.Contains(this);
				}
			}

			public Plugins.CWFile ToCWFile() {
				Plugins.CWDirectory dir = ((ParentDir == null) ? null : ParentDir.ToCWDir());

				return new TSDev.Plugins.CWFile(
					this.GetHashCode(),
					SimpleName,
					RelativePath,
					dir,
					isDirty,
					isForeign,
					isForcedReload,
					isPendingReload,
					FileIcon
				);
			}

			#region IComparable Members

			public int CompareTo(object obj) {
				if (obj is CProject.File)
					return String.Compare((obj as CProject.File).SimpleName, this.SimpleName, true) * -1;
				else
					return 0;
			}

			#endregion


			#region ICloneable Members

			public object Clone() {
				return this.MemberwiseClone();
			}

			#endregion
		}

		#region Breakpoint Code


		// Breakpoint Stuff
		[Serializable]
        public class BreakpointCollection : CollectionBase, ICloneable {
			public virtual void Add(Breakpoint entry) {
				// Check if the breakpoint already exists and remove it
				if (HasBreakpointAt(entry.file, entry.LineNumber))
					RemoveAtLine(entry.file, entry.LineNumber);

				if (g.IsDebugging) {
					// Tell the debugger we're adding a breakpoint
					g.LogDebug("CPROJECT::BreakpointCollection::Add: Send breakpoint to debugger");
					string filerelpath = CProject.Breakpoint.FixPath(CProject.PathGetRelative(Path.GetFullPath(entry.file.RelativePath), Path.GetDirectoryName(Path.GetFullPath(g.Project.DebugExe))));
					frmMain.stc_DebugQueue.Add("BRKSET " + filerelpath + " " + Convert.ToString(entry.LineNumber + 1) + " false " + entry.PassCount.ToString() + " " + entry.Conditional + "\n");
				}

				this.List.Add(entry);
			}

			public virtual void Remove(Breakpoint entry) {
				if (g.IsDebugging) {
					// Tell the program that we're clearing a breakpoint
					g.LogDebug("CPROJECT::BreakpointCollection::Remove: Clear breakpoint from debugger");
					string filerelpath = CProject.Breakpoint.FixPath(CProject.PathGetRelative(Path.GetFullPath(entry.file.RelativePath), Path.GetDirectoryName(Path.GetFullPath(g.Project.DebugExe))));
					frmMain.stc_DebugQueue.Add("BRKCLR " + filerelpath + " " + Convert.ToString(entry.LineNumber + 1) + "\n");
				}

				this.List.Remove(entry);
			}

			public void RemoveAtLine(CProject.File file, int LineNum) {
				foreach(Breakpoint brk in this.List) {
					if (brk.file == file && brk.LineNumber == LineNum) {
						Remove(brk);
						return;
					}
				}
			}

			public bool HasBreakpointAt(CProject.File file, int LineNum) {
				return HasIdentical(new CProject.Breakpoint(file, LineNum));
			}

			public virtual bool Contains(Breakpoint entry) {
				return this.List.Contains(entry);
			}

			public virtual bool HasIdentical(Breakpoint entry) {
				foreach(Breakpoint brk in this.List) {
					if (brk.file == entry.file && brk.LineNumber == entry.LineNumber)
						return true;
				}

				return false;
			}

			public Breakpoint this[CProject.File file, int LineNum] {
				get {
					foreach(Breakpoint brk in this.List) {
						if (brk.file == file && brk.LineNumber == LineNum)
							return brk;
					}

					return null;
				}
			}

			public virtual Breakpoint this[int Index] {
				get {
					return (Breakpoint)this.List[Index];
				}
			}

			public object Clone() {
				object newcoll = this.MemberwiseClone();
				return newcoll;
			}
		}

		[Serializable]
        public class Breakpoint {
			public static string FixPath(string path) {
				// Turn a path into a Torque-represented path
				path = path.Substring(2, path.Length - 2).Replace("\\", "/");
				return path;
			}

			public Breakpoint(CProject.File file, int LineNumber) {
				this.file = file;
				this.LineNumber = LineNumber;
				this.Enabled = true;
			}

			public Breakpoint(CProject.File file, int LineNumber, int PassCount) {
				this.file = file;
				this.LineNumber = LineNumber;
				this.Enabled = true;
				this.PassCount = PassCount;
			}

			public Breakpoint(CProject.File file, int LineNumber, int PassCount, string Conditional) {
				this.file = file;
				this.LineNumber = LineNumber;
				this.Enabled = true;
				this.PassCount = PassCount;
				this.Conditional = Conditional;
			}

			public bool Sent = false;
			public File file = null;
			public int LineNumber = 0;
			public bool Enabled = false;
			public int PassCount = 0;
			public string Conditional = "true";
		}
		#endregion

		// Macro stuff
		[Serializable]
		public struct SerializedMacro {
			public string MacroName;
			public string MacroXML;
			public int MacroNum;
		}

        public class Macro {
			public Macro(string macro_name, ActiproSoftware.SyntaxEditor.Commands.MacroCommand macro_cmd) {
				this.MacroName = macro_name;
				this.MacroCmd = macro_cmd;
				this.MacroNum = -1;
			}

			public string MacroName;
			public ActiproSoftware.SyntaxEditor.Commands.MacroCommand MacroCmd;
			public int MacroNum;
		}

		[Serializable]
        public class TokenKey {
			public TokenKey() { }
			public TokenKey(string FuncName, string[] FuncParams, string FuncDescr, int LineNumber) {
				this.FuncName = FuncName;
				this.FuncDescr = FuncDescr;
				this.FuncParams = FuncParams;
				this.LineNumber = LineNumber;
			}

			public string FuncName;
			public string[] FuncParams;
			public string FuncDescr;
			public int LineNumber;
		}

		[Serializable]
        public class TokenObject {
			public TokenObject() {}

			[Serializable]
            public class ObjectDescr {
				public string FuncName;
				public string[] FuncParams;
				public string FuncDescr;
				public CProject.File FuncFile;
				public int FuncOffset = -1;
			}

			
			public string ObjectName;
			public string ObjectType;

			public Hashtable ObjectFunctions = new Hashtable();
			public ArrayList ObjectProperties = new ArrayList();

			public bool isInternal = false;
			public CProject.File ObjectFileDecl = null;
			public int ObjectDeclOffset = -1;

			public void RemoveFileRefs(CProject.File file) {
				Monitor.Enter(ObjectFunctions);
				Monitor.Enter(ObjectProperties);

				Hashtable _temp_ht = (Hashtable)ObjectFunctions.Clone();

				IDictionaryEnumerator enumer = _temp_ht.GetEnumerator();

				while(enumer.MoveNext()) {
					if (((ObjectDescr)enumer.Value).FuncFile.Equals(file))
						ObjectFunctions.Remove(enumer.Key);
				}
				

				if ((ObjectFileDecl != null) && ObjectFileDecl.Equals(file)) {
					ObjectFileDecl = null;
					ObjectDeclOffset = -1;
				}

				Monitor.Exit(ObjectFunctions);
				Monitor.Exit(ObjectProperties);
			}
		}

        public class TokenizerQueue {
			public CProject.File file;
			public string code;
			public bool needFile;
		}

		public CProject() { }

		#region Deserialization Constructor
		protected CProject(SerializationInfo info, StreamingContext context) {
			//this.FileList = (ArrayList)info.GetValue("FileList", typeof(ArrayList));
			//this.DirList = (ArrayList)info.GetValue("DirList", typeof(ArrayList));

			g.LogDebug("CPROJECT::ctor: Enter (Deserialization)");

			try {
				g.LogDebug("CPROJECT::ctor: Trying to get file version");
				int proj_filever = (int)info.GetValue("___PROJECT_FILE_VERSION", typeof(int));

				if (proj_filever != 1) {
					g.LogDebug("CPROJECT::ctor: Failed");
					throw new Exception("The project is from an older version of TorqueDev / Codeweaver that is completely incompatible with the current version.");
				}

			} catch {
				g.LogDebug("CPROJECT::ctor: Failed");
				throw new Exception("The project is from an older version of TorqueDev / Codeweaver that is completely incompatible with the current version.");
			}

			/* Try to load the new file collection data; if not, we need to convert */
			try {
				g.LogDebug("CPROJECT::ctor: Load directory and file information");
				this.FileList = (FileCollection)info.GetValue("FileList_V2", typeof(FileCollection));
				this.DirList = (DirectoryCollection)info.GetValue("DirList_V2", typeof(DirectoryCollection));
			} catch {
				g.LogDebug("CPROJECT::ctor: Failed; need to update project file");
				DialogResult result = MessageBox.Show("This project contains data from an older version of TorqueDev / Codewaver (namely, the " +
					"way projects store file and directory data has changed).  Do you want to update your project file to the latest version? " +
					"(Note: Selecting 'no' will leave you with a blank project)", "Project Conversion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

				this.FileList = new FileCollection();
				this.DirList = new DirectoryCollection();

				if (result == DialogResult.Yes) {
					// Convert the old data
					ArrayList oldfiles = (ArrayList)info.GetValue("FileList", typeof(ArrayList));
					ArrayList olddirs = (ArrayList)info.GetValue("DirList", typeof(ArrayList));

					foreach (CProject.File oldfile in oldfiles)
						this.FileList.Add(oldfile);

					foreach (CProject.Directory olddir in olddirs)
						this.DirList.Add(olddir);
				}
			}

			g.LogDebug("CPROJECT::ctor: Loading project properties");
			//this._TokenObjList = (Hashtable)info.GetValue("TokenObject", typeof(Hashtable));
			this.ProjectName = (string)info.GetValue("#ProjectName#", typeof(string));
			//this.ProjectPath = (string)info.GetValue("#FullRootPath#", typeof(string));
			this.ProjectPath = System.IO.Directory.GetCurrentDirectory();

			// Try to load the debugger data; if nothing, just quit
			try {
				g.LogDebug("CPROJECT::ctor: Loading debugger properties");
				this.DebugEnabled = (bool)info.GetValue("DebugEn", typeof(bool));
				this.DebugExe = (string)info.GetValue("DebugExe", typeof(string));
				this.DebugParams = (string)info.GetValue("DebugParams", typeof(string));
				this.DebugPasswd = (string)info.GetValue("DebugPasswd", typeof(string));
				this.DebugPort = (int)info.GetValue("DebugPort", typeof(int));
			} catch {}

			// Try to load the last open file list; if not, just quit
			try {
				g.LogDebug("CPROJECT::ctor: Loading previous state");
				this.OpenFiles = (FileCollection)info.GetValue("OpenFiles", typeof(FileCollection));
				this.OpenFilesState = (byte[])info.GetValue("OpenFilesState", typeof(byte[]));
			} catch {}
			
			// Deserialize the macro list
			try {
				g.LogDebug("CPROJECT::ctor: Reloading macros");
				ArrayList macrolist = new ArrayList();
				this.MacroList = new Hashtable();
				macrolist = (ArrayList)info.GetValue("SerializedMacroList", typeof(ArrayList));

				foreach(CProject.SerializedMacro sermac in macrolist) {
					MemoryStream mem = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(sermac.MacroXML));
					System.Xml.XmlTextReader xmlread = new System.Xml.XmlTextReader(mem);
					
					ActiproSoftware.SyntaxEditor.Commands.MacroCommand newcmd = new ActiproSoftware.SyntaxEditor.Commands.MacroCommand();
					newcmd.ReadFromXml(xmlread);

					xmlread.Close();
					mem.Close();
					
					CProject.Macro newmac = new CProject.Macro(sermac.MacroName, newcmd);
					newmac.MacroNum = sermac.MacroNum;

					this.MacroList.Add(newmac.MacroName, newmac);
				}
			} catch (Exception exc) {
				g.LogDebug("CPROJECT::ctor: Failed");
				MessageBox.Show("Error in loading saved macros.  Some macros may not be available.\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			// Try to see if the project type is set
			try {
				g.LogDebug("CPROJECT::ctor: Loading project type");
				this.ProjectType = Convert.ToInt16(info.GetValue("#ProjectType#", typeof(short)));
			} catch {
				g.LogDebug("CPROJECT::ctor: Failed");
				MessageBox.Show("This project is from an older version of TorqueDev / Codeweaver and does not include a project type definition.  It has automatically been set to \"TGE\".  You will need to edit the project properties to change the project type to the appropriate value.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.ProjectType = 0;
			}

			// Grab the variable watch list if we can
			try {
				g.LogDebug("CPROJECT::ctor: Loading watch list");
				this.VarWatchList = (ArrayList)info.GetValue("WatchVars", typeof(ArrayList));
			} catch {}

			// Take care of the new debug options
			try {
				g.LogDebug("CPROJECT::ctor: Loading debug auto insert info");
				this.DebugAutoInsert = Convert.ToBoolean(info.GetValue("DebugAutoInsert", typeof(bool)));
				this.DebugMainCs = (string)(info.GetValue("DebugMainCs", typeof(string)));
			} catch {
				// Reset the debugging info if we've failed to 
				// convert the new debug settings
				MessageBox.Show("This project does not contain extended debug information (due to conversion from an older version of TorqueDev). " +
					"The project's debug information will be cleared.  You can run the new Debug Wizard available from the \"Debug\" menu to " +
					"define the debugger settings.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DebugExe = "";
				this.DebugEnabled = false;
				this.DebugParams = "";
				this.DebugPasswd = "";
				this.DebugPort = 8777;
				this.DebugAutoInsert = false;
				this.DebugMainCs = "";
			}

			// See if we can get the last project finds/replaces
			try {
				g.LogDebug("CPROJECT::ctor: Loading finds/replaces");
				this.Finds = (ArrayList)info.GetValue("RecentFinds", typeof(ArrayList));
				this.Replaces = (ArrayList)info.GetValue("RecentReplaces", typeof(ArrayList));
			} catch { }

			// Clean up any foreign files that are left in the project
			g.LogDebug("CPROJECT::ctor: Remove foreign files");
			FileList.CleanUpForeigns();
		}
		#endregion

		public void RemoveObjectsInFile(CProject.File file) {
			g.LogDebug("CPROJECT::RemoveObjectsInFile: Enter - " + file.RelativePath);

			foreach(CProject.TokenObject tokobj in this.TokenObjList.Values)
				tokobj.RemoveFileRefs(file);
		}
		
		public Hashtable MacroList = new Hashtable();
		public string DebugExe = "";
		public string DebugPasswd = "";
		public int DebugPort = 8777;
		public volatile bool DebugEnabled = false;
		public string DebugParams = "";

		public FileCollection OpenFiles = new FileCollection();
		public byte[] OpenFilesState = null;
		
		public short ProjectType = 0;
		public Hashtable PluginConfigs = new Hashtable();
		public BreakpointCollection ProjectBreaks = new BreakpointCollection();

		/* New debug stuff (1.2 D7) */
		public string DebugMainCs = "";
		public bool DebugAutoInsert = false;

		/* New project find/replace stuff */
		public ArrayList Finds = new ArrayList();
		public ArrayList Replaces = new ArrayList();
		/* END */
		
		/* Not stored in file */
		public Hashtable ErrorList = new Hashtable();
		public ArrayList VarWatchList = new ArrayList();
		/* END */

		//public volatile ArrayList FileList = new ArrayList();
		//public volatile ArrayList DirList = new ArrayList();
		public volatile FileCollection FileList = new FileCollection();
		public volatile DirectoryCollection DirList = new DirectoryCollection();

		private volatile Hashtable _TokenObjList = new Hashtable();
		private volatile TreeNode _public_tree = new TreeNode();
		private volatile TreeNode _private_tree = new TreeNode();
		private volatile ArrayList __tokenizer_queue = new ArrayList();
		private volatile bool __needs_rehash = false;

		private Thread t_TokenQueue;

		#region Property Declarations
		public Hashtable TokenObjList {
			get { return _TokenObjList; }
			set { 
				_TokenObjList = value; 
				
				//if (onExplorerModified != null)
				//	onExplorerModified();
			}
		}

		public TreeNode PublicTreeListing {
			get { return _public_tree; }
		}

		public TreeNode PrivateTreeListing {
			get { return _private_tree; }
		}
		#endregion

		public void StartThread() {
			g.LogDebug("CPROJECT::StartThread: Enter");
			if (t_TokenQueue != null)
				return;

			g.LogDebug("CPROJECT::StartThread: Starting tokenizer thread");
			t_TokenQueue = new Thread(new ThreadStart(this.TokenizerThread));
			t_TokenQueue.Start();
		}

		public void StopThread() {
			g.LogDebug("CPROJECT::StopThread: Enter");
			if (t_TokenQueue == null)
				return;

			g.LogDebug("CPROJECT::StopThread: Aborting tokenizer thread");
			t_TokenQueue.Abort();
			t_TokenQueue.Join(5000);
			t_TokenQueue = null;
		}

		public void DisseminateLoadInformation(CAutoComplete ac) {
			g.LogDebug("CPROJECT::DisseminateLoadInformation: Enter");
			this.RebuildPrimaryTree(ac);
			this.RebuildVisibleTree();
		}

		public void _Queue(CProject.TokenizerQueue tq) {
			g.LogDebug("CPROJECT::_Queue: Enqueing " + tq.file.RelativePath);
			this.__tokenizer_queue.Add(tq);
		}

		public Plugins.CWProject ToCWProject() {
			return new Plugins.CWProject(
				this.ProjectName, this.ProjectPath, this.DebugExe, this.DebugPasswd, this.DebugPort,
				this.DebugEnabled, this.DebugParams,
				((this.ProjectType == 0) ? Plugins.ProjectType.TGE : ((this.ProjectType == 1)) ? Plugins.ProjectType.TSE : TSDev.Plugins.ProjectType.T2D),
				this.DebugMainCs, this.DebugAutoInsert
			);
		}

		public static string PathGetRelative(string AbsolutePath, string StartFromDir) {
			const UInt32 FILE_ATTRIBUTE_DIRECTORY = 0x10;
			UInt32 dwAttr1 = FILE_ATTRIBUTE_DIRECTORY;
			UInt32 dwAttr2 = 0;

			string relative_path = "".PadLeft(260, '\0');
			PathRelativePathTo(relative_path, StartFromDir, dwAttr1, AbsolutePath, dwAttr2);

			return relative_path.Trim('\0');
		}

		public static void SaveProject(string filename, CProject project) {
			g.LogDebug("CPROJECT::SaveProject: Enter");

			try {
				g.LogDebug("CPROJECT::SaveProject: Saving to " + filename);
				FileStream fstream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
				BinaryFormatter fmtBinary = new BinaryFormatter();

				g.LogDebug("CPROJECT::SaveProject: Serializing");
				fmtBinary.Serialize(fstream, project);

				g.LogDebug("CPROJECT::SaveProject: Closing");
				fstream.Close();
			} catch (Exception exc) {
				g.LogDebug("CPROJECT::SaveProject: Failed: " + exc.Message);
				MessageBox.Show("An error occurred while attempting to save your project:\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		public static CProject LoadProject(string filename) {
			g.LogDebug("CPROJECT::LoadProject: Enter");

			FileStream fstream;
			try {
				g.LogDebug("CPROJECT::LoadProject: Opening " + filename);
				 fstream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
			} catch (Exception exc) {
				g.LogDebug("CPROJECT::LoadProject: Failed: " + exc.Message);
				System.Windows.Forms.MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
				return null; 
			}

			BinaryFormatter fmtBinary = new BinaryFormatter();

			try {
				g.LogDebug("CPROJECT::LoadProject: Deserializing");
				CProject project = (CProject)fmtBinary.Deserialize(fstream);

				g.LogDebug("CPROJECT::LoadProject: Closing");
				fstream.Close();

				return project;
			} catch (Exception exc) {
				g.LogDebug("CPROJECT::LoadProject: Failed: " + exc.Message);
				g.LogDebug("CPROJECT::LoadProject: Closing");

				fstream.Close(); 
				throw new Exception(exc.InnerException.Message); 
			}
			
		}
		
		public void TokenizerThread() {
			// Calls tokenization functions from
			Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
			XParser parser = new XParser();

			g.LogDebug("CPROJECT::TokenizerThread: INIT thread");
			while(Thread.CurrentThread.ThreadState == ThreadState.Running) {
				if (Thread.CurrentThread.ThreadState != ThreadState.Running) {
					g.LogDebug("CPROJECT::TokenizerThread: Aborted; breaking");
					break;
				}

				//Thread.Sleep(0);
				Thread.Sleep(1000);

				if (this.__tokenizer_queue.Count == 0) {
					if (this.__needs_rehash) {
						g.LogDebug("CPROJECT::TokenizerThread: __needs_rehash");
						this.__needs_rehash = false;
						g.Main.InitExplorer();
					}
					continue;
				}
				
				while (this.__tokenizer_queue.Count != 0) {
					CProject.TokenizerQueue tq = (CProject.TokenizerQueue)this.__tokenizer_queue[0];

					g.LogDebug("CPROJECT::TokenizerThread: Got " + tq.file.RelativePath + " for tokenization");

					// Do not deal with files that are "foreign" or "text"
					if (tq.file.isForeign || tq.file.isText) {
						g.LogDebug("CPROJECT::TokenizerThread: Foreign/text; return");
						this.__tokenizer_queue.RemoveAt(0);
						continue;
					}
					Monitor.Enter(this.TokenObjList);
					Monitor.Enter(tq.file);

					foreach (CProject.TokenObject tokobj in this.TokenObjList.Values) {
						tokobj.RemoveFileRefs(tq.file);
					}

					if (tq.needFile) {
						System.IO.Directory.SetCurrentDirectory(this.ProjectPath);
						tq.file.TokenList = frmMain.parser.MakeTokenListingFromFile(tq.file.RelativePath);
						this._TokenObjList = frmMain.parser.MakeObjectListingFromFile(tq.file.RelativePath, tq.file, this._TokenObjList);
					} else {
						tq.file.TokenList = frmMain.parser.MakeTokenListing(tq.code);
						this._TokenObjList = frmMain.parser.MakeObjectListing(tq.code, tq.file, this._TokenObjList);
					}

					Monitor.Exit(this.TokenObjList);
					Monitor.Exit(tq.file);


					RebuildVisibleTree();
					//RefreshEditorDropdowns();


					this.__tokenizer_queue.RemoveAt(0);
					this.__needs_rehash = true;

					//System.GC.Collect(System.GC.MaxGeneration);
				}
			}
		}

		public void ScanProject() {

		}

		private void RebuildPrimaryTree(CAutoComplete ac) {
			//CAutoComplete ac = new CAutoComplete();
			//ac.LoadAutoComplete(System.Windows.Forms.Application.StartupPath + "\\autocomplete.txt");

			TreeNode public_ns = new TreeNode("Public Exports", 0, 0);

			foreach(CAutoComplete.ACEntry entry in ac.ReturnAll().Values) {
				for (int i = 0; i < entry.FormattedName.Count; i++) {
					TreeNode new_node = new TreeNode(entry.UnformattedName[i].ToString() + " (" + entry.UnformattedParameters[i].ToString() + ")", 1, 1); // [" + entry.UnformattedDescription[i].ToString() + "]" , 1, 1);
					new_node.Tag = entry;

					public_ns.Nodes.Add(new_node);
				}
			}

			foreach(CAutoComplete.ClassEntry cls in ac.ReturnAllClasses().Values) {
				TreeNode new_node = new TreeNode(cls.ClassName, 4, 4);
				new_node.Tag = cls;

				public_ns.Nodes.Add(new_node);
			}

			public_ns.Expand();

			this._public_tree = public_ns;
		}

		private void RefreshEditorDropdowns() {
			/*foreach(Crownwood.DotNetMagic.Controls.TabPage tab in g.Main._al_Documents) {
				if (tab.Control is UCEditor) {
					(tab.Control as UCEditor).Invoke((tab.Control as UCEditor)._DropDownMake, new object[0] {});
				}
			}*/
			foreach (PrimaryTab<UCEditor> ed in g.Editors)
				ed.Control.Invoke(ed.Control._DropDownMake, new object[0] { });

		}

		public void RescanProjectObjects() {

			// Re-queue all the files in the project for processing
			// First clear all the objects in the active files
			foreach (CProject.File file in this.FileList) {
				RemoveObjectsInFile(file);
			}

			// Now requeue all the files
			foreach(CProject.File file in this.FileList) {
				CProject.TokenizerQueue tq = new CProject.TokenizerQueue();
				tq.file = file;
				tq.needFile = true;
				tq.code = "";

				this._Queue(tq);
			}
		}

		private void RebuildVisibleTree() {
			
			TreeNode private_ns = new TreeNode("Local Objects", 0, 0);

			try {
				Monitor.Enter(this.FileList);

				foreach(CProject.File file in this.FileList) {
					if (file.TokenList.Count == 0)
						continue;

					Monitor.Enter(file.TokenList);

					foreach(CProject.TokenKey token in file.TokenList.Values) {
						string tparams = "";
						if (token.FuncParams == null)
							tparams = "void";
						else
							tparams = String.Join(", ", token.FuncParams);

						TreeNode node = new TreeNode(token.FuncName + " (" + tparams + ")", 2, 2);
					
						ArrayList al = new ArrayList();
						al.Add(token);
						al.Add(file);

						node.Tag = al;

						private_ns.Nodes.Add(node);
					}
					Monitor.Exit(file.TokenList);
				}
			} catch (Exception exc) { MessageBox.Show(exc.Message); return; }

			Monitor.Exit(this.FileList);

			// Enumerate actual "objects" stored in the database
			Monitor.Enter(this.TokenObjList);
			foreach(CProject.TokenObject tokobj in this.TokenObjList.Values) {
				// Can we create a root object definition?
				if (tokobj.ObjectFileDecl != null) {
					TreeNode node = new TreeNode(tokobj.ObjectName + " (" + tokobj.ObjectType + ")", 3, 3);
					node.Tag = tokobj;

					private_ns.Nodes.Add(node);
				}

				// Let's enumerate through the objects functions, if there are any
				Monitor.Enter(tokobj.ObjectFunctions);
				foreach(CProject.TokenObject.ObjectDescr func in tokobj.ObjectFunctions.Values) {
					string f_params = "void";

					if (func.FuncParams != null)
						f_params = String.Join(", ", func.FuncParams);


					TreeNode node = new TreeNode(tokobj.ObjectName + "::" + func.FuncName + " (" + f_params + ")", 5, 5);
					node.Tag = func;

					private_ns.Nodes.Add(node);
				}
				Monitor.Exit(tokobj.ObjectFunctions);
			}
			Monitor.Exit(this.TokenObjList);

			private_ns.Expand();
			this._private_tree = private_ns;
		}

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			g.LogDebug("CPROJECT::Serialize: Enter");

			info.SetType(typeof(CProject));
			// Save Header
			info.AddValue("\r\n**\r\n** TorqueDev/CW Project (" + Application.ProductVersion + " PFV 1)   http://www.torquedev.com/   S.B. <sbacsa@netmercs.net>\r\n**\r\n** This is a serialized binary object.  Alteration of this file would be ... unwise.\r\n**\r\n\r\n", "");

			// Save version info
			info.AddValue("___TORQUEDEV_VERSION", System.Windows.Forms.Application.ProductVersion);
			info.AddValue("___PROJECT_FILE_VERSION", 1);
			info.AddValue("___TORQUEDEV_SIGNATURE", "SB__NETMERCS");

			//info.AddValue("FileList", this.FileList);
			//info.AddValue("DirList", this.DirList);
			info.AddValue("FileList_V2", this.FileList);
			info.AddValue("DirList_V2", this.DirList);

			//info.AddValue("TokenObject", this._TokenObjList);
			info.AddValue("#ProjectName#", this.ProjectName);
			//info.AddValue("#FullRootPath#", this.ProjectPath);
			info.AddValue("#ProjectType#", this.ProjectType, typeof(short));

			// Save debugger information
			info.AddValue("DebugEn", this.DebugEnabled);
			info.AddValue("DebugExe", this.DebugExe);
			info.AddValue("DebugParams", this.DebugParams);
			info.AddValue("DebugPasswd", this.DebugPasswd);
			info.AddValue("DebugPort", this.DebugPort);

			// Save recent searches
			info.AddValue("RecentFinds", this.Finds);
			info.AddValue("RecentReplaces", this.Replaces);

			// Save new debugger information
			info.AddValue("DebugMainCs", this.DebugMainCs);
			info.AddValue("DebugAutoInsert", this.DebugAutoInsert);

			// Save open file list
			info.AddValue("OpenFiles", this.OpenFiles);
			info.AddValue("OpenFilesState", this.OpenFilesState);

			// Save the project's variable watch list
			info.AddValue("WatchVars", this.VarWatchList);

			g.LogDebug("CPROJECT::Serialize: Saved data");

			ArrayList al_compiled = new ArrayList();
			
			g.LogDebug("CPROJECT::Serialize: Saving macros");
			foreach(CProject.Macro macro in this.MacroList.Values) {
				try {
					MemoryStream mem = new MemoryStream();
					System.Xml.XmlWriter xmlwrite = new System.Xml.XmlTextWriter(mem, System.Text.Encoding.Default);
					macro.MacroCmd.WriteToXml(xmlwrite);

					xmlwrite.Flush();
					xmlwrite.Close();

					CProject.SerializedMacro sermac = new CProject.SerializedMacro();
					sermac.MacroName = macro.MacroName;
					sermac.MacroNum = macro.MacroNum;
					sermac.MacroXML = System.Text.ASCIIEncoding.ASCII.GetString(mem.ToArray());

					mem.Close();
					al_compiled.Add(sermac);
				} catch {}
			}

			info.AddValue("SerializedMacroList", al_compiled);
		}

		#endregion

		#region IDisposable Members

		public void Dispose() {
			this.StopThread();
			this.t_TokenQueue = null;
		}

		#endregion
	}
}
