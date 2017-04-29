using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MemCollabItem : BaseUIBehaviour
{
	public enum PLAY_ANIM
	{
		Back_To_Front,
		Front_To_Back,
		Front_To_Hide
	}

	public int index;

	private Transform m_Background;

	private Transform m_Icon;

	private Image m_spPet;

	private Animator m_animator;

	private Button m_button;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_animator = base.get_gameObject().GetComponent<Animator>();
		this.m_Background = base.FindTransform("Background");
		this.m_Icon = base.FindTransform("Icon");
		this.m_spPet = base.FindTransform("Pet").GetComponent<Image>();
		this.m_button = base.get_gameObject().GetComponent<Button>();
		this.m_button.get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	private void OnDisable()
	{
		this.ShowAnimation(false, string.Empty);
	}

	private void OnButtonClick()
	{
		MemCollabUIView.Instance.OnMemCollabClick(this.index);
	}

	public void F2B_Half()
	{
		this.m_Background.SetAsLastSibling();
	}

	public void B2F_Half()
	{
		this.m_Background.SetAsFirstSibling();
	}

	public void ShowAnimation(bool isShow, string name = "")
	{
		Animator component = base.GetComponent<Animator>();
		component.set_enabled(isShow);
		if (isShow)
		{
			component.Play(name, 0, 0f);
		}
	}

	public void SetIsClicklock(bool islock)
	{
		this.m_button.set_enabled(!islock);
	}

	public void SetIcon(SpriteRenderer spr)
	{
		ResourceManager.SetSprite(this.m_spPet, spr);
	}

	public void SetBack()
	{
		this.SetIsClicklock(false);
		this.F2B_Half();
		this.m_Icon.set_localEulerAngles(new Vector3(0f, 180f, 0f));
		this.m_Background.set_localEulerAngles(new Vector3(0f, 180f, 0f));
		this.m_Icon.set_localScale(Vector3.get_one());
		this.m_Background.set_localScale(Vector3.get_one());
	}

	public void SetHide()
	{
		this.SetIsClicklock(true);
		this.m_Icon.set_localScale(Vector3.get_zero());
		this.m_Background.set_localScale(Vector3.get_zero());
	}

	public void SetAnimation(MemCollabItem.PLAY_ANIM pa)
	{
		switch (pa)
		{
		case MemCollabItem.PLAY_ANIM.Back_To_Front:
			this.ShowAnimation(true, "B2F");
			this.SetIsClicklock(true);
			break;
		case MemCollabItem.PLAY_ANIM.Front_To_Back:
			this.ShowAnimation(true, "F2B");
			this.SetIsClicklock(false);
			break;
		case MemCollabItem.PLAY_ANIM.Front_To_Hide:
			this.ShowAnimation(true, "F2H");
			this.SetIsClicklock(true);
			break;
		}
	}
}
