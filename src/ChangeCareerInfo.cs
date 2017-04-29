using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeCareerInfo : BaseUIBehaviour
{
	private int _profession;

	private GameObject m_goInfoBackground;

	private GameObject m_goInfo;

	private GameObject m_goCareer;

	private GameObject m_goBlockTask;

	private List<ChangeCareerTask> m_tasks = new List<ChangeCareerTask>();

	private List<ChangeCareerSkill> m_skills = new List<ChangeCareerSkill>();

	private Text m_lblSkillDesc;

	private List<ChangeCareerFeature> m_features = new List<ChangeCareerFeature>();

	private Text m_lblFeaturesDesc;

	private GameObject m_goCareerButton;

	private RawImage m_texCareerPicBg;

	private Button m_btnCareerPic;

	private Image m_texCareerPic;

	private Image m_spCareerNameBg;

	private Image m_spCareerName;

	private GameObject m_goCareerButtonTask;

	private GameObject m_goCareerButtonActivation;

	private GameObject m_goTaskTipRoot;

	private Text m_lblTaskTip;

	private GameObject m_goTaskTipDiamond;

	private Text m_lblTaskTipDiamondNum;

	private GameObject m_goCurrentCareerTip;

	public int profession
	{
		get
		{
			return this._profession;
		}
		set
		{
			this._profession = value;
			this.SetCareer();
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_goInfoBackground = base.FindTransform("InfoBackground").get_gameObject();
		this.m_goInfo = base.FindTransform("Info").get_gameObject();
		this.m_goCareer = base.FindTransform("Career").get_gameObject();
		this.m_tasks.Clear();
		for (int i = 1; i <= 3; i++)
		{
			Transform transform = base.FindTransform("Task" + i);
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ChangeCareerTask");
			UGUITools.SetParent(transform.get_gameObject(), instantiate2Prefab, true);
			this.m_tasks.Add(instantiate2Prefab.GetComponent<ChangeCareerTask>());
		}
		this.m_goBlockTask = base.FindTransform("BlockTask").get_gameObject();
		this.m_skills.Clear();
		for (int j = 1; j <= 3; j++)
		{
			Transform transform2 = base.FindTransform("ChangeCareerSkill" + j);
			this.m_skills.Add(transform2.get_gameObject().AddComponent<ChangeCareerSkill>());
		}
		this.m_lblSkillDesc = base.FindTransform("SkillDesc").GetComponent<Text>();
		this.m_features.Clear();
		for (int k = 1; k <= 4; k++)
		{
			Transform transform3 = base.FindTransform("ChangeCareerFeature" + k);
			this.m_features.Add(transform3.get_gameObject().AddComponent<ChangeCareerFeature>());
		}
		this.m_lblFeaturesDesc = base.FindTransform("FeaturesDesc").GetComponent<Text>();
		this.m_btnCareerPic = base.FindTransform("CareerPic").GetComponent<Button>();
		this.m_texCareerPicBg = base.FindTransform("CareerPicBg").GetComponent<RawImage>();
		this.m_texCareerPic = base.FindTransform("CareerPicImage").GetComponent<Image>();
		this.m_spCareerNameBg = base.FindTransform("CareerNameBg").GetComponent<Image>();
		this.m_spCareerName = base.FindTransform("CareerName").GetComponent<Image>();
		this.m_goCareerButton = base.FindTransform("CareerButton").get_gameObject();
		this.m_goCareerButtonTask = base.FindTransform("CareerButtonTask").get_gameObject();
		this.m_goCareerButtonActivation = base.FindTransform("CareerButtonActivation").get_gameObject();
		this.m_goTaskTipRoot = base.FindTransform("TaskTipRoot").get_gameObject();
		this.m_lblTaskTip = base.FindTransform("TaskTip").GetComponent<Text>();
		this.m_goTaskTipDiamond = base.FindTransform("TaskTipDiamond").get_gameObject();
		this.m_lblTaskTipDiamondNum = base.FindTransform("TaskTipDiamondNum").GetComponent<Text>();
		this.m_goCurrentCareerTip = base.FindTransform("CurrentCareerTip").get_gameObject();
		this.m_goCareerButtonActivation.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnChallenge));
		this.m_goCareerButtonTask.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnCareerTask));
		this.m_goCareerButton.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnBtnCareerSelected));
		this.m_btnCareerPic.get_onClick().AddListener(new UnityAction(this.OnBtnCareerSelected));
		base.FindTransform("ButtonArrow").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnArrow));
	}

	private void OnDisable()
	{
		this.ShowAnimation(false);
	}

	private void OnBtnCareerSelected()
	{
		if (ChangeCareerUIView.Instance != null)
		{
			ChangeCareerUIView.Instance.SetCurrentInfo(this);
		}
	}

	private void OnBtnCareerTask()
	{
		ChangeCareerManager.Instance.OnClickSelectCareer(this.profession);
	}

	private void OnChallenge()
	{
		ChangeCareerInstanceManager.Instance.EnterChangeCareerInstance(this.profession);
	}

	private void OnArrow()
	{
		if (ChangeCareerUIView.Instance != null)
		{
			ChangeCareerUIView.Instance.SwitchCurrentInfo();
		}
	}

	public void RefreshFeatureInfo(ZhuanZhiJiChuPeiZhi dataZZCP)
	{
		this.m_lblFeaturesDesc.set_text(GameDataUtils.GetChineseContent(dataZZCP.jobDescribe, false));
		int num = 0;
		while (num < this.m_features.get_Count() && num < dataZZCP.jobEvaluate.get_Count())
		{
			this.m_features.get_Item(num).SetName(GameDataUtils.GetChineseContent(1400 + num, false));
			this.m_features.get_Item(num).SetStar(dataZZCP.jobEvaluate.get_Item(num));
			num++;
		}
	}

	public void RefreshSkillInfo()
	{
		RoleCreate roleCreate = DataReader<RoleCreate>.Get(this.profession);
		if (roleCreate == null)
		{
			return;
		}
		for (int i = 0; i < this.m_skills.get_Count(); i++)
		{
			int key = 0;
			if (i == 0)
			{
				key = roleCreate.skill1.get_Item(0).value;
			}
			else if (i == 1)
			{
				key = roleCreate.skill2.get_Item(0).value;
			}
			else if (i == 2)
			{
				key = roleCreate.skill3.get_Item(0).value;
			}
			Skill skill = DataReader<Skill>.Get(key);
			if (skill == null)
			{
				this.m_skills.get_Item(i).get_gameObject().SetActive(false);
			}
			else
			{
				this.m_skills.get_Item(i).get_gameObject().SetActive(true);
				this.m_skills.get_Item(i).m_info = this;
				this.m_skills.get_Item(i).SetIcon(GameDataUtils.GetIcon(skill.icon));
				if (skill.describeId > 0)
				{
					this.m_skills.get_Item(i).SetDesc(this.m_lblSkillDesc, GameDataUtils.GetChineseContent(skill.describeId, false));
				}
				else
				{
					this.m_skills.get_Item(i).SetDesc(this.m_lblSkillDesc, string.Empty);
				}
				if (i == 0)
				{
					this.m_skills.get_Item(i).SetSelectBtnState(true);
					this.m_skills.get_Item(i).SetDesc();
				}
			}
		}
	}

	public void RefreshTaskInfo()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(this.profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return;
		}
		this.m_goCareerButtonTask.SetActive(false);
		this.m_goBlockTask.SetActive(false);
		this.m_goCareerButtonActivation.SetActive(false);
		this.m_goTaskTipRoot.SetActive(false);
		this.m_goTaskTipDiamond.SetActive(false);
		this.m_lblTaskTip.get_gameObject().SetActive(false);
		this.m_goCurrentCareerTip.SetActive(false);
		List<CareerTask> careerTasks = ChangeCareerManager.Instance.GetCareerTasks(this.profession);
		switch (this.GetTaskStatus(careerTasks))
		{
		case ChangeCareerTaskStatus.ReqIsFirstTime:
			this.m_goCareerButtonTask.SetActive(true);
			break;
		case ChangeCareerTaskStatus.ReqIsNoFirstTime:
			this.m_goCareerButtonTask.SetActive(true);
			break;
		case ChangeCareerTaskStatus.ReqIsRightNow:
			this.m_goCareerButtonTask.SetActive(true);
			this.m_goTaskTipRoot.SetActive(false);
			break;
		case ChangeCareerTaskStatus.Tasking:
			this.m_goBlockTask.SetActive(true);
			this.RefreshTasks(careerTasks);
			break;
		case ChangeCareerTaskStatus.ActivationIsFirstTime:
			this.m_goCareerButtonActivation.SetActive(true);
			this.m_goTaskTipRoot.SetActive(true);
			this.m_lblTaskTip.get_gameObject().SetActive(true);
			this.m_lblTaskTip.set_text("首次转职免费选择进阶职业");
			break;
		case ChangeCareerTaskStatus.ActivationIsNoFirstTime:
			this.m_goCareerButtonTask.SetActive(true);
			this.m_goTaskTipRoot.SetActive(true);
			this.m_goTaskTipDiamond.SetActive(true);
			this.m_lblTaskTip.get_gameObject().SetActive(true);
			this.m_lblTaskTip.set_text(string.Format("VIP{0}玩家可付费激活第二进阶职业", zhuanZhiJiChuPeiZhi.vipLevel));
			this.m_lblTaskTipDiamondNum.set_text("x" + zhuanZhiJiChuPeiZhi.price);
			break;
		case ChangeCareerTaskStatus.IsCurrentCareer:
			this.m_goCurrentCareerTip.SetActive(true);
			break;
		}
	}

	private ChangeCareerTaskStatus GetTaskStatus(List<CareerTask> dataTasks)
	{
		if (this.profession == EntityWorld.Instance.EntSelf.TypeID)
		{
			return ChangeCareerTaskStatus.IsCurrentCareer;
		}
		if (ChangeCareerManager.Instance.IsCareerChanged(this.profession))
		{
			return ChangeCareerTaskStatus.ReqIsRightNow;
		}
		if (ChangeCareerManager.Instance.IsCareerChanged())
		{
			return ChangeCareerTaskStatus.ActivationIsNoFirstTime;
		}
		if (dataTasks == null || dataTasks.get_Count() == 0)
		{
			if (ChangeCareerManager.Instance.GetTaskFirstTime())
			{
				return ChangeCareerTaskStatus.ReqIsFirstTime;
			}
			return ChangeCareerTaskStatus.ReqIsNoFirstTime;
		}
		else
		{
			int i;
			for (i = 0; i < dataTasks.get_Count(); i++)
			{
				if (dataTasks.get_Item(i).status == CareerTask.TaskStatus.TaskReceived || dataTasks.get_Item(i).status == CareerTask.TaskStatus.WaittingCommit)
				{
					return ChangeCareerTaskStatus.Tasking;
				}
			}
			if (i != dataTasks.get_Count())
			{
				return ChangeCareerTaskStatus.None;
			}
			if (ChangeCareerManager.Instance.IsCareerChanged())
			{
				return ChangeCareerTaskStatus.ActivationIsNoFirstTime;
			}
			return ChangeCareerTaskStatus.ActivationIsFirstTime;
		}
	}

	private void RefreshTasks(List<CareerTask> dataTasks)
	{
		for (int i = 0; i < this.m_tasks.get_Count(); i++)
		{
			this.m_tasks.get_Item(i).get_gameObject().SetActive(false);
		}
		if (dataTasks == null)
		{
			return;
		}
		for (int j = 0; j < dataTasks.get_Count(); j++)
		{
			this.m_tasks.get_Item(j).get_gameObject().SetActive(true);
			CareerTask careerTask = dataTasks.get_Item(j);
			int taskId = careerTask.taskId;
			ZhuanZhiRenWu zhuanZhiRenWu = DataReader<ZhuanZhiRenWu>.Get(taskId);
			if (zhuanZhiRenWu == null)
			{
				Debug.LogError("GameData.ZhuanZhiRenWu no exist, id = " + taskId);
			}
			else
			{
				bool flag = careerTask.status == CareerTask.TaskStatus.Finish;
				this.m_tasks.get_Item(j).SetName(zhuanZhiRenWu, careerTask.count, flag);
				this.m_tasks.get_Item(j).SetFinish(flag);
			}
		}
	}

	private void SetCareer()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(this.profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return;
		}
		this.SetCareer(zhuanZhiJiChuPeiZhi);
	}

	private void SetCareer(ZhuanZhiJiChuPeiZhi dataZZCP)
	{
		ResourceManager.SetSprite(this.m_texCareerPic, ResourceManager.GetIconSprite(dataZZCP.jobPic5));
		this.m_texCareerPic.SetNativeSize();
		ResourceManager.SetSprite(this.m_spCareerNameBg, ResourceManager.GetIconSprite(dataZZCP.jobNameBg));
		ResourceManager.SetSprite(this.m_spCareerName, ResourceManager.GetIconSprite(dataZZCP.jobNameImage));
	}

	public void RefreshAll()
	{
		ZhuanZhiJiChuPeiZhi zhuanZhiJiChuPeiZhi = DataReader<ZhuanZhiJiChuPeiZhi>.Get(this.profession);
		if (zhuanZhiJiChuPeiZhi == null)
		{
			return;
		}
		this.RefreshTaskInfo();
		this.RefreshSkillInfo();
		this.RefreshFeatureInfo(zhuanZhiJiChuPeiZhi);
	}

	public void Show(bool isShowButton, bool isShowInfo)
	{
		this.m_goCareer.SetActive(isShowButton);
		this.m_goInfoBackground.SetActive(isShowInfo);
		this.m_goInfo.SetActive(isShowInfo);
	}

	public void SetCareerPos(float button_x, float pic_x)
	{
		RectTransform rectTransform = this.m_goCareerButton.get_transform() as RectTransform;
		rectTransform.set_anchoredPosition(new Vector2(button_x, rectTransform.get_anchoredPosition().y));
		this.m_texCareerPic.get_rectTransform().set_anchoredPosition(new Vector2(pic_x, this.m_texCareerPic.get_rectTransform().get_anchoredPosition().y));
	}

	public void SetCareerButtonClick(bool isClick)
	{
		this.m_goCareerButton.GetComponent<Button>().set_interactable(isClick);
		this.m_btnCareerPic.set_enabled(isClick);
	}

	public void SetCareerButtonBlack(bool isBlack)
	{
		Color32 color;
		if (isBlack)
		{
			color = new Color32(100, 100, 100, 255);
		}
		else
		{
			color = new Color32(255, 255, 255, 255);
		}
		this.m_texCareerPicBg.set_color(color);
		this.m_texCareerPic.set_color(color);
		this.m_spCareerNameBg.set_color(color);
		this.m_spCareerName.set_color(color);
	}

	public void ShowAnimation(bool isShow)
	{
		Animator component = base.GetComponent<Animator>();
		component.set_enabled(isShow);
		if (isShow)
		{
			component.Play("open", 0, 0f);
		}
	}

	public void SetSkillSelected(ChangeCareerSkill ccSkill)
	{
		for (int i = 0; i < this.m_skills.get_Count(); i++)
		{
			this.m_skills.get_Item(i).SetSelectBtnState(this.m_skills.get_Item(i) == ccSkill);
		}
	}
}
