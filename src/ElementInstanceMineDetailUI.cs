using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceMineDetailUI : UIBase
{
	private ButtonCustom BtnChoosePet;

	private ButtonCustom BtnOut;

	private ButtonCustom BtnFight;

	private Image ImageMineProduce;

	private Text TextMineName;

	private Text TextProduceTitle;

	private Text TextProduce;

	private Text TextDesFight;

	private Image ImageMineIcon;

	private Transform PetIcon;

	public string blockID = "null";

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.7f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextMineName = base.FindTransform("TextMineName").GetComponent<Text>();
		this.TextDesFight = base.FindTransform("TextDesFight").GetComponent<Text>();
		this.BtnChoosePet = base.FindTransform("BtnChoosePet").GetComponent<ButtonCustom>();
		this.BtnOut = base.FindTransform("BtnOut").GetComponent<ButtonCustom>();
		this.BtnFight = base.FindTransform("BtnFight").GetComponent<ButtonCustom>();
		this.TextProduceTitle = base.FindTransform("TextProduceTitle").GetComponent<Text>();
		this.TextProduce = base.FindTransform("TextProduce").GetComponent<Text>();
		this.TextDesFight = base.FindTransform("TextDesFight").GetComponent<Text>();
		this.ImageMineProduce = base.FindTransform("ImageMineProduce").GetComponent<Image>();
		this.ImageMineIcon = base.FindTransform("ImageMineIcon").GetComponent<Image>();
		this.PetIcon = base.FindTransform("PetIcon");
		this.TextProduceTitle.set_text(GameDataUtils.GetChineseContent(502312, false));
		this.BtnChoosePet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnChoosePet);
		this.BtnFight.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnFight);
		this.BtnOut.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnOut);
	}

	private void Start()
	{
		if (ElementInstanceManager.Instance.GetBlockInfo(this.blockID) != null && !ElementInstanceManager.Instance.GetBlockInfo(this.blockID).isChallenge)
		{
			ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.ElementCopy, base.get_transform(), 0);
		}
		else
		{
			UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.GetComponent<RectTransform>().SetAsLastSibling();
		if (ElementInstanceManager.Instance.GetBlockInfo(this.blockID) != null && !ElementInstanceManager.Instance.GetBlockInfo(this.blockID).isChallenge)
		{
			ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.ElementCopy, base.get_transform(), 0);
		}
		else
		{
			UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		UIManagerControl.Instance.HideUI("ChangePetChooseUI");
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetSelectPetToMiningRes, new Callback(this.OnGetSelectPetToMiningRes));
		EventDispatcher.AddListener(EventNames.OnGetEvacuatePetRes, new Callback(this.OnGetEvacuatePetRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetSelectPetToMiningRes, new Callback(this.OnGetSelectPetToMiningRes));
		EventDispatcher.RemoveListener(EventNames.OnGetEvacuatePetRes, new Callback(this.OnGetEvacuatePetRes));
	}

	private void RefreshDes()
	{
		MinePetInfo minePetInfo = ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.Find((MinePetInfo a) => a.blockId == this.blockID);
		BlockInfo blockInfo = ElementInstanceManager.Instance.m_elementCopyLoginPush.activateBlocks.Find((BlockInfo a) => a.blockId == this.blockID);
		YKuangJingKu yKuangJingKu = DataReader<YKuangJingKu>.Get(blockInfo.incidentTypeId);
		this.TextMineName.set_text(yKuangJingKu.holdName);
		string text = GameDataUtils.GetChineseContent(502311, false);
		text = text.Replace("{s1}", yKuangJingKu.itemAddTime.get_Item(0).ToString());
		Debug.LogError(string.Concat(new object[]
		{
			ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.get_Count(),
			"===============",
			this.blockID,
			"============",
			minePetInfo
		}));
		int num = 0;
		if (minePetInfo != null)
		{
			PetInfo petInfo = PetManager.Instance.GetPetInfo(minePetInfo.petId);
			if (DataReader<Pet>.Get(petInfo.petId).element == yKuangJingKu.petType)
			{
				num = yKuangJingKu.petStar.get_Item(petInfo.star - 1);
			}
		}
		this.TextProduce.set_text((yKuangJingKu.itemAddTime.get_Item(0) + num).ToString() + GameDataUtils.GetChineseContent(502318, false));
		Debug.LogError("data.item[0]  " + yKuangJingKu.item.get_Item(0));
		ResourceManager.SetSprite(this.ImageMineProduce, GameDataUtils.GetIcon(DataReader<Items>.Get(yKuangJingKu.item.get_Item(0)).icon));
		ResourceManager.SetSprite(this.ImageMineIcon, GameDataUtils.GetIcon(yKuangJingKu.icon));
	}

	private void RefreshPet()
	{
		if (ElementInstanceManager.Instance.GetBlockInfo(this.blockID).isChallenge)
		{
			this.BtnFight.get_gameObject().SetActive(false);
			this.BtnChoosePet.get_gameObject().SetActive(true);
			this.TextDesFight.set_text(GameDataUtils.GetChineseContent(502313, false));
			MinePetInfo minePetInfo = ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.Find((MinePetInfo a) => a.blockId == this.blockID);
			if (minePetInfo != null)
			{
				this.PetIcon.get_gameObject().SetActive(true);
				PetInfo petInfo = PetManager.Instance.GetPetInfo(minePetInfo.petId);
				Pet dataPet = DataReader<Pet>.Get(petInfo.petId);
				ResourceManager.SetSprite(this.PetIcon.FindChild("ImageIcon").GetComponent<Image>(), PetManager.Instance.GetSelfPetIcon2(dataPet));
				ResourceManager.SetSprite(this.PetIcon.FindChild("ImageFrame").GetComponent<Image>(), PetManager.GetPetFrame01(petInfo.star));
				this.BtnOut.get_gameObject().SetActive(true);
			}
			else
			{
				this.PetIcon.get_gameObject().SetActive(false);
				this.BtnOut.get_gameObject().SetActive(false);
			}
		}
		else
		{
			this.TextDesFight.set_text(GameDataUtils.GetChineseContent(502314, false));
			this.BtnChoosePet.get_gameObject().SetActive(false);
			this.BtnOut.get_gameObject().SetActive(false);
			this.BtnFight.get_gameObject().SetActive(true);
		}
	}

	public void RefreshUI()
	{
		this.RefreshDes();
		this.RefreshPet();
		if (ElementInstanceManager.Instance.GetBlockInfo(this.blockID) != null && !ElementInstanceManager.Instance.GetBlockInfo(this.blockID).isChallenge)
		{
			ChangePetChooseUI changePetChooseUI = UIManagerControl.Instance.OpenUI("ChangePetChooseUI", null, false, UIType.NonPush) as ChangePetChooseUI;
			changePetChooseUI.RefreshUI(PetFormationType.FORMATION_TYPE.ElementCopy, base.get_transform(), 0);
		}
		else
		{
			UIManagerControl.Instance.HideUI("ChangePetChooseUI");
		}
	}

	private void OnClickBtnFight(GameObject sender)
	{
		Debug.LogError("-------------OnClickBtnFight");
		if (ElementInstanceManager.Instance.m_elementCopyLoginPush.exploreEnergy > 0)
		{
			ElementInstanceManager.Instance.SendStartToFightReq(this.blockID, delegate
			{
				this.Show(false);
			});
		}
		else
		{
			ElementInstanceManager.Instance.BuyRecovery();
		}
	}

	private void OnClickBtnChoosePet(GameObject sender)
	{
		ElementInstancePetChooseUI elementInstancePetChooseUI = UIManagerControl.Instance.OpenUI("ElementInstancePetChooseUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as ElementInstancePetChooseUI;
		elementInstancePetChooseUI.currentBlockID = this.blockID;
	}

	private void OnClickBtnOut(GameObject sender)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(502306, false), GameDataUtils.GetChineseContent(502305, false), delegate
		{
		}, delegate
		{
			ElementInstanceManager.Instance.SendEvacuatePetReq(this.blockID);
			this.RefreshUI();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}

	private void OnGetSelectPetToMiningRes()
	{
		this.RefreshPet();
		this.Show(false);
	}

	private void OnGetEvacuatePetRes()
	{
		this.RefreshUI();
	}
}
