using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class CameraPathNpc
{
	private ActorModel npc;

	private Queue<string> actionQueue;

	private Vector3 positionTo;

	private float angleTo;

	private float moveTime;

	public CameraPathNpc(ActorModel npc, List<string> actionQueue, Vector3 positionFrom, Vector3 positionTo, float angleFrom, float angleTo, float moveTime)
	{
		Debug.LogError(string.Concat(new object[]
		{
			"npc=",
			npc,
			" positionFrom=",
			positionFrom
		}));
		this.npc = npc;
		this.actionQueue = new Queue<string>(actionQueue);
		this.positionTo = positionTo;
		this.angleTo = angleTo;
		this.moveTime = moveTime;
		npc.SetPosition(positionFrom);
		npc.SetForward(Quaternion.AngleAxis(angleFrom, Vector3.get_up()) * npc.get_transform().get_forward());
		if (npc.resGUID == EntityWorld.Instance.ActSelf.resGUID)
		{
			EquipCustomization equipCustomizationer = EntityWorld.Instance.EntSelf.EquipCustomizationer;
			npc.EquipOn(equipCustomizationer.GetIdOfWeapon(), 0);
			npc.EquipOn(equipCustomizationer.GetIdOfClothes(), 0);
		}
		this.DoAction();
	}

	private bool IsRunAction(string action)
	{
		return action == "run" || action == "run_city";
	}

	private void DoAction()
	{
		if (this.actionQueue.get_Count() == 0)
		{
			this.npc.PreciseSetAction("idle");
		}
		else if (this.IsRunAction(this.actionQueue.Peek()))
		{
			float speed = Vector3.Distance(this.npc.get_transform().get_position(), this.positionTo) / this.moveTime;
			this.npc.SetAction(this.actionQueue.Peek(), null);
			this.npc.SetForward(Quaternion.AngleAxis(this.angleTo, Vector3.get_up()) * this.npc.get_transform().get_forward());
			this.npc.NavEndCallBack = null;
			this.npc.MoveTo(this.positionTo, speed, new Action(this.DoAction));
			this.actionQueue.Dequeue();
		}
		else
		{
			this.npc.NavEndCallBack = null;
			this.npc.SetAction(this.actionQueue.Peek(), new Action(this.DoAction));
			this.actionQueue.Dequeue();
		}
	}
}
