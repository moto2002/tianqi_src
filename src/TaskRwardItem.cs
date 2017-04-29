using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TaskRwardItem : MonoBehaviour
{
	public Image frame;

	public Image icon;

	public Text textNum;

	public void UdateRewardItem(int itemId, int num)
	{
		Items item = BackpackManager.Instance.GetItem(itemId);
		if (item == null)
		{
			Debug.LogError("尼玛！这个物品不存在了ID：" + itemId);
			return;
		}
		ResourceManager.SetSprite(this.frame, GameDataUtils.GetItemFrame(itemId));
		ResourceManager.SetSprite(this.icon, GameDataUtils.GetIcon(item.littleIcon));
		this.textNum.set_text("x" + string.Concat(num));
	}
}
