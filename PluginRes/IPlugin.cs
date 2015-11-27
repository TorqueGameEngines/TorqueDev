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
using System.Windows.Forms;

namespace TSDev.Plugins {


    #region Exceptions
    /// <summary>
    /// Thrown when there is an exception in any plugin routine
    /// </summary>
    public class PluginException : Exception {
        /// <summary>
        /// Constructor.  Thrown when there is an exception in any plugin routine
        /// </summary>
        /// <param name="message">The error message to return to the user</param>
        /// <param name="call">The call that threw the exception</param>
        public PluginException(string message, string call) : base(message) {
            this._call = call;
        }

        private string _call = "";

        /// <summary>
        /// Gets or sets the call that threw the exception
        /// </summary>
        public string Call {
            get {
                return _call;
            }
            set {
                _call = value;
            }
        }
    }
    #endregion

	#region Intellicode Stuff
	public enum IntellicodeType {
		DeclaredVariableCompletion = 0,
		DeclaredObjectCompletion = 1
	}

	public enum TriggerStateWhen {
		DefaultState,
		TaggedStringState,
		StringState,
		RegionState,
		PreProcState,
		CommentState,
		AnyState
	}

	public class InfopopCollection : CollectionBase {
		public virtual void Add(Infopop entry) {
			this.List.Add(entry);
		}

		public virtual void Remove(Infopop entry) {
			this.List.Remove(entry);
		}

		public virtual Infopop this[int Index] {
			get {
				return (Infopop)this.List[Index];
			}
		}
	}

	public class Infopop {
		public Infopop(string FinalDisplay) {
			this.FinalDisplay = FinalDisplay;
		}

		public Infopop(string InfopopName, string InfopopParams, string InfopopRetType, string InfopopDescr, string FinalDisplay) {
			this.InfopopDescr = InfopopDescr;
			this.InfopopName = InfopopName;
			this.InfopopParams = InfopopParams;
			this.InfopopRetType = InfopopRetType;
			this.FinalDisplay = FinalDisplay;
		}
		public string InfopopName = "";
		public string InfopopParams = "";
		public string InfopopRetType = "";
		public string InfopopDescr = "";
		public string FinalDisplay = "";
	}

	public class IntellicodeEntryCollection : CollectionBase {
		public virtual void Add(IntellicodeEntry entry) {
			this.List.Add(entry);
		}

		public virtual void Remove(IntellicodeEntry entry) {
			this.List.Remove(entry);
		}

		public virtual IntellicodeEntry this[int Index] {
			get {
				return (IntellicodeEntry)this.List[Index];
			}
		}
	}

	public class IntellicodeEntry {
		public IntellicodeEntry(string EntryName, string EntryDescription, int EntryIconIndex) {
			this.EntryDescription = EntryDescription;
			this.EntryName = EntryName;
			this.EntryIconIndex = EntryIconIndex;
		}
		public IntellicodeEntry(string EntryName, string EntryDescription, int EntryIconIndex, string EntryInsertBefore, string EntryInsertAfter) {
			this.EntryDescription = EntryDescription;
			this.EntryName = EntryName;
			this.EntryIconIndex = EntryIconIndex;
			this.EntryInsertAfter = EntryInsertAfter;
			this.EntryInsertBefore = EntryInsertBefore;
			//this.EntryParams = EntryParams;
		}

		public string EntryName = "";
		public string EntryDescription = "";
		public string EntryInsertBefore = "";
		public string EntryInsertAfter = "";
		public string EntryParams = "";
		public int EntryIconIndex = 0;
	}
	#endregion

	#region Plugin Marshal Structs
	

	public struct CWObjects {
		public struct CWObject {
			public CWObject(CWObjectType typ, string inh, string nm, string parms, string descr, int defnoffset) {
				objtype = typ;
				inherits = inh;
				name = nm;
				parameters = parms;
				description = descr;
				defined_offset = defnoffset;
			}
			public CWObjectType objtype;
			public string inherits;
			public string name;
			public string parameters;
			public string description;

			public int defined_offset;
		}

		public CWObjects(CWFile? file, ArrayList objs) {
			this.file = file;
			objects = objs;
		}

		public CWFile? file;
		public ArrayList objects;
	}
	public struct CWErrors {
		public CWErrors(CWFile? file, ArrayList errors) {
			this.file = file;
			file_errors = errors;
		}

		public struct CWError {
			public CWError(string errdesc, int errline, int errcol) {
				error_desc = errdesc;
				error_col = errcol;
				error_line = errline;
			}

			public string error_desc;
			public int error_line;
			public int error_col;
		}

		public CWFile? file;
		public ArrayList file_errors;
		
	}
	public struct CWFile {
		public CWFile(int handle, string sname, string relpath, CWDirectory pardir, bool dirty, bool foreign, bool forcereload, bool pendreload, int file_icon) {
			id = handle;
			simple_name = sname;
			relative_path = relpath;
			parent_dir = pardir;
			is_dirty = dirty;
			is_forced_reload = forcereload;
			is_foreign = foreign;
			is_pending_reload = pendreload;
			this.file_icon = file_icon;
		}

		public readonly int id;
		public string simple_name;
		public string relative_path;
		public CWDirectory parent_dir;
		public bool is_dirty;
		public bool is_foreign;
		public bool is_forced_reload;
		public bool is_pending_reload;
		public int file_icon;
	}

	public class CWDirectory {
		public CWDirectory(int handle, string name, CWDirectory pardir) {
			id = handle;
			this.name = name;
			parent_dir = pardir;
		}

		public readonly int id;
		public string name;
		public CWDirectory parent_dir;
	}

	public struct CWProject {
		public CWProject(string name, string path, string dExe, string dPass, int dPort, bool dEn, string dParams, ProjectType ptype, string docMaincs, bool docEn) {
			this.name = name;
			this.path = path;
			debugger_exe = dExe;
			debugger_passwd = dPass;
			debugger_port = dPort;
			debugger_enabled = dEn;
			debugger_params = dParams;
			type = ptype;
			debugger_oneclick_enabled = docEn;
			debugger_oneclick_maincs = docMaincs;

		}

		public string name;
		public string path;
		public string debugger_exe;
		public string debugger_passwd;
		public int debugger_port;
		public bool debugger_enabled;
		public string debugger_params;
		public ProjectType type;
		public string debugger_oneclick_maincs;
		public bool debugger_oneclick_enabled;
	}
	#endregion

	#region Attribute Definitions
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CodeweaverPluginAttribute : Attribute {
		readonly string _name;
		readonly Version _version;
		readonly string _description;
		readonly string _author;
		readonly string _copyright;
		readonly Version _cw_valid_until;
		readonly Guid _guid;

		/// <summary>
		/// Designates a class as a TorqueDev plugin.  Note that the class must also implement ICodeweaverPlugin.
		/// </summary>
		/// <param name="GUID">A GUID, to uniquely identify the plugin</param>
		/// <param name="PluginName">The name of the plugin</param>
		/// <param name="PluginDescription">A description of what the plugin does</param>
		/// <param name="PluginAuthor">The author of the plugin</param>
		/// <param name="PluginCopyright">Any copyrights that the pertain to the plugin</param>
		/// <param name="PluginVersion">The version of the plugin</param>
		/// <param name="PluginValidUntilCWVersion">The upper-limit of the version of TorqueDev that the plugin will work with.</param>
		public CodeweaverPluginAttribute(string GUID, string PluginName, string PluginDescription, string PluginAuthor, string PluginCopyright, string PluginVersion, string PluginValidUntilCWVersion) {
			this._name = PluginName;
			this._description = PluginDescription;
			this._copyright = PluginCopyright;
			this._author = PluginAuthor;
			this._version = new Version(PluginVersion);
			this._guid = new Guid(GUID);

			this._cw_valid_until = new Version(PluginValidUntilCWVersion);
		}

		public Guid PluginGuid {
			get { return _guid; }
		}

		public Version PluginVersion {
			get { return _version; }
		}

		public string PluginName {
			get { return _name; }
		}

		public string PluginDescription {
			get { return _description; }
		}

		public string PluginAuthor {
			get { return _author; }
		}

		public string PluginCopyright {
			get { return _copyright; }
		}

		public Version PluginValidWithCW {
			get { return _cw_valid_until; }
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CodeweaverSCCAttribute : Attribute {
		readonly string _name;
		readonly Version _version;
		readonly string _description;
		readonly string _author;
		readonly string _copyright;
		readonly Version _cw_valid_until;

		readonly bool _has_checkin;
		readonly bool _has_checkout;
		readonly bool _has_open_project;
		readonly bool _has_diff;
		readonly bool _has_history;
		readonly bool _has_undo;
		readonly bool _has_import_project;
		readonly bool _has_rename;
		readonly bool _has_del;
		readonly bool _has_del_project;

		readonly Guid _guid;

		/// <summary>
		/// Designates a class as a TorqueDev Source Code Control plugin.  The class must implement ICodeweaverSCCPlugin.
		/// </summary>
		/// <param name="GUID">A Guid, that uniquely identifies the plugin</param>
		/// <param name="PluginName">The name of the plugin</param>
		/// <param name="PluginDescription">The description of the plugin</param>
		/// <param name="PluginAuthor">The author of the plugin</param>
		/// <param name="PluginCopyright">Any copyrights that may pertain to the plugin</param>
		/// <param name="PluginVersion">The version of the plugin</param>
		/// <param name="PluginValidUntilCWVersion">The upper-limit of the version of TorqueDev that the plugin will work with.</param>
		/// <param name="ProvidesCheckin">True if the plugin provides the ability to check in files</param>
		/// <param name="ProvidesCheckout">True if the plugin provides the ability to check out files</param>
		/// <param name="ProvidesImportProjectInto">True if the plugin allows for projects to be imported into source control</param>
		/// <param name="ProvidesOpenProjectFrom">True if the plugin allows for projects to be opened directly from source control</param>
		/// <param name="ProvidesDiff">True if the plugin provides the ability to perform comparisons between versions</param>
		/// <param name="ProvidesHistory">True if the plugin provides the ability to view version history of an item</param>
		/// <param name="ProvidesUndo">True if the plugin provides the ability to undo a checkout</param>
		/// <param name="ProvidesRename">True if the plugin provides the ability to rename files in the source control repository</param>
		/// <param name="ProvidesDelete">True if the plugin allows for files to be removed from source control</param>
		/// <param name="ProvidesRemoveProjectFrom">True if the plugin allows for whole projects to be removed from source control</param>
		public CodeweaverSCCAttribute(string GUID, string PluginName, string PluginDescription, string PluginAuthor, string PluginCopyright, Version PluginVersion, Version PluginValidUntilCWVersion,
					bool ProvidesCheckin, bool ProvidesCheckout, bool ProvidesImportProjectInto, 
					bool ProvidesOpenProjectFrom, bool ProvidesDiff, bool ProvidesHistory, 
					bool ProvidesUndo, bool ProvidesRename, bool ProvidesDelete, bool ProvidesRemoveProjectFrom) 
		{
			this._name = PluginName;
			this._description = PluginDescription;
			this._copyright = PluginCopyright;
			this._author = PluginAuthor;
			this._version = PluginVersion;

			this._guid = new Guid(GUID);

			this._has_checkin = ProvidesCheckin;
			this._has_checkout = ProvidesCheckout;
			this._has_del = ProvidesDelete;
			this._has_del_project = ProvidesRemoveProjectFrom;
			this._has_diff = ProvidesDiff;
			this._has_history = ProvidesHistory;
			this._has_import_project = ProvidesImportProjectInto;
			this._has_open_project = ProvidesOpenProjectFrom;
			this._has_rename = ProvidesRename;
			this._has_undo = ProvidesUndo;

			this._cw_valid_until = PluginValidUntilCWVersion;
		}

		public Guid PluginGuid { get { return _guid; } }

		public bool ProvidesCheckin { get { return _has_checkin; } }
		public bool ProvidesCheckout { get { return _has_checkout; } }
		public bool ProvidesDelete { get { return _has_del; } }
		public bool ProvidesRemoveProjectFrom { get { return _has_del_project; } }
		public bool ProvidesDiff { get { return _has_diff; } }
		public bool ProvidesHistory { get { return _has_history; } }
		public bool ProvidesImportProjectInto { get { return _has_import_project; } }
		public bool ProvidesOpenProjectFrom { get { return _has_open_project; } }
		public bool ProvidesRename { get { return _has_rename; } }
		public bool ProvidesUndo { get { return _has_undo; } }


		public Version PluginVersion {
			get { return _version; }
		}

		public string PluginName {
			get { return _name; }
		}

		public string PluginDescription {
			get { return _description; }
		}

		public string PluginAuthor {
			get { return _author; }
		}

		public string PluginCopyright {
			get { return _copyright; }
		}

		public Version PluginValidWithCW {
			get { return _cw_valid_until; }
		}
	}
	#endregion

	#region Enums
	public enum CWObjectType {
		DefinedFunction = 1,
		DefinedClassFunction = 2,
		DefinedClassProperty = 3,
		DefinedClass = 4,
		EngineClass = 5,
		EngineClassProperty = 6,
		EngineFunction = 7,
		EngineClassFunction = 8

	}

	public enum CWIntellicodeIcon {
		NoIcon = -1,
		PrivateConstant = 0,
		InternalConstant = 1,
		ProtectedConstant = 2,
		PublicConstant = 3,
		PrivateEvent = 4,
		InternalEvent = 5,
		ProtectedEvent = 6,
		PublicEvent = 7,
		PrivateField = 8,
		InternalField = 9,
		ProtectedField = 10,
		PublicField = 11,
		PrivateMethod = 12,
		InternalMethod = 13,
		ProtectedMethod = 14,
		PublicMethod = 15,
		PrivateProperty = 16,
		InternalProperty = 17,
		ProtectedProperty = 18,
		PublicProperty = 19,
		Operator = 20,
		Assembly = 21,
		Namespace = 22,
		PrivateClass = 23,
		InternalClass = 24,
		ProtectedClass = 25,
		PublicClass = 26,
		PrivateStructure = 27,
		InternalStructure = 28,
		ProtectedStructure = 29,
		PublicStructure = 30,
		PrivateInterface = 31,
		InternalInterface = 32,
		ProtectedInterface = 33,
		PublicInterface = 34,
		PrivateEnumeration = 35,
		InternalEnumeration = 36,
		ProtectedEnumeration = 37,
		PublicEnumeration = 38,
		PrivateDelegate = 39,
		InternalDelegate = 40,
		ProtectedDelegate = 41,
		PublicDelegate = 42,
		XmlTag = 43,
		XmlAttribute = 44,
		XmlComment = 45,
		XmlProcessingInstruction = 46,
		XmlCDataSection = 47,
		Warning = 48,
		EnumerationItem = 49,
		Keyword = 50,
		CodeSnippet = 51

	}

	public enum CWProjectTreeIcon {
		FolderClosedIcon = 0,
		FolderOpenIcon = 1,
		TorqueDocumentIcon = 2,
		ShapesIcon = 3,
		QuestionMarkIcon = 4,
		FolderCubesIcon = 5,
		GearIcon = 6,
		LightningIcon = 7,
		GearMagnifyIcon = 8,
		PlainDocumentPulseIcon = 9,
		ScrollMagnifyIcon = 10,
		PlainDocumentPlusIcon = 11,
		PlainDocumentAttachIcon = 12,
		PlainDocumentCertIcon = 13,
		PlainDocumentCheckIcon = 14,
		PlainDocumentRedXIcon = 15,
		PlainDocumentRedArrowDownIcon = 16,
		PlainDocumentPencilIcon = 17,
		PlainDocumentGreenArrowRightIcon = 18,
		PlainDocumentLockIcon = 19,
		BinocularsIcon = 20,
		MethodIcon = 21,
		RedExclamationIcon = 22,
		ImageIcon = 23,
		WindowIcon = 24,
		PlainDocumentIcon = 25,
		RichContentIcon = 26,
		HierarchyIcon = 27,
		DLLIcon = 28,
		TorqueDocumentCheckedOutIcon = 29,
		TorqueDocumentAddedIcon = 30,
		TorqueDocumentCheckedInIcon = 31
	}

	public enum CWLicenseType {
		StandardLicense = 1,
		ProfessionalLicense = 2,
		ManagedLicense = 3,
		DonatorLicense = 4,
		ProfessionalSiteLicense = 5,
		ManagedSiteLicense = 6
	}

	public enum ProjectType {
		TGE = 1,
		TSE = 2,
		T2D = 3
	}
	#endregion

	public interface ICodeweaverSCCPlugin : ICodeweaverPlugin {
		bool CWSCCInit(Version CWVersion, CWLicenseType UserLicenseType);

		bool CWIsProjectUnderSourceControl();
		bool CWIsFileUnderSourceControl(CWFile FileDetails);


		bool CWFileSCCCheckOut(CWFile FileDetails);
		bool CWFileSCCCheckIn(CWFile FileDetails);
		bool CWFileSCCGetLastestVersion(CWFile FileDetails);
		bool CWFileSCCAdd(CWFile FileDetails);
		bool CWFileSCCRemove(CWFile FileDetails);
		bool CWFileSCCCompare(CWFile FileDetails);
		bool CWFileSCCViewHistory(CWFile FileDetails);
		bool CWFileSCCRename(CWFile FileDetails);
		bool CWProjectSCCAdd(CWProject ProjectDetails);
		bool CWProjectSCCRemove(CWProject ProjectDetails);

		bool CWProjectOpenFromSCC();
		
	}

	public interface ICodeweaverPlugin {
		bool CWPluginInit(Version CWVersion, CWLicenseType UserLicenseType, IDEControl ControlObject);

		void CWProjectLoad(CWProject ProjectDetails, ref bool cancel);
		//void CWFileBeforeLoad(CWFile FileDetails, ref bool cancel);
		void CWFileAfterLoad(CWFile FileDetails);
		//void CWFileBeforeClose(CWFile FileDetails, ref bool cancel);
		void CWFileAfterClose(CWFile FileDetails);
		void CWProjectClose();
		//void CWFileBeforeSave(CWFile FileDetails, ref bool cancel);
		void CWFileAfterSave(CWFile FileDetails);
		void CWIDEClosing(ref bool cancel);
		//void CWFileRename(CWFile FileDetails, string NewFilename);
		//void CWFileAfterRename(CWFile FileDetails);
		void CWEditorTriggerFire(CWFile FileDetails, string TriggerName);

		//void CWBeforeDisplayInfopop(CWFile FileDetails, ref InfopopCollection infopopCollection);
		

		void CWEnvironmentTabClosing(Control Tab, ref bool cancel);
		void CWEnvironmentTabSwitchedTo(Control Tab);

		void CWAboutDialog();

		Guid CWPluginGuid { get; }

	}

	public interface ICodeweaverConfigPage {
		void CWOnConfigCommit();
		void CWOnConfigLoad();
	}
}
