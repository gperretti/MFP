using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MFP_API : MonoBehaviour {

	public string libraryVersion = "3.0";
	public string mfURL = "http://dev1.mfp.makingfun.com/mfp/api/";
	public string gameName = "";
	public string APIKey = "";


	private static MFP_API _instance = null;
	public static MFP_API i(){
		return _instance; 
	}

	MFP_Auth _mf_authentication = null;
	MFP_Auth mf_authentication{
		get {
			if(_mf_authentication == null)
				_mf_authentication = new MFP_Auth();
			return _mf_authentication;
		}
	}
	MFP_User mf_user = null;
		
	void Awake(){
		
		_instance = this;
		
		DontDestroyOnLoad(gameObject); // Don't get destroyed when loading a new level.
	}
	
	
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createAccount(string userName, string email, string pass, MFP_Callbacks.MFPResponseCallback successResponse, MFP_Callbacks.MFPResponseCallback errorResponse){

		mf_user = new MFP_User().createUser(userName, email, pass);
		mf_authentication.createAccount(mf_user, successResponse, errorResponse);
	}
}
