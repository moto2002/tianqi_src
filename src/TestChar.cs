using System;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour
{
	private const float fadeLength = 0.6f;

	private const int typeWidth = 150;

	private const int buttonWidth = 20;

	public Transform source;

	public Transform target;

	private Transform[] targetHips;

	private Dictionary<string, SkinnedMeshRenderer> targetSmr = new Dictionary<string, SkinnedMeshRenderer>();

	private Dictionary<string, Dictionary<string, Transform>> data = new Dictionary<string, Dictionary<string, Transform>>();

	private int currentHairMesh = 1;

	private int currentPantsMesh = 1;

	private int currentShoesMesh = 1;

	private int currentTopMesh = 1;

	private void Start()
	{
		SkinnedMeshRenderer[] componentsInChildren = this.source.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		SkinnedMeshRenderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = array[i];
			string[] array2 = skinnedMeshRenderer.get_name().Split(new char[]
			{
				'-'
			});
			if (!this.targetSmr.ContainsKey(array2[0]))
			{
				this.data.Add(array2[0], new Dictionary<string, Transform>());
				GameObject gameObject = new GameObject();
				gameObject.set_name(array2[0]);
				gameObject.get_transform().set_parent(this.target);
				SkinnedMeshRenderer skinnedMeshRenderer2 = gameObject.AddComponent<SkinnedMeshRenderer>();
				this.targetSmr.Add(array2[0], skinnedMeshRenderer2);
			}
			this.data.get_Item(array2[0]).Add(array2[1], skinnedMeshRenderer.get_transform());
		}
		this.targetHips = this.target.GetComponentsInChildren<Transform>();
		using (Dictionary<string, Dictionary<string, Transform>>.Enumerator enumerator = this.data.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, Dictionary<string, Transform>> current = enumerator.get_Current();
				string key = current.get_Key();
				if (key != null)
				{
					if (TestChar.<>f__switch$map12 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
						dictionary.Add("eyes", 0);
						dictionary.Add("face", 1);
						dictionary.Add("hair", 2);
						dictionary.Add("pants", 3);
						dictionary.Add("shoes", 4);
						dictionary.Add("top", 5);
						TestChar.<>f__switch$map12 = dictionary;
					}
					int num;
					if (TestChar.<>f__switch$map12.TryGetValue(key, ref num))
					{
						switch (num)
						{
						case 0:
							this.InitCharactor("eyes", "1");
							break;
						case 1:
							this.InitCharactor("face", "1");
							break;
						case 2:
							this.InitCharactor("hair", "1");
							break;
						case 3:
							this.InitCharactor("pants", "1");
							break;
						case 4:
							this.InitCharactor("shoes", "1");
							break;
						case 5:
							this.InitCharactor("top", "1");
							break;
						}
					}
				}
			}
		}
	}

	private void InitCharactor(string part, string item)
	{
		SkinnedMeshRenderer component = this.data.get_Item(part).get_Item(item).GetComponent<SkinnedMeshRenderer>();
		List<Transform> list = new List<Transform>();
		Transform[] bones = component.get_bones();
		for (int i = 0; i < bones.Length; i++)
		{
			Transform transform = bones[i];
			Transform[] array = this.targetHips;
			for (int j = 0; j < array.Length; j++)
			{
				Transform transform2 = array[j];
				if (!(transform2.get_name() != transform.get_name()))
				{
					list.Add(transform2);
					break;
				}
			}
		}
		this.targetSmr.get_Item(part).set_sharedMesh(component.get_sharedMesh());
		this.targetSmr.get_Item(part).set_bones(list.ToArray());
		Material[] materials = new Material[]
		{
			component.get_materials()[0]
		};
		this.targetSmr.get_Item(part).set_materials(materials);
	}

	private void ChangeMesh(string part, string item)
	{
		this.SetCurrentMesh(part, item);
		SkinnedMeshRenderer component = this.data.get_Item(part).get_Item(item).GetComponent<SkinnedMeshRenderer>();
		List<Transform> list = new List<Transform>();
		Transform[] bones = component.get_bones();
		for (int i = 0; i < bones.Length; i++)
		{
			Transform transform = bones[i];
			Transform[] array = this.targetHips;
			for (int j = 0; j < array.Length; j++)
			{
				Transform transform2 = array[j];
				if (!(transform2.get_name() != transform.get_name()))
				{
					list.Add(transform2);
					break;
				}
			}
		}
		this.targetSmr.get_Item(part).set_sharedMesh(component.get_sharedMesh());
		this.targetSmr.get_Item(part).set_bones(list.ToArray());
		Material[] materials = new Material[]
		{
			component.get_materials()[0]
		};
		this.targetSmr.get_Item(part).set_materials(materials);
	}

	private void ChangeMaterial(string part, string item)
	{
		this.SetCurrentMaterial(part, item);
		int currentMeshType = this.GetCurrentMeshType(part);
		SkinnedMeshRenderer component = this.data.get_Item(part).get_Item(currentMeshType.ToString()).GetComponent<SkinnedMeshRenderer>();
		int num = int.Parse(item) - 1;
		Material[] materials = new Material[]
		{
			component.get_materials()[num]
		};
		this.targetSmr.get_Item(part).set_materials(materials);
	}

	private int GetCurrentMeshType(string part)
	{
		if (part != null)
		{
			if (TestChar.<>f__switch$map13 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
				dictionary.Add("hair", 0);
				dictionary.Add("pants", 1);
				dictionary.Add("shoes", 2);
				dictionary.Add("top", 3);
				TestChar.<>f__switch$map13 = dictionary;
			}
			int num;
			if (TestChar.<>f__switch$map13.TryGetValue(part, ref num))
			{
				switch (num)
				{
				case 0:
					return this.currentHairMesh;
				case 1:
					return this.currentPantsMesh;
				case 2:
					return this.currentShoesMesh;
				case 3:
					return this.currentTopMesh;
				}
			}
		}
		return 1;
	}

	private void SetCurrentMesh(string part, string item)
	{
		int num = int.Parse(item);
		if (part != null)
		{
			if (TestChar.<>f__switch$map14 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(4);
				dictionary.Add("hair", 0);
				dictionary.Add("pants", 1);
				dictionary.Add("shoes", 2);
				dictionary.Add("top", 3);
				TestChar.<>f__switch$map14 = dictionary;
			}
			int num2;
			if (TestChar.<>f__switch$map14.TryGetValue(part, ref num2))
			{
				switch (num2)
				{
				case 0:
					this.currentHairMesh = num;
					break;
				case 1:
					this.currentPantsMesh = num;
					break;
				case 2:
					this.currentShoesMesh = num;
					break;
				case 3:
					this.currentTopMesh = num;
					break;
				}
			}
		}
	}

	private void SetCurrentMaterial(string part, string item)
	{
	}

	private void OnGUI()
	{
		this.AddGuiButtonsOfChangeMesh("hair");
		this.AddGuiButtonsOfChangeMesh("pants");
		this.AddGuiButtonsOfChangeMesh("shoes");
		this.AddGuiButtonsOfChangeMesh("top");
	}

	private void AddGuiButtonsOfChangeMesh(string type)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("1", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.ChangeMesh(type, "1");
		}
		if (GUILayout.Button("2", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.ChangeMesh(type, "2");
		}
		GUILayout.Box("change " + type + " mesh", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		});
		GUILayout.Box("change " + type + " materials", new GUILayoutOption[]
		{
			GUILayout.Width(150f)
		});
		if (GUILayout.Button("1", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.ChangeMaterial(type, "1");
		}
		if (GUILayout.Button("2", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.ChangeMaterial(type, "2");
		}
		if (GUILayout.Button("3", new GUILayoutOption[]
		{
			GUILayout.Width(20f)
		}))
		{
			this.ChangeMaterial(type, "3");
		}
		GUILayout.EndHorizontal();
	}
}
