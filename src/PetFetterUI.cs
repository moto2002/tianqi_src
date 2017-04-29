using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetFetterUI : UIBase
{
	private class SortNode
	{
		public int id;

		public int weight;
	}

	public static PetFetterUI Instance;

	private Dictionary<int, int> havingPets = new Dictionary<int, int>();

	private GridLayoutGroup scrollLayout;

	private GameObject lastClickCell;

	private void Awake()
	{
		PetFetterUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnEnable()
	{
		this.Init();
	}

	private void Init()
	{
		this.havingPets.Clear();
		using (Dictionary<long, PetInfo>.Enumerator enumerator = PetManager.Instance.MaplistPet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, PetInfo> current = enumerator.get_Current();
				this.havingPets.Add(current.get_Value().petId, current.get_Value().petId);
			}
		}
		Transform transform = base.get_transform().FindChild("west").FindChild("scroll");
		this.scrollLayout = transform.FindChild("scrollLayout").GetComponent<GridLayoutGroup>();
		transform.GetComponent<ScrollRect>().set_verticalNormalizedPosition(1f);
		this.SetScroll();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			PetFetterUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void SetScroll()
	{
		for (int i = 0; i < this.scrollLayout.get_transform().get_childCount(); i++)
		{
			Transform child = this.scrollLayout.get_transform().GetChild(i);
			Object.Destroy(child.get_gameObject());
		}
		List<ChongWuJiBan> dataList = DataReader<ChongWuJiBan>.DataList;
		List<PetFetterUI.SortNode> list = new List<PetFetterUI.SortNode>();
		using (List<ChongWuJiBan>.Enumerator enumerator = dataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ChongWuJiBan current = enumerator.get_Current();
				List<int> linkedPetId = DataReader<ChongWuJiBan>.Get(current.linkedId).linkedPetId;
				int num = 0;
				for (int j = 0; j < linkedPetId.get_Count(); j++)
				{
					if (this.havingPets.ContainsKey(linkedPetId.get_Item(j)))
					{
						num++;
					}
				}
				list.Add(new PetFetterUI.SortNode
				{
					id = current.linkedId,
					weight = num
				});
			}
		}
		list.Sort(new Comparison<PetFetterUI.SortNode>(this.CompareNodes));
		for (int k = 0; k < list.get_Count(); k++)
		{
			ChongWuJiBan chongWuJiBan = DataReader<ChongWuJiBan>.Get(list.get_Item(k).id);
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(WidgetName.PetFetterScrollCell);
			instantiate2Prefab.get_transform().SetParent(this.scrollLayout.get_transform(), false);
			instantiate2Prefab.set_name(chongWuJiBan.linkedId.ToString());
			instantiate2Prefab.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickScrollCell);
			instantiate2Prefab.get_gameObject().SetActive(true);
			this.SetImgPets(instantiate2Prefab, chongWuJiBan.linkedId);
			this.SetName(instantiate2Prefab, chongWuJiBan.linkedId);
			if (k == 0)
			{
				this.lastClickCell = instantiate2Prefab;
				this.SetDetail(int.Parse(instantiate2Prefab.get_name()));
				this.SetScrollCellHightlight(instantiate2Prefab, true);
			}
		}
	}

	private int CompareNodes(PetFetterUI.SortNode a, PetFetterUI.SortNode b)
	{
		return -(a.weight - b.weight);
	}

	private void SetImgPets(GameObject cell, int linkedId)
	{
		List<int> linkedPetId = DataReader<ChongWuJiBan>.Get(linkedId).linkedPetId;
		for (int i = 0; i < linkedPetId.get_Count(); i++)
		{
			Pet pet = DataReader<Pet>.Get(linkedPetId.get_Item(i));
			SpriteRenderer selfPetIcon = PetManager.Instance.GetSelfPetIcon2(pet);
			Transform transform = cell.get_transform().FindChild("east").FindChild("imgGrid" + i);
			Image component = transform.GetComponent<Image>();
			string name = DataReader<PinZhiYanSe>.Get(pet.petColour).name;
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite(name));
			Image component2 = transform.FindChild("imgPet").GetComponent<Image>();
			ResourceManager.SetSprite(component2, selfPetIcon);
			if (this.havingPets.ContainsKey(linkedPetId.get_Item(i)))
			{
				ImageColorMgr.SetImageColor(component, false);
				ImageColorMgr.SetImageColor(component2, false);
			}
			else
			{
				ImageColorMgr.SetImageColor(component, true);
				ImageColorMgr.SetImageColor(component2, true);
			}
		}
	}

	private void SetName(GameObject cell, int linkedId)
	{
		int name = DataReader<ChongWuJiBan>.Get(linkedId).name;
		Text component = cell.get_transform().FindChild("west").FindChild("texName").GetComponent<Text>();
		component.set_text(GameDataUtils.GetChineseContent(name, false));
	}

	private void SetDetail(int linkedId)
	{
		ChongWuJiBan chongWuJiBan = DataReader<ChongWuJiBan>.Get(linkedId);
		Transform transform = base.get_transform().FindChild("east");
		Text component = transform.FindChild("texDetail").GetComponent<Text>();
		component.set_text(GameDataUtils.GetChineseContent(chongWuJiBan.desc, false));
		List<string> attrTexts = PetEvoGlobal.GetAttrTexts(chongWuJiBan.linkedAttrId);
		Debug.LogError("linkedId=" + linkedId);
		Debug.LogError("chongWuJiBanRow.linkedAttrId=" + chongWuJiBan.linkedAttrId);
		Debug.LogError("SetDetail=" + attrTexts.get_Count());
		Text[] array = new Text[3];
		Text[] array2 = new Text[3];
		for (int i = 0; i < 3; i++)
		{
			array[i] = transform.FindChild("attrKey" + (i + 1)).GetComponent<Text>();
			array2[i] = transform.FindChild("attrVal" + (i + 1)).GetComponent<Text>();
			array[i].set_text(string.Empty);
			array2[i].set_text(string.Empty);
		}
		for (int j = 0; j < attrTexts.get_Count(); j++)
		{
			string[] array3 = attrTexts.get_Item(j).Split(new char[]
			{
				' '
			});
			array[j].set_text(array3[0]);
			array2[j].set_text(array3[1]);
		}
	}

	private void SetScrollCellHightlight(GameObject cell, bool isHightlight)
	{
		Transform transform = cell.get_transform().FindChild("west");
		Transform transform2 = transform.FindChild("hightlight");
		if (transform2 != null)
		{
			Object.Destroy(transform2.get_gameObject());
		}
		Image component = cell.get_transform().Find("east").GetComponent<Image>();
		if (isHightlight)
		{
			GameObject gameObject = new GameObject();
			Image image = gameObject.AddComponent<Image>();
			ResourceManager.SetSprite(image, ResourceManager.GetIconSprite("pet_frame_pre"));
			image.set_type(1);
			gameObject.set_name("hightlight");
			gameObject.get_transform().set_localPosition(Vector3.get_zero());
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			gameObject.get_transform().set_localScale(Vector3.get_one());
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.set_sizeDelta(new Vector2(98f, 155f));
			gameObject.get_transform().SetParent(transform, false);
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("dailytask_frame_2"));
		}
		else
		{
			ResourceManager.SetSprite(component, ResourceManager.GetIconSprite("dailytask_frame_1"));
		}
	}

	private void OnClickScrollCell(GameObject currClickCell)
	{
		Debug.LogError("OnClickScrollCell=" + currClickCell.get_name());
		this.SetDetail(int.Parse(currClickCell.get_name()));
		this.SetScrollCellHightlight(this.lastClickCell, false);
		this.SetScrollCellHightlight(currClickCell, true);
		this.lastClickCell = currClickCell;
	}
}
