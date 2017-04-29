using GameData;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class RisePoint : MonoBehaviour
{
	public Image frame;

	private Image pointIcon;

	private int id;

	private Transform mask;

	private CanvasGroup group;

	private int fxNextUid;

	private int openUid;

	private void Awake()
	{
		this.pointIcon = base.GetComponent<Image>();
		this.mask = base.get_transform().FindChild("mask");
		this.group = this.frame.GetComponent<CanvasGroup>();
		EventTriggerListener expr_3E = EventTriggerListener.Get(base.get_gameObject());
		expr_3E.onDown = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_3E.onDown, new EventTriggerListener.VoidDelegateGameObject(this.OnDownIcon));
		EventTriggerListener expr_6A = EventTriggerListener.Get(base.get_gameObject());
		expr_6A.onUp = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_6A.onUp, new EventTriggerListener.VoidDelegateGameObject(this.OnUpIcon));
		EventDispatcher.AddListener<int, bool, bool>(EventNames.LightPoint, new Callback<int, bool, bool>(this.OnLightPoint));
	}

	public void OnLightPoint(int arg1, bool isNext, bool isYellow)
	{
		if (arg1 == this.id)
		{
			Debug.LogError(this.id + "OnLightPoint" + isYellow);
			this.mask.get_gameObject().SetActive(false);
			this.OpenFxLight(isYellow);
			if (isNext)
			{
				this.OpenFxCanNextLight(0u);
			}
			EventDispatcher.Broadcast(EventNames.LightPointOver);
		}
	}

	public void FadeInOutAlpha(Action callBack)
	{
		float timeCtrl = 0.25f;
		int t = 3;
		base.StartCoroutine(this.FadeInOutAlpha(timeCtrl, t, callBack));
	}

	[DebuggerHidden]
	private IEnumerator FadeInOutAlpha(float timeCtrl, int t, Action callBack)
	{
		RisePoint.<FadeInOutAlpha>c__Iterator40 <FadeInOutAlpha>c__Iterator = new RisePoint.<FadeInOutAlpha>c__Iterator40();
		<FadeInOutAlpha>c__Iterator.timeCtrl = timeCtrl;
		<FadeInOutAlpha>c__Iterator.t = t;
		<FadeInOutAlpha>c__Iterator.callBack = callBack;
		<FadeInOutAlpha>c__Iterator.<$>timeCtrl = timeCtrl;
		<FadeInOutAlpha>c__Iterator.<$>t = t;
		<FadeInOutAlpha>c__Iterator.<$>callBack = callBack;
		<FadeInOutAlpha>c__Iterator.<>f__this = this;
		return <FadeInOutAlpha>c__Iterator;
	}

	private void OnDownIcon(GameObject go)
	{
		Debug.LogError(base.get_transform().get_position().y);
		TipsUI.Instance.ShowAttribute(this.id, new Vector3(base.get_transform().get_position().x, base.get_transform().get_position().y + 0.1f, base.get_transform().get_position().z));
	}

	private void OnUpIcon(GameObject go)
	{
		EventDispatcher.Broadcast("UIManagerControl.HideTipsUI");
	}

	public void SetData(LianTiShuXing point, bool isAction, bool isYellow, bool isNext = false)
	{
		if (point != null)
		{
			this.id = point.id;
			base.get_transform().set_localPosition(new Vector3((float)point.coordinate.get_Item(0), (float)point.coordinate.get_Item(1), 0f));
			ResourceManager.SetSprite(this.pointIcon, GameDataUtils.GetIcon(point.activateIcons));
			this.mask.get_gameObject().SetActive(!isAction);
			if (isAction)
			{
				this.frame.get_gameObject().SetActive(true);
				ResourceManager.SetSprite(this.frame, ResourceManager.GetIconSprite((!isYellow) ? "quan_1" : "quan_2"));
			}
			if (isNext)
			{
				this.OpenFxCanNextLight(0u);
			}
			if (CharacterManager.Instance.AllLightPoint.Exists((int e) => e == this.id))
			{
				this.OpenFxLight(false);
			}
		}
	}

	private void OpenSetFrame(bool isYellow)
	{
		if (this.frame.get_gameObject().get_activeSelf())
		{
			return;
		}
		this.frame.get_gameObject().SetActive(true);
		ResourceManager.SetSprite(this.frame, ResourceManager.GetIconSprite((!isYellow) ? "quan_1" : "quan_2"));
	}

	public void OpenFxCanNextLight(uint time)
	{
	}

	public void FxLight()
	{
	}

	public void OpenFxLight(bool isYellow)
	{
	}

	private void OnDestroy()
	{
		EventDispatcher.RemoveListener<int, bool, bool>(EventNames.LightPoint, new Callback<int, bool, bool>(this.OnLightPoint));
	}
}
