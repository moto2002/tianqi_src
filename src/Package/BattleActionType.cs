using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleActionType")]
	[Serializable]
	public class BattleActionType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "UseSkill", Value = 0)]
			UseSkill,
			[ProtoEnum(Name = "CancelUseSkill", Value = 1)]
			CancelUseSkill,
			[ProtoEnum(Name = "AttrChanged", Value = 2)]
			AttrChanged,
			[ProtoEnum(Name = "Bleed", Value = 3)]
			Bleed,
			[ProtoEnum(Name = "Treat", Value = 4)]
			Treat,
			[ProtoEnum(Name = "UpdateEffect", Value = 5)]
			UpdateEffect,
			[ProtoEnum(Name = "RemoveEffect", Value = 6)]
			RemoveEffect,
			[ProtoEnum(Name = "Relive", Value = 7)]
			Relive,
			[ProtoEnum(Name = "PetEnterBattleField", Value = 8)]
			PetEnterBattleField,
			[ProtoEnum(Name = "PetLeaveBattleField", Value = 9)]
			PetLeaveBattleField,
			[ProtoEnum(Name = "Fit", Value = 10)]
			Fit,
			[ProtoEnum(Name = "ExitFit", Value = 11)]
			ExitFit,
			[ProtoEnum(Name = "AddBuff", Value = 12)]
			AddBuff,
			[ProtoEnum(Name = "UpdateBuff", Value = 13)]
			UpdateBuff,
			[ProtoEnum(Name = "RemoveBuff", Value = 14)]
			RemoveBuff,
			[ProtoEnum(Name = "SuckBlood", Value = 15)]
			SuckBlood,
			[ProtoEnum(Name = "LegalizeHp", Value = 16)]
			LegalizeHp,
			[ProtoEnum(Name = "AddSkill", Value = 17)]
			AddSkill,
			[ProtoEnum(Name = "RemoveSkill", Value = 18)]
			RemoveSkill,
			[ProtoEnum(Name = "AddFilter", Value = 19)]
			AddFilter,
			[ProtoEnum(Name = "RemoveFilter", Value = 20)]
			RemoveFilter,
			[ProtoEnum(Name = "Teleport", Value = 21)]
			Teleport,
			[ProtoEnum(Name = "Fix", Value = 50)]
			Fix = 50,
			[ProtoEnum(Name = "EndFix", Value = 51)]
			EndFix,
			[ProtoEnum(Name = "Static", Value = 52)]
			Static,
			[ProtoEnum(Name = "EndStatic", Value = 53)]
			EndStatic,
			[ProtoEnum(Name = "Taunt", Value = 54)]
			Taunt,
			[ProtoEnum(Name = "EndTaunt", Value = 55)]
			EndTaunt,
			[ProtoEnum(Name = "SuperArmor", Value = 56)]
			SuperArmor,
			[ProtoEnum(Name = "EndSuperArmor", Value = 57)]
			EndSuperArmor,
			[ProtoEnum(Name = "IgnoreDmgFormula", Value = 58)]
			IgnoreDmgFormula,
			[ProtoEnum(Name = "EndIgnoreDmgFormula", Value = 59)]
			EndIgnoreDmgFormula,
			[ProtoEnum(Name = "CloseRenderer", Value = 60)]
			CloseRenderer,
			[ProtoEnum(Name = "EndCloseRenderer", Value = 61)]
			EndCloseRenderer,
			[ProtoEnum(Name = "Stun", Value = 62)]
			Stun,
			[ProtoEnum(Name = "EndStun", Value = 63)]
			EndStun,
			[ProtoEnum(Name = "MoveCast", Value = 64)]
			MoveCast,
			[ProtoEnum(Name = "EndMoveCast", Value = 65)]
			EndMoveCast,
			[ProtoEnum(Name = "EndFitAction", Value = 67)]
			EndFitAction = 67,
			[ProtoEnum(Name = "Assault", Value = 68)]
			Assault,
			[ProtoEnum(Name = "EndAssault", Value = 69)]
			EndAssault,
			[ProtoEnum(Name = "EndKnock", Value = 71)]
			EndKnock = 71,
			[ProtoEnum(Name = "EndSkillManage", Value = 73)]
			EndSkillManage = 73,
			[ProtoEnum(Name = "EndLoading", Value = 75)]
			EndLoading = 75,
			[ProtoEnum(Name = "EndSkillPress", Value = 77)]
			EndSkillPress = 77,
			[ProtoEnum(Name = "MakeDead", Value = 78)]
			MakeDead,
			[ProtoEnum(Name = "AtkProof", Value = 79)]
			AtkProof,
			[ProtoEnum(Name = "EndAtkProof", Value = 80)]
			EndAtkProof,
			[ProtoEnum(Name = "NewBatchNty", Value = 81)]
			NewBatchNty,
			[ProtoEnum(Name = "AllLoadDoneNty", Value = 82)]
			AllLoadDoneNty,
			[ProtoEnum(Name = "Weak", Value = 83)]
			Weak,
			[ProtoEnum(Name = "EndWeak", Value = 84)]
			EndWeak,
			[ProtoEnum(Name = "ChangeCamp", Value = 85)]
			ChangeCamp,
			[ProtoEnum(Name = "Incurable", Value = 86)]
			Incurable,
			[ProtoEnum(Name = "EndIncurable", Value = 87)]
			EndIncurable
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
