using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using LitJson;

public class MFP_Demo : MonoBehaviour {


	string version = "unknown";

	public GameObject menuGUI;
	public GameObject createAccountGUI;

	public enum gameStates{
		menu,
		createAccount,
		login,
	}

	public gameStates gameState = gameStates.menu;

	// Use this for initialization
	void Start () {
		version = "Library Version: " + MFP_API.i ().libraryVersion;
	}
	
	// Update is called once per frame
	void Update () {

		switch (gameState) {

		case gameStates.menu:
			createAccountGUI.SetActive(false);
			menuGUI.SetActive(true);
			break;

		case gameStates.createAccount:
			createAccountGUI.SetActive(true);
			menuGUI.SetActive(false);
			break;

		default:
			createAccountGUI.SetActive(false);
			menuGUI.SetActive(true);
			break;
		}

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
		
		gameState = gameStates.createAccount;
	}

	public void onMenuLoginButton(){
		
		gameState = gameStates.login;
	}
	
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

}
