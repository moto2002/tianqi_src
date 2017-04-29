using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCareerInstanceManager
{
	public int dst_profession;

	public bool IsWinWithChange;

	public bool IsQuit;

	private static ChangeCareerInstanceManager instance;

	public static ChangeCareerInstanceManager Instance
	{
		get
		{
			if (ChangeCareerInstanceManager.instance == null)
			{
				ChangeCareerInstanceManager.instance = new ChangeCareerInstanceManager();
			}
			return ChangeCareerInstanceManager.instance;
		}
	}

	protected ChangeCareerInstanceManager()
	{
		this.AddInstanceListeners();
	}

	public void AddInstanceListeners()
	{
		EventDispatcher.AddListener<bool>(LocalInstanceEvent.LocalInstanceFinish, new Callback<bool>(this.ChangeCareerInstanceFinish));
	}

	public void RemoveInstanceListeners()
	{
		EventDispatcher.RemoveListener<bool>(LocalInstanceEvent.LocalInstanceFinish, new Callback<bool>(this.ChangeCareerInstanceFinish));
	}

	public void EnterChangeCareerInstance(int profession)
	{
		this.IsWinWithChange = false;
		this.IsQuit = false;
		this.dst_profession = profession;
		RoleCreate roleCreate = DataReader<RoleCreate>.Get(profession);
		if (roleCreate == null)
		{
			return;
		}
		FuBenJiChuPeiZhi fuBenJiChuPeiZhi = DataReader<FuBenJiChuPeiZhi>.Get(ChangeCareerInstance.ChangeCareerInstanceDataID);
		if (fuBenJiChuPeiZhi == null)
		{
			return;
		}
		ChangeCareerManager.Instance.SendInChallengeNty();
		InstanceManager.ChangeInstanceManager(ChangeCareerInstance.Instance, false);
		InstanceManager.SimulateEnterField(10, null);
		InstanceManager.SimulateSwicthMap(ChangeCareerInstance.Instance.InstanceData.scene, LocalInstanceHandler.Instance.CreateSelfInfo(fuBenJiChuPeiZhi.type, fuBenJiChuPeiZhi.id, fuBenJiChuPeiZhi.scene, profession, roleCreate.modle, this.GetDecorations(profession), null, this.GetBattleSkillInfos(profession)), null, 0);
	}

	protected void ChangeCareerInstanceFinish(bool isWin)
	{
		if (InstanceManager.CurrentInstanceType != ChangeCareerInstance.Instance.Type)
		{
			return;
		}
		if (isWin)
		{
			InstanceManager.InstanceWin();
		}
		else
		{
			this.IsWinWithChange = false;
			if (this.IsQuit)
			{
				ChangeCareerInstance.Instance.ShowLoseUI();
			}
			else
			{
				InstanceManager.InstanceLose();
			}
		}
	}

	public List<BattleSkillInfo> GetBattleSkillInfos(int profession)
	{
		List<BattleSkillInfo> list = new List<BattleSkillInfo>();
		RoleCreate roleCreate = DataReader<RoleCreate>.Get(profession);
		if (roleCreate == null)
		{
			return null;
		}
		RoleCreate roleCreate2 = DataReader<RoleCreate>.Get(EntityWorld.Instance.EntSelf.TypeID);
		if (roleCreate2 == null)
		{
			return null;
		}
		list.Clear();
		if (roleCreate.normalAttack.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.normalAttack.get_Item(0).key,
				skillId = roleCreate.normalAttack.get_Item(0).value
			});
		}
		if (roleCreate.attack1.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.attack1.get_Item(0).key,
				skillId = roleCreate.attack1.get_Item(0).value
			});
		}
		if (roleCreate.attack2.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.attack2.get_Item(0).key,
				skillId = roleCreate.attack2.get_Item(0).value
			});
		}
		if (roleCreate.attack3.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.attack3.get_Item(0).key,
				skillId = roleCreate.attack3.get_Item(0).value
			});
		}
		if (roleCreate.attack4.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.attack4.get_Item(0).key,
				skillId = roleCreate.attack4.get_Item(0).value
			});
		}
		if (roleCreate.skill1.get_Count() > 0 && roleCreate2.skill1.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = 11,
				skillId = roleCreate.skill1.get_Item(0).value,
				skillLv = Mathf.Max(1, SkillUIManager.Instance.GetSkillLvByID(roleCreate.skill1.get_Item(0).value))
			});
		}
		if (roleCreate.skill2.get_Count() > 0 && roleCreate2.skill2.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = 12,
				skillId = roleCreate.skill2.get_Item(0).value,
				skillLv = Mathf.Max(1, SkillUIManager.Instance.GetSkillLvByID(roleCreate.skill2.get_Item(0).value))
			});
		}
		if (roleCreate.skill3.get_Count() > 0 && roleCreate2.skill3.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = 13,
				skillId = roleCreate.skill3.get_Item(0).value,
				skillLv = Mathf.Max(1, SkillUIManager.Instance.GetSkillLvByID(roleCreate.skill3.get_Item(0).value))
			});
		}
		if (roleCreate.roll.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.roll.get_Item(0).key,
				skillId = roleCreate.roll.get_Item(0).value
			});
		}
		if (roleCreate.roll2.get_Count() > 0)
		{
			list.Add(new BattleSkillInfo
			{
				skillIdx = roleCreate.roll2.get_Item(0).key,
				skillId = roleCreate.roll2.get_Item(0).value
			});
		}
		return list;
	}

	public MapObjDecorations GetDecorations(int profession)
	{
		MapObjDecorations mapObjDecorations = new MapObjDecorations();
		mapObjDecorations.career = profession;
		mapObjDecorations.wingId = EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Experience);
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Necklace)));
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Pant)));
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Shirt)));
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Shoe)));
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Waist)));
		mapObjDecorations.equipIds.Add(this.FindMappingEquipId(profession, EquipGlobal.GetEquipCfgIDByPos(EquipLibType.ELT.Weapon)));
		return mapObjDecorations;
	}

	private int FindMappingEquipId(int profession, int equipId)
	{
		if (equipId <= 0)
		{
			return 0;
		}
		string[] array = this.FindEquipTransFrom(equipId);
		if (array == null)
		{
			return 0;
		}
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			if (array2.Length >= 2 && int.Parse(array2[0]) == profession)
			{
				return int.Parse(array2[1]);
			}
		}
		return 0;
	}

	private string[] FindEquipTransFrom(int equipId)
	{
		string text = EntityWorld.Instance.EntSelf.TypeID + ":" + equipId;
		List<ZhuangBeiJiChengYingShe> dataList = DataReader<ZhuangBeiJiChengYingShe>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			ZhuangBeiJiChengYingShe zhuangBeiJiChengYingShe = dataList.get_Item(i);
			string[] array = zhuangBeiJiChengYingShe.equipTransFrom.Split(new char[]
			{
				';'
			});
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == text)
				{
					return array;
				}
			}
		}
		return null;
	}
}
