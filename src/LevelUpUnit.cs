using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class LevelUpUnit : BaseUIBehaviour
{
	private Text lblEnd;

	private BaseTweenScale m_BaseTweenScale;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.lblEnd = base.FindTransform("AttEnd").GetComponent<Text>();
		this.lblEnd.set_text(string.Empty);
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		VisibilityBinder visibilityBinder = base.FindTransform("Background").get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "BGVisibility";
		TextBinder textBinder = base.FindTransform("AttBegin").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "AttBegin";
		textBinder = base.FindTransform("AttBegin1").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "AttBegin1";
		ActionBinder actionBinder = base.get_gameObject().AddComponent<ActionBinder>();
		actionBinder.BindingProxy = base.get_gameObject();
		actionBinder.CallActionOfVec3Binding.MemberName = "AttEnd";
		actionBinder.actoncall_vec3 = new Action<Vector3>(this.Num);
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	private void OnDisable()
	{
		this.lblEnd.set_text(string.Empty);
	}

	private void Num(Vector3 vec3)
	{
		if (this.m_BaseTweenScale == null)
		{
			this.m_BaseTweenScale = this.lblEnd.get_gameObject().AddComponent<BaseTweenScale>();
		}
		ChangeNumAnim changeNumAnim = this.lblEnd.get_gameObject().AddMissingComponent<ChangeNumAnim>();
		changeNumAnim.SetRolling(false);
		this.lblEnd.get_transform().set_localScale(Vector3.get_one());
		if (vec3.z == -1f)
		{
			this.lblEnd.set_text(string.Empty);
		}
		else if (vec3.z == 0f)
		{
			this.lblEnd.set_text(((int)vec3.y).ToString());
		}
		else
		{
			changeNumAnim.ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.lblEnd, (long)((int)vec3.x), (long)((int)vec3.y), delegate
			{
				this.m_BaseTweenScale.ChangeScaleTo(new Vector2(1.35f, 1.35f), 0.01f);
			}, delegate(string arg)
			{
				this.lblEnd.set_text(arg);
				SoundManager.Instance.CirculationPlayUI(10052, false);
			}, delegate
			{
				this.m_BaseTweenScale.ChangeScaleTo(Vector2.get_one(), 0.01f);
				EventDispatcher.Broadcast(EventNames.RollingNext);
			});
		}
	}
}
