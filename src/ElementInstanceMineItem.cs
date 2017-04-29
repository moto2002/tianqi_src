using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ElementInstanceMineItem : BaseUIBehaviour
{
	public Text TextMineName;

	public Image ImageMine;

	public Text TextMineSpeedUnit;

	public Image ImageMineProduce;

	public Text TextMineSpeed;

	public ButtonCustom BtnOut;

	public Image ImagePet;

	public Text TextTimeLast;

	public MinePetInfo minePetInfo;

	public string timeID;

	private float timeCalDelta;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TextMineName = base.FindTransform("TextMineName").GetComponent<Text>();
		this.ImageMine = base.FindTransform("ImageMine").GetComponent<Image>();
		this.TextMineSpeedUnit = base.FindTransform("TextMineSpeedUnit").GetComponent<Text>();
		this.TextMineSpeed = base.FindTransform("TextMineSpeed").GetComponent<Text>();
		this.BtnOut = base.FindTransform("BtnOut").GetComponent<ButtonCustom>();
		this.ImagePet = base.FindTransform("ImagePet").GetComponent<Image>();
		this.ImageMineProduce = base.FindTransform("ImageMineProduce").GetComponent<Image>();
		this.TextTimeLast = base.FindTransform("TextTimeLast").GetComponent<Text>();
	}

	public void ResetTimeCal()
	{
		this.timeCalDelta = 0f;
		this.TextTimeLast.set_text(TimeConverter.ChangeSecsToString(ElementInstanceManager.Instance.GetTimeCal(this.minePetInfo.blockId)));
	}

	private void Update()
	{
		this.timeCalDelta += Time.get_deltaTime();
		if (this.timeCalDelta > 1f)
		{
			this.TextTimeLast.set_text(TimeConverter.ChangeSecsToString(ElementInstanceManager.Instance.GetTimeCal(this.minePetInfo.blockId)));
		}
	}
}
