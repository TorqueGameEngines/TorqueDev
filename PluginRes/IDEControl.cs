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
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace TSDev.Plugins
{
	public class IDEControl {
		#region Delegate Declarations
		public delegate void ___RegisterCustomProjectMenu(ToolStripMenuItem custmenu);
		public ___RegisterCustomProjectMenu __RegisterCustomProjectMenu;

		public delegate CWFile? ___GetFile(int id);
		public ___GetFile __GetFile;

		public delegate CWFile?[] ___GetFiles();
		public ___GetFiles __GetFiles;

		public delegate CWDirectory ___GetDirectory(int id);
		public ___GetDirectory __GetDirectory;

		public delegate CWDirectory[] ___GetDirectories();
		public ___GetDirectories __GetDirectories;

		public delegate void ___RegisterCustomTopMenu(ToolStripMenuItem custmenu);
		public ___RegisterCustomTopMenu __RegisterCustomTopMenu;

		public delegate void ___RegisterCustomEditorContextMenu(ToolStripMenuItem custmenu);
		public ___RegisterCustomEditorContextMenu __RegisterCustomEditorContextMenu;

		public delegate void ___RegisterCustomPluginMenu(ToolStripMenuItem custmenu);
		public ___RegisterCustomPluginMenu __RegisterCustomPluginMenu;

		public delegate void ___DeregisterCustomEditorContextMenu(ToolStripMenuItem custmenu);
		public ___DeregisterCustomEditorContextMenu __DeregisterCustomEditorContextMenu;

		public delegate void ___DeregisterCustomPluginMenu(ToolStripMenuItem custmenu);
		public ___DeregisterCustomPluginMenu __DeregisterCustomPluginMenu;

		public delegate void ___DeregisterCustomTopMenu(ToolStripMenuItem custmenu);
		public ___DeregisterCustomTopMenu __DeregisterCustomTopMenu;

		public delegate void ___DeregisterCustomProjectMenu(ToolStripMenuItem custmenu);
		public ___DeregisterCustomProjectMenu __DeregisterCustomProjectMenu;

		public delegate void ___RegisterCustomConfigTab(ICodeweaverPlugin _this, string TabText, Control CustomConfigTab);
		public ___RegisterCustomConfigTab __RegisterCustomConfigTab;

		public delegate void ___DeregisterCustomConfigTab(Control CustomConfigTab);
		public ___DeregisterCustomConfigTab __DeregisterCustomConfigTab;

		public delegate void ___RegisterEnvironmentTab(ICodeweaverPlugin _this, string TabText, int TabIcon, Control TabContent);
		public ___RegisterEnvironmentTab __RegisterEnvironmentTab;

		public delegate void ___UpdateEnvironmentTab(Control Tab, string SetTabTextTo, int SetTabIconTo);
		public ___UpdateEnvironmentTab __UpdateEnvironmentTab;

		public delegate void ___DeregisterEnvironmentTab(Control Tab);
		public ___DeregisterEnvironmentTab __DeregisterEnvironmentTab;

		public delegate void ___SpawnBrowser(string url, bool allownav);
		public ___SpawnBrowser __SpawnBrowser;

		public delegate void ___RegisterWindow(string WindowText, int WindowIcon, Control Window);
		public ___RegisterWindow __RegisterWindow;

		public delegate void ___UpdateWindow(Control Window, string WindowTextSetTo, int WindowIconSetTo);
		public ___UpdateWindow __UpdateWindow;

		public delegate void ___DeregisterWindow(Control Window);
		public ___DeregisterWindow __DeregisterWindow;

		public delegate void ___RegisterToolbar(ToolStrip Toolbar);
		public ___RegisterToolbar __RegisterToolbar;

		public delegate void ___DeregisterToolbar(ToolStrip Toolbar);
		public ___DeregisterToolbar __DeregisterToolbar;

		public delegate void ___RegisterForAutocompletionUpdates(bool Register);
		public ___RegisterForAutocompletionUpdates __RegisterForAutocompletionUpdates;

		public delegate void ___OpenFile(CWFile FileInfo, int JumpToOffset);
		public ___OpenFile __OpenFile;

		public delegate void ___CloseFile(CWFile FileInfo, bool PromptForSaveIfDirty, bool ForceSaveIfDirty);
		public ___CloseFile __CloseFile;

		public delegate void ___OpenProject(string ProjectPath);
		public ___OpenProject __OpenProject;

		public delegate bool ___CloseProject();
		public ___CloseProject __CloseProject;

		public delegate CWFile ___AddFile(string filepath, CWDirectory parent);
		public ___AddFile __AddFile;

		public delegate void ___RemoveFile(CWFile FileInfo);
		public ___RemoveFile __RemoveFile;

		public delegate CWDirectory ___AddDirectory(string dirname, CWDirectory parent);
		public ___AddDirectory __AddDirectory;

		public delegate void ___SetFile(CWFile File);
		public ___SetFile __SetFile;

		public delegate void ___SetDirectory(CWDirectory Directory);
		public ___SetDirectory __SetDirectory;

		public delegate void ___SetProject(CWProject Project);
		public ___SetProject __SetProject;

		public delegate string ___EditorGetText(CWFile File);
		public ___EditorGetText __EditorGetText;

		public delegate bool ___IsFileOpen(CWFile File);
		public ___IsFileOpen __IsFileOpen;

		public delegate void ___RemoveDirectory(CWDirectory Directory, bool DelFilesOnDisk);
		public ___RemoveDirectory __RemoveDirectory;

		public delegate string ___EditorGetSelected(CWFile File);
		public ___EditorGetSelected __EditorGetSelected;

		public delegate void ___EditorInsertText(CWFile File, int location, string text);
		public ___EditorInsertText __EditorInsertText;

		public delegate void ___EditorSetText(CWFile File, string text);
		public ___EditorSetText __EditorSetText;

		public delegate void ___EditorSetCaret(CWFile File, int[] position, bool isOffset);
		public ___EditorSetCaret __EditorSetCaret;

		public delegate int[] ___EditorGetCaret(CWFile File, bool TranslateToOffset);
		public ___EditorGetCaret __EditorGetCaret;

		public delegate CWErrors ___IntellicodeScanFile(CWFile File);
		public ___IntellicodeScanFile __IntellicodeScanFile;

		public delegate CWErrors[] ___IntellicodeScanProject();
		public ___IntellicodeScanProject __IntellicodeScanProject;

		public delegate CWErrors ___IntellicodeScanExternalFile(string path);
		public ___IntellicodeScanExternalFile __IntellicodeScanExternalFile;

		public delegate CWErrors ___IntellicodeScanString(string code);
		public ___IntellicodeScanString __IntellicodeScanString;

		public delegate CWObjects ___IntellicodeGetObjectsInFile(CWFile file);
		public ___IntellicodeGetObjectsInFile __IntellicodeGetObjectsInFile;

		public delegate CWObjects[] ___IntellicodeGetObjectsInProject();
		public ___IntellicodeGetObjectsInProject __IntellicodeGetObjectsInProject;

		public delegate CWObjects ___IntellicodeGetEngineObjects();
		public ___IntellicodeGetEngineObjects __IntellicodeGetEngineObjects;

		public delegate int ___AddProjectTreeIconResource(Image icon);
		public ___AddProjectTreeIconResource __AddProjectTreeIconResource;

		public delegate int ___AddWorkspaceTabIconResource(Image icon);
		public ___AddWorkspaceTabIconResource __AddWorkspaceTabIconResource;

		public delegate int ___AddWindowIconResource(Image icon);
		public ___AddWindowIconResource __AddWindowIconResource;

		public delegate CWFile? ___GetActiveFile();
		public ___GetActiveFile __GetActiveFile;

		public delegate void ___SaveFile(CWFile File);
		public ___SaveFile __SaveFile;

		public delegate void ___SaveProject();
		public ___SaveProject __SaveProject;

		public delegate bool ___IsProjectOpen();
		public ___IsProjectOpen __IsProjectOpen;

		public delegate void ___CreateProject(CWProject project, string saveto);
		public ___CreateProject __CreateProject;

		public delegate void ___DebugAddBreakpoint(CWFile File, int line, int pass_count, bool clear_after_hit, string conditional);
		public ___DebugAddBreakpoint __DebugAddBreakpoint;

		public delegate void ___DebugDelBreakpoint(CWFile File, int line);
		public ___DebugDelBreakpoint __DebugDelBreakpoint;

		public delegate int[] ___DebugGetBreakpoints(CWFile File);
		public ___DebugGetBreakpoints __DebugGetBreakpoints;

		public delegate void ___AddIndicatorSpan(CWFile File, string IndicatorName, int startoffset, int endoffset, Image marginIcon, Color lineForeColor, Color lineBackColor, bool Bold, bool Italic, bool Underline);
		public ___AddIndicatorSpan __AddIndicatorSpan;

		public delegate void ___AddIndicatorIcon(CWFile File, string IndicatorName, int line, Image marginIcon);
		public ___AddIndicatorIcon __AddIndicatorIcon;

		public delegate void ___RemoveIndicator(CWFile File, string IndicatorName, int line);
		public ___RemoveIndicator __RemoveIndicator;

		public delegate void ___RefreshProjectList();
		public ___RefreshProjectList __RefreshProjectList;

		public delegate void ___RegisterEditorTrigger(ICodeweaverPlugin _this, Keys TriggerKey, Keys TriggerModifiers, string TriggerName, TriggerStateWhen context);
		public ___RegisterEditorTrigger __RegisterEditorTrigger;

		public delegate void ___DeregisterEditorTrigger(string TriggerName);
		public ___DeregisterEditorTrigger __DeregisterEditorTrigger;

		public delegate void ___DebugExecute(bool RunProcessesBeforeDebug);
		public ___DebugExecute __DebugExecute;

		public delegate void ___IntellicodeDisplayMemberlist(IntellicodeEntryCollection entries);
		public ___IntellicodeDisplayMemberlist __IntellicodeDisplayMemberlist;

		public delegate void ___IntellicodeDisplayInfopop(InfopopCollection entries);
		public ___IntellicodeDisplayInfopop __IntellicodeDisplayInfopop;

		public delegate void ___ConfigSetValue(ICodeweaverPlugin _this, string configKey, object configValue);
		public ___ConfigSetValue __ConfigSetValue;

		public delegate object ___ConfigGetValue(ICodeweaverPlugin _this, string configKey, object defaultValue);
		public ___ConfigGetValue __ConfigGetValue;

		public delegate void ___FocusEnvironmentTab(Control Tab);
		public ___FocusEnvironmentTab __FocusEnvironmentTab;

		public delegate ArrayList ___DebugWatchListGet();
		public ___DebugWatchListGet __DebugWatchListGet;

		public delegate void ___DebugWatchListSet(ArrayList watchList);
		public ___DebugWatchListSet __DebugWatchListSet;

		public delegate void ___DebugWatchListPush();
		public ___DebugWatchListPush __DebugWatchListPush;
		#endregion

		#region Function Declarations
		public object ConfigGetValue(ICodeweaverPlugin _this, string configKey, object defaultValue) {
			return __ConfigGetValue(_this, configKey, defaultValue);
		}

		public void ConfigSetValue(ICodeweaverPlugin _this, string configKey, object configValue) {
			__ConfigSetValue(_this, configKey, configValue);
		}

		public void RegisterCustomEditorContextMenu(ToolStripMenuItem custmenu) {
			__RegisterCustomEditorContextMenu(custmenu);
		}

		public void DeregisterCustomEditorContextMenu(ToolStripMenuItem custmenu) {
			__DeregisterCustomEditorContextMenu(custmenu);
		}

		public void IntellicodeDisplayInfopop(InfopopCollection entries) {
			__IntellicodeDisplayInfopop(entries);
		}

		public void IntellicodeDisplayMemberlist(IntellicodeEntryCollection entries) {
			__IntellicodeDisplayMemberlist(entries);
		}

		public void DebugExecute(bool HaltDebugging) {
			__DebugExecute(HaltDebugging);
		}

		public void RegisterEditorTrigger(ICodeweaverPlugin _this, Keys TriggerKey, Keys TriggerModifiers, string TriggerName, TriggerStateWhen context) {
			__RegisterEditorTrigger(_this, TriggerKey, TriggerModifiers, TriggerName, context);
		}

		public void DeregisterEditorTrigger(string TriggerName) {
			__DeregisterEditorTrigger(TriggerName);
		}

		public void RefreshProjectList() {
			__RefreshProjectList();
		}

		public void AddIndicatorSpan(CWFile File, string IndicatorName, int startoffset, int endoffset, Image marginIcon, Color lineForeColor, Color lineBackColor, bool Bold, bool Italic, bool Underline) {
			__AddIndicatorSpan(File, IndicatorName, startoffset, endoffset, marginIcon,
					lineForeColor, lineBackColor, Bold, Italic, Underline);
		}

		public void AddIndicatorIcon(CWFile File, string IndicatorName, int line, Image marginIcon) {
			__AddIndicatorIcon(File, IndicatorName, line, marginIcon);
		}

		public void RemoveIndicator(CWFile File, string IndicatorName, int line) {
			__RemoveIndicator(File, IndicatorName, line);
		}

		public int[] DebugGetBreakpoints(CWFile File) {
			return __DebugGetBreakpoints(File);
		}

		public void DebugDelBreakpoint(CWFile File, int line) {
			__DebugDelBreakpoint(File, line);
		}

		public void DebugAddBreakpoint(CWFile File, int line, int pass_count, bool clear_after_hit, string conditional) {
			__DebugAddBreakpoint(File, line, pass_count, clear_after_hit, conditional);
		}

		public void CreateProject(CWProject project, string saveto) {
			__CreateProject(project, saveto);
		}

		public bool IsProjectOpen() {
			return __IsProjectOpen();
		}

		public void SaveProject() {
			__SaveProject();
		}

		public void SaveFile(CWFile File) {
			__SaveFile(File);
		}

		public CWFile? GetActiveFile() {
			return __GetActiveFile();
		}

		public int[] EditorGetCaret(CWFile File, bool TranslateToOffset) {
			return __EditorGetCaret(File, TranslateToOffset);
		}

		public int AddWindowIconResource(Image icon) {
			return __AddWindowIconResource(icon);
		}

		public int AddWorkspaceTabIconResource(Image icon) {
			return __AddWorkspaceTabIconResource(icon);
		}

		public int AddProjectTreeIconResource(Image icon) {
			return __AddProjectTreeIconResource(icon);
		}

		public CWObjects IntellicodeGetEngineObjects() {
			return __IntellicodeGetEngineObjects();
		}

		public CWObjects[] IntellicodeGetObjectsInProject() {
			return __IntellicodeGetObjectsInProject();
		}

		public CWObjects IntellicodeGetObjectsInFile(CWFile file) {
			return __IntellicodeGetObjectsInFile(file);
		}

		public CWErrors IntellicodeScanString(string code) {
			return __IntellicodeScanString(code);
		}

		public CWErrors IntellicodeScanExternalFile(string path) {
			return __IntellicodeScanExternalFile(path);
		}

		public CWErrors IntellicodeScanFile(CWFile File) {
			return __IntellicodeScanFile(File);
		}

		public CWErrors[] IntellicodeScanProject() {
			return __IntellicodeScanProject();
		}

		public void EditorSetCaret(CWFile File, int[] position, bool isOffset) {
			__EditorSetCaret(File, position, isOffset);
		}

		public void EditorSetText(CWFile File, string text) {
			__EditorSetText(File, text);
		}

		public void EditorInsertText(CWFile File, int offset, string text) {
			__EditorInsertText(File, offset, text);
		}

		public string EditorGetSelected(CWFile File) {
			return __EditorGetSelected(File);
		}

		public void RemoveDirectory(CWDirectory Directory, bool DelFilesOnDisk) {
			__RemoveDirectory(Directory, DelFilesOnDisk);
		}

		public bool IsFileOpen(CWFile File) {
			return  __IsFileOpen(File);
		}

		public string EditorGetText(CWFile File) {
			return __EditorGetText(File);
		}

		public void SetFile(CWFile File) {
			__SetFile(File);
		}

		public void SetDirectory(CWDirectory Directory) {
			__SetDirectory(Directory);
		}

		public void SetProject(CWProject Project) {
			__SetProject(Project);
		}

		public CWDirectory AddDirectory(string dirname, CWDirectory parent) {
			return __AddDirectory(dirname, parent);
		}

		public void RemoveFile(CWFile FileInfo) {
			__RemoveFile(FileInfo);
		}

		public CWFile AddFile(string filepath, CWDirectory parent) {
			return __AddFile(filepath, parent);
		}

		public bool CloseProject() {
			return __CloseProject();
		}

		public void OpenProject(string ProjectFile) {
			__OpenProject(ProjectFile);
		}

		public void CloseFile(CWFile FileInfo, bool PromptForSaveIfDirty, bool ForceSaveIfDirty) {
			__CloseFile(FileInfo, PromptForSaveIfDirty, ForceSaveIfDirty);
		}

		public void OpenFile(CWFile FileInfo, int JumpToOffset) {
			__OpenFile(FileInfo, JumpToOffset);
		}

		public void RegisterForAutoCompletionUpdates(bool Register) {
			__RegisterForAutocompletionUpdates(Register);
		}

		public void DeregisterToolbar(ToolStrip Toolbar) {
			__DeregisterToolbar(Toolbar);
		}

		public void RegisterToolbar(ToolStrip Toolbar) {
			__RegisterToolbar(Toolbar);
		}

		public void DeregisterWindow(Control Window) {
			__DeregisterWindow(Window);
		}

		public void UpdateWindow(Control Window, string WindowTextSetTo, int WindowIconSetTo) {
			__UpdateWindow(Window, WindowTextSetTo, WindowIconSetTo);
		}

		public void RegisterWindow(string WindowText, int WindowIcon, Control Window) {
			__RegisterWindow(WindowText, WindowIcon, Window);
		}

		public void SpawnBrowser(string Url, bool AllowNavigate) {
			__SpawnBrowser(Url, AllowNavigate);
		}

		public void DeregisterEnvironmentTab(Control Tab) {
			__DeregisterEnvironmentTab(Tab);
		}

		public void UpdateEnvironmentTab(Control Tab, string SetTabTextTo, int SetTabIconTo) {
			__UpdateEnvironmentTab(Tab, SetTabTextTo, SetTabIconTo);
		}

		public void RegisterEnvironmentTab(ICodeweaverPlugin _this, string TabText, int TabIcon, Control TabContent) {
			__RegisterEnvironmentTab(_this, TabText, TabIcon, TabContent);
		}

		public void DeregisterCustomConfigTab(Control CustomConfigTab) {
			__DeregisterCustomConfigTab(CustomConfigTab);
		}

		public void RegisterCustomConfigTab(ICodeweaverPlugin _this, string TabText, Control CustomConfigTab) {
			__RegisterCustomConfigTab(_this, TabText, CustomConfigTab);
		}

		public void DeregisterCustomProjectMenu(ToolStripMenuItem custmenu) {
			__DeregisterCustomTopMenu(custmenu);
		}

		public void DeregisterCustomTopMenu(ToolStripMenuItem custmenu) {
			__DeregisterCustomTopMenu(custmenu);
		}

		public void DeregisterCustomPluginMenu(ToolStripMenuItem custmenu) {
			__DeregisterCustomPluginMenu(custmenu);
		}

		public void RegisterCustomPluginMenu(ToolStripMenuItem custmenu) {
			__RegisterCustomPluginMenu(custmenu);
		}

		public void RegisterCustomProjectMenu(ToolStripMenuItem custmenu) {
			__RegisterCustomProjectMenu(custmenu);
		}

		public void RegisterCustomTopMenu(ToolStripMenuItem custmenu) {
			__RegisterCustomTopMenu(custmenu);
		}

		public CWFile? GetFile(int id) {
			return __GetFile(id);
		}

		public CWFile?[] GetFiles() {
			return __GetFiles();
		}

		public CWDirectory GetDirectory(int id) {
			return __GetDirectory(id);
		}

		public CWDirectory[] GetDirectories(int id) {
			return __GetDirectories();
		}

		public void FocusEnvironmentTab(Control Tab) {
			__FocusEnvironmentTab(Tab);
		}

		public ArrayList DebugWatchListGet() {
			return __DebugWatchListGet();
		}

		public void DebugWatchListSet(ArrayList watchList) {
			__DebugWatchListSet(watchList);
		}

		public void DebugWatchListPush() {
			__DebugWatchListPush();
		}
		#endregion


	}

	public class IDEControlSCC {
		public delegate void ___RegisterCustomSCCMenu(ToolStripMenuItem custmenu);
		public ___RegisterCustomSCCMenu __RegisterCustomSCCMenu;

		public delegate void ___RegisterSCCConfigPage(Control ConfigPage);
		public ___RegisterSCCConfigPage __RegisterSCCConfigPage;

		public void RegisterSCCConfigPage(Control ConfigPage) {
			__RegisterSCCConfigPage(ConfigPage);
		}

		public void RegisterCustomSCCMenu(ToolStripMenuItem custmenu) {
			__RegisterCustomSCCMenu(custmenu);
		}
		
	}

}
