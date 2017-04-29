using Foundation.Core.Databinding;
using System;
using System.Collections;
using UnityEngine.UI;

public class BattlePassUIPet : BaseUIBehaviour
{
	private Image m_spBattlePassUIPetBarFg;

	private float m_fDstExp;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_spBattlePassUIPetBarFg = base.FindTransform("BattlePassUIPetBarFg").GetComponent<Image>();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ImageBinder imageBinder = base.FindTransform("BattlePassUIPetIcon").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.SpriteBinding.MemberName = "Icon";
		FillAmmountBinder fillAmmountBinder = base.FindTransform("BattlePassUIPetBarFg").get_gameObject().AddComponent<FillAmmountBinder>();
		fillAmmountBinder.FillValueBinding.MemberName = "ExpAmount";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	public void CacheExp()
	{
		this.m_fDstExp = this.m_spBattlePassUIPetBarFg.get_fillAmount();
		this.m_spBattlePassUIPetBarFg.set_fillAmount(0f);
	}

	public void ExpAnim()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("from", 0);
		hashtable.Add("to", this.m_fDstExp);
		hashtable.Add("time", 0.5f);
		hashtable.Add("onupdate", "ExpTween");
		iTween.ValueTo(base.get_gameObject(), hashtable);
	}

	private void ExpTween(float amount)
	{
		this.m_spBattlePassUIPetBarFg.set_fillAmount(amount);
	}
}
