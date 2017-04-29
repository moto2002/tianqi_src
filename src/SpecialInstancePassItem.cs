using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstancePassItem : MonoBehaviour
{
	public Image icon;

	public Image frame;

	public Text gooNum;

	private Items item;

	private void Start()
	{
		base.get_transform().FindChild("Button").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickItem);
	}

	private void OnClickItem(GameObject go)
	{
		ItemTipUIViewModel.ShowItem(this.item.id, null);
	}

	internal void SetData(int id, long num)
	{
		this.item = BackpackManager.Instance.GetItem(id);
		ResourceManager.SetSprite(this.icon, GameDataUtils.GetIcon(this.item.icon));
		ResourceManager.SetSprite(this.frame, GameDataUtils.GetItemFrame(this.item.id));
		this.gooNum.set_text(num.ToString());
	}
}
