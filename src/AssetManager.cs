using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine;
using XEngine.AssetLoader;

public class AssetManager : AssetManagerBase
{
	public class AssetOfControllerManager
	{
		protected static Dictionary<string, RuntimeAnimatorController> mapAssets = new Dictionary<string, RuntimeAnimatorController>();

		public static void SetController(Animator animator, int modelId, bool inBattle)
		{
			if (animator == null)
			{
				return;
			}
			if (animator.get_runtimeAnimatorController() != null && !AssetManager.AssetOfControllerManager.IsControllerHasCity(modelId))
			{
				return;
			}
			RuntimeAnimatorController controller = AssetManager.AssetOfControllerManager.GetController(AssetManager.AssetOfControllerManager.GetControllerName(modelId, inBattle));
			animator.set_runtimeAnimatorController(controller);
		}

		public static void Clear()
		{
		}

		private static RuntimeAnimatorController GetController(string name)
		{
			if (AssetManager.AssetOfControllerManager.mapAssets.ContainsKey(name) && AssetManager.AssetOfControllerManager.mapAssets.get_Item(name) != null)
			{
				return AssetManager.AssetOfControllerManager.mapAssets.get_Item(name);
			}
			AssetManager.AssetOfControllerManager.mapAssets.set_Item(name, null);
			Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfController(name), typeof(RuntimeAnimatorController));
			if (@object != null)
			{
				AssetManager.AssetOfControllerManager.mapAssets.set_Item(name, @object as RuntimeAnimatorController);
			}
			return AssetManager.AssetOfControllerManager.mapAssets.get_Item(name);
		}

		private static string GetControllerName(int modelId, bool inBattle)
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
			string action = avatarModel.action;
			string text = action;
			if (!inBattle)
			{
				text = action + "_city";
			}
			if (!string.IsNullOrEmpty(FileSystem.GetPathOfController(text)))
			{
				return text;
			}
			return action;
		}

		private static bool IsControllerHasCity(int modelId)
		{
			if (modelId <= 0)
			{
				return false;
			}
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
			return !string.IsNullOrEmpty(avatarModel.action) && !string.IsNullOrEmpty(FileSystem.GetPathOfController(avatarModel.action + "_city"));
		}
	}

	public class AssetOfNoPool
	{
		public static void LoadAssetNoAB(string path, Type type, Action<Object> finish_callback)
		{
			if (string.IsNullOrEmpty(path))
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(null);
				}
				return;
			}
			AssetLoader.LoadAsset(path, type, delegate(Object obj)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(obj);
				}
				AssetLoader.UnloadAsset(path, null);
			});
		}

		public static void LoadAsset(string path, Type type, Action<Object> finish_callback)
		{
			if (string.IsNullOrEmpty(path))
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(null);
				}
				return;
			}
			AssetLoader.LoadAsset(path, type, delegate(Object obj)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(obj);
				}
			});
		}

		public static Object LoadAssetNowNoAB(string path, Type type)
		{
			Object result = AssetManager.AssetOfNoPool.LoadAssetNow(path, type);
			AssetLoader.UnloadAsset(path, null);
			return result;
		}

		public static Object LoadAssetNow(string path, Type type)
		{
			return AssetLoader.LoadAssetNow(path, type);
		}
	}

	public class AssetOfSpineManager
	{
		protected static Dictionary<string, Object> mapAssets = new Dictionary<string, Object>();

		public static void LoadAssetWithPool(string path, Action<bool> finish_callback)
		{
			if (string.IsNullOrEmpty(path))
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(false);
				}
				return;
			}
			if (AssetManager.AssetOfSpineManager.mapAssets.ContainsKey(path) && AssetManager.AssetOfSpineManager.mapAssets.get_Item(path) != null)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke(true);
				}
				return;
			}
			AssetManager.AssetOfNoPool.LoadAssetNoAB(path, typeof(Object), delegate(Object obj)
			{
				if (obj != null)
				{
					if (!AssetManager.AssetOfSpineManager.mapAssets.ContainsKey(path))
					{
						AssetManager.AssetOfSpineManager.mapAssets.set_Item(path, obj);
					}
					if (finish_callback != null)
					{
						finish_callback.Invoke(true);
					}
				}
				else if (finish_callback != null)
				{
					finish_callback.Invoke(false);
				}
			});
		}

		public static Object GetAssetWithPool(string path)
		{
			if (AssetManager.AssetOfSpineManager.mapAssets.ContainsKey(path))
			{
				return AssetManager.AssetOfSpineManager.mapAssets.get_Item(path);
			}
			Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(path, typeof(GameObject));
			AssetManager.AssetOfSpineManager.mapAssets.set_Item(path, @object);
			return @object;
		}

		public static void Clear()
		{
			Dictionary<string, Object> dictionary = new Dictionary<string, Object>();
			using (Dictionary<string, Object>.Enumerator enumerator = AssetManager.AssetOfSpineManager.mapAssets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, Object> current = enumerator.get_Current();
					if (ResourceRegulation.is_inwhite_spine(current.get_Key()))
					{
						dictionary.set_Item(current.get_Key(), current.get_Value());
					}
				}
			}
			AssetManager.AssetOfSpineManager.mapAssets.Clear();
			AssetManager.AssetOfSpineManager.mapAssets = dictionary;
		}
	}

	public class AssetOfTPManager
	{
		private static AssetManager.AssetOfTPManager instance;

		private static Dictionary<string, Dictionary<string, SpriteRenderer>> m_atlas_uisprites = new Dictionary<string, Dictionary<string, SpriteRenderer>>();

		private static List<string> delete_list = new List<string>();

		private static Dictionary<string, int> m_reference_atlas = new Dictionary<string, int>();

		public static AssetManager.AssetOfTPManager Instance
		{
			get
			{
				if (AssetManager.AssetOfTPManager.instance == null)
				{
					AssetManager.AssetOfTPManager.instance = new AssetManager.AssetOfTPManager();
				}
				return AssetManager.AssetOfTPManager.instance;
			}
		}

		public void Init()
		{
		}

		public static void LoadAtlas(string atlas_no_suffix, Action<bool> finish_callback)
		{
			TPAtlasLoader.LoadAtlasToSprites(atlas_no_suffix, delegate(Dictionary<string, SpriteRenderer> obj)
			{
				if (obj == null)
				{
					if (finish_callback != null)
					{
						finish_callback.Invoke(false);
					}
				}
				else
				{
					AssetManager.AssetOfTPManager.AddToUiSprites(atlas_no_suffix, obj);
					if (finish_callback != null)
					{
						finish_callback.Invoke(true);
					}
				}
			});
		}

		public static void LoadAtlasNow(string atlas_no_suffix)
		{
			Dictionary<string, SpriteRenderer> sprites = TPAtlasLoader.LoadAtlasNow(atlas_no_suffix);
			AssetManager.AssetOfTPManager.AddToUiSprites(atlas_no_suffix, sprites);
		}

		public static void LoadAtlas(string[] atlas_no_suffix_list, Action finish_callback, int index = 0)
		{
			if (atlas_no_suffix_list == null || atlas_no_suffix_list.Length == 0 || index >= atlas_no_suffix_list.Length)
			{
				if (finish_callback != null)
				{
					finish_callback.Invoke();
				}
				return;
			}
			if (TPAtlasLoader.IsInPool(atlas_no_suffix_list[index]) || string.IsNullOrEmpty(atlas_no_suffix_list[index]))
			{
				AssetManager.AssetOfTPManager.LoadAtlas(atlas_no_suffix_list, finish_callback, ++index);
			}
			else
			{
				TimerHeap.AddTimer(0u, 0, delegate
				{
					AssetManager.AssetOfTPManager.LoadAtlas(atlas_no_suffix_list[index], delegate(bool isSuccess)
					{
						AssetManager.AssetOfTPManager.LoadAtlas(atlas_no_suffix_list, finish_callback, ++index);
					});
				});
			}
		}

		public static SpriteRenderer GetSpriteRenderer(string sprite_name)
		{
			if (string.IsNullOrEmpty(sprite_name))
			{
				return null;
			}
			sprite_name = GameDataUtils.SplitString4Dot0(sprite_name);
			SpriteRenderer spriteRendererIfExist = AssetManager.AssetOfTPManager.GetSpriteRendererIfExist(sprite_name);
			if (spriteRendererIfExist != null)
			{
				return spriteRendererIfExist;
			}
			if (FileSystem.HasKey(sprite_name))
			{
				string path = FileSystem.GetPath(sprite_name, string.Empty);
				AssetManager.AssetOfTPManager.LoadAtlasNow(path);
				spriteRendererIfExist = AssetManager.AssetOfTPManager.GetSpriteRendererIfExist(sprite_name);
				if (spriteRendererIfExist != null)
				{
					return spriteRendererIfExist;
				}
				spriteRendererIfExist = AssetManager.AssetOfTPManager.GetSpriteRendererIfExist("Empty");
				if (spriteRendererIfExist != null)
				{
					return spriteRendererIfExist;
				}
				return null;
			}
			else
			{
				spriteRendererIfExist = AssetManager.AssetOfTPManager.GetSpriteRendererIfExist("Empty");
				if (spriteRendererIfExist != null)
				{
					return spriteRendererIfExist;
				}
				return null;
			}
		}

		private static SpriteRenderer GetSpriteRendererIfExist(string sprite_name)
		{
			string text = FileSystem.GetPath(sprite_name, string.Empty);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			text = text.ToLower();
			if (!AssetManager.AssetOfTPManager.m_atlas_uisprites.ContainsKey(text))
			{
				return null;
			}
			SpriteRenderer result;
			if (!AssetManager.AssetOfTPManager.m_atlas_uisprites.get_Item(text).TryGetValue(sprite_name, ref result))
			{
				return null;
			}
			return result;
		}

		public static void ReleaseAll()
		{
			using (Dictionary<string, Dictionary<string, SpriteRenderer>>.KeyCollection.Enumerator enumerator = AssetManager.AssetOfTPManager.m_atlas_uisprites.get_Keys().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.get_Current();
					AssetManager.MinusAssetRef(FileSystem.GetPath(current + "_pb", string.Empty));
				}
			}
			AssetManager.AssetOfTPManager.m_atlas_uisprites.Clear();
			AssetManager.AssetOfTPManager.m_reference_atlas.Clear();
		}

		public static void ReleaseNoRef()
		{
			AssetManager.AssetOfTPManager.delete_list.Clear();
			using (Dictionary<string, int>.Enumerator enumerator = AssetManager.AssetOfTPManager.m_reference_atlas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, int> current = enumerator.get_Current();
					if (current.get_Value() <= 0)
					{
						if (!ResourceRegulation.is_inwhite_uiatlas(current.get_Key()))
						{
							AssetManager.AssetOfTPManager.delete_list.Add(current.get_Key());
						}
					}
				}
			}
			for (int i = 0; i < AssetManager.AssetOfTPManager.delete_list.get_Count(); i++)
			{
				string text = AssetManager.AssetOfTPManager.delete_list.get_Item(i);
				AssetManager.AssetOfTPManager.ReleaseAtlas(text.Substring(0, text.get_Length() - "_atlas".get_Length()));
			}
		}

		private static void ReleaseAtlas(string src)
		{
			src = src.ToLower();
			AssetManager.AssetOfTPManager.m_reference_atlas.Remove(ConstTP.src_To_suffix_atlas(src));
			if (AssetManager.AssetOfTPManager.m_atlas_uisprites.ContainsKey(src))
			{
				AssetManager.AssetOfTPManager.m_atlas_uisprites.set_Item(src, null);
				AssetManager.AssetOfTPManager.m_atlas_uisprites.Remove(src);
			}
			AssetManager.MinusAssetRef(FileSystem.GetPath(src + "_pb", string.Empty));
		}

		private static void InitReference(string src_with_suffix_atlas)
		{
			if (ResourceRegulation.is_inwhite_uiatlas(src_with_suffix_atlas))
			{
				return;
			}
			if (!AssetManager.AssetOfTPManager.m_reference_atlas.ContainsKey(src_with_suffix_atlas))
			{
				AssetManager.AssetOfTPManager.m_reference_atlas.set_Item(src_with_suffix_atlas, 0);
			}
		}

		public static void AddReferenceCount(Image image)
		{
			if (image != null && image.get_sprite() != null && image.get_sprite().get_texture() != null)
			{
				AssetManager.AssetOfTPManager.AddReferenceCount(image.get_sprite().get_texture().get_name());
			}
		}

		public static void AddReferenceCount(SpriteRenderer spr)
		{
			if (spr != null && spr.get_sprite() != null && spr.get_sprite().get_texture() != null)
			{
				AssetManager.AssetOfTPManager.AddReferenceCount(spr.get_sprite().get_texture().get_name());
			}
		}

		private static void AddReferenceCount(string src_with_suffix_atlas)
		{
			src_with_suffix_atlas = src_with_suffix_atlas.ToLower();
			if (ResourceRegulation.is_inwhite_uiatlas(src_with_suffix_atlas))
			{
				return;
			}
			if (AssetManager.AssetOfTPManager.m_reference_atlas.ContainsKey(src_with_suffix_atlas))
			{
				AssetManager.AssetOfTPManager.m_reference_atlas.set_Item(src_with_suffix_atlas, AssetManager.AssetOfTPManager.m_reference_atlas.get_Item(src_with_suffix_atlas) + 1);
			}
			else
			{
				AssetManager.AssetOfTPManager.m_reference_atlas.set_Item(src_with_suffix_atlas, 1);
			}
		}

		public static void MinusReferenceCount(Image image)
		{
			if (image != null && image.get_sprite() != null && image.get_sprite().get_texture() != null)
			{
				AssetManager.AssetOfTPManager.MinusReferenceCount(image.get_sprite().get_texture().get_name());
			}
		}

		private static void MinusReferenceCount(string src_with_suffix_atlas)
		{
			src_with_suffix_atlas = src_with_suffix_atlas.ToLower();
			if (ResourceRegulation.is_inwhite_uiatlas(src_with_suffix_atlas))
			{
				return;
			}
			if (AssetManager.AssetOfTPManager.m_reference_atlas.ContainsKey(src_with_suffix_atlas))
			{
				AssetManager.AssetOfTPManager.m_reference_atlas.set_Item(src_with_suffix_atlas, AssetManager.AssetOfTPManager.m_reference_atlas.get_Item(src_with_suffix_atlas) - 1);
			}
			else
			{
				AssetManager.AssetOfTPManager.m_reference_atlas.set_Item(src_with_suffix_atlas, 0);
			}
		}

		private static void AddToUiSprites(string src, Dictionary<string, SpriteRenderer> sprites)
		{
			if (sprites == null)
			{
				return;
			}
			src = src.ToLower();
			if (!AssetManager.AssetOfTPManager.m_atlas_uisprites.ContainsKey(src))
			{
				AssetManager.AssetOfTPManager.m_atlas_uisprites.set_Item(src, sprites);
				AssetManager.AssetOfTPManager.InitReference(ConstTP.src_To_suffix_atlas(src));
				AssetManager.AddAssetRef(ConstTP.src_To_suffix_prefab(src));
			}
		}

		public static void PrintMessage()
		{
			Debug.LogError("m_atlas_uisprites: count = " + AssetManager.AssetOfTPManager.m_atlas_uisprites.get_Count());
			using (Dictionary<string, Dictionary<string, SpriteRenderer>>.Enumerator enumerator = AssetManager.AssetOfTPManager.m_atlas_uisprites.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, Dictionary<string, SpriteRenderer>> current = enumerator.get_Current();
					Debug.LogError(string.Concat(new object[]
					{
						"========>m_atlas_uisprites key = ",
						current.get_Key(),
						", value = ",
						current.get_Value().get_Count()
					}));
				}
			}
		}

		public static void PrintMessageRef()
		{
			using (Dictionary<string, int>.Enumerator enumerator = AssetManager.AssetOfTPManager.m_reference_atlas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, int> current = enumerator.get_Current();
					Debug.LogError(string.Concat(new object[]
					{
						"========>reference key = ",
						current.get_Key(),
						", value = ",
						current.get_Value()
					}));
				}
			}
		}
	}

	private static List<string> mEquipCustomizationAsset = new List<string>();

	private static List<string> delete_list = new List<string>();

	public static Object LoadAssetNowWithPool(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		if (AssetManager.IsInPool(path))
		{
			return AssetManagerBase.mapAssets.get_Item(path);
		}
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNow(path, typeof(Object));
		AssetManagerBase.mapAssets.set_Item(path, @object);
		AssetManager.AddAssetBundleRef(path);
		AssetManager.InitAssetReference(path);
		return AssetManagerBase.mapAssets.get_Item(path);
	}

	public static void LoadAssetWithPool(string path, Action<bool> finish_callback)
	{
		if (string.IsNullOrEmpty(path))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(false);
			}
			return;
		}
		if (AssetManager.IsInPool(path))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
			return;
		}
		AssetLoader.LoadAsset(path, typeof(GameObject), delegate(Object obj)
		{
			AssetManagerBase.mapAssets.set_Item(path, obj);
			AssetManager.AddAssetBundleRef(path);
			AssetManager.InitAssetReference(path);
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
		});
	}

	public static Object LoadAssetNowWithPoolNoAB(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		if (AssetManagerBase.mapAssets.ContainsKey(path) && AssetManagerBase.mapAssets.get_Item(path) != null)
		{
			return AssetManagerBase.mapAssets.get_Item(path);
		}
		Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(path, typeof(Object));
		AssetManagerBase.mapAssets.set_Item(path, @object);
		AssetManager.InitAssetReference(path);
		return AssetManagerBase.mapAssets.get_Item(path);
	}

	public static void LoadAssetWithPoolNoAB(string path, Action<bool> finish_callback)
	{
		if (string.IsNullOrEmpty(path))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(false);
			}
			return;
		}
		if (AssetManagerBase.mapAssets.ContainsKey(path) && AssetManagerBase.mapAssets.get_Item(path) != null)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
			return;
		}
		AssetManager.AssetOfNoPool.LoadAssetNoAB(path, typeof(GameObject), delegate(Object obj)
		{
			AssetManagerBase.mapAssets.set_Item(path, obj);
			AssetManager.InitAssetReference(path);
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
		});
	}

	public static bool IsInPool(string path)
	{
		return AssetManagerBase.mapAssets.ContainsKey(path) && AssetManagerBase.mapAssets.get_Item(path) != null;
	}

	public static void LoadAssetOfUI(string name, Action<bool> finish_callback)
	{
		AssetManager.LoadAssetWithPoolNoAB(FileSystem.GetPathOfPrefab(name), finish_callback);
	}

	public static Object LoadAssetNowOfUI(string name)
	{
		return AssetManager.LoadAssetNowWithPoolNoAB(FileSystem.GetPathOfPrefab(name));
	}

	public static Texture GetTexture(string name)
	{
		return AssetManager.LoadAssetNowWithPoolNoAB(FileSystem.GetPath(name, string.Empty)) as Texture;
	}

	public static void LoadEquipCustomizationAsset(string path, AssetCallback callback)
	{
		AssetManager.LoadAssetWithPool(path, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				if (!AssetManager.mEquipCustomizationAsset.Contains(path))
				{
					AssetManager.mEquipCustomizationAsset.Add(path);
				}
				if (callback != null)
				{
					callback(AssetManager.LoadAssetNowWithPool(path));
				}
			}
		});
	}

	public static void ClearEquipCustomizationAssets()
	{
		for (int i = 0; i < AssetManager.mEquipCustomizationAsset.get_Count(); i++)
		{
			AssetManager.ReleaseAssetWithAB(AssetManager.mEquipCustomizationAsset.get_Item(i));
		}
		AssetManager.mEquipCustomizationAsset.Clear();
	}

	public static void ReleaseAll()
	{
		AssetManager.delete_list.Clear();
		using (Dictionary<string, Object>.KeyCollection.Enumerator enumerator = AssetManagerBase.mapAssets.get_Keys().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				AssetManager.delete_list.Add(current);
			}
		}
		for (int i = 0; i < AssetManager.delete_list.get_Count(); i++)
		{
			AssetManager.ReleaseAssetWithAB(AssetManager.delete_list.get_Item(i));
		}
		AssetManagerBase.mapAssets.Clear();
		AssetManagerBase.mapAssetsRef.Clear();
		AssetManagerBase.mapAssetsToAssetBundleRef.Clear();
	}

	public static void ReleaseNoRef(bool unload_ab = true)
	{
		AssetManager.delete_list.Clear();
		using (Dictionary<string, int>.Enumerator enumerator = AssetManagerBase.mapAssetsRef.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, int> current = enumerator.get_Current();
				if (current.get_Value() <= 0)
				{
					AssetManager.delete_list.Add(current.get_Key());
				}
			}
		}
		if (unload_ab)
		{
			for (int i = 0; i < AssetManager.delete_list.get_Count(); i++)
			{
				AssetManager.ReleaseAssetWithAB(AssetManager.delete_list.get_Item(i));
			}
		}
		else
		{
			for (int j = 0; j < AssetManager.delete_list.get_Count(); j++)
			{
				AssetManager.ReleaseAsset(AssetManager.delete_list.get_Item(j));
			}
		}
	}

	private static void ReleaseAsset(string path)
	{
		if (!ResourceRegulation.is_inwhite_common(path))
		{
			AssetManagerBase.mapAssets.Remove(path);
			AssetManagerBase.mapAssetsRef.set_Item(path, 0);
		}
	}

	private static void ReleaseAssetWithAB(string path)
	{
		if (!ResourceRegulation.is_inwhite_common(path))
		{
			if (AssetManagerBase.mapAssets.ContainsKey(path))
			{
				AssetManagerBase.mapAssets.Remove(path);
			}
			if (AssetManagerBase.mapAssetsRef.ContainsKey(path))
			{
				AssetManagerBase.mapAssetsRef.Remove(path);
			}
		}
		if (!AssetManagerBase.mapAssetsToAssetBundleRef.ContainsKey(path))
		{
			return;
		}
		int num = AssetManagerBase.mapAssetsToAssetBundleRef.get_Item(path);
		AssetManagerBase.mapAssetsToAssetBundleRef.Remove(path);
		for (int i = 0; i < num; i++)
		{
			AssetLoader.UnloadAsset(path, null);
		}
	}

	private static void InitAssetReference(string path)
	{
		if (!AssetManagerBase.mapAssetsRef.ContainsKey(path))
		{
			AssetManagerBase.mapAssetsRef.set_Item(path, 0);
		}
	}

	public static void AddAssetRef(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		if (AssetManagerBase.mapAssetsRef.ContainsKey(path))
		{
			AssetManagerBase.mapAssetsRef.set_Item(path, AssetManagerBase.mapAssetsRef.get_Item(path) + 1);
		}
		else
		{
			AssetManagerBase.mapAssetsRef.set_Item(path, 1);
		}
	}

	public static void MinusAssetRef(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		if (AssetManagerBase.mapAssetsRef.ContainsKey(path))
		{
			AssetManagerBase.mapAssetsRef.set_Item(path, AssetManagerBase.mapAssetsRef.get_Item(path) - 1);
		}
	}

	public static void AddAssetBundleRef(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		if (AssetManagerBase.mapAssetsToAssetBundleRef.ContainsKey(path))
		{
			AssetManagerBase.mapAssetsToAssetBundleRef.set_Item(path, AssetManagerBase.mapAssetsToAssetBundleRef.get_Item(path) + 1);
		}
		else
		{
			AssetManagerBase.mapAssetsToAssetBundleRef.set_Item(path, 1);
		}
	}

	public static void PrintMessage(string contain_sub = "")
	{
		using (Dictionary<string, Object>.KeyCollection.Enumerator enumerator = AssetManagerBase.mapAssets.get_Keys().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				if (string.IsNullOrEmpty(contain_sub))
				{
					Debug.LogError("=>mapAssets, Name = " + current);
				}
				else if (current.ToLower().Contains(contain_sub.ToLower()))
				{
					Debug.LogError("=>mapAssets, Name = " + current);
				}
			}
		}
	}

	public static void PrintMessageRef(string contain_sub = "")
	{
		using (Dictionary<string, int>.Enumerator enumerator = AssetManagerBase.mapAssetsRef.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, int> current = enumerator.get_Current();
				if (string.IsNullOrEmpty(contain_sub))
				{
					Debug.LogError(string.Concat(new object[]
					{
						"=>mapAssetsRef, key = ",
						current.get_Key(),
						", value = ",
						current.get_Value()
					}));
				}
				else if (current.get_Key().ToLower().Contains(contain_sub.ToLower()))
				{
					Debug.LogError(string.Concat(new object[]
					{
						"=>mapAssetsRef, key = ",
						current.get_Key(),
						", value = ",
						current.get_Value()
					}));
				}
			}
		}
		using (Dictionary<string, int>.Enumerator enumerator2 = AssetManagerBase.mapAssetsToAssetBundleRef.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<string, int> current2 = enumerator2.get_Current();
				Debug.LogError(string.Concat(new object[]
				{
					"=>mapAssetsToAssetBundleRef, key = ",
					current2.get_Key(),
					", value = ",
					current2.get_Value()
				}));
			}
		}
	}
}
