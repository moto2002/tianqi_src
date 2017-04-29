using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetFormationUnit : BaseUIBehaviour
{
	public List<PetFormationPet> pets = new List<PetFormationPet>();

	private GameObject m_goTextCurFormation;

	private GameObject m_goOpen;

	private GameObject m_goNotOpen;

	private Text m_lblTextNotOpen;

	private GameObject m_goDeselect;

	private GameObject m_goSelect;

	private GameObject m_goImageSelete;

	private int formation_index;

	public Action<int> actionFormation;

	public Action<int> actionChange;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.pets.Clear();
		for (int i = 1; i <= 3; i++)
		{
			this.pets.Add(base.FindTransform("PetSelect" + i).GetComponent<PetFormationPet>());
		}
		if (base.FindTransform("TextCurFormation") != null)
		{
			this.m_goTextCurFormation = base.FindTransform("TextCurFormation").get_gameObject();
		}
		this.m_goOpen = base.FindTransform("Open").get_gameObject();
		this.m_goNotOpen = base.FindTransform("NotOpen").get_gameObject();
		this.m_lblTextNotOpen = base.FindTransform("TextNotOpen").GetComponent<Text>();
		this.m_goDeselect = base.FindTransform("Deselect").get_gameObject();
		this.m_goSelect = base.FindTransform("Select").get_gameObject();
		if (base.FindTransform("ImageSelete") != null)
		{
			this.m_goImageSelete = base.FindTransform("ImageSelete").get_gameObject();
		}
		base.FindTransform("BtnSelectFormation").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickFormation);
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChange);
	}

	private void OnClickFormation(GameObject sender)
	{
		if (this.actionFormation != null)
		{
			this.actionFormation.Invoke(this.formation_index);
		}
	}

	private void OnClickChange(GameObject sender)
	{
		if (this.actionChange != null)
		{
			this.actionChange.Invoke(this.formation_index);
		}
	}

	public void SetAction(Action<int> _actionFormation, Action<int> _actionChange, int index)
	{
		this.actionFormation = _actionFormation;
		this.actionChange = _actionChange;
		this.formation_index = index;
	}

	public void ShowCurrentFormationFlag(bool isShow)
	{
		if (this.m_goTextCurFormation != null)
		{
			this.m_goTextCurFormation.SetActive(isShow);
		}
	}

	public void ShowOpen(bool isOpen)
	{
		this.m_goOpen.SetActive(isOpen);
		this.m_goNotOpen.SetActive(!isOpen);
	}

	public void SetNoOpen(string text)
	{
		this.m_lblTextNotOpen.set_text(text);
	}

	public void SetSelect(bool isSelect)
	{
	}

	public void ShowImageSelete(bool isShow)
	{
		if (this.m_goImageSelete)
		{
			this.m_goImageSelete.SetActive(isShow);
		}
	}
}
