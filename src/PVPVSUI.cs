using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PVPVSUI : UIBase
{
	public Vector3 endPos = Vector3.get_zero();

	public Vector3 startPos = Vector3.get_zero();

	public Transform Right;

	public Transform Left;

	public GameObject[] MyPets;

	public GameObject[] OpponentPets;

	private RawImage rightBg;

	private RawImage leftBg;

	private Text rightName;

	private Text rightFighting;

	private Text rightIntegral;

	private Image rightIntegralIcon;

	private Text leftName;

	private Text leftFighting;

	private Text leftIntegral;

	private Image leftIntegralIcon;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		this.rightBg = base.FindTransform("Rightbg").GetComponent<RawImage>();
		this.leftBg = base.FindTransform("Leftbg").GetComponent<RawImage>();
		this.rightName = base.FindTransform("RightName").GetComponent<Text>();
		this.rightFighting = base.FindTransform("Rightfighting").GetComponent<Text>();
		this.rightIntegral = base.FindTransform("RightIntegralValue").GetComponent<Text>();
		this.rightIntegralIcon = base.FindTransform("RightIntegralIcon").GetComponent<Image>();
		this.leftName = base.FindTransform("LeftName").GetComponent<Text>();
		this.leftFighting = base.FindTransform("Leftfighting").GetComponent<Text>();
		this.leftIntegral = base.FindTransform("LeftIntegralValue").GetComponent<Text>();
		this.leftIntegralIcon = base.FindTransform("LeftIntegralIcon").GetComponent<Image>();
		this.Init();
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
		this.EnterPVP();
	}

	private void MoveOnPath()
	{
		Animator component = this.Right.GetComponent<Animator>();
		Animator component2 = this.Left.GetComponent<Animator>();
		Animator component3 = base.get_transform().FindChild("VS").GetComponent<Animator>();
		component.Play("RightVs", 0, 0f);
		component2.Play("LeftVs", 0, 0f);
		component3.Play("VSAnim", 0, 0f);
	}

	private void Init()
	{
		MatchRoleInfo matchData = PVPManager.Instance.MatchData;
		ResourceManager.SetTexture(this.leftBg, UIUtils.GetRolePVPImage(EntityWorld.Instance.EntSelf.TypeID));
		this.leftName.set_text(string.Format("{1} Lv.{0}", EntityWorld.Instance.EntSelf.Lv, EntityWorld.Instance.EntSelf.Name));
		this.leftFighting.set_text(string.Format("战斗力: {0}", EntityWorld.Instance.EntSelf.Fighting));
		this.leftIntegral.set_text(string.Format("积分: <color=#ffeb4b>{0}</color>", PVPManager.Instance.PVPData.score));
		ResourceManager.SetSprite(this.leftIntegralIcon, ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(PVPManager.Instance.PVPData.score, true)));
		ResourceManager.SetTexture(this.rightBg, UIUtils.GetRolePVPImage((int)matchData.career));
		this.rightName.set_text(string.Format("{1} Lv.{0}", matchData.lv, matchData.name));
		this.rightFighting.set_text(string.Format("战斗力: {0}", matchData.fighting));
		this.rightIntegral.set_text(string.Format("积分: <color=#ffeb4b>{0}</color>", matchData.score));
		ResourceManager.SetSprite(this.rightIntegralIcon, ResourceManager.GetIconSprite(PVPManager.Instance.GetIntegralByScore(matchData.score, true)));
	}

	public void EnterPVP()
	{
		UIManagerControl.Instance.UnLoadUIPrefab("PVPVSUI");
	}
}
