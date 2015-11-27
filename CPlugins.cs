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
using TSDev.Plugins;
using System.IO;
using ActiproSoftware.WinUICore;
using System.Drawing;
using Crownwood.DotNetMagic.Controls;

namespace TSDev
{
	internal static class CPlugins {
		public static void RegisterCustomEditorContextMenu(ToolStripMenuItem custmenu) {
			if (g.CustomEditorContextMenus.Contains(custmenu))
				return;

			// Add it
			g.CustomEditorContextMenus.Add(custmenu);

			// Go through all the editor windows and add the custom menu
			/*foreach (UCEditor editor in g.Editors) {
				// Is this the first custom menu in the list?
				if (g.CustomEditorContextMenus.Count == 1)
					editor.ctxEditor.Items.Add(new ToolStripSeparator());

				editor.ctxEditor.Items.Add(custmenu);
			}*/
		}

		public static ArrayList DebugWatchListGet() {
			if (g.Project == null)
				return null;

			return g.Project.VarWatchList;
		}

		public static void DebugWatchListSet(ArrayList watchList) {
			if (g.Project == null)
				return;

			g.Project.VarWatchList = watchList;
		}

		public static void DebugWatchListPush() {
			if (g.Project == null || g.IsDebugging == false)
				return;

			// Push watch list to game engine
			if (g.Main.DebugUC == null)
				return;

			g.Main.DebugUC.WatchAskFor();
		}

		public static void DeregisterCustomEditorContextMenu(ToolStripMenuItem custmenu) {
			if (!g.CustomEditorContextMenus.Contains(custmenu))
				return;

			// Remove it
			g.CustomEditorContextMenus.Remove(custmenu);

			// Go through all the editor windows and delete the custom menu
			/*foreach (UCEditor editor in g.Editors) {
				editor.ctxEditor.Items.Remove(custmenu);

				// Is this the last custom menu in the list?
				// If so, remove the separator (last in the list)
				if (g.CustomEditorContextMenus.Count == 0)
					editor.ctxEditor.Items.RemoveAt(editor.ctxEditor.Items.Count - 1);
			}*/
		}

		public static void IntellicodeDisplayInfopop(InfopopCollection entries) {
			// Is an editor window active?
			if (g.Main.GetActiveEditor() == null)
				return;

			UCEditor editor = g.Main.GetActiveEditor();
			editor.txtEditor.IntelliPrompt.InfoTip.Info.Clear();

			// Display it
			foreach (Infopop info in entries) {
				editor.txtEditor.IntelliPrompt.InfoTip.Info.Add(
					"<b><u>" + info.InfopopRetType.Replace("<", "&lt;") + "</b></u> " +
					"<b>" + info.InfopopName.Replace("<", "&lt;") + "</b> " +
					"(<i>" + info.InfopopParams.Replace("<", "&lt;") + "</i>) " +
					((info.InfopopDescr.Trim() != "") ? "<br />" + info.InfopopDescr.Replace("<", "&lt;") : "")
				);
			}

			editor.txtEditor.IntelliPrompt.InfoTip.Show(editor.txtEditor.SelectedView.Selection.StartOffset);
		}

		public static void IntellicodeDisplayMemberlist(IntellicodeEntryCollection entries) {
			// Is an editor window active?
			if (g.Main.GetActiveEditor() == null)
				return;

			UCEditor editor = g.Main.GetActiveEditor();
			editor.txtEditor.IntelliPrompt.MemberList.Clear();

			// Display it
			foreach (IntellicodeEntry ic in entries) {
				editor.txtEditor.IntelliPrompt.MemberList.Add(new ActiproSoftware.SyntaxEditor.IntelliPromptMemberListItem(
					ic.EntryName, ic.EntryIconIndex, 
					"<b>" + ic.EntryName.Replace("<", "&lt;") + " (</b>" +
					ic.EntryParams + "<b>)</b> " + 
					((ic.EntryDescription != "") ? "<br />" + ic.EntryDescription.Replace("<", "&lt;") : "")
					, ic.EntryInsertBefore, ic.EntryInsertAfter)
				);
			}
		}

		public static void DebugExecute(bool HaltDebugging) {
			if (g.Project == null || g.Project.DebugEnabled == false)
				throw new PluginException("No project is open, or debugging is not enabled.", "DebugExecute");

			if (g.IsDebugging == HaltDebugging)
				g.Main.SwitchDebug(HaltDebugging);
		}

		public static void RegisterEditorTrigger(ICodeweaverPlugin _this, Keys TriggerKeys, Keys TriggerModifiers, string TriggerName, TriggerStateWhen TriggerWhen) {
			if (g.EditorTriggers.HasKey(TriggerName))
				throw new PluginException("A trigger by this name already exists.", "RegisterEditorTrigger");

			// Register the trigger with the collection
			g.EditorTriggers.Add(new EditorTrigger(_this, TriggerKeys, TriggerModifiers, TriggerName, TriggerWhen));
		}

		public static void DeregisterEditorTrigger(string TriggerName) {
			if (!g.EditorTriggers.HasKey(TriggerName))
				return;

			// Deregister the trigger
			g.EditorTriggers.Remove(TriggerName);
		}

		public static void RefreshProjectList() {
			if (g.Project == null)
				return;

			// Refresh the project explorer view
			g.Main.InitProject();
		}

		public static void AddIndicatorSpan(CWFile File, string IndicatorName, int startoffset, int endoffset, Image marginIcon, Color lineForeColor, Color lineBackColor, bool Bold, bool Italic, bool Underline) {
			if (g.Project == null)
				throw new PluginException("Attempting to add indicator with no project open.", "AddIndicatorSpan");

			CProject.File file = GetFile(File);

			if (file == null)
				throw new PluginException("Invalid CWFile instance -- file is not in project.", "AddIndicatorSpan");

			// Check to see if the file is open
			UCEditor editor = GetEditor(file);

			if (editor == null)
				throw new PluginException("File specified is not open", "AddIndicatorSpan");

			// Finally, add the indicator
			editor.txtEditor.Document.Indicators.Add(
				new CIndicators.CustomIndicator(IndicatorName, marginIcon, lineForeColor, lineBackColor, Bold, Italic, Underline),
				startoffset, (endoffset - startoffset));

		}

		public static void AddIndicatorIcon(CWFile File, string IndicatorName, int line, Image marginIcon) {
			if (g.Project == null)
				throw new PluginException("Attempting to add indicator with no project open.", "AddIndicatorSpan");

			CProject.File file = GetFile(File);

			if (file == null)
				throw new PluginException("Invalid CWFile instance -- file is not in project.", "AddIndicatorSpan");

			// Check to see if the file is open
			UCEditor editor = GetEditor(file);

			if (editor == null)
				throw new PluginException("File specified is not open", "AddIndicatorSpan");
			
			// Add the indicator
			editor.txtEditor.Document.Indicators.Add(
				new CIndicators.CustomIconIndicator(IndicatorName, marginIcon), line);
		}

		public static void RemoveIndicator(CWFile File, string IndicatorName, int line) {
			if (g.Project == null)
				throw new PluginException("Attempting to add indicator with no project open.", "AddIndicatorSpan");

			CProject.File file = GetFile(File);

			if (file == null)
				throw new PluginException("Invalid CWFile instance -- file is not in project.", "AddIndicatorSpan");

			// Check to see if the file is open
			UCEditor editor = GetEditor(file);

			if (editor == null)
				throw new PluginException("File specified is not open", "AddIndicatorSpan");
			
			// Remove the indicator
			ArrayList toremove = new ArrayList();
			foreach (ActiproSoftware.SyntaxEditor.Indicator ind in editor.txtEditor.Document.Indicators) {
				if (ind is CIndicators.CustomIconIndicator) {
					if (line == -1)
						toremove.Add(ind);
					else if ((ind as CIndicators.CustomIconIndicator).LineIndex == line)
						toremove.Add(ind);
				} else if (ind is CIndicators.CustomIndicator) {
					toremove.Add(ind);
				}
			}

			foreach (ActiproSoftware.SyntaxEditor.Indicator ind in toremove)
				editor.txtEditor.Document.Indicators.Remove(ind);
		}

		public static int[] DebugGetBreakpoints(CWFile File) {
			AssertOpenProject("DebugGetBreakpoints");
			AssertValidFile(File, "DebugGetBreakpoints");

			CProject.File file = GetFile(File);
			
			// Go through the breakpoints
			ArrayList breakpoints = new ArrayList();
			foreach (CProject.Breakpoint bp in g.Project.ProjectBreaks) {
				if (bp.file == file)
					breakpoints.Add(bp.LineNumber);
			}

			// Turn it into an array
			int[] output = new int[breakpoints.Count];

			for (int i = 0; i < breakpoints.Count; i++)
				output[i] = (int)breakpoints[i];

			return output;
		}

		public static void DebugDelBreakpoint(CWFile File, int line) {
			AssertOpenProject("DebugDelBreakpoint");
			AssertValidFile(File, "DebugDelBreakpoint");

			CProject.File file = GetFile(File);

			// Find the breakpoint
			foreach (CProject.Breakpoint bp in g.Project.ProjectBreaks) {
				if (bp.file == file && bp.LineNumber == line) {
					g.Project.ProjectBreaks.Remove(bp);
					break;
				}
			}

			// Update the breakpoints if the file is open
			UCEditor editor = GetEditor(file);

			if (editor != null)
				editor.BreakpointRender();
		}

		public static void DebugAddBreakpoint(CWFile File, int line, int pass_count, bool clear_after_hit, string conditional) {
			AssertOpenProject("DebugAddBreakpoint");
			AssertValidFile(File, "DebugAddBreakpoint");

			CProject.File file = GetFile(File);

			// Create a new instance of the breakpoint
			CProject.Breakpoint bp = new CProject.Breakpoint(file, line, pass_count, ((conditional == "") ? "true" : conditional));

			if (clear_after_hit == true)
				throw new PluginException("NotImplemented: clear_after_hit cannot equal true.  The enhanced telnetdebugger does not support this attribute yet.", "DebugAddBreakpoint");

			// Add the breakpoint
			g.Project.ProjectBreaks.Add(bp);
		}

		public static void CreateProject(CWProject project, string saveto) {
			CProject newproject = new CProject();
			newproject.DebugAutoInsert = project.debugger_oneclick_enabled;
			newproject.DebugMainCs = project.debugger_oneclick_maincs;
			newproject.DebugEnabled = project.debugger_enabled;
			newproject.DebugExe = project.debugger_exe;
			newproject.DebugParams = project.debugger_params;
			newproject.DebugPasswd = project.debugger_passwd;
			newproject.DebugPort = project.debugger_port;
			newproject.ProjectName = project.name;
			newproject.ProjectPath = project.path;
			newproject.ProjectType = (short)project.type;

			// Save the project
			CProject.SaveProject(saveto, newproject);
		}

		public static bool IsProjectOpen() {
			return g.Project != null;
		}

		public static void SaveProject() {
			AssertOpenProject("SaveProject");

			g.Main.SaveProjectCommit(g.ProjectFN);
		}

		public static void SaveFile(CWFile File) {
			AssertOpenFile(File, "SaveFile");

			GetEditor(GetFile(File)).CommitSave(GetFile(File));
		}

		public static CWFile? GetActiveFile() {
			AssertOpenProject("GetActiveFile");

			UCEditor editor = g.Main.GetActiveEditor();

			if (editor == null)
				return null;

			return editor.g_curFile.ToCWFile();
		}

		public static int[] EditorGetCaret(CWFile File, bool TranslateToOffset) {
			AssertOpenProject("EditorGetCaret");
			AssertOpenFile(File, "EditorGetCaret");

			UCEditor editor = GetEditor(GetFile(File));

			if (TranslateToOffset) {
				return new int[] { editor.txtEditor.SelectedView.Selection.StartOffset };
			} else {
				ActiproSoftware.SyntaxEditor.Position pos = editor.txtEditor.Document.OffsetToPosition(editor.txtEditor.SelectedView.Selection.StartOffset);
				return new int[] { pos.Line, pos.Character };
			}
		}

		public static int AddWindowIconResource(Image icon) {
			throw new PluginException("Deprecated.", "AddWindowIconResource");
		}

		public static int AddWorkspaceTabIconResource(Image icon) {
			g.Main.ilDocBar.Images.Add(icon);

			return g.Main.ilDocBar.Images.Count - 1;
		}

		public static int AddProjectTreeIconResource(Image icon) {
			g.Main.ilProject.Images.Add(icon);

			return g.Main.ilProject.Images.Count - 1;
		}

		#region Intellicode-specific Plugin Functions

		public static CWObjects IntellicodeGetEngineObjects() {
			// Gets all the engine objects
			AssertOpenProject("IntellicodeGetEngineObjects");

			CWObjects outobj = new CWObjects(null, new ArrayList());

			foreach (CAutoComplete.ClassEntry cls in frmMain.ac.ReturnAllClasses().Values) {
				outobj.objects.Add(new CWObjects.CWObject(
					CWObjectType.EngineClass,
					cls.ClassInheritsFrom,
					cls.ClassName,
					"", "", -1));

				foreach (CAutoComplete.ClassEntry.FuncEntry funcs in cls.func_list) {
					outobj.objects.Add(new CWObjects.CWObject(
						CWObjectType.EngineClassFunction,
						cls.ClassName,
						funcs.func_name,
						funcs.func_params,
						funcs.func_descr,
						-1));
				}

				foreach (CAutoComplete.ClassEntry.PropEntry prop in cls.prop_list) {
					outobj.objects.Add(new CWObjects.CWObject(
						CWObjectType.EngineClassProperty,
						cls.ClassName,
						prop.prop_name,
						"",
						prop.prop_descr,
						-1));
				}
			}

			return outobj;
		}

		public static CWObjects[] IntellicodeObjectsInProject() {
			AssertOpenProject("IntellicodeObjectsInProject");

			CWObjects[] outobjs = new CWObjects[g.Project.FileList.Count];

			for (int i = 0; i < outobjs.Length; i++)
				outobjs[i] = IntellicodeObjectsInFile((g.Project.FileList[i] as CProject.File).ToCWFile());

			return outobjs;
		}

		public static CWObjects IntellicodeObjectsInFile(CWFile File) {
			AssertOpenProject("IntellicodeObjectsInProject");
			AssertValidFile(File, "IntellicodeObjectsInProject");

			CProject.File file = GetFile(File);

			CWObjects objout = new CWObjects(File, new ArrayList());

			foreach (CProject.TokenKey tok in file.TokenList.Values) {
				objout.objects.Add(new CWObjects.CWObject(
					CWObjectType.DefinedFunction,
					"",
					tok.FuncName,
					String.Join(", ", tok.FuncParams),
					tok.FuncDescr,
					tok.LineNumber));
			}

			foreach (CProject.TokenObject tokobj in g.Project.TokenObjList.Values) {
				if (tokobj.ObjectFileDecl != file)
					continue;

				objout.objects.Add(new CWObjects.CWObject(
					CWObjectType.DefinedClass,
					tokobj.ObjectType,
					tokobj.ObjectName,
					"",
					"",
					tokobj.ObjectDeclOffset));

				foreach (CProject.TokenObject.ObjectDescr objdescr in tokobj.ObjectFunctions) {
					if (objdescr.FuncFile != file)
						continue;

					objout.objects.Add(new CWObjects.CWObject(
						CWObjectType.DefinedClassFunction,
						tokobj.ObjectName,
						objdescr.FuncName,
						String.Join(", ", objdescr.FuncParams),
						objdescr.FuncDescr,
						objdescr.FuncOffset));
				}

			}

			return objout;
		}

		public static CWErrors IntellicodeScanString(string code) {
			// Scan a piece of code for errors
			CWErrors errs = new CWErrors(null, new ArrayList());

			MemoryStream stream_code = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(code));

			// Load up the lexer into memory
			netMercs.TorqueDev.Lexer.Scanner scanner = new netMercs.TorqueDev.Lexer.Scanner(stream_code);
			netMercs.TorqueDev.Lexer.Parser parser = new netMercs.TorqueDev.Lexer.Parser(scanner);

			// Parse the code
			parser.Parse();

			// Translate the errors
			foreach (netMercs.TorqueDev.Lexer.Error err in parser.errors.ErrCollection) {
				errs.file_errors.Add(new CWErrors.CWError(
					err.Text, err.Line, err.Column));
			}

			stream_code.Close();

			return errs;
		}

		public static CWErrors IntellicodeScanExternalFile(string path) {
			// Scan a file for errors
			CWErrors errs = new CWErrors(null, new ArrayList());

			// Load up the lexer into memory
			netMercs.TorqueDev.Lexer.Scanner scanner = new netMercs.TorqueDev.Lexer.Scanner(path);
			netMercs.TorqueDev.Lexer.Parser parser = new netMercs.TorqueDev.Lexer.Parser(scanner);

			// Parse the code
			parser.Parse();

			// Translate the errors
			foreach (netMercs.TorqueDev.Lexer.Error err in parser.errors.ErrCollection) {
				errs.file_errors.Add(new CWErrors.CWError(
					err.Text, err.Line, err.Column));
			}

			return errs;
		}

		public static CWErrors IntellicodeScanFile(CWFile File) {
			AssertOpenProject("IntellicodeScanFile");
			AssertValidFile(File, "IntellicodeScanFile");

			netMercs.TorqueDev.Lexer.ErrorCollection errcoll = null;
			CWErrors errout = new CWErrors(File, new ArrayList());

			errcoll = g.Main.ScanFile(GetFile(File));

			// Translate the errors
			foreach (netMercs.TorqueDev.Lexer.Error err in errcoll) {
				errout.file_errors.Add(new CWErrors.CWError(
					err.Text, err.Line, err.Column));
			}

			return errout;
		}

		public static CWErrors[] IntellicodeScanProject() {
			AssertOpenProject("IntellicodeScanProject");

			CWErrors[] errsout = new CWErrors[g.Project.FileList.Count];

			for (int i = 0; i < errsout.Length; i++)
				errsout[i] = IntellicodeScanFile((g.Project.FileList[i] as CProject.File).ToCWFile());

			return errsout;
		}

		#endregion

		public static void EditorSetCaret(CWFile File, int[] position, bool isOffset) {
			AssertOpenProject("EditorSetCaret");
			AssertOpenFile(File, "EditorSetCaret");

			UCEditor editor = GetEditor(GetFile(File));

			if (!isOffset) {
				// Try to cast the line/column thing into an offset
				try {
					int pos = editor.txtEditor.Document.PositionToOffset(new ActiproSoftware.SyntaxEditor.Position(position[0], position[1]));

					editor.txtEditor.SelectedView.Selection.StartOffset = pos;
					editor.txtEditor.SelectedView.Selection.EndOffset = pos;
				} catch {
					throw new PluginException("Unable to cast the specified line/column combination into an offset.", "EditorSetCaret");
				}
			} else {
				editor.txtEditor.SelectedView.Selection.StartOffset = position[0];
				editor.txtEditor.SelectedView.Selection.EndOffset = position[0];
			}
		}

		public static void EditorSetText(CWFile File, string text) {
			AssertOpenProject("EditorSetText");
			AssertOpenFile(File, "EditorSetText");

			GetEditor(GetFile(File)).txtEditor.Document.Text = text;
		}

		public static void EditorInsertText(CWFile File, int offset, string text) {
			AssertOpenProject("EditorInsertText");
			AssertOpenFile(File, "EditorInsertText");

			EditorSetCaret(File, new int[] { offset }, true);
			GetEditor(GetFile(File)).txtEditor.SelectedView.InsertText(text);
		}

		public static string EditorGetSelected(CWFile File) {
			AssertOpenProject("EditorInsertText");
			AssertOpenFile(File, "EditorInsertText");

			return GetEditor(GetFile(File)).txtEditor.SelectedView.SelectedText;
		}

		public static void RemoveDirectory(CWDirectory Dir, bool DelFilesOnDisk) {
			AssertOpenProject("RemoveDirectory");
			AssertValidDirectory(Dir, "RemoveDirectory");

			g.Main.DeleteDirectory(GetDir(Dir), DelFilesOnDisk);
		}

		public static bool IsFileOpen(CWFile File) {
			AssertOpenProject("IsFileOpen");
			AssertValidFile(File, "IsFileOpen");

			return GetEditor(GetFile(File)) != null;
		}

		public static string EditorGetText(CWFile File) {
			AssertOpenProject("EditorGetText");
			AssertOpenFile(File, "EditorGetText");

			return GetEditor(GetFile(File)).txtEditor.Document.Text;
		}

		public static void SetFile(CWFile File) {
			AssertOpenProject("SetFile");
			AssertValidFile(File, "SetFile");

			CProject.File actfile = GetFile(File);

			actfile.FileIcon = File.file_icon;
			actfile.isDirty = File.is_dirty;
			actfile.isForcedReload = File.is_forced_reload;
			actfile.isForeign = File.is_foreign;
			actfile.isPendingReload = File.is_pending_reload;
			actfile.RelativePath = File.relative_path;
			actfile.SimpleName = File.simple_name;

			if (File.parent_dir != null) {
				AssertValidDirectory(File.parent_dir, "SetFile__SetDirectoryOnFile");
				actfile.ParentDir = GetDir(File.parent_dir);
			} else {
				actfile.ParentDir = null;
			}
		}

		public static void SetDirectory(CWDirectory Dir) {
			AssertOpenProject("SetDirectory");
			AssertValidDirectory(Dir, "SetDirectory");

			CProject.Directory actdir = GetDir(Dir);

			actdir.name = Dir.name;

			if (Dir.parent_dir == null) {
				actdir.parent = null;
			} else {
				AssertValidDirectory(Dir.parent_dir, "SetDirectory__SetParentDirectory");

				CProject.Directory newparent = GetDir(Dir.parent_dir);

				if (newparent.GetHashCode() == actdir.GetHashCode())
					throw new PluginException("Setting the parent directory to itself would create a recursive relationship.", "SetDirectory");

				actdir.parent = newparent;
			}
			
		}

		public static void SetProject(CWProject Project) {
			AssertOpenProject("SetProject");

			g.Project.ProjectName = Project.name;
			g.Project.ProjectPath = Project.path;
			g.Project.ProjectType = (short)Project.type;
			g.Project.DebugAutoInsert = Project.debugger_oneclick_enabled;
			g.Project.DebugEnabled = Project.debugger_enabled;
			g.Project.DebugExe = Project.debugger_exe;
			g.Project.DebugMainCs = Project.debugger_oneclick_maincs;
			g.Project.DebugParams = Project.debugger_params;
			g.Project.DebugPasswd = Project.debugger_passwd;
			g.Project.DebugPort = Project.debugger_port;
		}

		public static CWDirectory AddDirectory(string dirname, CWDirectory parent) {
			AssertOpenProject("AddDirectory");

			if (parent != null)
				AssertValidDirectory(parent, "AddDirectory__CheckParent");

			CProject.Directory newdir = new CProject.Directory(dirname, ((parent == null) ? null : GetDir(parent)));
			g.Project.DirList.Add(newdir);

			return newdir.ToCWDir();
		}

		public static void RemoveFile(CWFile FileInfo) {
			AssertOpenProject("RemoveFile");
			AssertValidFile(FileInfo, "RemoveFile");

			g.Main.DeleteFile(GetFile(FileInfo), false);
		}

		public static CWFile AddFile(string filepath, CWDirectory parent) {
			AssertOpenProject("AddFile");

			if (parent != null)
				AssertValidDirectory(parent, "AddFile__CheckParent");

			if (!File.Exists(filepath))
				throw new PluginException(filepath + " does not exist.", "AddFile");

			CProject.File newfile = g.Main.AddExistingFile(filepath, ((parent == null) ? null : GetDir(parent)));
			return newfile.ToCWFile();
		}

		public static bool CloseProject() {
			AssertOpenProject("CloseProject");

			return g.Main.PerformCloseOperations();
		}

		public static void OpenProject(string ProjectFile) {
			g.Main.OpenProject(ProjectFile);
		}

		public static void CloseFile(CWFile FileInfo, bool PromptForSaveIfDirty, bool ForceSaveIfDirty) {
			AssertOpenProject("CloseFile");
			AssertOpenFile(FileInfo, "CloseFile");

			g.Main.CloseFile(GetFile(FileInfo), PromptForSaveIfDirty, ForceSaveIfDirty);
		}

		public static void OpenFile(CWFile FileInfo, int JumpToOffset) {
			AssertOpenProject("OpenFile");
			AssertValidFile(FileInfo, "OpenFile");

			g.Main.OpenFile(GetFile(FileInfo), JumpToOffset, true);
		}

		public static void RegisterForAutoCompletionUpdates(bool Register) {
			throw new PluginException("Feature not implemented", "RegisterForAutoCompletionUpdates");
		}

		public static void DeregisterToolbar(ToolStrip Toolbar) {
			if (g.Main.tscToolbars.TopToolStripPanel.Contains(Toolbar))
				g.Main.tscToolbars.TopToolStripPanel.Container.Remove(Toolbar);

			// Resize the toolstrip
			int finalsize = 0;

			foreach (ToolStrip ts in g.Main.tscToolbars.TopToolStripPanel.Container.Components)
				finalsize += ts.Height;

			g.Main.tscToolbars.Height = finalsize;
		}

		public static void RegisterToolbar(ToolStrip Toolbar) {
			if (g.Main.tscToolbars.TopToolStripPanel.Contains(Toolbar))
				return;

			g.Main.tscToolbars.TopToolStripPanel.Container.Add(Toolbar);

			// Resize the toolstrip
			int finalsize = 0;

			foreach (ToolStrip ts in g.Main.tscToolbars.TopToolStripPanel.Container.Components)
				finalsize += ts.Height;

			g.Main.tscToolbars.Height = finalsize;
		}

		public static void DeregisterWindow(Control Window) {
			if (!g.WorkspaceWindows.Contains(Window))
				return;

			// Force-remove from the docking manager
			WorkspaceWindow ww = g.WorkspaceWindows.FindWorkspaceWindow(Window);
			
			g.Main.dockMgr.Contents.Remove(ww.Content);
			g.WorkspaceWindows.Remove(ww);

		}

		public static void UpdateWindow(Control Window, string WindowSetTextTo, int WindowSetIconTo) {
			if (!g.WorkspaceWindows.Contains(Window))
				return;

			WorkspaceWindow ww = g.WorkspaceWindows.FindWorkspaceWindow(Window);
			ww.Content.ImageIndex = WindowSetIconTo;
			ww.Content.Title = WindowSetTextTo;

			ww.CaptionText = WindowSetTextTo;
		}

		public static void RegisterWindow(string WindowText, int WindowIcon, Control WindowContent) {
			if (g.WorkspaceWindows.Contains(WindowContent))
				return;

			Crownwood.DotNetMagic.Docking.Content newcontent = g.Main.dockMgr.Contents.Add(WindowContent, WindowText);
			newcontent.ImageList = g.Main.ilProject;
			newcontent.ImageIndex = WindowIcon;

			g.Main.dockMgr.AddContentWithState(newcontent, Crownwood.DotNetMagic.Docking.State.DockRight);

			g.WorkspaceWindows.Add(new WorkspaceWindow(
				newcontent,
				WindowText,
				null));
		}

		public static void SpawnBrowser(string Url, bool AllowNavigate) {
			g.Main.SpawnBrowser(Url, AllowNavigate);
		}

		public static void DeregisterEnvironmentTab(Control Tab) {
			PrimaryTab<Control> tab = g.OtherTabs.FindByControl(Tab);

			if (tab == null)
				return;

			// Remove its thing from the tabs
			g.Main.CloseTab<Control>(tab);

			// Remove it from the registry
			g.OtherTabs.Remove(tab);
		}

		public static void UpdateEnvironmentTab(Control Tab, string SetTabTextTo, int SetTabIconTo) {
			PrimaryTab<Control> tab = g.OtherTabs.FindByControl(Tab);

			if (tab == null)
				return;

			tab.page.Title = SetTabTextTo;
			tab.page.ImageIndex = SetTabIconTo;
		}

		public static void FocusEnvironmentTab(Control Tab) {
			PrimaryTab<Control> tab = g.OtherTabs.FindByControl(Tab);

			if (tab == null)
				return;

			tab.page.Selected = true;
		}

		public static void RegisterEnvironmentTab(ICodeweaverPlugin _this, string TabText, int TabIcon, Control TabContent) {
			// Make sure there isn't already a tab with this same control in it
			if (g.OtherTabs.FindByControl(TabContent) != null)
				return;

			// Add the tab
			Crownwood.DotNetMagic.Controls.TabPage newpage = new Crownwood.DotNetMagic.Controls.TabPage(
				TabText,
				TabContent,
				g.Main.ilDocBar,
				TabIcon
			);

			// Add the tab to the collection
			g.OtherTabs.Add(new PrimaryTab<Control>(newpage, TabContent, _this));

			// Display the tab in the interface
			g.Main.tabMain.ActiveLeaf.TabPages.Add(newpage);
		}

		public static void DeregisterCustomConfigTab(Control Tab) {
			CustomConfigTab ct = g.CustomConfigTabs.FindByControl(Tab);

			if (ct == null)
				return;

			g.CustomConfigTabs.Remove(ct);
			GC.ReRegisterForFinalize(Tab);
		}

		public static void RegisterCustomConfigTab(ICodeweaverPlugin _this, string TabText, Control Tab) {
			CustomConfigTab ct = g.CustomConfigTabs.FindByControl(Tab);

			if (ct != null)
				return;

			g.CustomConfigTabs.Add(new CustomConfigTab(_this, Tab, TabText));
			GC.SuppressFinalize(Tab);
		}

		public static void RegisterCustomProjectMenu(ToolStripMenuItem custmenu) {
			g.Main.ctmProject.Items.Add(custmenu);
		}

		public static void RegisterCustomTopMenu(ToolStripMenuItem custmenu) {
			g.Main.mnuMain.Items.Insert(
				g.Main.mnuMain.Items.Count - 2,
				custmenu);
		}

		public static void RegisterCustomPluginMenu(ToolStripMenuItem custmenu) {
			g.Main.mnuPlugins.DropDownItems.Add(custmenu);
		}

		public static void DergisterCustomProjectMenu(ToolStripMenuItem custmenu) {
			g.Main.ctmProject.Items.Remove(custmenu);
		}

		public static void DeregisterCustomTopMenu(ToolStripMenuItem custmenu) {
			g.Main.mnuMain.Items.Remove(custmenu);
		}

		public static void DeregisterCustomPluginMenu(ToolStripMenuItem custmenu) {
			g.Main.mnuPlugins.DropDownItems.Remove(custmenu);
		}

		public static CWFile? GetFile(int id) {
			AssertOpenProject("GetFile");

			foreach (CProject.File file in g.Project.FileList) {
				if (file.GetHashCode() == id)
					return file.ToCWFile();
			}

			return null;
		}

		public static CWFile?[] GetFiles() {
			AssertOpenProject("GetFiles");

			CWFile?[] outfiles = new CWFile?[g.Project.FileList.Count];

			for (int i = 0; i < g.Project.FileList.Count; i++)
				outfiles[i] = GetFile((g.Project.FileList[i] as CProject.File).GetHashCode());

			return outfiles;
		}

		public static CWDirectory GetDirectory(int id) {
			AssertOpenProject("GetFiles");

			foreach (CProject.Directory dir in g.Project.DirList) {
				if (dir.GetHashCode() == id)
					return dir.ToCWDir();
			}

			return null;
		}

		public static CWDirectory[] GetDirectories() {
			AssertOpenProject("GetFiles");

			CWDirectory[] outdirs = new CWDirectory[g.Project.DirList.Count];

			for (int i = 0; i < g.Project.DirList.Count; i++)
				outdirs[i] = GetDirectory((g.Project.DirList[i] as CProject.Directory).GetHashCode());

			return outdirs;
		}

		public static object ConfigGetValue(ICodeweaverPlugin _this, string configKey, object defaultValue) {
			if (!g.Config.PluginConfigs.ContainsKey(_this.CWPluginGuid.ToString() + "__" + configKey))
				return defaultValue;

			return g.Config.PluginConfigs[_this.CWPluginGuid.ToString() + "__" + configKey];
		}

		public static void ConfigSetValue(ICodeweaverPlugin _this, string configKey, object configValue) {
			if (g.Config.PluginConfigs.ContainsKey(_this.CWPluginGuid.ToString() + "__" + configKey))
				g.Config.PluginConfigs[_this.CWPluginGuid + "__" + configKey] = configValue;
			else
				g.Config.PluginConfigs.Add(_this.CWPluginGuid.ToString() + "__" + configKey, configValue);
		}



		////////////////////////////////////////////////////////
		//////			INTERNAL CALLS						////
		////////////////////////////////////////////////////////

		private static void AssertOpenProject(string caller) {
			if (g.Project == null)
				throw new PluginException("Project not open.", caller);
		}

		private static void AssertValidFile(CWFile file, string caller) {
			if (GetFile(file) == null)
				throw new PluginException("Invalid file handle.", caller);
		}

		private static void AssertValidDirectory(CWDirectory dir, string caller) {
			if (GetDir(dir) == null)
				throw new PluginException("Invalid directory handle.", caller);
		}

		private static void AssertOpenFile(CWFile file, string caller) {
			CProject.File actfile = GetFile(file);

			if (actfile == null)
				throw new PluginException("Invalid file handle.", caller);

			if (GetEditor(actfile) == null)
				throw new PluginException("File not open", caller);
		}

		private static UCEditor GetEditor(CProject.File File) {
			if (File == null)
				return null;

			foreach (PrimaryTab<UCEditor> editor in g.Editors) {
				if (editor.Control.g_curFile == File)
					return editor.Control;
			}

			return null;
		}

		private static CProject.File GetFile(CWFile file) {
			foreach (CProject.File fileX in g.Project.FileList) {
				if (fileX.GetHashCode() == file.id)
					return fileX;
			}

			return null;
		}

		private static CProject.Directory GetDir(CWDirectory dir) {
			foreach (CProject.Directory dirX in g.Project.DirList) {
				if (dirX.GetHashCode() == dir.GetHashCode())
					return dirX;
			}

			return null;
		}
	}
}
