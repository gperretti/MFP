using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using LitJson;

public class MFP_Demo : MonoBehaviour {


	string version = "unknown";

	public GameObject menuGUI;
	public GameObject createAccountGUI;
	public GameObject loginAccountGUI;

	public enum gameStates{
		menu,
		createAccount,
		login,
	}

	public gameStates gameState = gameStates.menu;

	// Use this for initialization
	void Start () {

		disableAllGUI();
		version = "Library Version: " + MFP_API.i ().libraryVersion;
	}
	
	// Update is called once per frame
	void Update () {

		switch (gameState) {

		case gameStates.menu:
			menuGUI.SetActive(true);
			break;

		case gameStates.createAccount:
			createAccountGUI.SetActive(true);
			break;
			
		case gameStates.login:
			loginAccountGUI.SetActive(true);
			break;

		default:
			menuGUI.SetActive(true);
			break;
		}

	}

	void disableAllGUI(){
		menuGUI.SetActive(false);
		createAccountGUI.SetActive(false);
		loginAccountGUI.SetActive(false);
	}

	void OnGUI(){

		// MFP version
		GUI.Label(new Rect(10, 10, 200, 20), version);


	}

	public void changeState( gameStates newState ){

		switch( newState ){
			default:
			{
				break;
			}
		}
	}

	public void onMenuCreateAccountButton(){
		disableAllGUI();
		gameState = gameStates.createAccount;
	}

	public void onMenuLoginButton(){
		disableAllGUI();
		gameState = gameStates.login;
	}


	#region Create Account
	public void onCreateButton(){

		string name = GameObject.Find("UserName_Input").GetComponent<InputField>().text;
		string email = GameObject.Find("Email_Input").GetComponent<InputField>().text;
		string pass = GameObject.Find("Password_Input").GetComponent<InputField>().text;

		bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
	
		MFP_API.i ().createAccount(name, email, pass, createAccountSuccessResponse, createAccountErrorResponse);
	}

	void createAccountSuccessResponse(JsonData aResult){

	}

	void createAccountErrorResponse(JsonData aResult){
		
	}
	#endregion

	#region Login Account
	public void onLoginButton(){
		
		//string name = GameObject.Find("UserName_Input").GetComponent<InputField>().text;
		string email = GameObject.Find("Email_Input").GetComponent<InputField>().text;
		string pass = GameObject.Find("Password_Input").GetComponent<InputField>().text;
		
		bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		
		MFP_API.i ().loginUser(email, pass, loginUserSuccessResponse, loginUserErrorResponse);
	}
	
	void loginUserSuccessResponse(JsonData aResult){
		
	}
	
	void loginUserErrorResponse(JsonData aResult){
		
	}
	
	#endregion
}
