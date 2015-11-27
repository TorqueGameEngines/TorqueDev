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
using System.Security.Cryptography;
using System.Security;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TSDev
{
	/***********************
	 * 
	 * Used for cryptography functions:
	 * 
	 * PUBLIC KEY:
	 *		<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter></DSAKeyValue>
	 * 
	 * 
	 * PRIVATE/PUBLIC KEYPAIR:
	 *		<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter><X>q5FlYAqyvS2hR8UM3FUtrhF6ZqE=</X></DSAKeyValue>
	 * 
	 * 
	 * *********************/

	internal abstract class CVerify
	{
		
		//public static string PrivateKey = "<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter><X>q5FlYAqyvS2hR8UM3FUtrhF6ZqE=</X></DSAKeyValue>";
		public static string PublicKey = "<DSAKeyValue><P>v9B+VOY0xg7z8tlT+7ALGLZDGCcKbheTYd7eIUfBmU8BloVn97DnU49HGWx/KIx5BRkf87JAtUBENQ+NEVoNRIvsowOuGY5biP5xdCOw+OuKEoVobgXM6cQ+FHhm7dKCOfKw4onlBZSbykyE/g3z9EJjlmSmMgB/yCGHSJSyOTU=</P><Q>rpQWFUx8Y3w2CDXc3MVuP8ugjXk=</Q><G>JQZg0wH3U0zy1DvlVrL7BwveZncLNcVaxG8HvRKMHiON+w2BAzGM7xBnXe96nJ/BStvJFzJq6y29azD/kvC2U9/t2ALh8PanBhWlbmTIS6BopyJ0RxpuM5sRtTamXnLgI7y4B6rTfnFfFX7U8eldV6CRUmDifh0ZZxbD78/BFYo=</G><Y>R3busGxRGdW6lTbcGZAQlvkoLlxhIkQqPeLepBx4mHXcd6epC4IK/FuMOM0stLuwX5WFz15oxILoxZDfyO0WwiP0bjyJ9t7DrerYdH7JXhg1aeRzhKJFaQPojYkvla3cNDaoZPw25jQ8kEigvL69HDxFm1nPM076b9dBEV+yymc=</Y><J>AAAAARlGTpkJFSjk5pWsdf0YhNUiTmDoBG4wVsb1dU0vn/67Z+du7y8R4HfRwoAUvKBzNuclISy1Zx6y9XuyauAtgL315gpQwlbXkZfeTNVlNOajUDd0Y3g+MYuNZ2iYqbxIv4DDfvhcHm6sSX5Z1A==</J><Seed>fpbN84HbBsVGshjSsj3Rl6iHPcY=</Seed><PgenCounter>Alo=</PgenCounter></DSAKeyValue>";

		internal class AuthInfo {
			public AuthInfo(string version, string name, string expires, string machinecode, string featurebit, string hash, string signature, string email) {
				this.version = version;
				this.name = name;
				this.expires = expires;
				this.machinecode = machinecode;
				this.featurebit = featurebit;
				this.hash = hash;
				this.signature = signature;
				this.email = email;
			}

			public string version = "", name = "", expires = "", machinecode = "", featurebit = "", hash = "", signature = "", email = "";
		}

		internal static bool VerifyLocalKeyfile(string filename, out AuthInfo authinfo) {
			StreamReader reader = null; 
			string data = "";

			authinfo = null;

			try {
				reader = new StreamReader(filename);
				data = reader.ReadToEnd();
				reader.Close();
			} catch (Exception exc) {
				MessageBox.Show("Offline key authorization failure: Could not read keyfile.\n\n\n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			Regex rx = new Regex(@"\s*Keyfile(\n*|\s*)?" + 
				@"(?<inner>" +
				@"(?>" +
				@"\{(?<LEVEL>)" +
				@"|" +
				@"\};(?<-LEVEL>)" +
				@"|" +
				@"(?!\{|\};)." +
				@")+" +
				@"(?(LEVEL)(?!))" +
				@")"
				, RegexOptions.IgnoreCase | RegexOptions.Singleline);

			Match topmatch = rx.Match(data);

			if (!topmatch.Success) {
				MessageBox.Show("Offline key authorization failure: Could not find valid 'keyfile' block.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			string keyfile_data = topmatch.Groups["inner"].Value;

			// Once we have the top matched group, parse each line in it
			rx = new Regex(@"\s*\b(?<name>[A-Z]*)\b\s*=\s*((" + "\"" + @"\s*(?<single>.*?)\s*" + "\"" + @"\s*;)|(\{(?<multi>.*?)\};))", RegexOptions.Singleline | RegexOptions.IgnoreCase);

			// 1 = name
			// 4 = normal definition
			// 6 = multiline definition

			string version = "";
			string name = "";
			string expires = "";
			string machinecode = "";
			string featurebit = "";
			string hash = "";
			string signature = "";
			string email = "";
			
			foreach(Match m in rx.Matches(keyfile_data)) {
				switch(m.Groups["name"].Value.ToLower()) {
					case "version":
						version = m.Groups["single"].Value;
						break;
					case "name":
						name = m.Groups["single"].Value;
						break;
					case "expires":
						expires = m.Groups["single"].Value;
						break;
					case "machinecode":
						machinecode = m.Groups["single"].Value;
						break;
					case "featurebit":
						featurebit = m.Groups["single"].Value;
						break;
					case "hash":
						hash = m.Groups["single"].Value;
						break;
					case "keysignature":
						signature = m.Groups["multi"].Value;
						break;
					case "email":
						email = m.Groups["single"].Value;
						break;
				}
			}

			// Clean up signature
			signature = signature.Replace(" ", "");
			signature = signature.Replace("\t", "");
			signature = signature.Replace("\n", "");
			signature = signature.Replace("\r", "");


			// Check field validity
			if (version == "" || expires == "" || machinecode == "" || featurebit == "" || hash == "" || name == "" || email == "" || signature == "") {
				MessageBox.Show("Offline key authorization failure: Keyfile construction incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Check version
			if (version != "1.00") {
				MessageBox.Show("Offline key authorization failure: Incorrect version (Expected '1.00')", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Check expiration date
			try {
				DateTime dt = DateTime.Parse(expires);
				if (DateTime.Now.Ticks >= dt.Ticks) {
					MessageBox.Show("Offline key authorization has expired.  Please visit www.torquedev.com to request a new offline authorization file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
			} catch (Exception exc) {
				MessageBox.Show("Offline key authorization failure: Unable to verify date stamp.  Error returned was: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Check if the machine codes match
			if (machinecode != CConfig.GetHardKey()) {
				MessageBox.Show("Offline key authorization failure: Machine code mismatch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Verify hash of data
			string sighash = CConfig.SHA1(version + name + machinecode + expires + featurebit + email);

			if (sighash != hash) {
				MessageBox.Show("Offline key authorization failure: Checksum mismatch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Verify signature by first loading the public key into memory
			DSA dsa = new DSACryptoServiceProvider();

			try {
				dsa.FromXmlString(CVerify.PublicKey);
			} catch (Exception exc) {
				MessageBox.Show("Offline key authorization failure: Unable to load DSA public key.  Error returned was: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			// Verify the data against the signature
			try {
				if (!dsa.VerifySignature(Convert.FromBase64String(hash), Convert.FromBase64String(signature))) {
					MessageBox.Show("Offline key authorization failure: File signature is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					dsa.Clear();
					return false;
				} else {
					// Signature checked out.  Create the passback reference
					authinfo = new AuthInfo(version, name, expires, machinecode, featurebit, hash, signature, email);
					return true;
				}
			} catch (Exception exc) {
				MessageBox.Show("Offline key authorization failure: DSA verification failure.  Error returned was: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				dsa.Clear();
				return false;
			}

		}
	}
}
