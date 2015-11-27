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
using System.IO;
using System.Collections;

namespace netMercs.TorqueDev.Lexer {

	public class Token {
		public int kind;    // token kind
		public int pos;     // token position in the source text (starting at 0)
		public int col;     // token column (starting at 0)
		public int line;    // token line (starting at 1)
		public string val;  // token value
		public Token next;  // ML 2005-03-11 Tokens are kept in linked list
	}

	public class Buffer {
		public const char EOF = (char)256;
		const int MAX_BUFFER_LENGTH = 64 * 1024; // 64KB
		byte[] buf;         // input buffer
		int bufStart;       // position of first byte in buffer relative to input stream
		int bufLen;         // length of buffer
		int fileLen;        // length of input stream
		int pos;            // current position in buffer
		Stream stream;      // input stream (seekable)
		bool isUserStream;  // was the stream opened by the user?

		public Buffer(Stream s, bool isUserStream) {
			stream = s; this.isUserStream = isUserStream;
			fileLen = bufLen = (int)s.Length;
			if (stream.CanSeek && bufLen > MAX_BUFFER_LENGTH) bufLen = MAX_BUFFER_LENGTH;
			buf = new byte[bufLen];
			bufStart = Int32.MaxValue; // nothing in the buffer so far
			Pos = 0; // setup buffer to position 0 (start)
			if (bufLen == fileLen) Close();
		}

		~Buffer() { Close(); }

		void Close() {
			if (!isUserStream && stream != null) {
				stream.Close();
				stream = null;
			}
		}

		public int Read() {
			if (pos < bufLen) {
				return buf[pos++];
			} else if (Pos < fileLen) {
				Pos = Pos; // shift buffer start to Pos
				return buf[pos++];
			} else {
				return EOF;
			}
		}

		public int Peek() {
			if (pos < bufLen) {
				return buf[pos];
			} else if (Pos < fileLen) {
				Pos = Pos; // shift buffer start to Pos
				return buf[pos];
			} else {
				return EOF;
			}
		}

		public string GetString(int beg, int end) {
			int len = end - beg;
			char[] buf = new char[len];
			int oldPos = Pos;
			Pos = beg;
			for (int i = 0; i < len; i++) buf[i] = (char)Read();
			Pos = oldPos;
			return new String(buf);
		}

		public int Pos {
			get { return pos + bufStart; }
			set {
				if (value < 0) value = 0;
				else if (value > fileLen) value = fileLen;
				if (value >= bufStart && value < bufStart + bufLen) { // already in buffer
					pos = value - bufStart;
				} else if (stream != null) { // must be swapped in
					stream.Seek(value, SeekOrigin.Begin);
					bufLen = stream.Read(buf, 0, buf.Length);
					bufStart = value; pos = 0;
				} else {
					pos = fileLen - bufStart; // make Pos return fileLen
				}
			}
		}
	}

	public class Scanner {
		const char EOL = '\n';
		const int eofSym = 0; /* pdt */
		const int charSetSize = 256;
		const int maxT = 75;
		const int noSym = 75;
		short[] start = {
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0, 56,  2,  0, 51, 52, 59,  4, 20, 21, 61, 57, 26, 58, 53, 62,
	 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 55, 19, 65, 60, 66, 27,
	 50,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
	  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1, 31,  0, 32, 64,  1,
	  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,
	  1,  1,  1, 54,  1,  1,  1,  1,  1,  1,  1, 23, 63, 24, 28,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
	  -1};


		public Buffer buffer; // scanner buffer

		Token t;          // current token
		char ch;          // current input character
		int pos;          // column number of current character
		int line;         // line number of current character
		int lineStart;    // start position of current line
		int oldEols;      // EOLs that appeared in a comment;
		BitArray ignore;  // set of characters to be ignored by the scanner

		Token tokens;     // list of tokens already peeked (first token is a dummy)
		Token pt;         // current peek token

		char[] tval = new char[128]; // text of current token
		int tlen;         // length of current token

		public Scanner(string fileName) {
			try {
				Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				buffer = new Buffer(stream, false);
				Init();
			} catch (IOException) {
				Console.WriteLine("--- Cannot open file {0}", fileName);
				System.Environment.Exit(1);
			}
		}

		public Scanner(Stream s) {
			buffer = new Buffer(s, true);
			Init();
		}

		void Init() {
			pos = -1; line = 1; lineStart = 0;
			oldEols = 0;
			NextCh();
			ignore = new BitArray(charSetSize + 1);
			ignore[' '] = true;  // blanks are always white space
			ignore[9] = true; ignore[10] = true; ignore[13] = true;
			pt = tokens = new Token();  // first token is a dummy
		}

		void NextCh() {
			if (oldEols > 0) { ch = EOL; oldEols--; } else {
				ch = (char)buffer.Read(); pos++;
				// replace isolated '\r' by '\n' in order to make
				// eol handling uniform across Windows, Unix and Mac
				if (ch == '\r' && buffer.Peek() != '\n') ch = EOL;
				if (ch == EOL) { line++; lineStart = pos + 1; }
			}

		}

		void AddCh() {
			if (tlen >= tval.Length) {
				char[] newBuf = new char[2 * tval.Length];
				Array.Copy(tval, 0, newBuf, 0, tval.Length);
				tval = newBuf;
			}
			tval[tlen++] = ch;
			NextCh();
		}



		bool Comment0() {
			int level = 1, line0 = line, lineStart0 = lineStart;
			NextCh();
			if (ch == '/') {
				NextCh();
				for (; ; ) {
					if (ch == 10) {
						level--;
						if (level == 0) { oldEols = line - line0; NextCh(); return true; }
						NextCh();
					} else if (ch == Buffer.EOF) return false;
					else NextCh();
				}
			} else {
				if (ch == EOL) { line--; lineStart = lineStart0; }
				pos = pos - 2; buffer.Pos = pos + 1; NextCh();
			}
			return false;
		}

		bool Comment1() {
			int level = 1, line0 = line, lineStart0 = lineStart;
			NextCh();
			if (ch == '*') {
				NextCh();
				for (; ; ) {
					if (ch == '*') {
						NextCh();
						if (ch == '/') {
							level--;
							if (level == 0) { oldEols = line - line0; NextCh(); return true; }
							NextCh();
						}
					} else if (ch == Buffer.EOF) return false;
					else NextCh();
				}
			} else {
				if (ch == EOL) { line--; lineStart = lineStart0; }
				pos = pos - 2; buffer.Pos = pos + 1; NextCh();
			}
			return false;
		}


		void CheckLiteral() {
			switch (t.val) {
				case "if": t.kind = 8; break;
				case "else": t.kind = 11; break;
				case "switch": t.kind = 12; break;
				case "while": t.kind = 16; break;
				case "for": t.kind = 17; break;
				case "break": t.kind = 18; break;
				case "continue": t.kind = 19; break;
				case "return": t.kind = 20; break;
				case "package": t.kind = 21; break;
				case "function": t.kind = 22; break;
				case "datablock": t.kind = 25; break;
				case "case": t.kind = 27; break;
				case "default": t.kind = 28; break;
				case "new": t.kind = 31; break;
				case "or": t.kind = 53; break;
				case "TAB": t.kind = 67; break;
				case "NL": t.kind = 68; break;
				case "SPC": t.kind = 69; break;
				case "true": t.kind = 73; break;
				case "false": t.kind = 74; break;
				default: break;
			}
		}

		Token NextToken() {
			while (ignore[ch]) NextCh();
			if (ch == '/' && Comment0() || ch == '/' && Comment1()) return NextToken();
			int apx = 0;
			t = new Token();
			t.pos = pos; t.col = pos - lineStart + 1; t.line = line;
			int state = start[ch];
			tlen = 0; AddCh();

			switch (state) {
				case -1: { t.kind = eofSym; break; } // NextCh already done
				case 0: { t.kind = noSym; break; }   // NextCh already done
				case 1:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 1; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 2:
					if ((ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '!' || ch >= '#' && ch <= '[' || ch >= ']' && ch <= 255)) { AddCh(); goto case 2; } else if (ch == '"') { AddCh(); goto case 3; } else if (ch == 92) { AddCh(); goto case 16; } else { t.kind = noSym; break; }
				case 3: { t.kind = 2; break; }
				case 4:
					if ((ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '&' || ch >= '(' && ch <= '[' || ch >= ']' && ch <= 255)) { AddCh(); goto case 4; } else if (ch == 39) { AddCh(); goto case 5; } else if (ch == 92) { AddCh(); goto case 17; } else { t.kind = noSym; break; }
				case 5: { t.kind = 3; break; }
				case 6:
					if ((ch >= '0' && ch <= '9')) { AddCh(); goto case 6; } else { t.kind = 4; break; }
				case 7:
					if ((ch >= '0' && ch <= '9')) { AddCh(); goto case 7; } else { t.kind = 4; break; }
				case 8: {
						tlen -= apx;
						pos = pos - apx - 1; line = t.line;
						buffer.Pos = pos + 1; NextCh();
						t.kind = 4; break;
					}
				case 9:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 9; } else if (ch == ':') { AddCh(); goto case 11; } else { t.kind = 5; break; }
				case 10:
					if ((ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 9; } else { t.kind = noSym; break; }
				case 11:
					if (ch == ':') { AddCh(); goto case 10; } else { t.kind = noSym; break; }
				case 12:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 12; } else if (ch == ':') { AddCh(); goto case 14; } else { t.kind = 6; break; }
				case 13:
					if ((ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 12; } else { t.kind = noSym; break; }
				case 14:
					if (ch == ':') { AddCh(); goto case 13; } else { t.kind = noSym; break; }
				case 15:
					if ((ch >= '0' && ch <= '9')) { AddCh(); goto case 15; } else if (ch == '.') { apx++; AddCh(); goto case 18; } else { t.kind = 4; break; }
				case 16:
					if ((ch == '"' || ch == '$' || ch == 39 || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == 92 || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 2; } else { t.kind = noSym; break; }
				case 17:
					if ((ch == '"' || ch == '$' || ch == 39 || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == 92 || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 4; } else { t.kind = noSym; break; }
				case 18:
					if ((ch <= '/' || ch >= ':' && ch <= 255)) { apx++; AddCh(); goto case 8; } else if ((ch >= '0' && ch <= '9')) { apx = 0; AddCh(); goto case 7; } else { t.kind = noSym; break; }
				case 19: { t.kind = 7; break; }
				case 20: { t.kind = 9; break; }
				case 21: { t.kind = 10; break; }
				case 22: { t.kind = 13; break; }
				case 23: { t.kind = 14; break; }
				case 24: { t.kind = 15; break; }
				case 25: { t.kind = 23; break; }
				case 26: { t.kind = 24; break; }
				case 27: { t.kind = 30; break; }
				case 28: { t.kind = 34; break; }
				case 29: { t.kind = 35; break; }
				case 30: { t.kind = 36; break; }
				case 31: { t.kind = 39; break; }
				case 32: { t.kind = 40; break; }
				case 33: { t.kind = 42; break; }
				case 34: { t.kind = 43; break; }
				case 35: { t.kind = 44; break; }
				case 36: { t.kind = 45; break; }
				case 37: { t.kind = 46; break; }
				case 38: { t.kind = 47; break; }
				case 39: { t.kind = 48; break; }
				case 40: { t.kind = 49; break; }
				case 41: { t.kind = 50; break; }
				case 42: { t.kind = 51; break; }
				case 43: { t.kind = 52; break; }
				case 44: { t.kind = 54; break; }
				case 45: { t.kind = 57; break; }
				case 46: { t.kind = 58; break; }
				case 47: { t.kind = 59; break; }
				case 48: { t.kind = 62; break; }
				case 49: { t.kind = 63; break; }
				case 50: { t.kind = 66; break; }
				case 51:
					if ((ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 9; } else if (ch == '=') { AddCh(); goto case 47; } else { t.kind = noSym; break; }
				case 52:
					if ((ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 12; } else if (ch == '=') { AddCh(); goto case 37; } else { t.kind = 72; break; }
				case 53:
					if ((ch >= '0' && ch <= '9')) { AddCh(); goto case 6; } else { t.kind = 38; break; }
				case 54:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'v' || ch >= 'x' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == 'w') { AddCh(); goto case 67; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 55:
					if (ch == ':') { AddCh(); goto case 25; } else { t.kind = 26; break; }
				case 56:
					if (ch == '=') { AddCh(); goto case 45; } else { t.kind = 29; break; }
				case 57:
					if (ch == '+') { AddCh(); goto case 29; } else if (ch == '=') { AddCh(); goto case 33; } else { t.kind = 32; break; }
				case 58:
					if (ch == '-') { AddCh(); goto case 30; } else if (ch == '=') { AddCh(); goto case 34; } else { t.kind = 33; break; }
				case 59:
					if (ch == '=') { AddCh(); goto case 38; } else if (ch == '&') { AddCh(); goto case 44; } else { t.kind = 37; break; }
				case 60:
					if (ch == '=') { AddCh(); goto case 46; } else { t.kind = 41; break; }
				case 61:
					if (ch == '=') { AddCh(); goto case 35; } else { t.kind = 70; break; }
				case 62:
					if (ch == '=') { AddCh(); goto case 36; } else { t.kind = 71; break; }
				case 63:
					if (ch == '=') { AddCh(); goto case 39; } else if (ch == '|') { AddCh(); goto case 43; } else { t.kind = 55; break; }
				case 64:
					if (ch == '=') { AddCh(); goto case 40; } else { t.kind = 56; break; }
				case 65:
					if (ch == '<') { AddCh(); goto case 68; } else if (ch == '=') { AddCh(); goto case 48; } else { t.kind = 60; break; }
				case 66:
					if (ch == '>') { AddCh(); goto case 69; } else if (ch == '=') { AddCh(); goto case 49; } else { t.kind = 61; break; }
				case 67:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'h' || ch >= 'j' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == 'i') { AddCh(); goto case 70; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 68:
					if (ch == '=') { AddCh(); goto case 41; } else { t.kind = 64; break; }
				case 69:
					if (ch == '=') { AddCh(); goto case 42; } else { t.kind = 65; break; }
				case 70:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 's' || ch >= 'u' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == 't') { AddCh(); goto case 71; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 71:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'b' || ch >= 'd' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == 'c') { AddCh(); goto case 72; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 72:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'g' || ch >= 'i' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == 'h') { AddCh(); goto case 73; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }
				case 73:
					if ((ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z')) { AddCh(); goto case 1; } else if (ch == '$') { AddCh(); goto case 22; } else { t.kind = 1; t.val = new String(tval, 0, tlen); CheckLiteral(); return t; }

			}
			t.val = new String(tval, 0, tlen);
			return t;
		}

		// get the next token (possibly a token already seen during peeking)
		public Token Scan() {
			if (tokens.next == null) {
				return NextToken();
			} else {
				pt = tokens = tokens.next;
				return tokens;
			}
		}

		// peek for the next token, ignore pragmas
		public Token Peek() {
			if (pt.next == null) {
				do {
					pt = pt.next = NextToken();
				} while (pt.kind > maxT); // skip pragmas
			} else {
				do {
					pt = pt.next;
				} while (pt.kind > maxT);
			}
			return pt;
		}

		// make sure that peeking starts at the current scan position
		public void ResetPeek() { pt = tokens; }

	} // end Scanner

}