using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FaceUIView : UIBase
{
	public static UIBase Instance;

	private Transform m_tranFaceSuits;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0f;
		this.isClick = true;
	}

	private void Awake()
	{
		FaceUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_tranFaceSuits = base.FindTransform("FaceSuits");
		this.LoadFaces();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
	}

	private void LoadFaces()
	{
		List<ChatManager.FaceSuit> faceSuits = ChatManager.Instance.GetFaceSuits();
		for (int i = 0; i < faceSuits.get_Count(); i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("Item2FaceSuit");
			UGUITools.SetParent(this.m_tranFaceSuits.get_gameObject(), instantiate2Prefab, false, "Item2Face" + i);
			Item2FaceSuit item2FaceSuit = instantiate2Prefab.AddUniqueComponent<Item2FaceSuit>();
			item2FaceSuit.SetFaceScale(1.5f);
			RectTransform rectTransform = item2FaceSuit.get_transform() as RectTransform;
			rectTransform.set_anchorMin(new Vector2(0.5f, 0.5f));
			rectTransform.set_anchorMax(new Vector2(0.5f, 0.5f));
			rectTransform.set_pivot(new Vector2(0.5f, 0.5f));
			rectTransform.set_sizeDelta(new Vector2(0f, 0f));
			UGUITools.ResetTransform(rectTransform);
			int num = faceSuits.get_Item(i).num;
			item2FaceSuit.SetFaces(faceSuits.get_Item(i).icons, delegate
			{
				if (ChatUIViewModel.Instance != null && ChatUIView.Instance != null)
				{
					if (ChatUIView.Instance.m_ChatInputUnit.FaceNum >= ChatManager.MAX_NUM_2_FACE)
					{
						FloatTextAddManager.Instance.AddFloatText(GameDataUtils.GetChineseContent(502063, false), Color.get_green());
					}
					else
					{
						ChatUIViewModel expr_5D = ChatUIViewModel.Instance;
						expr_5D.SendContent += ChatManager.FacePlaceholder + num.ToString("D2");
					}
				}
			});
		}
	}
}
