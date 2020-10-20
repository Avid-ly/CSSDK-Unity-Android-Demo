using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

using CSSDK;

public class TestCSSDKCall : MonoBehaviour
{
	private const string PRODUCTID = "600167";
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

    public void onInitClick() {


        string productId = PRODUCTID;

#if UNITY_IOS && !UNITY_EDITOR
            productId = "1000152";

#elif UNITY_ANDROID && !UNITY_EDITOR

#endif
        
        CSSDKApi.init(productId);
        showLogMsg("onInitClick press  " + productId);

    }

	public void onShowClick() {

		CSSDKApi.show();
        showLogMsg("onShowClick press  ");

    }


   public void onHaveNewMessageClick() {
        CSSDKApi.haveNewMessage(new System.Action<bool,string>(onHaveNewMessageCallback));

        showLogMsg("onHaveNewMessageClick press  ");
    }

    public void onAddExtraParamClick() {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("hotfixversion", "1.0.0.21");
        CSSDKApi.addExtraParam(dic);
        showLogMsg("addExtraParam press  ");
    }

     public void onGetVersionClick() {


        string sdkVersion = CSSDKApi.getVersion();

        showLogMsg("onGetVersionClick press sdkVersion : " + sdkVersion);  
    }

    private void onHaveNewMessageCallback(bool result,string msg)
    {
        showLogMsg("onHaveNewMessageCallback  "+"result: "+result+"msg: "+msg);
    }



    private void showLogMsg(string msg){
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = msg;
        Debug.Log ("===> msg " + msg);
    }

}

