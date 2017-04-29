using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetManagerBase : BaseSubSystemManager
{
	private List<int> formation_lvs;

	public int GetSelfPetModel(int petId)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet.model == null || pet.model.get_Count() == 0)
		{
			return 0;
		}
		return this.GetSelfPetModel(pet);
	}

	public int GetSelfPetModel(Pet dataPet)
	{
		if (dataPet.model == null || dataPet.model.get_Count() == 0)
		{
			return 0;
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById((int)dataPet.id);
		if (petInfoById != null)
		{
			return PetManagerBase.GetPlayerPetModel(dataPet, petInfoById.star);
		}
		return dataPet.model.get_Item(0);
	}

	public List<int> GetSelfPetSkills(Pet dataPet)
	{
		if (dataPet.model == null || dataPet.model.get_Count() == 0)
		{
			return new List<int>();
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById((int)dataPet.id);
		if (petInfoById != null)
		{
			return PetManagerBase.GetPlayerPetSkills(dataPet, petInfoById.star);
		}
		return new List<int>();
	}

	public static int GetPlayerPetModel(int petId, int star)
	{
		Pet dataPet = DataReader<Pet>.Get(petId);
		return PetManagerBase.GetPlayerPetModel(dataPet, star);
	}

	public static int GetPlayerPetModel(Pet dataPet, int star)
	{
		if (dataPet.model == null || dataPet.model.get_Count() == 0)
		{
			return 0;
		}
		if (star - 1 >= 0 && star - 1 < dataPet.model.get_Count())
		{
			return dataPet.model.get_Item(star - 1);
		}
		return dataPet.model.get_Item(0);
	}

	public static List<int> GetPlayerPetSkills(Pet dataPet, int star)
	{
		List<int> list = new List<int>();
		if (dataPet.model == null || dataPet.talent.get_Count() == 0)
		{
			return new List<int>();
		}
		for (int i = 0; i < dataPet.talent.get_Count(); i++)
		{
			if (star >= dataPet.talentStart.get_Item(i))
			{
				list.AddRange(DataReader<ChongWuTianFu>.Get(dataPet.talent.get_Item(i)).parameter);
			}
		}
		return list;
	}

	public SpriteRenderer GetSelfPic(Pet dataPet)
	{
		if (dataPet.pic == null || dataPet.pic.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById((int)dataPet.id);
		if (petInfoById != null)
		{
			return GameDataUtils.GetIcon(this.GetPic(dataPet, petInfoById.star));
		}
		return GameDataUtils.GetIcon(dataPet.pic.get_Item(0));
	}

	private int GetPic(Pet dataPet, int star)
	{
		if (star - 1 >= 0 && star - 1 < dataPet.pic.get_Count())
		{
			return dataPet.pic.get_Item(star - 1);
		}
		return dataPet.pic.get_Item(0);
	}

	public SpriteRenderer GetSelfPetIcon(int petId)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet.icon == null || pet.icon.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		return this.GetSelfPetIcon(pet);
	}

	public SpriteRenderer GetSelfPetIcon(Pet dataPet)
	{
		if (dataPet.icon == null || dataPet.icon.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById((int)dataPet.id);
		if (petInfoById != null)
		{
			return PetManagerBase.GetPlayerPetIcon(dataPet, petInfoById.star);
		}
		return GameDataUtils.GetIcon(dataPet.icon.get_Item(0));
	}

	public static SpriteRenderer GetPlayerPetIcon(int petId, int star)
	{
		Pet dataPet = DataReader<Pet>.Get(petId);
		return PetManagerBase.GetPlayerPetIcon(dataPet, star);
	}

	public static SpriteRenderer GetPlayerPetIcon(Pet dataPet, int star)
	{
		if (dataPet.icon == null || dataPet.icon.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		if (star - 1 >= 0 && star - 1 < dataPet.icon.get_Count())
		{
			return GameDataUtils.GetIcon(dataPet.icon.get_Item(star - 1));
		}
		return GameDataUtils.GetIcon(dataPet.icon.get_Item(0));
	}

	public SpriteRenderer GetSelfPetIcon2(int petId)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet.icon2 == null || pet.icon2.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		return this.GetSelfPetIcon2(pet);
	}

	public SpriteRenderer GetSelfPetIcon2(Pet dataPet)
	{
		if (dataPet.icon2 == null || dataPet.icon2.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		PetInfo petInfoById = PetManager.Instance.GetPetInfoById((int)dataPet.id);
		if (petInfoById != null)
		{
			return PetManagerBase.GetPlayerPetIcon2(dataPet, petInfoById.star);
		}
		return GameDataUtils.GetIcon(dataPet.icon2.get_Item(0));
	}

	public static SpriteRenderer GetPlayerPetIcon2(int petId, int star)
	{
		Pet dataPet = DataReader<Pet>.Get(petId);
		return PetManagerBase.GetPlayerPetIcon2(dataPet, star);
	}

	public static SpriteRenderer GetPlayerPetIcon2(Pet dataPet, int star)
	{
		if (dataPet == null || dataPet.icon2 == null || dataPet.icon2.get_Count() == 0)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		if (star - 1 >= 0 && star - 1 < dataPet.icon2.get_Count())
		{
			return GameDataUtils.GetIcon(dataPet.icon2.get_Item(star - 1));
		}
		return GameDataUtils.GetIcon(dataPet.icon2.get_Item(0));
	}

	public static bool IsPetLimit(long uuid)
	{
		PetInfo petInfo = PetManager.Instance.GetPetInfo(uuid);
		return petInfo != null && PetManagerBase.IsPetLimit(petInfo.petId);
	}

	public static bool IsPetLimit(int petId)
	{
		Pet dataPet = DataReader<Pet>.Get(petId);
		return PetManagerBase.IsPetLimit(dataPet);
	}

	public static bool IsPetLimit(Pet dataPet)
	{
		return dataPet == null || (InstanceManagerUI.InstanceID > 0 && (DataReader<FuBenJiChuPeiZhi>.Get(InstanceManagerUI.InstanceID) == null || InstanceManagerUI.GetInstanceLimitPetType() == dataPet.petType));
	}

	public static string GetPetTypeName(int type)
	{
		if (type > 0)
		{
			return GameDataUtils.GetChineseContent(500900 + type, false);
		}
		return string.Empty;
	}

	public List<int> GetFormationLvs()
	{
		if (this.formation_lvs == null)
		{
			GlobalParams globalParams = DataReader<GlobalParams>.Get("formationOpenLv");
			string[] array = globalParams.value.Split(new char[]
			{
				';'
			});
			this.formation_lvs = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				this.formation_lvs.Add(int.Parse(array[i]));
			}
		}
		return this.formation_lvs;
	}

	public static string GetBackground(int type)
	{
		return "bg_pet" + type;
	}

	public static string GetBackgroundById(int petId)
	{
		Pet pet = DataReader<Pet>.Get(petId);
		if (pet != null)
		{
			return PetManagerBase.GetBackground(pet.petType);
		}
		return string.Empty;
	}

	public static SpriteRenderer GetPetType(Pet dataPet)
	{
		return GameDataUtils.GetIcon(dataPet.petType + 350);
	}

	public static SpriteRenderer GetPetZi(Pet dataPet)
	{
		return GameDataUtils.GetIcon(dataPet.petType + 360);
	}

	public static SpriteRenderer GetPetLimitType(int type)
	{
		return GameDataUtils.GetIcon(type + 370);
	}

	public static SpriteRenderer GetPetLimitType(Pet dataPet)
	{
		return GameDataUtils.GetIcon(dataPet.petType + 370);
	}

	public static float GetCityPetModelZoom(Pet dataPet, int modelId)
	{
		int i = 0;
		while (i < dataPet.model.get_Count())
		{
			if (dataPet.model.get_Item(i) == modelId)
			{
				if (i < dataPet.zoom.get_Count())
				{
					return dataPet.zoom.get_Item(i);
				}
				return 1f;
			}
			else
			{
				i++;
			}
		}
		return 1f;
	}
}
