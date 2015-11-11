using UnityEngine;
using System.Collections;

public class MFP_User {


	string _userName = null;
	public string userName {
		get {
			return _userName;
		}
		set {
			_userName = value;
		}
	}

	string _email = null;
	public string email {
		get {
			return _email;
		}
		set {
			_email = value;
		}
	}

	string _pass = null;
	public string pass {
		get {
			return _pass;
		}
		set {
			_pass = value;
		}
	}

	string _token = null;
	public string token {
		get {
			return _token;
		}
		set {
			_token = value;
		}
	}


	public MFP_User createUser(string name, string email, string pass){

		this.userName = name;
		this.email = email;
		this.pass = pass;

		return this;
	}
}
