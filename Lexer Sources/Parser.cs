
using System;
using System.Collections;

namespace netMercs.TorqueDev.Lexer {



public class Parser {
	const int _EOF = 0;
	const int _ident = 1;
	const int _stringToken = 2;
	const int _stringTaggedToken = 3;
	const int _allNumbers = 4;
	const int _globalVar = 5;
	const int _localVar = 6;
	const int maxT = 75;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.Error(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}
	
	bool WeakSeparator (int n, int syFol, int repFol) {
		bool[] s = new bool[maxT+1];
		if (la.kind == n) { Get(); return true; }
		else if (StartOf(repFol)) return false;
		else {
			for (int i=0; i <= maxT; i++) {
				s[i] = set[syFol, i] || set[repFol, i] || set[0, i];
			}
			SynErr(n);
			while (!s[la.kind]) Get();
			return StartOf(syFol);
		}
	}
	
	void Statement() {
		switch (la.kind) {
		case 14: {
			Block();
			break;
		}
		case 7: {
			Get();
			break;
		}
		case 1: case 2: case 3: case 4: case 5: case 6: case 9: case 29: case 32: case 33: case 34: case 35: case 36: case 37: case 73: case 74: {
			StatementExpr();
			Expect(7);
			break;
		}
		case 8: {
			Get();
			Expect(9);
			Expr();
			Expect(10);
			Statement();
			if (la.kind == 11) {
				Get();
				Statement();
			}
			break;
		}
		case 12: case 13: {
			if (la.kind == 12) {
				Get();
			} else {
				Get();
			}
			Expect(9);
			Expr();
			Expect(10);
			Expect(14);
			while (la.kind == 27 || la.kind == 28) {
				SwitchBlock();
			}
			Expect(15);
			break;
		}
		case 16: {
			Get();
			Expect(9);
			Expr();
			Expect(10);
			Statement();
			break;
		}
		case 17: {
			Get();
			Expect(9);
			if (StartOf(1)) {
				StatementExpr();
			}
			Expect(7);
			if (StartOf(1)) {
				Expr();
			}
			Expect(7);
			if (StartOf(1)) {
				StatementExpr();
			}
			Expect(10);
			Statement();
			break;
		}
		case 18: {
			Get();
			Expect(7);
			break;
		}
		case 19: {
			Get();
			Expect(7);
			break;
		}
		case 20: {
			Get();
			while (StartOf(1)) {
				Expr();
			}
			Expect(7);
			break;
		}
		case 21: {
			Get();
			Expect(1);
			break;
		}
		case 22: {
			Get();
			Expect(1);
			if (la.kind == 23) {
				Get();
				Expect(1);
			}
			Expect(9);
			if (StartOf(1)) {
				Argument();
				while (la.kind == 24) {
					Get();
					Argument();
				}
			}
			Expect(10);
			Block();
			break;
		}
		case 25: {
			Get();
			if (StartOf(2)) {
				AssignOp();
				Expr();
			} else if (la.kind == 1) {
				Get();
				Expect(9);
				if (la.kind == 1 || la.kind == 2) {
					if (la.kind == 1) {
						Get();
					} else {
						Get();
					}
					if (la.kind == 26) {
						Get();
						Expect(1);
					}
				}
				Expect(10);
				Statement();
			} else SynErr(76);
			break;
		}
		case 31: {
			Particle();
			break;
		}
		default: SynErr(77); break;
		}
	}

	void Block() {
		Expect(14);
		while (StartOf(3)) {
			Statement();
		}
		Expect(15);
	}

	void StatementExpr() {
		UnaryOp();
		if (la.kind == 5 || la.kind == 6) {
			Variable();
		} else if (StartOf(2)) {
			AssignOp();
			if (la.kind == 31) {
				Particle();
			}
			Expr();
		} else if (la.kind == 7 || la.kind == 10) {
		} else SynErr(78);
	}

	void Expr() {
		UnaryOp();
		if (la.kind == 5 || la.kind == 6) {
			Variable();
		} else if (StartOf(4)) {
			if (la.kind == 29) {
				Get();
			}
			OrExpr();
			if (la.kind == 30) {
				Get();
				Expr();
				Expect(26);
				Expr();
			}
		} else if (StartOf(2)) {
			AssignOp();
			if (la.kind == 31) {
				Particle();
			}
			Expr();
		} else SynErr(79);
	}

	void SwitchBlock() {
		SwitchLabel();
		while (la.kind == 27 || la.kind == 28) {
			SwitchLabel();
		}
		if (StartOf(3)) {
			Statement();
			while (StartOf(3)) {
				Statement();
			}
		}
	}

	void Argument() {
		Expr();
	}

	void AssignOp() {
		switch (la.kind) {
		case 41: {
			Get();
			break;
		}
		case 42: {
			Get();
			break;
		}
		case 43: {
			Get();
			break;
		}
		case 44: {
			Get();
			break;
		}
		case 45: {
			Get();
			break;
		}
		case 46: {
			Get();
			break;
		}
		case 47: {
			Get();
			break;
		}
		case 48: {
			Get();
			break;
		}
		case 49: {
			Get();
			break;
		}
		case 50: {
			Get();
			break;
		}
		case 51: {
			Get();
			break;
		}
		default: SynErr(80); break;
		}
	}

	void Particle() {
		Expect(31);
	}

	void UnaryOp() {
		while (StartOf(5)) {
			switch (la.kind) {
			case 29: {
				Get();
				break;
			}
			case 32: {
				Get();
				break;
			}
			case 33: {
				Get();
				break;
			}
			case 34: {
				Get();
				break;
			}
			case 35: {
				Get();
				break;
			}
			case 36: {
				Get();
				break;
			}
			case 37: {
				Get();
				break;
			}
			}
		}
		Primary();
	}

	void Variable() {
		if (la.kind == 6) {
			Get();
		} else if (la.kind == 5) {
			Get();
		} else SynErr(81);
	}

	void SwitchLabel() {
		if (la.kind == 27) {
			Get();
			Expr();
			Expect(26);
		} else if (la.kind == 28) {
			Get();
			Expect(26);
		} else SynErr(82);
	}

	void OrExpr() {
		AndExpr();
		while (la.kind == 52 || la.kind == 53) {
			if (la.kind == 52) {
				Get();
			} else {
				Get();
			}
			UnaryOp();
			AndExpr();
		}
	}

	void Primary() {
		if (la.kind == 1) {
			Get();
		} else if (StartOf(6)) {
			Literal();
		} else if (la.kind == 4) {
			Get();
		} else if (la.kind == 5 || la.kind == 6) {
			Variable();
		} else if (la.kind == 9) {
			Get();
			Expr();
			Expect(10);
		} else SynErr(83);
		while (StartOf(7)) {
			switch (la.kind) {
			case 35: {
				Get();
				break;
			}
			case 36: {
				Get();
				break;
			}
			case 23: {
				Get();
				Expect(1);
				break;
			}
			case 38: {
				Get();
				Expect(1);
				break;
			}
			case 39: {
				Get();
				Expr();
				while (la.kind == 24) {
					Get();
					Expr();
				}
				Expect(40);
				break;
			}
			case 9: {
				Get();
				if (StartOf(1)) {
					Argument();
					if (la.kind == 10 || la.kind == 24 || la.kind == 26) {
						if (la.kind == 26) {
							Get();
							Expect(1);
						} else {
							while (la.kind == 24) {
								Get();
								Argument();
							}
						}
					}
				}
				Expect(10);
				if (la.kind == 14) {
					Block();
				}
				break;
			}
			}
		}
	}

	void Literal() {
		if (la.kind == 2) {
			Get();
		} else if (la.kind == 3) {
			Get();
		} else if (la.kind == 73) {
			Get();
		} else if (la.kind == 74) {
			Get();
		} else SynErr(84);
	}

	void AndExpr() {
		BitOrExpr();
		while (la.kind == 54) {
			Get();
			UnaryOp();
			BitOrExpr();
		}
	}

	void BitOrExpr() {
		BitXorExpr();
		while (la.kind == 55) {
			Get();
			UnaryOp();
			BitXorExpr();
		}
	}

	void BitXorExpr() {
		BitAndExpr();
		while (la.kind == 56) {
			Get();
			UnaryOp();
			BitAndExpr();
		}
	}

	void BitAndExpr() {
		StrEqlExpr();
		while (la.kind == 37) {
			Get();
			UnaryOp();
			EqlExpr();
		}
	}

	void StrEqlExpr() {
		EqlExpr();
		while (la.kind == 29 || la.kind == 59) {
			if (la.kind == 29) {
				Get();
			}
			Expect(59);
			UnaryOp();
			EqlExpr();
		}
	}

	void EqlExpr() {
		RelExpr();
		while (la.kind == 57 || la.kind == 58) {
			if (la.kind == 57) {
				Get();
			} else {
				Get();
			}
			UnaryOp();
			RelExpr();
		}
	}

	void RelExpr() {
		ShiftExpr();
		while (StartOf(8)) {
			if (la.kind == 60) {
				Get();
			} else if (la.kind == 61) {
				Get();
			} else if (la.kind == 62) {
				Get();
			} else {
				Get();
			}
			UnaryOp();
			ShiftExpr();
		}
	}

	void ShiftExpr() {
		AddExpr();
		while (la.kind == 64 || la.kind == 65) {
			if (la.kind == 64) {
				Get();
			} else {
				Get();
			}
			UnaryOp();
			AddExpr();
		}
	}

	void AddExpr() {
		MulExpr();
		while (StartOf(9)) {
			switch (la.kind) {
			case 32: {
				Get();
				break;
			}
			case 33: {
				Get();
				break;
			}
			case 66: {
				Get();
				break;
			}
			case 67: {
				Get();
				break;
			}
			case 68: {
				Get();
				break;
			}
			case 69: {
				Get();
				break;
			}
			}
			UnaryOp();
			MulExpr();
		}
	}

	void MulExpr() {
		while (la.kind == 70 || la.kind == 71 || la.kind == 72) {
			if (la.kind == 70) {
				Get();
			} else if (la.kind == 71) {
				Get();
			} else {
				Get();
			}
			UnaryOp();
		}
	}

	void TorqueScript() {
		while (StartOf(3)) {
			Statement();
		}
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		TorqueScript();

    Expect(0);
	}
	
	bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
		{x,T,T,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,x, x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,T, T,T,T,T, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
		{x,T,T,T, T,T,T,T, T,T,x,x, T,T,T,x, T,T,T,T, T,T,T,x, x,T,x,x, x,T,x,T, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,x, x},
		{T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, T,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,x, x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
		{x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,x, x},
		{x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,T, T,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, T,T,x,x, x,x,x,x, x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text
	
	private ErrorCollection coll = new ErrorCollection(null);
	public ErrorCollection ErrCollection {
		get {
			return coll;
		}
	}

	public void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "ident expected"; break;
			case 2: s = "stringToken expected"; break;
			case 3: s = "stringTaggedToken expected"; break;
			case 4: s = "allNumbers expected"; break;
			case 5: s = "globalVar expected"; break;
			case 6: s = "localVar expected"; break;
			case 7: s = "\";\" expected"; break;
			case 8: s = "\"if\" expected"; break;
			case 9: s = "\"(\" expected"; break;
			case 10: s = "\")\" expected"; break;
			case 11: s = "\"else\" expected"; break;
			case 12: s = "\"switch\" expected"; break;
			case 13: s = "\"switch$\" expected"; break;
			case 14: s = "\"{\" expected"; break;
			case 15: s = "\"}\" expected"; break;
			case 16: s = "\"while\" expected"; break;
			case 17: s = "\"for\" expected"; break;
			case 18: s = "\"break\" expected"; break;
			case 19: s = "\"continue\" expected"; break;
			case 20: s = "\"return\" expected"; break;
			case 21: s = "\"package\" expected"; break;
			case 22: s = "\"function\" expected"; break;
			case 23: s = "\"::\" expected"; break;
			case 24: s = "\",\" expected"; break;
			case 25: s = "\"datablock\" expected"; break;
			case 26: s = "\":\" expected"; break;
			case 27: s = "\"case\" expected"; break;
			case 28: s = "\"default\" expected"; break;
			case 29: s = "\"!\" expected"; break;
			case 30: s = "\"?\" expected"; break;
			case 31: s = "\"new\" expected"; break;
			case 32: s = "\"+\" expected"; break;
			case 33: s = "\"-\" expected"; break;
			case 34: s = "\"~\" expected"; break;
			case 35: s = "\"++\" expected"; break;
			case 36: s = "\"--\" expected"; break;
			case 37: s = "\"&\" expected"; break;
			case 38: s = "\".\" expected"; break;
			case 39: s = "\"[\" expected"; break;
			case 40: s = "\"]\" expected"; break;
			case 41: s = "\"=\" expected"; break;
			case 42: s = "\"+=\" expected"; break;
			case 43: s = "\"-=\" expected"; break;
			case 44: s = "\"*=\" expected"; break;
			case 45: s = "\"/=\" expected"; break;
			case 46: s = "\"%=\" expected"; break;
			case 47: s = "\"&=\" expected"; break;
			case 48: s = "\"|=\" expected"; break;
			case 49: s = "\"^=\" expected"; break;
			case 50: s = "\"<<=\" expected"; break;
			case 51: s = "\">>=\" expected"; break;
			case 52: s = "\"||\" expected"; break;
			case 53: s = "\"or\" expected"; break;
			case 54: s = "\"&&\" expected"; break;
			case 55: s = "\"|\" expected"; break;
			case 56: s = "\"^\" expected"; break;
			case 57: s = "\"!=\" expected"; break;
			case 58: s = "\"==\" expected"; break;
			case 59: s = "\"$=\" expected"; break;
			case 60: s = "\"<\" expected"; break;
			case 61: s = "\">\" expected"; break;
			case 62: s = "\"<=\" expected"; break;
			case 63: s = "\">=\" expected"; break;
			case 64: s = "\"<<\" expected"; break;
			case 65: s = "\">>\" expected"; break;
			case 66: s = "\"@\" expected"; break;
			case 67: s = "\"TAB\" expected"; break;
			case 68: s = "\"NL\" expected"; break;
			case 69: s = "\"SPC\" expected"; break;
			case 70: s = "\"*\" expected"; break;
			case 71: s = "\"/\" expected"; break;
			case 72: s = "\"%\" expected"; break;
			case 73: s = "\"true\" expected"; break;
			case 74: s = "\"false\" expected"; break;
			case 75: s = "??? expected"; break;
			case 76: s = "invalid Statement"; break;
			case 77: s = "invalid Statement"; break;
			case 78: s = "invalid StatementExpr"; break;
			case 79: s = "invalid Expr"; break;
			case 80: s = "invalid AssignOp"; break;
			case 81: s = "invalid Variable"; break;
			case 82: s = "invalid SwitchLabel"; break;
			case 83: s = "invalid Primary"; break;
			case 84: s = "invalid Literal"; break;

			default: s = "error " + n; break;
		}
		
		coll.Add(new Error(s, line, col));
		count++;
	}

	public void SemErr (int line, int col, int n) {
		Console.WriteLine(errMsgFormat, line, col, ("error " + n));
		count++;
	}

	public void Error (int line, int col, string s) {
		Console.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public void Exception (string s) {
		//Console.WriteLine(s); 
		//System.Environment.Exit(1);
	}
} // Errors

public class ErrorCollection : CollectionBase, ICloneable {
	public ErrorCollection(TSDev.CProject.File file) {
		this.FileLnk = file;
	}

	public virtual void Add(Error err) {
		this.List.Add(err);
	}

	public virtual void Remove(Error err) {
		this.List.Remove(err);
	}

	public Error this[int index] {
		get {
			return (Error)this.List[index];
		}
	}

	public TSDev.CProject.File FileLnk = null;

	#region ICloneable Members

	public object Clone() {
		return this.MemberwiseClone();
	}

	#endregion
}

public class Error : ICloneable {
	public Error (string ErrorText, int Line, int Column) {
		this.Text = ErrorText;
		this.Line = Line;
		this.Column = Column;
	}

	public string Text = "";
	public int Line = 0;
	public int Column = 0;
	#region ICloneable Members

	public object Clone() {
		return this.MemberwiseClone();
	}

	#endregion
}

}