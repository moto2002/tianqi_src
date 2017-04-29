using System;
using UnityEngine;
using XEngineActor;

internal class TimelineNpc
{
	public ActorModel npc;

	private Vector3 posB;

	private Vector3 towardB;

	private float time;

	public int npcId;

	private string actionName;

	public TimelineNpc(string actionName, int npcId, Vector3 posA, Vector3 towardA, Vector3 posB, Vector3 towardB, float time)
	{
		this.actionName = actionName;
		this.npcId = npcId;
		this.posB = posB;
		this.towardB = towardB;
		this.time = time;
		this.npc = TimelineGlobal.GetNpc(npcId);
		this.npc.ModelType = ActorModelType.CG;
		this.npc.SetPosition(posA);
		this.npc.SetForward(towardA);
		this.SetLayerRecursive(this.npc.get_transform());
		if (npcId == EntityWorld.Instance.ActSelf.resGUID)
		{
			this.npc.EquipOn(EntityWorld.Instance.EntSelf.EquipCustomizationer.GetIdOfWeapon(), 0);
			this.npc.EquipOn(EntityWorld.Instance.EntSelf.EquipCustomizationer.GetIdOfClothes(), 0);
		}
		this.SwitchingAction();
	}

	private void SetLayerRecursive(Transform transform)
	{
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Transform child = transform.GetChild(i);
			this.SetLayerRecursive(child.get_transform());
		}
	}

	public void SwitchingAction(string actionName, int npcId, Vector3 posA, Vector3 towardA, Vector3 posB, Vector3 towardB, float time)
	{
		this.actionName = actionName;
		this.npcId = npcId;
		this.posB = posB;
		this.towardB = towardB;
		this.time = time;
		this.npc.SetPosition(posA);
		this.npc.SetForward(towardA);
		this.SwitchingAction();
	}

	private void MoveCallback()
	{
		if (this.actionName == "die")
		{
			return;
		}
		this.npc.PreciseSetAction("idle");
	}

	private void SkillCallback()
	{
		if (this.actionName == "die")
		{
			return;
		}
		this.npc.PreciseSetAction("idle");
	}

	public void SwitchingAction()
	{
		if (this.actionName == "run" || this.actionName == "run_city")
		{
			this.npc.SetForward(this.towardB);
			float speed = Vector3.Distance(this.npc.get_transform().get_position(), this.posB) / this.time;
			this.npc.SetAction(this.actionName, null);
			this.npc.NavEndCallBack = null;
			this.npc.MoveTo(this.posB, speed, new Action(this.MoveCallback));
		}
		else
		{
			this.npc.NavEndCallBack = null;
			this.npc.SetAction(this.actionName, new Action(this.SkillCallback));
		}
	}
}
