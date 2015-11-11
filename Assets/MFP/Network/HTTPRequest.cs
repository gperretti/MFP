using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class HTTPRequest : MonoBehaviour {

	public string url;

	#region Requests
	public delegate void FuncFinishCallback(WWW www);
	public delegate void FuncFailCallback(WWW www);
   
    public FuncFinishCallback funcFinishCallback = null;
	public FuncFailCallback funcFailCallback = null;
	
	//=====================================================
	public static HTTPRequest requestWithURL(string url) {

		GameObject go = new GameObject("Request");
		
		HTTPRequest httpRequest = go.AddComponent<HTTPRequest>();
		httpRequest.url = url;
		
		DontDestroyOnLoad(go);
		
		return httpRequest;
	}
	//=====================================================
	public WWW GET() {
	    WWW www = new WWW (url);
		
	    StartCoroutine (WaitForRequest (www));
	    
		return www; 
	}
	//=====================================================
	public WWW POST(Dictionary<string,string> header, Dictionary<string,object> body) {

		var form = new WWWForm();

		foreach(KeyValuePair<string, object> post_arg in body) {
	       form.AddField(post_arg.Key, post_arg.Value.ToString());
	    }

		byte[] rawData = form.data;
		WWW www = new WWW(url, rawData, header);
	
		StartCoroutine(WaitForRequest(www));
	   
		return www; 
    }
	//=====================================================
	public void setDidFinishCallback(FuncFinishCallback func) {
		funcFinishCallback = func;
	}
	//=====================================================
	public void setDidFailCallback(FuncFailCallback func) {
		funcFailCallback = func;
	}
	//=====================================================
	private IEnumerator WaitForRequest(WWW www) {
        yield return www;
			
        // check for errors
        if (www.error == null) {
			if(funcFinishCallback != null) {
				try {
					funcFinishCallback(www);
				}
				catch (NullReferenceException e) {
					Debug.LogWarning("funcFinishCallback: no exixte el objeto destino (" + e + ")"); 
				}
			}
			this.requestFinished(www);
        } else {
			if(funcFailCallback != null) {
				try {
					funcFailCallback(www);
				}
				catch (NullReferenceException e) {
					Debug.LogWarning("funcFailCallback: no exixte el objeto destino (" + e + ")"); 
				}
			}
			
			this.requestFailed(www);
        }    
		
		Destroy(this.gameObject);
    }
	//=====================================================================
	void requestFinished(WWW request) {
		Debug.Log("RequestFinished = " + request.text); 
	}
	//=====================================================================
	void requestFailed(WWW request) {
		Debug.Log("requestFailed = " + request.error); 
	}
	//=====================================================

	#endregion

	public void Post(string handler, Dictionary<string, object> bodyParams, MFP_Callbacks.MFPResponseCallback res, MFP_Callbacks.MFPResponseCallback fail) {
		
		//Debug.Log("Post message " + message);
		Dictionary<string, object> fullMessageMap = emptyMessage();

		string jsonMessage = JsonMapper.ToJson(fullMessageMap);

		HTTPRequest request = HTTPRequest.requestWithURL(MFP_API.i ().mfURL  + handler);	

		if (res != null) {
			request.funcFinishCallback = (www) => {
				JsonData data = JsonMapper.ToObject(www.text);
				res(data);
			};
		}
		
		if (fail != null) {
			request.funcFailCallback = (www) => {
				JsonData data = JsonMapper.ToObject(www.text);
				res(data);
			};
		}

		Dictionary<string, object> body = (Dictionary<string, object>) fullMessageMap["body"];
		Dictionary<string, object> headers = (Dictionary<string, object>) fullMessageMap["header"];
		foreach (KeyValuePair<string, object> entry in bodyParams)
		{
			body[entry.Key] = entry.Value;
		}	
		//Dictionary<string,string> dicValues = new Dictionary<string, string>();
		//dicValues["json"] = JsonMapper.ToJson(body);

		Dictionary<string,string> headerValues = new Dictionary<string, string>();
		foreach (KeyValuePair<string, object> entry in headers){
			headerValues[entry.Key] = (string)entry.Value;
		}	

		request.POST(headerValues, body);
	}
	
	public void Post(string message, Dictionary<string, object> bodyParams, MFP_Callbacks.MFPResponseCallback res) {
		this.Post(message, bodyParams, res, null);
	}
	
	public void Post(string message, MFP_Callbacks.MFPResponseCallback res, MFP_Callbacks.MFPResponseCallback fail) {
		this.Post(message, new Dictionary<string, object>(), res, fail);
	}
	
	public void Post(string message, MFP_Callbacks.MFPResponseCallback res) {
		this.Post (message, new Dictionary<string, object>(), res);
	}
	
	public void Post(string message) {
		this.Post (message, new Dictionary<string, object>(), null, null);	
	}
	
	/* Sample MF message:
		 {
			  "_t": "mfmessage",
			  "header": {
			    "_t": "mfheader"
			  },
			  "body": {
			    "_t": "pingReq"
			  }
		} 
	*/
	private Dictionary<string, object> emptyMessage() {

		Dictionary<string, object> message = new Dictionary<string, object>();	

		Dictionary<string, object> header = new Dictionary<string, object>();
		header.Add("X-API-KEY", MFP_API.i ().APIKey);
		header.Add("Content-Type","application/json");
		header.Add("Accept", "application/json");
		message["header"] = header;
		message["body"] = new Dictionary<string, object>();
		return message;
	}
}
