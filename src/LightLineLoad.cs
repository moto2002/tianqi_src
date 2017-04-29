using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class LightLineLoad : MonoBehaviour
{
	public Image Line;

	[HideInInspector]
	public int Id;

	[HideInInspector]
	public int sort;

	private CanvasGroup group;

	private void Awake()
	{
		this.group = base.GetComponent<CanvasGroup>();
	}

	public void SetLine(LineType lineType)
	{
		string spriteName = string.Empty;
		switch (lineType)
		{
		case LineType.Norml:
			spriteName = "xian_1";
			break;
		case LineType.Blue:
			spriteName = "xian_2";
			break;
		case LineType.Yellow:
			spriteName = "xian_3";
			break;
		}
		ResourceManager.SetSprite(this.Line, ResourceManager.GetIconSprite(spriteName));
	}

	public void LoadAnimation(LineType name, float sumTime, Action handler)
	{
		this.SetLine(name);
		base.StartCoroutine(this.LoadAlpha());
		base.StartCoroutine(this.LoadCallBack(sumTime, handler));
	}

	[DebuggerHidden]
	private IEnumerator LoadCallBack(float sumTime, Action handler)
	{
		LightLineLoad.<LoadCallBack>c__Iterator5B <LoadCallBack>c__Iterator5B = new LightLineLoad.<LoadCallBack>c__Iterator5B();
		<LoadCallBack>c__Iterator5B.sumTime = sumTime;
		<LoadCallBack>c__Iterator5B.handler = handler;
		<LoadCallBack>c__Iterator5B.<$>sumTime = sumTime;
		<LoadCallBack>c__Iterator5B.<$>handler = handler;
		return <LoadCallBack>c__Iterator5B;
	}

	[DebuggerHidden]
	private IEnumerator LoadAlpha()
	{
		LightLineLoad.<LoadAlpha>c__Iterator5C <LoadAlpha>c__Iterator5C = new LightLineLoad.<LoadAlpha>c__Iterator5C();
		<LoadAlpha>c__Iterator5C.<>f__this = this;
		return <LoadAlpha>c__Iterator5C;
	}

	public void FadeInOutYellowAlpha(float timeCtrl, int times)
	{
		base.StartCoroutine(this.FadeInOutAlpha(timeCtrl, times));
	}

	[DebuggerHidden]
	private IEnumerator FadeInOutAlpha(float timeCtrl, int t)
	{
		LightLineLoad.<FadeInOutAlpha>c__Iterator5D <FadeInOutAlpha>c__Iterator5D = new LightLineLoad.<FadeInOutAlpha>c__Iterator5D();
		<FadeInOutAlpha>c__Iterator5D.timeCtrl = timeCtrl;
		<FadeInOutAlpha>c__Iterator5D.t = t;
		<FadeInOutAlpha>c__Iterator5D.<$>timeCtrl = timeCtrl;
		<FadeInOutAlpha>c__Iterator5D.<$>t = t;
		<FadeInOutAlpha>c__Iterator5D.<>f__this = this;
		return <FadeInOutAlpha>c__Iterator5D;
	}

	[DebuggerHidden]
	private IEnumerator LoadAnim(float sumTime, Action handler)
	{
		LightLineLoad.<LoadAnim>c__Iterator5E <LoadAnim>c__Iterator5E = new LightLineLoad.<LoadAnim>c__Iterator5E();
		<LoadAnim>c__Iterator5E.sumTime = sumTime;
		<LoadAnim>c__Iterator5E.handler = handler;
		<LoadAnim>c__Iterator5E.<$>sumTime = sumTime;
		<LoadAnim>c__Iterator5E.<$>handler = handler;
		<LoadAnim>c__Iterator5E.<>f__this = this;
		return <LoadAnim>c__Iterator5E;
	}
}
