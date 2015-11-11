using UnityEngine;
using System.Collections;
using LitJson;

public class MFP_Callbacks: MonoBehaviour {

	public delegate void MFPResponseCallback(JsonData aResult);
    
	public MFPResponseCallback successCallback = null;
	public MFPResponseCallback errorCallback = null;

}
