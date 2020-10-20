using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CSSDK
{

	public class CSSDKApi
	{
		private static CSSDKCall sdkCall = null;


		private static void instanceOfCall() {
			if (sdkCall == null) {
				sdkCall = new CSSDKCall ();
			}
		}
		
		public static void init (string productId) {
			Debug.Log("===> call init in CSSDKAPI");
			instanceOfCall ();
			if(productId == null || productId == ""){
				Debug.Log("===> no productid");
				return;
			}
			sdkCall.initCall (productId);
		}

		public static void show () {
			Debug.Log("===> call show in CSSDKAPI");
			instanceOfCall ();
			sdkCall.showCall ();
		}


		public static void haveNewMessage (Action<bool,string> callback) {
			Debug.Log("===> call haveNewMessage in CSSDKAPI");
			instanceOfCall ();
			sdkCall.haveNewMessageCall (callback);
		}

		public static void addExtraParam (Dictionary<string, string> dic) {
			Debug.Log("===> call addExtraParam in CSSDKAPI");
			instanceOfCall ();
			sdkCall.addExtraParamCall (dic);
		}

		public static string getVersion () {
			Debug.Log("===> call getVersion in CSSDKAPI");
			instanceOfCall ();
			return sdkCall.getVersionCall ();
		}

        
    }

}
