
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UPTrace
{
	public class UPTraceCall {



#if UNITY_IOS && !UNITY_EDITOR

			[DllImport("__Internal")]
			private static extern void initAnalysisSDKForIos(string gameName, string funName, string productid, string channelid, int zone);


			[DllImport("__Internal")]
			private static extern void disableAccessPrivacyInformationForIos();

			[DllImport("__Internal")]
			private static extern string getUserIdForIos();

			[DllImport("__Internal")]
			private static extern string getOpenIdForIos();


			[DllImport("__Internal")]
			private static extern void logMapForIos(string key, string json);

			[DllImport("__Internal")]
			private static extern void logStringForIos(string key, string value);

			[DllImport("__Internal")]
			private static extern void logKeyForIos(string key);

			[DllImport("__Internal")]
			private static extern void countKeyForIos(string key);


			[DllImport("__Internal")]
			private static extern void logZFServerIapAndExtra(string playerId, string gameAccountServer, string receiptDataString, bool isbase64, string json);

			[DllImport("__Internal")]
			private static extern void logZFPlayerIapAndExtra(string playerId, string receiptDataString, bool isbase64, string json);

			[DllImport("__Internal")]
			private static extern void thirdpartyLogZFWithPlayerId(string playerId, string thirdparty, string receiptDataString);  

			[DllImport("__Internal")]
			private static extern void thirdpartyLogZFWithServerPlayerId(string playerId, string gameAccountServer, string thirdparty, string receiptDataString); 


			[DllImport("__Internal")]
			private static extern void guestLoginWithGameId(string playerId);

			[DllImport("__Internal")]
			private static extern void facebookLoginWithGameId(string playerId, string openId, string openToken);	

			[DllImport("__Internal")]
			private static extern void twitterLoginWithPlayerId(string playerId, string twitterId, string twitterUserName, string twitterAuthToken);

			[DllImport("__Internal")]
			private static extern void portalLoginWithPlayerId(string playerId, string portalId);

			[DllImport("__Internal")]
			private static extern void commonIosLogin(string type, string playerId, string token);

			[DllImport("__Internal")]
			private static extern void logIosAASLogin(string loginType, string playerId, string loginToken, string ggid, string extensionJson);


			[DllImport("__Internal")]
			private static extern void initDurationReportForIos(string serverName, string serverZone, string uid, string ggid);

			[DllImport("__Internal")]
			private static extern void becomeActiveForIos();

			[DllImport("__Internal")]
			private static extern void resignActiveForIos();

			[DllImport("__Internal")]
			private static extern void getConversionDataForIos(string gameName, string funName, string afConversionData);

			[DllImport("__Internal")]
			private static extern void getPayUserLayerForIos(string gameName, string funName);

			[DllImport("__Internal")]
			private static extern void setAppsFlyerIdForIos(string appsFlyerId);
			

#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.aly.unity.UPTraceProxy";
			private readonly static string JavaClassStaticMethod_InitTrace = "initTrace";
			private readonly static string JavaClassStaticMethod_DisableAccessPrivacyInformation = "disableAccessPrivacyInformation";
			private readonly static string JavaClassStaticMethod_GetCustomerId = "getCustomerId";
			private readonly static string JavaClassStaticMethod_SetCustomerId = "setCustomerId";
			private readonly static string JavaClassStaticMethod_GetOpenId = "getOpenId";
			private readonly static string JavaClassStaticMethod_GetUserId = "getUserId";
        // added in 4002
            private readonly static string JavaClassStaticMethod_EnalbeDebugMode = "enalbeDebugMode";

			private readonly static string JavaClassStaticMethod_LogMap = "logMap";
			private readonly static string JavaClassStaticMethod_LogString = "logString";
			private readonly static string JavaClassStaticMethod_LogKey = "logKey";
			private readonly static string JavaClassStaticMethod_Count = "countKey";

			private readonly static string JavaClassStaticMethod_LogReportWithServer = "logReportWithServer";
			private readonly static string JavaClassStaticMethod_LogReport = "logReport";

			private readonly static string JavaClassStaticMethod_LogReportWithServerAndExtraJson = "logReportWithServerAndExtraJson";
			private readonly static string JavaClassStaticMethod_LogReportWithExtraJson = "logReportWithExtraJson";


			private readonly static string JavaClassStaticMethod_GuestLogin = "guestLogin";
			private readonly static string JavaClassStaticMethod_HuaWeiLoginNonAuth = "huaWeiLoginNonAuth";
			private readonly static string JavaClassStaticMethod_HuaWeiLogin = "huaWeiLogin";
			private readonly static string JavaClassStaticMethod_CommonLogin = "commonLogin";
			private readonly static string JavaClassStaticMethod_FacebookLogin = "facebookLogin";
			private readonly static string JavaClassStaticMethod_TwitterLogin = "twitterLogin";
			private readonly static string JavaClassStaticMethod_PortalLogin = "portalLogin";

        	private readonly static string JavaClassStaticMethod_InitDruation = "initReport";
            private readonly static string JavaClassStaticMethod_OnAppResume= "onAppResume";
			private readonly static string JavaClassStaticMethod_OnAppPause = "onAppPause";

			private readonly static string JavaClassStaticMethod_GetConversionData = "getConversionData";
        // added in 4003
            private readonly static string JavaClassStaticMethod_LoginWithAASDK = "loginWithAASDK";
            private readonly static string JavaClassStaticMethod_GetPayUserLayer = "getPayUserLayer";
        // added in 4004
            private readonly static string JavaClassStaticMethod_SetAFId = "setAFId";


#else
        // "do nothing";
#endif



        public UPTraceCall() {
			UPTraceObject.getInstance ();
			#if UNITY_IOS && !UNITY_EDITOR
				Debug.Log ("===> UPTraceCall instanced.");
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> UPTraceCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
			#endif
		}

		public void logReportPlayIdForIap(string playerId, string receiptDataString, bool isbase64, Dictionary<string,string>dic) {
			if (null == playerId || playerId.Length == 0) {
				Debug.Log ("===> UPTraceCall.logReportPlayIdForIap(), error: the value of parameter playerId is null or empty.");
				return;
			}

			if (null == receiptDataString || receiptDataString.Length == 0) {
				Debug.Log ("===> UPTraceCall.logReportPlayIdForIap(), error: the value of parameter receiptDataString is null or empty.");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				string jsonmap = dicationaryToString (dic);

				if (jsonmap == null) {
					jsonmap = "";
				}
				logZFPlayerIapAndExtra (playerId, receiptDataString, isbase64, jsonmap);
			#endif
		}

		public void logReportServerForIap(string playerId, string gameAccountServer, string receiptDataString, bool isbase64, Dictionary<string,string>dic) {
			if (null == playerId || playerId.Length == 0) {
				Debug.Log ("===> UPTraceCall.logReportServerForIap(), error: the value of parameter playerId is null or empty.");
				return;
			}

			if (null == gameAccountServer || gameAccountServer.Length == 0) {
				Debug.Log ("===> UPTraceCall.logReportServerForIap(), error: the value of parameter gameAccountServer is null or empty.");
				return;
			}

			if (null == receiptDataString || receiptDataString.Length == 0) {
				Debug.Log ("===> UPTraceCall.logReportServerForIap(), error: the value of parameter receiptDataString is null or empty.");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				string jsonmap = dicationaryToString (dic);
		
				if (jsonmap == null) {
					jsonmap = "";
				}
				logZFServerIapAndExtra(playerId, gameAccountServer, receiptDataString, isbase64, jsonmap);
			#endif
		}
	
		public void initTtraceSDK (string productId, string channelId, UPTraceConstant.UPTraceSDKZoneEnum zone) {

			if (null == productId || productId.Length == 0) {
				Debug.Log ("===> UPTraceCall.initTtraceSDK(), error: the value of parameter productId is null or empty.");
				return;
			}

			if (null == channelId || channelId.Length == 0) {
				Debug.Log ("===> UPTraceCall.initTtraceSDK(), error: the value of parameter channelId is null or empty.");
				return;
			}

			int intzone = 0;
			if (zone == UPTraceConstant.UPTraceSDKZoneEnum.UPTraceSDKZoneDomestic) {
				intzone = 1;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			initAnalysisSDKForIos(UPTraceObject.Unity_Callback_Class_Name,
				UPTraceObject.Unity_Callback_Function_Name,
				productId,
				channelId,
				intzone);
		
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				//Debug.Log (JavaClassName);
				jc = new AndroidJavaClass (JavaClassName);
			}
			jc.CallStatic (JavaClassStaticMethod_InitTrace, 
				UPTraceObject.Unity_Callback_Class_Name,
				UPTraceObject.Unity_Callback_Function_Name,
				productId,
				channelId,
				intzone);
			#endif
		}

		public void logDictionary(string key, Dictionary<string, string> dic) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}

			string value = dicationaryToString (dic);
			if (value == null) {
				Debug.Log ("can't trace a null dictionary." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			logMapForIos(key, value);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogMap, key, value);
			}
			#endif
		}

		public void logString(string key, string value) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}
				
			if (value == null) {
				Debug.Log ("can't trace a null value." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			logStringForIos(key, value);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogString, key, value);
			}
			#endif
		}

		public void logKey(string key) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}
				

			#if UNITY_IOS && !UNITY_EDITOR
			logKeyForIos(key);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogKey, key);
			}
			#endif
		}

		public void countKey(string key) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}


			#if UNITY_IOS && !UNITY_EDITOR
			countKeyForIos(key);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_Count, key);
			}
			#endif
		}



		public void disableAccessPrivacyInformation() {
			#if UNITY_IOS && !UNITY_EDITOR
			disableAccessPrivacyInformationForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_DisableAccessPrivacyInformation);
			}
			#endif
		}

		public void setCustomerId(string curstomerId) {

			if (curstomerId == null) {
				Debug.Log ("===> fail to call setCustomerId(), curstomerId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> setCustomerId() is not supported by IOS." );
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_SetCustomerId, curstomerId);
			}
			#endif
		}

		public string getCustomerId() {

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> getCustomerId() is not supported by IOS." );
			return "";
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetCustomerId);
			}
			return "";
			#else
			return "";
			#endif
		}

		public string getOpenId() {

			#if UNITY_IOS && !UNITY_EDITOR
			//Debug.Log ("===> getOpenId() is not supported by IOS." );
			return getOpenIdForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetOpenId);
			}
			return "";
			#else
			return "";
			#endif
		}


		public string getUserId() {

			#if UNITY_IOS && !UNITY_EDITOR
			return getUserIdForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetUserId);
			}
			return "";
			#else
			return "";
			#endif
		}


        public string enalbeDebugMode(bool isOpen)
        {

#if UNITY_IOS && !UNITY_EDITOR
			return getUserIdForIos();
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				 jc.CallStatic(JavaClassStaticMethod_EnalbeDebugMode,isOpen);
			}
			return "";
#else
            return "";
#endif
        }

        public void logReportWithServer(string gameAccountId, string gameAccountServer, string iabPurchaseOriginalJson,
			string iabPurchaseSignature, Dictionary<string,string> dic) {

			if (gameAccountId == null) {
				Debug.Log ("===> fail to call logReportWithServer(), gameAccountId can't be null." );
				return;
			}

			if (gameAccountServer == null) {
				Debug.Log ("===> fail to call logReportWithServer(), gameAccountServer can't be null." );
				return;
			}

			if (iabPurchaseOriginalJson == null) {
				Debug.Log ("===> fail to call logReportWithServer(), iabPurchaseOriginalJson can't be null." );
				return;
			}

			if (iabPurchaseSignature == null) {
				Debug.Log ("===> fail to call logReportWithServer(), iabPurchaseSignature can't be null." );
				return;
			}
			 

			#if UNITY_IOS && !UNITY_EDITOR
				thirdpartyLogZFWithServerPlayerId(gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				string jsonmap = dicationaryToString (dic);
				if (jsonmap == null) {
					jsonmap = "";
				}
				jc.CallStatic (JavaClassStaticMethod_LogReportWithServerAndExtraJson, gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature, jsonmap);
			}
			#endif
		}

		public void logReport(string gameAccountId, string iabPurchaseOriginalJson, string iabPurchaseSignature, Dictionary<string,string> dic) {

			if (gameAccountId == null) {
				Debug.Log ("===> fail to call logReport(), gameAccountId can't be null." );
				return;
			}

			if (iabPurchaseOriginalJson == null) {
				Debug.Log ("===> fail to call logReport(), iabPurchaseOriginalJson can't be null." );
				return;
			}

			if (iabPurchaseSignature == null) {
				Debug.Log ("===> fail to call logReport(), iabPurchaseSignature can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			thirdpartyLogZFWithPlayerId(gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				string jsonmap = dicationaryToString (dic);
				if (jsonmap == null) {
					jsonmap = "";
				}
				jc.CallStatic (JavaClassStaticMethod_LogReportWithExtraJson, gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature, jsonmap);
			}
			#endif
		}


		public void huaWeiLoginNonAuth(string appId, string cpId, string gameServerId, string playerId) {
			if (appId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), appId can't be null." );
				return;
			}

			if (cpId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), cpId can't be null." );
				return;
			}

			if (gameServerId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), gameServerId can't be null." );
				return;
			}

			if (playerId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), playerId can't be null." );
				return;
			}


			#if UNITY_IOS && !UNITY_EDITOR
		
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_HuaWeiLoginNonAuth, appId, cpId, gameServerId, playerId);
			}
			#endif
		}

		public void huaweiLogin(string appId, string cpId, string gameServerId, string playerId, 
			string playerSSign, string playerLevel, string ts, string displayname) {
			if (appId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), appId can't be null." );
				return;
			}

			if (cpId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), cpId can't be null." );
				return;
			}

			if (gameServerId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), gameServerId can't be null." );
				return;
			}

			if (playerId == null) {
				Debug.Log ("===> fail to call huaweiLogin(), playerId can't be null." );
				return;
			}

			if (playerSSign == null) {
				Debug.Log ("===> fail to call huaweiLogin(), playerSSign can't be null." );
				return;
			}

			if (playerLevel == null) {
				Debug.Log ("===> fail to call huaweiLogin(), playerLevel can't be null." );
				return;
			}
			if (ts == null) {
				Debug.Log ("===> fail to call huaweiLogin(), ts can't be null." );
				return;
			}
			if (null == displayname) {
				displayname = "";
			}
			#if UNITY_IOS && !UNITY_EDITOR
	
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_HuaWeiLogin, appId, cpId, gameServerId, playerId, playerSSign, playerLevel, ts, displayname);
			}
			#endif
		}

		public void commonLogin(string type, string playerId, string token) {
			if (type == null) {
				Debug.Log ("===> fail to call commonLogin(), type can't be null." );
				return;
			}

			if (playerId == null) {
				Debug.Log ("===> fail to call commonLogin(), playerId can't be null." );
				return;
			}


			if (token == null) {
				Debug.Log ("===> fail to call commonLogin(), token can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
				commonIosLogin(type, playerId, token);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_CommonLogin, type, playerId, token);
			}
			#endif
		}
			
		public void guestLogin(string playerId) {

			if (playerId == null) {
				Debug.Log ("===> fail to call guestLogin(), playerId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			guestLoginWithGameId(playerId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_GuestLogin, playerId);
			}
			#endif
		}
			
		public void facebookLogin(string playerId, string openId, string openToken) {

			if (playerId == null) {
				Debug.Log ("===> fail to call facebookLogin(), gameAccountId can't be null." );
				return;
			}

			if (openId == null) {
				Debug.Log ("===> fail to call facebookLogin(), openId can't be null." );
				return;
			}

			if (openToken == null) {
				Debug.Log ("===> fail to call facebookLogin(), openToken can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			facebookLoginWithGameId(playerId, openId, openToken);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_FacebookLogin, playerId, openId, openToken);
			}
			#endif
		}

		public void portalLogin(string playerId, string portalId) {

			if (playerId == null) {
				Debug.Log ("===> fail to call portalLogin(), gameAccountId can't be null." );
				return;
			}

			if (portalId == null) {
				Debug.Log ("===> fail to call portalLogin(), portalId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			portalLoginWithPlayerId(playerId, portalId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_PortalLogin, playerId, portalId);
			}
			#endif
		}

		public void twitterLogin(string playerId, string twitterId, string twitterUserName, string twitterAuthToken) {

			if (playerId == null) {
				Debug.Log ("===> fail to call twitterLogin(), gameAccountId can't be null." );
				return;
			}

			if (twitterId == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterId can't be null." );
				return;
			}

			if (twitterUserName == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterUserName can't be null." );
				return;
			}

			if (twitterAuthToken == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterAuthToken can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			twitterLoginWithPlayerId(playerId, twitterId, twitterUserName, twitterAuthToken);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_TwitterLogin, playerId, twitterId, twitterUserName, twitterAuthToken);
			}
			#endif
		}


		public void logLogin(string loginType, string playerId, string loginToken, string ggid, string extensionJson) {

			if (string.IsNullOrEmpty(loginType)) {
				Debug.Log ("===> fail to call logLogin(), loginType can't be null." );
				return;
			}
            
			if (string.IsNullOrEmpty(playerId)) {
				Debug.Log ("===> fail to call logLogin(), playerId can't be null.");
				return;
			}
            
			if (string.IsNullOrEmpty(ggid)) {
				Debug.Log ("===> fail to call logLogin(), ggid can't be null.");
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			logIosAASLogin(loginType, playerId, loginToken, ggid, extensionJson);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LoginWithAASDK, loginType,playerId, ggid, loginToken, extensionJson);
			}
			#endif
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

		public void initDurationReport(string serverName, string serverZone, string uid, string ggid) {

			if (serverName == null) {
				Debug.Log ("===> fail to call initDurationReport(), serverName can't be null." );
				return;
			}

			if (serverZone == null) {
				Debug.Log ("===> fail to call initDurationReport(), serverZone can't be null." );
				return;
			}

			if (uid == null) {
				Debug.Log ("===> fail to call initDurationReport(), uid can't be null." );
				return;
			}

			if (ggid == null) {
				Debug.Log ("===> fail to call initDurationReport(), ggid can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			initDurationReportForIos(serverName, serverZone, uid, ggid);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_InitDruation, serverName, serverZone, uid, ggid);
			}
			#endif
		}

		public void becomeActive() {

			#if UNITY_IOS && !UNITY_EDITOR
			becomeActiveForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_OnAppResume);
			}
			#endif
		}

		public void resignActive() {

			#if UNITY_IOS && !UNITY_EDITOR
			resignActiveForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_OnAppPause);
			}
			#endif
		}

		public void getConversionData (string afConversionData, Action<string> success, Action<string> fail) {
			// 设置callback回调
			UPTraceObject.getInstance ().setGetConversionDataCallback (success, fail);
			// 调用原生的方法
			#if UNITY_IOS && !UNITY_EDITOR
				getConversionDataForIos(UPTraceObject.Unity_Callback_Class_Name,UPTraceObject.Unity_Callback_Function_Name,afConversionData);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_GetConversionData,afConversionData);
			}
			#endif
		}

		public void getPayUserLayer (Action<string> success, Action<string> fail) {
			Debug.Log ("getPayUserLayer in tracecall" );
			// 设置callback回调
			UPTraceObject.getInstance ().setGetPayUserLayerCallback (success, fail);
			// 调用原生的方法
			#if UNITY_IOS && !UNITY_EDITOR
				getPayUserLayerForIos(UPTraceObject.Unity_Callback_Class_Name,UPTraceObject.Unity_Callback_Function_Name);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_GetPayUserLayer);
			}
			#endif
		}
		
		public void setAFId(string afid) {
			#if UNITY_IOS && !UNITY_EDITOR
				setAppsFlyerIdForIos(afid);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_SetAFId,afid);
			}
			#endif
		}

	}


}
