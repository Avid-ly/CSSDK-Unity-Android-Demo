using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CssJsonHelper;
using System;

namespace CSSDK {
	public class CSSDKObject : MonoBehaviour
	{
		private static CSSDKObject instance = null;
		public static readonly string Unity_Callback_Class_Name = "CSSDK_Callback_Object";
		public static readonly string Unity_Callback_Function_Name = "onNativeCallback";

		public static readonly string Unity_Callback_Message_Key_Function = "callbackMessageKeyFunctionName";
		public static readonly string Unity_Callback_Message_Key_Parameter = "callbackMessageKeyParameter";

		// android
		private readonly static string Unity_Callback_Msg_Function_CSSDK_REPLY_Success = "CSSDK_REPLY_Success";
        private readonly static string Unity_Callback_Msg_Function_CSSDK_REPLY_Fail   = "CSSDK_REPLY_Fail";
		
        
        // ios
        private readonly static string Unity_Callback_Message_Function_HaveNewMessage_Complete  		= "HaveNewMessage_Complete";
		
		private readonly static string Unity_Callback_Message_Parameter_haveNewMessage      = "haveNewMessage";

		public static CSSDKObject getInstance()
		{
			if (instance == null) {
				GameObject polyCallback = new GameObject (Unity_Callback_Class_Name);
				polyCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (polyCallback);
				instance = polyCallback.AddComponent<CSSDKObject> ();
			}
			return instance;
		}

		Action<bool,string> haveNewMessageCallback;
      

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void setHaveNewMessageCallbackCallback(Action<bool,string> newMessageCallback) {
			haveNewMessageCallback=newMessageCallback;
		}

        // 将jsonobj转换了map<>
        public string getInnerJsonParamterValue(Hashtable jsonObj,string key) {
            string msg = "";
            if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
            {
                Hashtable innerJsonObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                if (innerJsonObj.ContainsKey(key)) { 
                    msg = (string)innerJsonObj[key];
                }
            }
            return msg;
        }


        public void onNativeCallback(string message) {

        	Debug.Log ("===> message : " + message);
			Hashtable jsonObj = (Hashtable)CssJsonHelper.JsonHelper.jsonDecode (message);

			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {

				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				// callback
				// Android
				if (function.Equals (Unity_Callback_Msg_Function_CSSDK_REPLY_Success)) {
                    if (haveNewMessageCallback != null) {
						haveNewMessageCallback (true, "success");
					}				

				}else if(function.Equals (Unity_Callback_Msg_Function_CSSDK_REPLY_Fail)){
        			if (haveNewMessageCallback != null) {
						haveNewMessageCallback (false, getInnerJsonParamterValue(jsonObj,"msg"));
					}			
				
				}else if (function.Equals (Unity_Callback_Message_Function_HaveNewMessage_Complete)) {

					Debug.Log("===> call function " + Unity_Callback_Message_Function_HaveNewMessage_Complete);

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter)) {
						string json = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
						Debug.Log("parameter json : " + json);

						Hashtable paraObj = (Hashtable)CssJsonHelper.JsonHelper.jsonDecode (json);

                    	bool succeed = (bool)paraObj[Unity_Callback_Message_Parameter_haveNewMessage];
                    	Debug.Log ("===> succeed " + succeed);
                    	
                    	if (succeed) {
							if (haveNewMessageCallback != null) {
								haveNewMessageCallback(succeed,"success");
							}
							else {
								Debug.Log ("===> can't run haveNewMessageCallback(), no delegate object.");
							}
						}else {
							if (haveNewMessageCallback != null) {
								haveNewMessageCallback(succeed,"fail");
							}
							else {
								Debug.Log ("===> can't run haveNewMessageCallback(), no delegate object.");
							}
						}
            		}
            		else {
            			Debug.Log("===> Does not contain Parameter");
            		}
				}

				//unkown call
				else {
					Debug.Log ("===> onTargetCallback unkown function:" + function);
				}
			}
        }
	}
}


