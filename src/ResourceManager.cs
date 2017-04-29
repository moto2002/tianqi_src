using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

public class ResourceManager : ResourceManagerBase
{
	private static ResourceManager mInstance;

	private Material _MatStencilFont;

	private Material _MatStencilAlphaBlendedETC01;

	private Material _MatStencilAlphaBlendedETC02;

	private Material _MatStencilAlphaBlendedETC03;

	private static bool IsModelDataInit = false;

	private static Dictionary<string, HashSet<int>> m_modelEffects = new Dictionary<string, HashSet<int>>();

	private static Dictionary<string, HashSet<int>> m_modelFxs = new Dictionary<string, HashSet<int>>();

	public static ResourceManager Instance
	{
		get
		{
			if (ResourceManager.mInstance == null)
			{
				ResourceManager.mInstance = new ResourceManager();
			}
			return ResourceManager.mInstance;
		}
	}

	public Material MatStencilFont
	{
		get
		{
			if (this._MatStencilFont == null)
			{
				Shader shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilFont");
				this._MatStencilFont = new Material(shader);
			}
			return this._MatStencilFont;
		}
	}

	public Material MatStencilAlphaBlendedETC01
	{
		get
		{
			if (this._MatStencilAlphaBlendedETC01 == null)
			{
				Shader shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilETC");
				this._MatStencilAlphaBlendedETC01 = new Material(shader);
			}
			return this._MatStencilAlphaBlendedETC01;
		}
	}

	public Material MatStencilAlphaBlendedETC02
	{
		get
		{
			if (this._MatStencilAlphaBlendedETC02 == null)
			{
				Shader shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilETC");
				this._MatStencilAlphaBlendedETC02 = new Material(shader);
			}
			return this._MatStencilAlphaBlendedETC02;
		}
	}

	public Material MatStencilAlphaBlendedETC03
	{
		get
		{
			if (this._MatStencilAlphaBlendedETC03 == null)
			{
				Shader shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilETC");
				this._MatStencilAlphaBlendedETC03 = new Material(shader);
			}
			return this._MatStencilAlphaBlendedETC03;
		}
	}

	public static GameObject GetInstantiate2Prefab(string name)
	{
		Object @object = AssetManager.LoadAssetNowOfUI(name);
		if (@object == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(@object) as GameObject;
		ResourceManager.SetInstantiateUIRef(gameObject, FileSystem.GetPathOfPrefab(name));
		return gameObject;
	}

	public static void SetInstantiateUIRef(GameObject goInstantiate, string resName)
	{
		if (SystemConfig.IsRefenenceControlOn)
		{
			ResourceManager.SetAssetRef(goInstantiate, resName);
		}
		Image[] componentsInChildren = goInstantiate.GetComponentsInChildren<Image>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].get_gameObject().AddUniqueComponent<UIImageRef>();
		}
		RawImage[] componentsInChildren2 = goInstantiate.GetComponentsInChildren<RawImage>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			componentsInChildren2[j].get_gameObject().AddUniqueComponent<UIImageRef>();
		}
	}

	public static void SetAssetRef(GameObject goInstantiate, string resName)
	{
		if (!string.IsNullOrEmpty(resName))
		{
			BaseBehaviour baseBehaviour = goInstantiate.AddComponent<BaseBehaviour>();
			baseBehaviour.ResName = resName;
		}
	}

	public static void SetSprite(Image image, SpriteRenderer spr)
	{
		if (image != null && spr != null)
		{
			if (UIImageRef.GetIsRefAwakeWithSetNull(image.get_gameObject()))
			{
				AssetManager.AssetOfTPManager.AddReferenceCount(spr);
				AssetManager.AssetOfTPManager.MinusReferenceCount(image);
			}
			image.set_sprite(spr.get_sprite());
			image.set_material(spr.get_sharedMaterial());
		}
	}

	public static void SetSprite(Image image, Image src)
	{
		if (image != null && src != null)
		{
			if (UIImageRef.GetIsRefAwakeWithSetNull(image.get_gameObject()))
			{
				AssetManager.AssetOfTPManager.AddReferenceCount(src);
				AssetManager.AssetOfTPManager.MinusReferenceCount(image);
			}
			image.set_sprite(src.get_sprite());
			image.set_material(src.get_material());
		}
	}

	public static void SetTexture(RawImage rawImage, string name)
	{
		if (rawImage != null)
		{
			Texture texture = ResourceManager.GetTexture(name);
			if (UIImageRef.GetIsRefAwakeWithSetNull(rawImage.get_gameObject()))
			{
				UIImageRef.AddReferenceCountOfRawImage(texture);
				UIImageRef.MinusReferenceCountOfRawImage(rawImage);
			}
			rawImage.set_texture(texture);
		}
	}

	public static bool IsTextureExist(string name)
	{
		return ResourceManager.GetTexture(name) != null;
	}

	public static void SetCodeTexture(RawImage rawImage, string name)
	{
		if (rawImage != null)
		{
			Texture codeTexture = ResourceManager.GetCodeTexture(name);
			if (UIImageRef.GetIsRefAwakeWithSetNull(rawImage.get_gameObject()))
			{
				UIImageRef.AddReferenceCountOfRawImage(codeTexture);
				UIImageRef.MinusReferenceCountOfRawImage(rawImage);
			}
			rawImage.set_texture(codeTexture);
		}
	}

	public static void SetIconSprite(Image image, string spriteName)
	{
		ResourceManager.SetSprite(image, ResourceManager.GetIconSprite(spriteName));
	}

	public static SpriteRenderer GetIconSprite(string spriteName)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		spriteName = GameDataUtils.SplitString4Dot0(spriteName);
		return AssetManager.AssetOfTPManager.GetSpriteRenderer(spriteName);
	}

	private static Texture GetTexture(string name)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullTexture();
		}
		name = GameDataUtils.SplitString4Dot0(name);
		return AssetManager.GetTexture(name);
	}

	public static void SetCodeSprite(Image image, string spriteName)
	{
		ResourceManager.SetSprite(image, ResourceManager.GetCodeSprite(spriteName));
	}

	public static SpriteRenderer GetCodeSprite(string spriteName)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		if (GameDataUtils.IsCodeIconExist(spriteName))
		{
			return AssetManager.AssetOfTPManager.GetSpriteRenderer(spriteName);
		}
		return AssetManager.AssetOfTPManager.GetSpriteRenderer("99999");
	}

	public static void SetCodeSprite(Image image, int id)
	{
		ResourceManager.SetSprite(image, ResourceManager.GetCodeSprite(id));
	}

	public static SpriteRenderer GetCodeSprite(int id)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		IconCode iconCode = DataReader<IconCode>.Get(id);
		if (iconCode != null)
		{
			return AssetManager.AssetOfTPManager.GetSpriteRenderer(iconCode.name);
		}
		return AssetManager.AssetOfTPManager.GetSpriteRenderer("99999");
	}

	private static Texture GetCodeTexture(string name)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullTexture();
		}
		return AssetManager.GetTexture(name);
	}

	public Object GetAsset2UIAnim(string name)
	{
		return AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPath(name, ".controller"), typeof(Object));
	}

	private static void InitModelData()
	{
		if (ResourceManager.IsModelDataInit)
		{
			return;
		}
		ResourceManager.IsModelDataInit = true;
		ModelData modelData = AssetManager.AssetOfNoPool.LoadAssetNowNoAB("ModelData/model", typeof(Object)) as ModelData;
		for (int i = 0; i < modelData.effects.get_Count(); i++)
		{
			string[] array = modelData.effects.get_Item(i).Split("-".ToCharArray());
			ResourceManager.m_modelEffects.Add(array[0], new HashSet<int>());
			string[] array2 = array[1].Split(",".ToCharArray());
			for (int j = 0; j < array2.Length; j++)
			{
				if (!string.IsNullOrEmpty(array2[j]))
				{
					ResourceManager.m_modelEffects.get_Item(array[0]).Add(int.Parse(array2[j]));
				}
			}
		}
		for (int k = 0; k < modelData.fxs.get_Count(); k++)
		{
			string[] array3 = modelData.fxs.get_Item(k).Split("-".ToCharArray());
			ResourceManager.m_modelFxs.Add(array3[0], new HashSet<int>());
			string[] array4 = array3[1].Split(",".ToCharArray());
			for (int l = 0; l < array4.Length; l++)
			{
				if (!string.IsNullOrEmpty(array4[l]))
				{
					ResourceManager.m_modelFxs.get_Item(array3[0]).Add(int.Parse(array4[l]));
				}
			}
		}
	}

	public static HashSet<int> GetModelOfEffects(int modelID)
	{
		ResourceManager.InitModelData();
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
		if (avatarModel == null)
		{
			string text = "AvatarModel表找不到数据, id = " + modelID;
			UIManagerControl.Instance.ShowToastText(text);
			return null;
		}
		if (ResourceManager.m_modelEffects.ContainsKey(avatarModel.action))
		{
			return ResourceManager.m_modelEffects.get_Item(avatarModel.action);
		}
		string text2 = string.Concat(new object[]
		{
			"Tools/PreprocessBeforeBuild, model数据索引中没有找到效应key, key = ",
			avatarModel.action,
			", modelId = ",
			modelID
		});
		UIManagerControl.Instance.ShowToastText(text2);
		return null;
	}

	public static HashSet<int> GetModelOfFXs(int modelID)
	{
		ResourceManager.InitModelData();
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelID);
		if (avatarModel == null)
		{
			string text = "AvatarModel表找不到数据, id = " + modelID;
			UIManagerControl.Instance.ShowToastText(text);
			return null;
		}
		if (ResourceManager.m_modelFxs.ContainsKey(avatarModel.action))
		{
			return ResourceManager.m_modelFxs.get_Item(avatarModel.action);
		}
		string text2 = string.Concat(new object[]
		{
			"Tools/PreprocessBeforeBuild, model数据索引中没有找到特效key, key = ",
			avatarModel.action,
			", modelId = ",
			modelID
		});
		UIManagerControl.Instance.ShowToastText(text2);
		return null;
	}

	public static void TestUINull(Object asset, string from)
	{
		if (!SystemConfig.IsDebugInfoOn)
		{
			return;
		}
		if (asset == null)
		{
			return;
		}
		if (asset.get_name() == "ChangePetChooseUI")
		{
			GameObject gameObject = asset as GameObject;
			if (gameObject != null)
			{
				Transform transform = gameObject.get_transform().FindChild("UIs/PetsBar/Pet1/NoPet/ImagePetBG");
				if (transform != null)
				{
					Image component = transform.GetComponent<Image>();
					if (component != null)
					{
						if (component.get_sprite() == null)
						{
							Debug.LogError("!!!!!!!!![011]===========>sprite is null, from = " + from);
						}
						else
						{
							Debug.LogError("!!!!!!!!![012]sprite is normal, from = " + from);
						}
					}
					else
					{
						Debug.LogError("!!!!!!!!![014]image is normal, from = " + from);
					}
				}
			}
			else
			{
				Debug.LogError("!!!!!!!!![013]image is null, from = " + from);
			}
			GameObject gameObject2 = Object.Instantiate(asset) as GameObject;
			if (gameObject2 != null)
			{
				Transform transform2 = gameObject2.get_transform().FindChild("UIs/PetsBar/Pet1/NoPet/ImagePetBG");
				if (transform2 != null)
				{
					Image component2 = transform2.GetComponent<Image>();
					if (component2 != null)
					{
						if (component2.get_sprite() == null)
						{
							Debug.LogError("!!!!!!!!![021]==============>sprite is null, from = " + from);
						}
						else
						{
							Debug.LogError("!!!!!!!!![022]sprite is normal, from = " + from);
						}
					}
				}
				Object.Destroy(gameObject2);
			}
		}
	}

	public static void SetImageToStencil(ref Image image, int index = 0)
	{
		if (image == null)
		{
			return;
		}
		UIImageRef component = image.GetComponent<UIImageRef>();
		if (component != null)
		{
			component.InitSelf();
		}
		Texture texture = image.get_material().GetTexture(UIConst.TEXTURE_MAIN);
		Texture texture2 = image.get_material().GetTexture(UIConst.TEXTURE_A);
		if (index == 0)
		{
			image.set_material(ResourceManager.Instance.MatStencilAlphaBlendedETC01);
		}
		else if (index == 1)
		{
			image.set_material(ResourceManager.Instance.MatStencilAlphaBlendedETC02);
		}
		else
		{
			image.set_material(ResourceManager.Instance.MatStencilAlphaBlendedETC03);
		}
		image.get_material().SetTexture(UIConst.TEXTURE_MAIN, texture);
		image.get_material().SetTexture(UIConst.TEXTURE_A, texture2);
	}

	public static void SetTextToStencil(ref Text text)
	{
		if (text == null)
		{
			return;
		}
		text.set_material(ResourceManager.Instance.MatStencilFont);
	}
}
