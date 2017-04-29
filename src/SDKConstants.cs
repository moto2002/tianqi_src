using System;
using System.Collections.Generic;

public class SDKConstants
{
	public const string Log = "log";

	public const string InitSDK = "initSDK";

	public const string Login = "login";

	public const string Logout = "exitSDK";

	public const string SubmitExtendData = "submitExtendData";

	public const string Pay = "pay";

	public const string ApplicationQuit = "applicationQuit";

	public const int sdk_default = 0;

	public const int sdk_telecom_Android = 27;

	public const int sdk_meiti_Android = 10001;

	public const int sdk_yunce_Android = 10002;

	public const int sdk_360_Android = 1;

	public const int sdk_360_ios = 2;

	public const int sdk_oppo_Android = 3;

	public const int sdk_oppo_ios = 4;

	public const int sdk_uc_Android = 5;

	public const int sdk_uc_ios = 6;

	public const int sdk_vivo_Android = 7;

	public const int sdk_vivo_ios = 8;

	public const int sdk_baidu_Android = 9;

	public const int sdk_baidu_ios = 10;

	public const int sdk_huawei_Android = 11;

	public const int sdk_huawei_ios = 12;

	public const int sdk_jinli_Android = 13;

	public const int sdk_jinli_ios = 14;

	public const int sdk_kupai_Android = 15;

	public const int sdk_kupai_ios = 16;

	public const int sdk_lianxiang_Android = 17;

	public const int sdk_lianxiang_ios = 18;

	public const int sdk_meizu_Android = 19;

	public const int sdk_meizu_ios = 20;

	public const int sdk_xiaomi_Android = 21;

	public const int sdk_xiaomi_ios = 22;

	public const int sdk_qq_Android = 23;

	public const int sdk_qq_ios = 24;

	public const int sdk_weixin_Android = 25;

	public const int sdk_weixin_ios = 26;

	public const int sdk_shengli_Android = 53;

	public const int sdk_shengli_ios = 54;

	public const int sdk_kuaifa_Android = 55;

	public const int sdk_cmge_Android = 56;

	public const int sdk_cmge_ios = 58;

	public const int sdk_yl_Android = 59;

	public const int sdk_yl_ios = 60;

	public const int sdk_type_ios = 998;

	public static Dictionary<int, string> sdkNameMap;

	static SDKConstants()
	{
		// 注意: 此类型已标记为 'beforefieldinit'.
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		dictionary.Add(0, "hsv_ios_tq");
		dictionary.Add(1, "360_Android");
		dictionary.Add(2, "360_ios");
		dictionary.Add(3, "oppo_Android");
		dictionary.Add(4, "oppo_ios");
		dictionary.Add(5, "uc_Android");
		dictionary.Add(6, "uc_ios");
		dictionary.Add(7, "vivo_Android");
		dictionary.Add(8, "vivo_ios");
		dictionary.Add(9, "baidu_Android");
		dictionary.Add(10, "baidu_ios");
		dictionary.Add(11, "huawei_Android");
		dictionary.Add(12, "huawei_ios");
		dictionary.Add(13, "jinli_Android");
		dictionary.Add(14, "jinli_ios");
		dictionary.Add(15, "kupai_Android");
		dictionary.Add(16, "kupai_ios");
		dictionary.Add(17, "lianxiang_Android");
		dictionary.Add(18, "lianxiang_ios");
		dictionary.Add(19, "meizu_Android");
		dictionary.Add(20, "meizu_ios");
		dictionary.Add(21, "xiaomi_Android");
		dictionary.Add(22, "xiaomi_ios");
		dictionary.Add(23, "qq_Android");
		dictionary.Add(24, "qq_ios");
		dictionary.Add(25, "weixin_Android");
		dictionary.Add(26, "weixin_ios");
		dictionary.Add(53, "shengli_Android");
		dictionary.Add(54, "shengli_ios");
		dictionary.Add(55, "kuaifa_Android");
		dictionary.Add(56, "cmge_Android");
		dictionary.Add(59, "yl_Android");
		dictionary.Add(998, "type_ios");
		dictionary.Add(27, "telecom_Android");
		dictionary.Add(10001, "meiti_Android");
		dictionary.Add(10002, "yunce_Android");
		SDKConstants.sdkNameMap = dictionary;
	}
}
