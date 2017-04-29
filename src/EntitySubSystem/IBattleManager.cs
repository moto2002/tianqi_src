using Package;
using System;

namespace EntitySubSystem
{
	public interface IBattleManager
	{
		void UseSkill(BattleAction_UseSkill data, bool isServerData);

		void AttrChanged(BattleAction_AttrChanged data, bool isServerData);

		void Bleed(BattleAction_Bleed data, bool isServerData);

		void Treat(BattleAction_Treat data, bool isServerData);

		void UpdateEffect(BattleAction_UpdateEffect data, bool isServerData);

		void RemoveEffect(BattleAction_RemoveEffect data, bool isServerData);

		void Relive(BattleAction_Relive data, bool isServerData);

		void PetEnterBattleField(BattleAction_PetEnterField data, bool isServerData);

		void PetLeaveBattleField(BattleAction_PetLeaveField data, bool isServerData);

		void Fit(BattleAction_Fit data, bool isServerData);

		void ExitFit(BattleAction_ExitFit data, bool isServerData);

		void AddBuff(BattleAction_AddBuff data, bool isServerData);

		void UpdateBuff(BattleAction_UpdateBuff data, bool isServerData);

		void RemoveBuff(BattleAction_RemoveBuff data, bool isServerData);

		void SuckBlood(BattleAction_SuckBlood data, bool isServerData);

		void LegalizeHp(BattleAction_LegalizeHp data, bool isServerData);

		void AddSkill(BattleAction_AddSkill data, bool isServerData);

		void RemoveSkill(BattleAction_RemoveSkill data, bool isServerData);

		void Teleport(BattleAction_Teleport data, bool isServerData);

		void Fixed(BattleAction_Fix data, bool isServerData);

		void EndFixed(BattleAction_EndFix data, bool isServerData);

		void Static(BattleAction_Static data, bool isServerData);

		void EndStatic(BattleAction_EndStatic data, bool isServerData);

		void Taunt(BattleAction_Taunt data, bool isServerData);

		void EndTaunt(BattleAction_EndTaunt data, bool isServerData);

		void SuperArmor(BattleAction_SuperArmor data, bool isServerData);

		void EndSuperArmor(BattleAction_EndSuperArmor data, bool isServerData);

		void IgnoreDmgFormula(BattleAction_IgnoreDmgFormula data, bool isServerData);

		void EndIgnoreDmgFormula(BattleAction_EndIgnoreDmgFormula data, bool isServerData);

		void CloseRenderer(BattleAction_CloseRenderer data, bool isServerData);

		void EndCloseRenderer(BattleAction_EndCloseRenderer data, bool isServerData);

		void Dizzy(BattleAction_Stun data, bool isServerData);

		void EndDizzy(BattleAction_EndStun data, bool isServerData);

		void MoveCast(BattleAction_MoveCast data, bool isServerData);

		void EndMoveCast(BattleAction_EndMoveCast data, bool isServerData);

		void EndFitAction(BattleAction_EndFitAction data, bool isServerData);

		void Assault(BattleAction_Assault data, bool isServerData);

		void EndAssault(BattleAction_EndAssault data, bool isServerData);

		void EndKnock(BattleAction_EndKnock data, bool isServerData);

		void EndSkillManage(BattleAction_EndSkillManage data, bool isServerData);

		void EndLoading(BattleAction_EndLoading data, bool isServerData);

		void EndSkillPress(BattleAction_EndSkillPress data, bool isServerData);

		void MakeDead(BattleAction_MakeDead data, bool isServerData);

		void AtkProof(BattleAction_AtkProof data, bool isServerData);

		void EndAtkProof(BattleAction_EndAtkProof data, bool isServerData);

		void Weak(BattleAction_Weak data, bool isServerData);

		void EndWeak(BattleAction_EndWeak data, bool isServerData);

		void ChangeCamp(BattleAction_ChangeCamp data, bool isServerData);

		void Incurable(BattleAction_Incurable data, bool isServerData);

		void EndIncurable(BattleAction_EndIncurable data, bool isServerData);
	}
}
