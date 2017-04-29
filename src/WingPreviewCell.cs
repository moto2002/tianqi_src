using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WingPreviewCell : BaseUIBehaviour
{
	private RawImage m_rawImage;

	private Text m_txtName;

	private Text m_txtCondition;

	private GameObject m_btnGet;

	private GameObject m_btnWear;

	private GameObject m_btnUndress;

	public Action actionButtonGet;

	public Action actionButtonWear;

	public Action actionButtonUndress;

	private GameObject m_model;

	private int m_wingModelId;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_rawImage = base.FindTransform("rawImage").GetComponent<RawImage>();
		this.m_txtName = base.FindTransform("txtName").GetComponent<Text>();
		this.m_txtCondition = base.FindTransform("txtCondition").GetComponent<Text>();
		this.m_btnGet = base.FindTransform("btnGet").get_gameObject();
		this.m_btnWear = base.FindTransform("btnWear").get_gameObject();
		this.m_btnUndress = base.FindTransform("btnUndress").get_gameObject();
		EventTriggerListener expr_97 = EventTriggerListener.Get(base.get_gameObject());
		expr_97.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_97.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragModel));
		base.FindTransform("btnGet").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonGetClick));
		base.FindTransform("btnWear").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonWearClick));
		base.FindTransform("btnUndress").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonUndressClick));
	}

	private void OnDragModel(PointerEventData eventData)
	{
		Transform expr_0B = this.m_model.get_transform();
		expr_0B.set_rotation(expr_0B.get_rotation() * Quaternion.AngleAxis(-eventData.get_delta().x, Vector3.get_up()));
	}

	private void OnButtonGetClick()
	{
		if (this.actionButtonGet != null)
		{
			this.actionButtonGet.Invoke();
		}
	}

	private void OnButtonWearClick()
	{
		if (this.actionButtonWear != null)
		{
			this.actionButtonWear.Invoke();
		}
	}

	private void OnButtonUndressClick()
	{
		if (this.actionButtonUndress != null)
		{
			this.actionButtonUndress.Invoke();
		}
	}

	public void SetRawImage(int wingModelId)
	{
		this.m_wingModelId = wingModelId;
		this.m_model = WingGlobal.SetRawImage(this.m_rawImage, wingModelId);
	}

	public void SetName(string name)
	{
		this.m_txtName.set_text(name);
	}

	public void SetCondition(bool isShow, string name = "")
	{
		this.m_txtCondition.get_gameObject().SetActive(isShow);
		this.m_txtCondition.set_text(name);
	}

	public void ShowButtonGet(bool isShow)
	{
		this.m_btnGet.SetActive(isShow);
	}

	public void ShowButtonWear(bool isShow)
	{
		this.m_btnWear.SetActive(isShow);
	}

	public void ShowButtonUndress(bool isShow)
	{
		this.m_btnUndress.SetActive(isShow);
	}

	public void DoOnApplicationPause()
	{
		this.SetRawImage(this.m_wingModelId);
	}
}
