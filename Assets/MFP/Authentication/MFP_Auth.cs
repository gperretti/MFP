using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MFP_Auth {
	
	public void createAccount(MFP_User user, MFP_Callbacks.MFPResponseCallback successResponse, MFP_Callbacks.MFPResponseCallback failResponse){

		/*
		mf_API_URL/players
		{  
		   "username":"playerOne",
		   "authToken":[  
		      {  
		         "provider":"email",
		         "email":"john.doe@email.com",
		         "password":"123456"
		      }
		   ]
		}
		*/


		Dictionary<string, object> tokens = new Dictionary<string, object>();
		tokens.Add("provider", "email");
		tokens.Add("email", user.email);
		tokens.Add("password", user.pass);

		Dictionary<string, object> bodyParams = new Dictionary<string, object>();
		bodyParams.Add("username", user.userName);
		bodyParams.Add("authTokens", tokens);

		HTTPRequest httpRequest = new HTTPRequest();
		httpRequest.Post("players", bodyParams, successResponse, failResponse);
	}
}
