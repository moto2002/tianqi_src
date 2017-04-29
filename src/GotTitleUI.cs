using GameData;
using System;
using UnityEngine.UI;

public class GotTitleUI : UIBase
{
	public Text Title;

	public Image TitleImage;

	public void setTitle(int titleId)
	{
		ChengHao chengHao = DataReader<ChengHao>.Get(titleId);
		if (chengHao.displayWay == 1)
		{
			this.Title.set_text(GameDataUtils.GetChineseContent(chengHao.icon, false));
			this.TitleImage.get_gameObject().SetActive(false);
			this.Title.get_gameObject().SetActive(true);
		}
		else if (chengHao.displayWay == 2)
		{
			ResourceManager.SetSprite(this.TitleImage, GameDataUtils.GetIcon(chengHao.icon));
			this.TitleImage.get_gameObject().SetActive(true);
			this.Title.get_gameObject().SetActive(false);
		}
		TimerHeap.AddTimer(3000u, 0, delegate
		{
			UIManagerControl.Instance.UnLoadUIPrefab("GotTitleUI");
		});
	}
}
