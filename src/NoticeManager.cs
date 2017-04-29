using System;
using System.Collections;
using System.Diagnostics;
using XUPorterJSON;

public class NoticeManager
{
	private static NoticeManager instance;

	public Hashtable DataHashtable
	{
		get;
		set;
	}

	public static NoticeManager Instance
	{
		get
		{
			if (NoticeManager.instance == null)
			{
				NoticeManager.instance = new NoticeManager();
			}
			return NoticeManager.instance;
		}
	}

	private NoticeManager()
	{
	}

	public void Init()
	{
		string text = "{";
		text += "\"1\":{\"title\":\"官方维护\",\"content\":\"<color=#FFFF00FF>亲爱的玩家朋友：</color>\n本次维护时间：2015年9月3日上午8:00~9:00\n如果在预定时间内无法完成维护内容，开机时间将顺延。维护期间给您带来的不便，敬请谅解，37游戏感谢所有玩家的支持和配合。\"}";
		text += ",\"2\":{\"title\":\"本周内容\",\"content\":\"以下内容在部分服务器放出（客户端升级至1.37.1版）\n201服务器测试放出以下内容：\n1、调整药品的抗药性数值，不同药品的抗药性不同。玩家可在战斗内使用道具的界面中查看药品的抗药性。\"}";
		text += ",\"3\": {\"title\": \"标题3\",\"content\": \"内容3\"}";
		text += "}";
		this.DataHashtable = text.hashtableFromJson();
	}

	[DebuggerHidden]
	public IEnumerator requestContent()
	{
		NoticeManager.<requestContent>c__Iterator31 <requestContent>c__Iterator = new NoticeManager.<requestContent>c__Iterator31();
		<requestContent>c__Iterator.<>f__this = this;
		return <requestContent>c__Iterator;
	}
}
