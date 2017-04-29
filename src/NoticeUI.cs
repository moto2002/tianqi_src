using Foundation.Core.Databinding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : UIBase
{
	public GameObject PrefabItemTitle;

	public GameObject PrefabItemContent;

	public GameObject PrefabSpaceTitle;

	public GameObject PrefabSpaceContent;

	private void Awake()
	{
		this.isClick = false;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		Transform transform = base.FindTransform("NoticeRect");
		try
		{
			Hashtable dataHashtable = NoticeManager.Instance.DataHashtable;
			for (int i = 1; i <= dataHashtable.get_Count(); i++)
			{
				Hashtable hashtable = dataHashtable.get_Item(i + string.Empty) as Hashtable;
				GameObject gameObject = Object.Instantiate<GameObject>(this.PrefabItemTitle);
				ResourceManager.SetInstantiateUIRef(gameObject, null);
				gameObject.get_transform().Find("Title").GetComponent<Text>().set_text(hashtable.get_Item("title").ToString());
				gameObject.get_transform().SetParent(transform.get_transform());
				gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.PrefabSpaceTitle);
				ResourceManager.SetInstantiateUIRef(gameObject2, null);
				gameObject2.get_transform().SetParent(transform.get_transform());
				gameObject2.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.PrefabItemContent);
				ResourceManager.SetInstantiateUIRef(gameObject3, null);
				gameObject3.GetComponent<Text>().set_text(hashtable.get_Item("content").ToString());
				gameObject3.get_transform().SetParent(transform.get_transform());
				gameObject3.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				GameObject gameObject4 = Object.Instantiate<GameObject>(this.PrefabSpaceContent);
				ResourceManager.SetInstantiateUIRef(gameObject4, null);
				gameObject4.get_transform().SetParent(transform.get_transform());
				gameObject4.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
			}
		}
		catch (Exception message)
		{
			Debuger.Error(message);
		}
		ButtonCustom expr_1D0 = base.FindTransform("Button").GetComponent<ButtonCustom>();
		expr_1D0.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_1D0.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickButtonConfirm));
		base.FindTransform("ContentBackground").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
	}

	protected override void OnEnable()
	{
		base.FindTransform("ContentBackground").GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	public void OnClickButtonConfirm(GameObject go)
	{
		base.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
		UIManagerControl.Instance.UnLoadUIPrefab("NoticeUI");
	}
}
