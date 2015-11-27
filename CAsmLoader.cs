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
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.IO;
using System.Diagnostics;
using TSDev;


namespace netMercs.Loader {

	public sealed class Embed {

            

		// this class is static, and should not be instantiated.

		private Embed() {}

 

 

		// Init is the only public callable method of this class.  It should be called once

		// (and only once!)

		// to register the LoadComponentAssembly method as the event handler to call should the 

		// AssemblyResolve event get fired.

		public static void Init(){

			ResolveEventHandler loadAssembly = new ResolveEventHandler(LoadComponentAssembly);

			AppDomain.CurrentDomain.AssemblyResolve += loadAssembly;

		}

 

		// This method is called when an assembly fails to load via probing path, GAC, etc.
		// Note that we return null to the CLR if we have no assembly to load.  This is 
		// expected behavior, so that the CLR may notify the rest of the application that
		// it could not load the assembly in question.
		static Assembly LoadComponentAssembly(Object sender, ResolveEventArgs args) {

			// We'll use this reference fairly often in the future...
			Assembly assembly = Assembly.GetExecutingAssembly();
                  
			// Get the requested assembly's simple name (no namespace info or file extension)
			string simpleName = args.Name.Substring(0, args.Name.IndexOf(',') );
                   
			string dllImageResourceName = getResourceLibName( simpleName, assembly );

			g.LogDebug("NMLOADER: Loading requested assembly: " + simpleName + " / " + assembly.ToString());

			return streamFromResource(dllImageResourceName, assembly);
                        
		}
 
		private static string getResourceLibName(string simpleLibName){
			return getResourceLibName( simpleLibName, Assembly.GetExecutingAssembly() );
		}
 
		// We will go through the list of resources in the assembly and using the 
		// simpleLibName, we will find if the dll resource is embedded in the assembly
		// Note that we return null on purpose if we didn't find anything.
		// This is because we also want to return null to the CLR if we have no assembly to load.
		private static string getResourceLibName(string simpleLibName, Assembly assembly) {
			if ( simpleLibName == null || assembly == null ) return null;
 
			simpleLibName += ".dll"; // assume that the file ends in this extension.
			string dllImageResourceName = null;
                  
			// We will iterate through the list of resources in this assembly,
			// looking for the name of the assembly that failed to load from disk
			foreach (string resourceName in assembly.GetManifestResourceNames()) {
				if (resourceName.Length < simpleLibName.Length) continue;
                              
				// if the simpleName and resourceName end the same (we drop namespace info here),
				// then this should be the embedded assembly that we are looking for.
				if (String.Compare(simpleLibName,
					0,
					resourceName,
					(resourceName.Length - simpleLibName.Length),
					simpleLibName.Length,
					true) == 0 ) {
                              
					dllImageResourceName = resourceName;
				}
			}           
			return dllImageResourceName;  
		}
 
		private static Assembly streamFromResource(string dllImageResourceName){
			return streamFromResource(dllImageResourceName, Assembly.GetExecutingAssembly() );
		}
 
		// this is the 'workhorse' of the class.  Once we've got a resource name in the assembly,
		// we stream the resource to a byte array, and load the Assembly from the byte array.
            
		private static Assembly streamFromResource(string dllImageResourceName, Assembly assembly){
			if ( dllImageResourceName == null || assembly == null ) return null;
                  
			Stream imageStream;
			imageStream = assembly.GetManifestResourceStream(dllImageResourceName);
			long bytestreamMaxLength = imageStream.Length;
            
			byte[] buffer = new byte[bytestreamMaxLength];
			imageStream.Read(buffer,0,(int)bytestreamMaxLength);
                  
			return  AssemblyBuilder.Load(buffer);
		}
	}
}