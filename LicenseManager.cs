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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Security.Cryptography;

namespace TSDev {
	class LicenseManager {
		public LicenseManager() {
			g.LogDebug("LicenseManager::ctor: Enter - Override Constructor () -- Load default license");

			// License override
			_user = "Standard Edition User";
			_company = "TorqueDev IDE";
			_limit = "Unlimited";
			_serial = "CWF-134444-00UL";
			_expires = new DateTime(1);
			_version = new Version("1.2");
			_version_limit = new Version("1.3");
			_signature = null;

			_features.Add("TGE", true);
			_features.Add("TSE", true);
			_features.Add("T2D", true);
			_features.Add("BROWSER", true);
			_features.Add("T_AUTOCOMPLETE", true);
			_features.Add("VARDECL", true);
			_features.Add("PLUGINS", true);
			_features.Add("MOREPLUGINS", false);
			_features.Add("SCC", false);
			_features.Add("MACROS", true);
			_features.Add("INTELLICODE", true);
			_features.Add("DEBUGGING", true);
			_features.Add("SNIPPETS", true);
			_features.Add("PRO", false);
			_features.Add("MANAGED", false);
			_features.Add("FREE", true);
			_features.Add("DONATOR", false);

			_valid = true;

			g.LogDebug("LicenseManager::ctor: Leave");
		}

		public LicenseManager(string licfile) {
			g.LogDebug("LicenseManager::ctor: Default constructor - Load license file - " + licfile);

			if (!File.Exists(licfile))
				throw new Exception("Unable to load license file ... it doesn't exist!");

			g.LogDebug("LicenseManager::ctor: Loading XML document");
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.Load(licfile);

			g.LogDebug("LicenseManager::ctor: Loaded XML document; extracting XML elements in LicenseFile");
			XmlNodeList licnodes = xmldoc.GetElementsByTagName("LicenseFile")[0].ChildNodes;

			foreach (XmlNode node in licnodes) {
				g.LogDebug("LicenseManager::ctor: Got " + node.Name + " with value " + node.InnerText);

				switch (node.Name) {
					case "LicensedToUser":
						_user = node.InnerText;
						break;
					case "LicensedToCompany":
						_company = node.InnerText;
						break;
					case "LicenseLimit":
						_limit = node.InnerText;
						break;
					case "LicenseSerial":
						_serial = node.InnerText;
						break;
					case "Expires":
						_expires = ((node.InnerText == "Never") ? new DateTime(1) : DateTime.Parse(node.InnerText));
						break;
					case "Version":
						_version = new Version(node.InnerText);
						break;
					case "VersionUpperLimit":
						_version_limit = new Version(node.InnerText);
						break;
					case "Signature":
						_signature = node.InnerText;
						break;
					case "FeatureBits":
						LoadFeatureBits(node.ChildNodes);
						break;
				}
			}

			//GenerateLicense();
			g.LogDebug("LicenseManager::ctor: Verifying license");
			_valid = VerifyLicense();

			g.LogDebug("LicenseManager::ctor: Leave");
		}

		private string _user = "";
		private string _company = "";
		private string _limit = "";
		private string _serial = "";
		private DateTime _expires = DateTime.Now;
		private Version _version = null;
		private Version _version_limit = null;
		private string _signature = "";
		private bool _valid = false;

		private Hashtable _features = new Hashtable();

		/* 
		 * This is the original private key for TorqueDev.  Since pro versions and 
		 * licenses were never officially released, it's kind of pointless so I left
		 * it in here for the sake of it being here.  I suppose you can use it if
		 * you want but what for?
		 * 
		 */
		//private string PrivateKey = "<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter><X>q5FlYAqyvS2hR8UM3FUtrhF6ZqE=</X></DSAKeyValue>";
		private string PublicKey = "<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter></DSAKeyValue>";

		public string LicensedUser { get { return _user; } }
		public string LicensedCompany { get { return _company; } }
		public string LicenseLimit { get { return _limit; } }
		public string LicenseSerial { get { return _serial; } }
		public DateTime LicenseExpires { get { return _expires; } }
		public Version LicenseVersion { get { return _version; } }
		public Version LicenseVersionLimit { get { return _version_limit; } }
		public string LicenseSignature { get { return _signature; } }
		public bool LicenseValid { get { return _valid; } }

		public bool this[string FeatureBit] {
			get {
				if (!_features.ContainsKey(FeatureBit))
					return false;

				return (bool)_features[FeatureBit];
			}
		}

		#region Load Feature Bits
		private void LoadFeatureBits(XmlNodeList features) {
			g.LogDebug("LicenseManager::LoadFeatureBits: Enter");

			foreach (XmlNode feature in features) {
				if (feature.Attributes == null || feature.Attributes["id"] == null || feature.Attributes["active"] == null)
					continue;

				g.LogDebug("LicenseManager::LoadFeatureBits: Got bit " + feature.Attributes["id"].Value + " with value " + feature.Attributes["active"].Value);

				switch (feature.Attributes["id"].Value) {
					case "0":									// TGE
						_features.Add("TGE", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "1":									// TSE
						_features.Add("TSE", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "2":									// T2D
						_features.Add("T2D", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "3":									// Web Browser
						_features.Add("BROWSER", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "4":									// Type-as-you-go Autocomplete
						_features.Add("T_AUTOCOMPLETE", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "5":									// __decl lines
						_features.Add("VARDECL", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "6":									// Plugin support
						_features.Add("PLUGINS", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "7":									// Allow more than 3 plugins
						_features.Add("MOREPLUGINS", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "8":									// Source code control
						_features.Add("SCC", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "9":									// Macros
						_features.Add("MACROS", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "10":									// Intellicode
						_features.Add("INTELLICODE", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "11":									// Debugging
						_features.Add("DEBUGGING", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "12":									// Snippets
						_features.Add("SNIPPETS", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "13":									// Professional Edition
						_features.Add("PRO", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "14":									// Managed Edition
						_features.Add("MANAGED", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "15":									// Free Edition
						_features.Add("FREE", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
					case "16":									// Donator's Edition
						_features.Add("DONATOR", Convert.ToBoolean(feature.Attributes["active"].Value));
						break;
				}
			}

			g.LogDebug("LicenseManager::LoadFeatureBits: Leave");
		}
		#endregion

		public static void WriteDefaultLicense(string licfile) {
			g.LogDebug("LicenseManager::WriteDefaultLicense: Enter");
			g.LogDebug("LicenseManager::WriteDefaultLicense: Writing to " + licfile);

			string[] towrite = new string[] {
				"<!-- " , 
				"		This license file is automatically generated.  Changing" ,
				"		parameters will invalidate the license.  Deleting this file",
				"		will cause it to be re-initialized when Codeweaver is reloaded.",
				"",
				"		To restore valid licenses, simply copy over the other license.xml",
				"		file to this location, replacing this file.",
				"",
				"		/* DEFAULT STANDARD EDITION LICENSE */",
				"		/* Features restricted:             */",
				"		/*		(o) Source Code Control     */",
				"		/*		(o) Plugins Limit is 3      */",
				"",
				"		To purchase a license, please visit www.torquedev.com.",
				"-->",
				"",
				"<LicenseFile>",
				"	<LicensedToUser>Standard Edition User</LicensedToUser>",
				"	<LicensedToCompany>Codeweaver IDE</LicensedToCompany>",
				"	<LicenseLimit>Unlimited</LicenseLimit>",
				"	<LicenseSerial>CWF-134444-00UL</LicenseSerial>",
				"	<FeatureBits>",
				"		<!-- The sequence of this matters; do not change -->",
				"		<Feature id=\"0\" active=\"true\" />",
				"		<Feature id=\"1\" active=\"true\" />",
				"		<Feature id=\"2\" active=\"true\" />",
				"		<Feature id=\"3\" active=\"true\" />",
				"		<Feature id=\"4\" active=\"true\" />",
				"		<Feature id=\"5\" active=\"true\" />",
				"		<Feature id=\"6\" active=\"true\" />",
				"		<Feature id=\"7\" active=\"false\" />",
				"		<Feature id=\"8\" active=\"false\" />",
				"		<Feature id=\"9\" active=\"true\" />",
				"		<Feature id=\"10\" active=\"true\" />",
				"		<Feature id=\"11\" active=\"true\" />",
				"		<Feature id=\"12\" active=\"true\" />",
				"		<Feature id=\"13\" active=\"false\" />",
				"		<Feature id=\"14\" active=\"false\" />",
				"		<Feature id=\"15\" active=\"true\" />",
				"		<Feature id=\"16\" active=\"false\" />",
				"	</FeatureBits>",
				"	<Expires>Never</Expires>",
				"	<Version>1.2</Version>",
				"	<VersionUpperLimit>1.3</VersionUpperLimit>",
				"	<Signature>h0laEyQn6g/aheGjS7B5G9Y0oQ8i52LyJJTXzvG/WVfVy/WacjfQng==</Signature>",
				"</LicenseFile>",
				"<!-- EOF -->"
			};

			// Write everything to the file
			FileStream fs = new FileStream(licfile, FileMode.Create, FileAccess.Write, FileShare.None);

			byte[] finalout = System.Text.ASCIIEncoding.ASCII.GetBytes(String.Join("\r\n", towrite));

			fs.Write(finalout, 0, finalout.Length);
			fs.Close();
		}

		/*private void GenerateLicense() {
			DSA dsa = new DSACryptoServiceProvider();
			dsa.FromXmlString(PrivateKey);

			string vfyhash =
				"Sam Bacsa" +
				"netMercs Group" +
				"Unlimited" +
				"CMS-102022-00UL" +
				"Never" +
				"1.2" +
				"1.3";

			// Grab all the featurebits
			foreach (bool val in _features.Values)
				vfyhash += val.ToString();

			// Hash it and get the result
			string hash = Convert.ToBase64String(dsa.CreateSignature(Convert.FromBase64String(CConfig.SHA1(vfyhash))));
		}*/

		private bool VerifyLicense() {
			// Verifies the loaded license details to see if they
			// check out
			g.LogDebug("LicenseManager::VerifyLicense: Enter");

			g.LogDebug("LicenseManager::VerifyLicense: Loading DSA object and public key");
			DSA dsa = new DSACryptoServiceProvider();
			dsa.FromXmlString(PublicKey);

			g.LogDebug("LicenseManager::VerifyLicense: Object loaded.  Key is " + PublicKey);

			string vfyhash = _user + _company + _limit + _serial + ((_expires.Year == 1) ? "Never" : _expires.ToString())
					+ _version.ToString() + _version_limit.ToString();

			// Load the feature bits
			foreach (bool val in _features.Values)
				vfyhash += val.ToString();

			g.LogDebug("LicenseManager::VerifyLicense: vfyhash is " + vfyhash);
			g.LogDebug("LicenseManager::VerifyLicense: Verifying signature");

			bool result = dsa.VerifySignature(
					Convert.FromBase64String(CConfig.SHA1(vfyhash)),
					Convert.FromBase64String(_signature)
			);

			g.LogDebug("LicenseManager::VerifyLicense: Result of signature verification is: " + result.ToString());
			g.LogDebug("LicenseManager::VerifyLicense: Leave");

			return result;
		}
	}
}
