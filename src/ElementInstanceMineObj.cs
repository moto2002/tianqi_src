using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceMineObj : BaseUIBehaviour
{
	private Image ImageMine;

	private Image ImagePetIcon;

	private Text TextMineName;

	private Text TextProduceValue;

	private Image ImageProduceIcon;

	private Text TextProduceNum;

	public ButtonCustom BtnGet;

	private Text TextMineTime;

	private string m_blockID = string.Empty;

	private float timeCalDelta;

	public string blockID
	{
		get
		{
			return this.m_blockID;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageMine = base.FindTransform("ImageMine").GetComponent<Image>();
		this.ImagePetIcon = base.FindTransform("ImagePetIcon").GetComponent<Image>();
		this.TextMineName = base.FindTransform("TextMineName").GetComponent<Text>();
		this.TextProduceValue = base.FindTransform("TextProduceValue").GetComponent<Text>();
		this.ImageProduceIcon = base.FindTransform("ImageProduceIcon").GetComponent<Image>();
		this.TextProduceNum = base.FindTransform("TextProduceNum").GetComponent<Text>();
		this.BtnGet = base.FindTransform("BtnGet").GetComponent<ButtonCustom>();
		this.TextMineTime = base.FindTransform("TextMineTime").GetComponent<Text>();
	}

	public void SetUI(MineInfo mineInfo, int index)
	{
		MinePetInfo mpi = ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.get_Item(index);
		this.m_blockID = mpi.blockId;
		BlockInfo blockInfo = ElementInstanceManager.Instance.m_elementCopyLoginPush.activateBlocks.Find((BlockInfo a) => a.blockId == mpi.blockId);
		YKuangJingKu yKuangJingKu = DataReader<YKuangJingKu>.Get(blockInfo.incidentTypeId);
		this.TextMineName.set_text(yKuangJingKu.holdName);
		ResourceManager.SetSprite(this.ImageProduceIcon, GameDataUtils.GetIcon(DataReader<Items>.Get(yKuangJingKu.item.get_Item(0)).icon));
		PetInfo petInfo = PetManager.Instance.GetPetInfo(mpi.petId);
		int num = 0;
		this.TextMineTime.set_text(TimeConverter.ChangeSecsToString(ElementInstanceManager.Instance.GetTimeCal(mineInfo.blockId)));
		if (DataReader<Pet>.Get(petInfo.petId).element == yKuangJingKu.petType)
		{
			num = yKuangJingKu.petStar.get_Item(petInfo.star - 1);
		}
		this.TextProduceValue.set_text((mineInfo.debrisInfos == null || mineInfo.debrisInfos.get_Count() <= 0) ? "0" : mineInfo.debrisInfos.get_Item(0).debrisNum.ToString());
		this.TextProduceNum.set_text((yKuangJingKu.itemAddTime.get_Item(0) + num).ToString() + "/小时");
		ResourceManager.SetSprite(this.ImagePetIcon, PetManager.Instance.GetSelfPetIcon(petInfo.petId));
		ResourceManager.SetSprite(this.ImageMine, GameDataUtils.GetIcon(yKuangJingKu.icon));
		if (mineInfo.debrisInfos.get_Count() == 0)
		{
			ImageColorMgr.SetImageColor(this.BtnGet.get_transform().FindChild("Image").GetComponent<Image>(), true);
			this.BtnGet.set_enabled(false);
		}
		else
		{
			ImageColorMgr.SetImageColor(this.BtnGet.get_transform().FindChild("Image").GetComponent<Image>(), false);
			this.BtnGet.set_enabled(true);
		}
	}

	public void ResetTimeCal()
	{
		this.timeCalDelta = 0f;
		this.TextMineTime.set_text(TimeConverter.ChangeSecsToString(ElementInstanceManager.Instance.GetTimeCal(this.m_blockID)));
	}

	private void Update()
	{
		this.timeCalDelta += Time.get_deltaTime();
		if (this.timeCalDelta > 1f)
		{
			this.TextMineTime.set_text(TimeConverter.ChangeSecsToString(ElementInstanceManager.Instance.GetTimeCal(this.m_blockID)));
		}
	}
}
