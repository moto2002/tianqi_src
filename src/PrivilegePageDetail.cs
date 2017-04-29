using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BindingContext))]
public class PrivilegePageDetail : BaseUIBehaviour
{
	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MockBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		ScrollRectCustom component = base.FindTransform("ItemSR").GetComponent<ScrollRectCustom>();
		component.onBeginDrag = delegate(PointerEventData data)
		{
			PrivilegeUIView.Instance.PrivilegeDetailSR.OnBeginDrag(data);
		};
		component.onEndDrag = delegate(PointerEventData data)
		{
			PrivilegeUIView.Instance.PrivilegeDetailSR.OnEndDrag(data);
		};
		component.onDrag = delegate(PointerEventData data)
		{
			PrivilegeUIView.Instance.PrivilegeDetailSR.OnDrag(data);
		};
		base.FindTransform("TextDetialPower").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(80261, true));
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		TextBinder textBinder = base.FindTransform("EffectContent").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "EffectContent";
		textBinder.SetHeight = true;
		textBinder = base.FindTransform("ItemsName").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "ItemsName";
		textBinder = base.FindTransform("BtnOpenText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "BtnOpenText";
		textBinder = base.FindTransform("notCanGetText").get_gameObject().AddComponent<TextBinder>();
		textBinder.BindingProxy = base.get_gameObject();
		textBinder.LabelBinding.MemberName = "CanNotGetText";
		VisibilityBinder visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("Node2Hide").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "Node2HideVisibility";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("BtnOpen").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowBtnOpen";
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("notCanGetText").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ShowCanNotGetTxt";
		ListBinder listBinder = base.FindTransform("ItemList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2Draw";
		listBinder.SourceBinding.MemberName = "ObatinItems";
		listBinder = base.FindTransform("ItemSpecialList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2SpecialItem";
		listBinder.SourceBinding.MemberName = "ObatinSpecialItems";
		listBinder = base.FindTransform("DiamondList").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.PrefabName = "Item2SpecialItem";
		listBinder.SourceBinding.MemberName = "ObatinDiamondItems";
		ImageBinder imageBinder = base.FindTransform("VIPLevel10").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel10";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("VIPLevel1").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "VIPLevel1";
		imageBinder.SetNativeSize = true;
		imageBinder = base.FindTransform("ImageDetialTitle").get_gameObject().AddComponent<ImageBinder>();
		imageBinder.BindingProxy = base.get_gameObject();
		imageBinder.SpriteBinding.MemberName = "ImageDetialTitleBg";
		imageBinder.SetNativeSize = true;
		visibilityBinder = base.get_gameObject().AddComponent<VisibilityBinder>();
		visibilityBinder.BindingProxy = base.get_gameObject();
		visibilityBinder.Target = base.FindTransform("ImageDetialTitleBg").get_gameObject();
		visibilityBinder.ValueBinding.MemberName = "ImageDetialTitleBgVisibility";
	}

	protected override void EventsBinding()
	{
		base.EventsBinding();
		ButtonBinder buttonBinder = base.FindTransform("BtnOpen").get_gameObject().AddComponent<ButtonBinder>();
		buttonBinder.BindingProxy = base.get_gameObject();
		buttonBinder.OnClickBinding.MemberName = "OnBtnOpenClick";
		buttonBinder.EnabledBinding.MemberName = "EnableOfBtnOpen";
	}

	public void UpdteSmallIcon(int count)
	{
		Transform transform = base.FindTransform("EffectContent");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			GameObject gameObject = transform.GetChild(i).get_gameObject();
			Object.Destroy(gameObject);
		}
		for (int j = 0; j < count; j++)
		{
			GameObject gameObject2 = new GameObject();
			gameObject2.set_name("Detialdiamond" + j);
			gameObject2.AddComponent<Image>();
			Image component = gameObject2.GetComponent<Image>();
			component.get_rectTransform().SetParent(transform);
			component.get_rectTransform().set_localPosition(new Vector3(-20f, -13.5f - (float)(27 * j), 0f));
			component.get_rectTransform().set_localScale(new Vector3(1f, 1f, 1f));
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("j_diamond001"));
		}
	}
}
