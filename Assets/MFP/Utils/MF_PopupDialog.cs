using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MF_PopupDialog : MonoBehaviour{

	/*
	public GameObject GUIControlsPrefab;
	public GUISkin GUISkin;
	
	private string Text;
	private Types Type;
	public float Timeout = 1;
	
	public static bool PopupActive;
	
	public static Hashtable data;
	
	public enum Types
	{
		OK,
		YesNo,
		NowLater
	}

	public enum Result
	{
		Yes,
		No,
		Never
	}

	enum Controls
	{
		Background,
		QuestText,
		YesButton,
		BackButton,
		OKButton,
		NowButton,
		LaterButton,
		NeverButton
	}

	void Awake()
	{
		data = new Hashtable();
		useGUILayout = false;
		Timeout = 1;
	}

	// Use this for initialization
	void Start ()
	{
	}

	//Parameters:
	//Quest Tool object
	//NPC Tool object
	//NPC Script
	void DoOpen( IList<object> values )
	{
		Text = values[0] as string;
		Type = (Types)values[1];
	}

	void Update( )
	{
		Timeout = Mathf.Max( Timeout - Time.deltaTime, 0 );

        if( GameScript.BackButtonPressed() && Type == Types.OK )
            ResultChosen( Result.Yes );
	}
	
	void OnGUI()
	{
		GUI.skin = GUISkin;
		GUI.depth = -1;
		GameUtil.SetControlPrefab(GUIControlsPrefab);

		GameUtil.DrawControl((int) Controls.Background);

		GameUtil.DrawControl((int) Controls.QuestText, Text);

		Result? result = null;

		switch (Type)
		{
			case Types.OK:
				if (GameUtil.DrawControl((int) Controls.OKButton) && Timeout <= 0f)
				{
					result = Result.Yes;
				}
				break;
			case Types.NowLater:
				if( GameUtil.DrawControl( (int)Controls.NowButton ) && Timeout <= 0f )
				{
					result = Result.Yes;
				}
				if( GameUtil.DrawControl( (int)Controls.LaterButton ) && Timeout <= 0f )
				{
					result = Result.No;
				}
//				if( GameUtil.DrawControl( (int)Controls.NeverButton ) && Timeout <= 0f )
//				{
//					result = Result.Never;
//				}
				break;
			default:
				if( GameUtil.DrawControl( (int)Controls.YesButton ) && Timeout <= 0f )
				{
					result = Result.Yes;
				}
				if( GameUtil.DrawControl( (int)Controls.BackButton ) && Timeout <= 0f )
				{
					result = Result.No;
				}
	
				break;
		}

		if (result != null)
		{
            ResultChosen( result );
		}
	}

    private void ResultChosen( Result? result )
    {
        if( transform.parent )
            transform.parent.SendMessage( "PopupResult", result, SendMessageOptions.DontRequireReceiver );
        //			else
        Invoke( "CloseMenu", 0.1f );

    }


	void CloseMenu()
	{
        PopupDialog.PopupActive = false;
//		Debug.LogError("C: Num Popups: " + PopupDialog.num_popups);
		if( Village )
			GameScript.PlaySound( GameScript.sfx_menuMove, Village.listener.transform.position, 1f, 1f, false, Village.listener,
			                      1 );
		else
			GameScript.PlaySound( GameScript.sfx_menuMove, GameScript.transform.position, 1f, 1f, false, GameScript.gameObject,
			                      1 );
		
		//Village.changeVillageState( VillageScript.villageStates.playing );
        GameScript.delayTouch();
        Destroy( gameObject );
	}

	public static GameObject Open( string text, Types type, MonoBehaviour caller )
	{
        PopupDialog.PopupActive = true;
//		Debug.LogError("B: Num Popups: " + PopupDialog.num_popups);
		var popup = Instantiate( (FindObjectOfType( typeof(GameScript) ) as GameScript).m_Popup_Prefab ) as GameObject;

		popup.SendMessage( "DoOpen", new object[]
					                           	{
					                           		text,
					                           		type
					                           	} );

		//this.transform.parent = popup.transform;
		popup.transform.parent = caller.transform;

		return popup;
	}
	public static void Close()
	{
		//		Debug.LogError("A: Num Popups: " + PopupDialog.num_popups);

        var popup = FindObjectOfType( typeof( PopupDialog ) ) as PopupDialog;
        if( popup )
           popup.CloseMenu( );
	}
	*/
}
