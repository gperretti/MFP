using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

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

		ArrayList tokenLists = new ArrayList ();
		Dictionary<string, object> tokens = new Dictionary<string, object>();
		tokens.Add("provider", "email");
		tokens.Add("email", user.email);
		tokens.Add("password", user.pass);
		tokenLists.Add (tokens);

		Dictionary<string, object> bodyParams = new Dictionary<string, object>();
		bodyParams.Add("username", user.userName);
		bodyParams.Add("authTokens", tokenLists);

		string bodyData = JsonMapper.ToJson (bodyParams);

		HTTPRequest httpRequest = HTTPRequest.requestWithURL(MFP_API.i ().mfURL);
		httpRequest.Post("players", bodyData, successResponse, failResponse);
	}
}
