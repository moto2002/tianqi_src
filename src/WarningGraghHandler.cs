using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class WarningGraghHandler
{
	protected static List<uint> warningFxTimer = new List<uint>();

	public static void Init()
	{
	}

	public static void Release()
	{
		WarningGraghHandler.ClearData();
	}

	public static void ClearData()
	{
		for (int i = 0; i < WarningGraghHandler.warningFxTimer.get_Count(); i++)
		{
			TimerHeap.DelTimer(WarningGraghHandler.warningFxTimer.get_Item(i));
		}
		WarningGraghHandler.warningFxTimer.Clear();
	}

	public static void AddWarningGragh(SkillWarningGraghMessage message)
	{
		if (!message.casterActor)
		{
			return;
		}
		Skill skillData = DataReader<Skill>.Get(message.skillID);
		if (skillData == null)
		{
			return;
		}
		XPoint caster = new XPoint
		{
			position = message.casterActor.FixTransform.get_position(),
			rotation = message.casterActor.FixTransform.get_rotation()
		};
		GraghMessage gragh = WarningGraghHandler.MarkWarningGraghByDynamicWarningArea(caster, message.targetPoint, skillData.dynamicWarningAreaOffset, skillData.dynamicWarningArea);
		if (gragh == null)
		{
			return;
		}
		int fxID = 0;
		fxID = FXManager.Instance.PlayFX((gragh.graghShape != GraghShape.Sector) ? 1104 : 1103, null, gragh.Center, gragh.fixBasePoint.rotation, message.casterActor.RealActionSpeed, 1f, 0, false, 0, null, delegate(ActorFX fx)
		{
			if (fx != null)
			{
				fx.VecterScale = ((gragh.graghShape != GraghShape.Sector) ? new Vector3(gragh.width, 1f, gragh.height) : new Vector3(gragh.range, 1f, gragh.range));
			}
			WarningGraghHandler.warningFxTimer.Add(TimerHeap.AddTimer((uint)skillData.skillWarningTime, 0, delegate
			{
				FXManager.Instance.DeleteFX(fxID);
			}));
		}, 1f, FXClassification.Normal);
		message.casterActor.FixFXSystem.AddActionFX(fxID);
	}

	protected static GraghMessage MarkWarningGraghByDynamicWarningArea(XPoint caster, XPoint target, List<int> offset, List<int> dynamicWarningArea)
	{
		if (dynamicWarningArea.get_Count() < 4)
		{
			return null;
		}
		XPoint xPoint = null;
		int num = dynamicWarningArea.get_Item(0);
		if (num != 1)
		{
			if (num == 2)
			{
				if (target == null)
				{
					return null;
				}
				xPoint = new XPoint();
				xPoint.position = target.position;
				xPoint.rotation = target.rotation;
			}
		}
		else
		{
			if (caster == null)
			{
				return null;
			}
			xPoint = new XPoint();
			xPoint.position = caster.position;
			xPoint.rotation = caster.rotation;
		}
		xPoint = xPoint.ApplyOffset(offset);
		List<int> list = new List<int>();
		for (int i = 1; i < 4; i++)
		{
			list.Add(dynamicWarningArea.get_Item(i));
		}
		return WarningGraghHandler.MarkWarningGragh(xPoint, list);
	}

	protected static GraghMessage MarkWarningGragh(XPoint basePoint, List<int> area)
	{
		if (area.get_Count() < 3)
		{
			return null;
		}
		float theRange = 0f;
		float theAngle = 0f;
		float theWidth = 0f;
		float theHeight = 0f;
		int num = area.get_Item(0);
		GraghShape theGraghShape;
		if (num != 1)
		{
			if (num != 2)
			{
				return null;
			}
			theGraghShape = GraghShape.Rect;
			theWidth = (float)area.get_Item(1) * 0.01f;
			theHeight = (float)area.get_Item(2) * 0.01f;
		}
		else
		{
			theGraghShape = GraghShape.Sector;
			theRange = (float)area.get_Item(1) * 0.01f;
			theAngle = (float)area.get_Item(2);
		}
		return new GraghMessage(basePoint, theGraghShape, theRange, theAngle, theWidth, theHeight);
	}
}
