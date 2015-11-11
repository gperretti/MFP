using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;
using System.Text;

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

	public void Post(string handler, string bodyData, MFP_Callbacks.MFPResponseCallback res, MFP_Callbacks.MFPResponseCallback fail) {

		string apiUrl =  MFP_API.i ().mfURL + handler;
		string responseJson = "";
		
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
		headers.Add("Accept", "application/json");
		headers.Add("X-API-KEY",  MFP_API.i ().APIKey);
		byte[] pData = Encoding.ASCII.GetBytes(bodyData.ToCharArray());
		
		
		WWW www = new WWW(apiUrl, pData, headers);
			
		while(!www.isDone){
			Debug.Log(www.progress);
		}
		
		if(www.error != null && www.error.Length > 0)
		{
			Debug.Log("There was an error getting the file - " + www.error);
		} else {
			Debug.Log(www.text);
		}
		Debug.Log("Post Response = " + responseJson); 
	}

	/*
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
	*/
}
