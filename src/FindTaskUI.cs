using Foundation.Core.Databinding;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class FindTaskUI : UIBase
{
	private Slider m_slider;

	private Text m_titleName;

	private Text m_sliderNum;

	private Text m_txContent;

	private Text m_txPrice;

	private float mPrice;

	private bool mIsGold = true;

	private string mTaskName;

	private Action<float> mChangeHandler;

	private Action<int> mConfirmHandler;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.alpha = 0.7f;
		this.isMask = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_slider = base.FindTransform("PNSlider").GetComponent<Slider>();
		this.m_titleName = base.FindTransform("titleName").GetComponent<Text>();
		this.m_sliderNum = base.FindTransform("PNNum").GetComponent<Text>();
		this.m_txContent = base.FindTransform("txContent").GetComponent<Text>();
		this.m_txPrice = base.FindTransform("txPrice").GetComponent<Text>();
		this.m_slider.get_onValueChanged().AddListener(new UnityAction<float>(this.OnChange));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
	}

	public void OnChange(float value = 1f)
	{
		if (value > 0f)
		{
			if (this.mChangeHandler != null)
			{
				this.mChangeHandler.Invoke(value);
			}
			this.m_sliderNum.set_text(this.m_slider.get_value().ToString());
		}
		if (this.m_slider.get_value() < 1f)
		{
			this.m_slider.set_value(1f);
		}
	}

	public void OnConfirm()
	{
		if (this.mConfirmHandler != null)
		{
			this.mConfirmHandler.Invoke((int)this.m_slider.get_value());
		}
		this.OnClose();
	}

	public void OnClose()
	{
		UIManagerControl.Instance.HideUI("FindTaskUI");
	}

	public void OnAdd()
	{
		Slider expr_06 = this.m_slider;
		expr_06.set_value(expr_06.get_value() + 1f);
	}

	public void OnSub()
	{
		if (this.m_slider.get_value() > 1f)
		{
			Slider expr_1B = this.m_slider;
			expr_1B.set_value(expr_1B.get_value() - 1f);
		}
	}

	public void OnOpen(string title, float max, float price, string taskName, bool isGold, Action<float> onChange = null, Action<int> onConfirm = null)
	{
		this.m_titleName.set_text(title);
		this.m_slider.set_maxValue(max);
		this.m_slider.set_value(max);
		this.mPrice = price;
		this.mIsGold = isGold;
		this.mTaskName = taskName;
		this.mChangeHandler = onChange;
		this.mConfirmHandler = onConfirm;
		this.OnChange(1f);
	}

	public void SetDetailFindTask()
	{
		this.m_txContent.set_text(string.Format(GameDataUtils.GetChineseContent(301038, false), this.mTaskName, this.m_slider.get_maxValue()));
		this.m_txPrice.set_text(string.Format(GameDataUtils.GetChineseContent(301039, false), new object[]
		{
			this.mPrice * this.m_slider.get_value(),
			(!this.mIsGold) ? GameDataUtils.GetChineseContent(301041, false) : GameDataUtils.GetChineseContent(301040, false),
			this.mPrice,
			(!this.mIsGold) ? GameDataUtils.GetChineseContent(301041, false) : GameDataUtils.GetChineseContent(301040, false)
		}));
	}

	public void SetDetailUseBatch()
	{
		this.m_txContent.set_text("可用物品数量: " + this.m_slider.get_maxValue());
		this.m_txPrice.set_text(string.Empty);
	}
}
