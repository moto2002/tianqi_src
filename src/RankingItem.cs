using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingItem : MonoBehaviour
{
	private Image imgRank;

	public Text textRank;

	private Transform m_bgAdd;

	private Transform head;

	private Image m_headBgImg;

	private Image m_headBgImg2;

	private Image m_headIcon;

	private Transform pet;

	private Text roleName;

	private Text roleLevelName;

	private Text level;

	private RankDataUnite m_RankData;

	private void Awake()
	{
		this.head = base.get_transform().FindChild("Head");
		this.m_bgAdd = base.get_transform().FindChild("ItemBgAddFigure");
		this.pet = base.get_transform().FindChild("petHead");
		this.imgRank = base.get_transform().FindChild("Top").GetComponent<Image>();
		this.m_headIcon = this.head.FindChild("Headicon").GetComponent<Image>();
		this.m_headBgImg = this.head.GetComponent<Image>();
		this.m_headBgImg2 = this.head.FindChild("Headbg2").GetComponent<Image>();
		this.roleName = base.get_transform().FindChild("Name").GetComponent<Text>();
		this.level = base.get_transform().FindChild("Levelvalue").GetComponent<Text>();
		this.roleLevelName = base.get_transform().FindChild("RoleLevel").GetComponent<Text>();
	}

	private void Start()
	{
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickItem);
	}

	private void OnClickItem(GameObject go)
	{
		FriendManager.Instance.SendFindBuddy(this.m_RankData.roleId);
	}

	public void UpdateItem(RankDataUnite data)
	{
		this.m_RankData = data;
		this.SetRank(data);
		this.SetType(data);
	}

	private void SetRank(RankDataUnite rank)
	{
		string rank2 = this.GetRank(rank.ranking);
		if (rank.ranking > 3)
		{
			this.SwitchRankImg(false);
			this.textRank.set_text(rank2);
			this.m_bgAdd.get_gameObject().SetActive(false);
		}
		else
		{
			this.SwitchRankImg(true);
			ResourceManager.SetSprite(this.imgRank, ResourceManager.GetIconSprite(rank2));
			this.m_bgAdd.get_gameObject().SetActive(true);
		}
	}

	private void SetType(RankDataUnite data)
	{
		string text = string.Empty;
		long num = 0L;
		switch (data.rankingType)
		{
		case RankingType.ENUM.Lv:
			this.SetHideHeadAndPetHead(false);
			text = "主角等级";
			num = data.num;
			ResourceManager.SetSprite(this.m_headIcon, UIUtils.GetRoleSmallIcon((int)data.career));
			if (this.m_headBgImg != null)
			{
				ResourceManager.SetSprite(this.m_headBgImg, GameDataUtils.GetItemFrameByColor(1));
			}
			if (this.m_headBgImg2 != null)
			{
				this.m_headBgImg2.set_enabled(false);
			}
			break;
		case RankingType.ENUM.Fighting:
			this.SetHideHeadAndPetHead(false);
			text = "综合战斗力";
			num = data.num;
			ResourceManager.SetSprite(this.m_headIcon, UIUtils.GetRoleSmallIcon((int)data.career));
			if (this.m_headBgImg != null)
			{
				ResourceManager.SetSprite(this.m_headBgImg, GameDataUtils.GetItemFrameByColor(1));
			}
			if (this.m_headBgImg2 != null)
			{
				this.m_headBgImg2.set_enabled(false);
			}
			break;
		case RankingType.ENUM.PetFighting:
			this.SetHideHeadAndPetHead(true);
			text = "宠物战斗力";
			num = data.num;
			if (this.m_headIcon != null)
			{
				ResourceManager.SetSprite(this.m_headIcon, PetManagerBase.GetPlayerPetIcon2((int)data.petId, data.petStar));
			}
			if (this.m_headBgImg != null)
			{
				ResourceManager.SetSprite(this.m_headBgImg, PetManager.GetPetFrame01(data.petStar));
			}
			if (this.m_headBgImg2 != null)
			{
				this.m_headBgImg2.set_enabled(true);
				ResourceManager.SetSprite(this.m_headBgImg2, PetManager.GetPetFrame02(data.petStar));
			}
			break;
		}
		this.roleName.set_text(data.roleName);
		this.roleLevelName.set_text(text + "：");
		this.level.set_text(num.ToString());
	}

	private string GetRank(int p)
	{
		switch (p)
		{
		case 1:
			return "icon_paiming_1";
		case 2:
			return "icon_paiming_2";
		case 3:
			return "icon_paiming_3";
		default:
			return p.ToString();
		}
	}

	private void SwitchRankImg(bool isImgRank)
	{
		this.imgRank.get_gameObject().SetActive(isImgRank);
		this.textRank.get_gameObject().SetActive(!isImgRank);
	}

	private void SetHideHeadAndPetHead(bool hideHead)
	{
		this.pet.get_gameObject().SetActive(hideHead);
	}
}
