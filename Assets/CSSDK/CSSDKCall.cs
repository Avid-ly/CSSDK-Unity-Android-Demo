
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UPTrace;
namespace CSSDK
{
    public class CSSDKCall {


#if UNITY_IOS && !UNITY_EDITOR

    		[DllImport("__Internal")]
			private static extern void setCallbackWithClassAndFunctionForIosCssdk(string callbackClassName, string callbackFunctionName);

			[DllImport("__Internal")]
			private static extern void initForIosCssdk(string productId);

			[DllImport("__Internal")]
			private static extern void showForIosCssdk();

			[DllImport("__Internal")]
			private static extern void haveNewMessageForIosCssdk();

			[DllImport("__Internal")]
			private static extern void addExtraParamForIosCssdk(string paramJson);

			[DllImport("__Internal")]
			private static extern string getVersionForIosCssdk();

			
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.css.sdk.unity.CSSDKProxy";
			private readonly static string JavaClassStaticMethod_init = "init";
            private readonly static string JavaClassStaticMethod_addExtraMsg= "addExtraMsg";
			private readonly static string JavaClassStaticMethod_feedback = "feedback";
			private readonly static string JavaClassStaticMethod_setNewReplayCallback = "setNewReplayCallback";

#else
        // "do nothing";
#endif


        public CSSDKCall() {
            CSSDKObject.getInstance();
#if UNITY_IOS && !UNITY_EDITOR
            setCallbackWithClassAndFunctionForIosCssdk(CSSDKObject.Unity_Callback_Class_Name, CSSDKObject.Unity_Callback_Function_Name);
				Debug.Log ("===> CSSDKCall instanced.");
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> CSSDKCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
#endif
        }

        public void initCall(string productId) {
            UPTraceApi.initTraceSDK (productId, "32401", UPTraceConstant.UPTraceSDKZoneEnum.UPTraceSDKZoneForeign);
            
			Debug.Log("===> call init in CSSDKcall");
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
            initForIosCssdk(productId);

#elif UNITY_ANDROID && !UNITY_EDITOR
       if (jc != null) {
                jc.CallStatic (JavaClassStaticMethod_init,productId);
            }
#endif
        }



        public void showCall() {
			Debug.Log("===> call show in CSSDKcall");
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
            showForIosCssdk();

#elif UNITY_ANDROID && !UNITY_EDITOR
       if (jc != null) {
                jc.CallStatic (JavaClassStaticMethod_feedback);
            }
#endif
        }

        public void haveNewMessageCall(Action<bool,string> callback) {
        	Debug.Log("===> call haveNewMessage in CSSDKcall");
            // 设置callback回调
            CSSDKObject.getInstance().setHaveNewMessageCallbackCallback(callback);
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
			haveNewMessageForIosCssdk();

#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
                jc.CallStatic (JavaClassStaticMethod_setNewReplayCallback,
                                CSSDKObject.Unity_Callback_Class_Name,
                                CSSDKObject.Unity_Callback_Function_Name);
            }
#endif
        }

        public void addExtraParamCall(Dictionary<string, string> dic){
            Debug.Log("===> call addExtraParam in CSSDKCall.");

         string paramJson=dicationaryToString(dic);
#if UNITY_IOS && !UNITY_EDITOR
			addExtraParamForIosCssdk(paramJson);

#elif UNITY_ANDROID && !UNITY_EDITOR
       if (jc != null) {
                jc.CallStatic (JavaClassStaticMethod_addExtraMsg,paramJson);
            }
#endif
        }

        public string getVersionCall(){
            Debug.Log("===> call getVersion in CSSDKCall.");
#if UNITY_IOS && !UNITY_EDITOR
			return getVersionForIosCssdk();

#elif UNITY_ANDROID && !UNITY_EDITOR
            return "";
#endif
            return "";
        }

        private string dicationaryToString(Dictionary<string, string> dic) {
            if (dic == null || dic.Count == 0) {
                return null;
            }

            string str = "{ \"array\":[";
            int len = dic.Count;
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in dic) {
                str += "{\"k\":\"" + kvp.Key + "\",";
                str += "\"v\":\"" + kvp.Value + "\"}";
                if (i < len - 1) {
                    str += ",";
                } else {
                    str += "]}";
                }
                i++;
            }

            //Debug.Log ("dicationaryToString:" + str);
            return str;
        }

    }
}
