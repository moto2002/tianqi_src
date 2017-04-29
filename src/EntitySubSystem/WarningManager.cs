using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public class WarningManager : ISubSystem, IWarningManager
	{
		protected EntityParent owner;

		protected ActorParent ownerActor;

		protected List<WarningMessage> lastWarningMessage = new List<WarningMessage>();

		protected float safeDistance = 0.5f;

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.ownerActor = this.owner.Actor;
			this.AddListeners();
		}

		public void OnDestroy()
		{
			this.RemoveListeners();
			this.ClearWarningMessage();
			this.owner = null;
		}

		protected virtual void AddListeners()
		{
			EventDispatcher.AddListener<SkillWarningMessage>(WarningManagerEvent.AddSkillWarningMessage, new Callback<SkillWarningMessage>(this.AddSkillWarningMessage));
			EventDispatcher.AddListener<EffectWarningMessage>(WarningManagerEvent.AddEffectWarningMessage, new Callback<EffectWarningMessage>(this.AddEffectWarningMessage));
		}

		protected virtual void RemoveListeners()
		{
			EventDispatcher.RemoveListener<SkillWarningMessage>(WarningManagerEvent.AddSkillWarningMessage, new Callback<SkillWarningMessage>(this.AddSkillWarningMessage));
			EventDispatcher.RemoveListener<EffectWarningMessage>(WarningManagerEvent.AddEffectWarningMessage, new Callback<EffectWarningMessage>(this.AddEffectWarningMessage));
		}

		public void UpdateActor(ActorParent actor)
		{
			this.ownerActor = actor;
		}

		protected void AddSkillWarningMessage(SkillWarningMessage message)
		{
			if (message.caster == null)
			{
				return;
			}
			if (message.target == null)
			{
				return;
			}
			if (message.caster.Camp == this.owner.Camp)
			{
				return;
			}
			Skill skill = DataReader<Skill>.Get(message.skillID);
			if (skill == null)
			{
				return;
			}
			if (skill.aiSkillMove != 1)
			{
				return;
			}
			this.ClearWarningMessage();
			this.MarkWarningMessageByDynamicWarningArea(message.caster, message.target, skill.dynamicWarningArea);
			using (Dictionary<int, XPoint>.Enumerator enumerator = message.effectMessage.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, XPoint> current = enumerator.get_Current();
					this.MarkWarningMessageByEffect(message.caster, current.get_Key(), current.get_Value());
				}
			}
		}

		protected void AddEffectWarningMessage(EffectWarningMessage message)
		{
			if (message.caster == null)
			{
				return;
			}
			this.ClearWarningMessage();
			this.MarkWarningMessageByEffect(message.caster, message.effectID, message.basePoint);
		}

		protected void MarkWarningMessageByDynamicWarningArea(EntityParent caster, EntityParent target, List<int> dynamicWarningArea)
		{
			if (dynamicWarningArea.get_Count() < 4)
			{
				return;
			}
			XPoint xPoint = null;
			int num = dynamicWarningArea.get_Item(0);
			if (num != 1)
			{
				if (num == 2)
				{
					if (!target.Actor)
					{
						return;
					}
					xPoint = new XPoint();
					xPoint.position = target.Actor.FixTransform.get_position();
					xPoint.rotation = target.Actor.FixTransform.get_rotation();
				}
			}
			else
			{
				if (!caster.Actor)
				{
					return;
				}
				xPoint = new XPoint();
				xPoint.position = caster.Actor.FixTransform.get_position();
				xPoint.rotation = caster.Actor.FixTransform.get_rotation();
			}
			List<int> list = new List<int>();
			for (int i = 1; i < 4; i++)
			{
				list.Add(dynamicWarningArea.get_Item(i));
			}
			float casterRadius = 0f;
			if (caster.Actor)
			{
				casterRadius = XUtility.GetHitRadius(caster.Actor.FixTransform);
			}
			this.MarkWarningMessage(xPoint, list, casterRadius);
		}

		protected void MarkWarningMessageByEffect(EntityParent caster, int effectID, XPoint basePoint)
		{
			if (basePoint == null)
			{
				return;
			}
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			if (effect.aiEffectMove != 1)
			{
				return;
			}
			if (effect.range == null)
			{
				return;
			}
			if (effect.range.get_Count() < 3)
			{
				return;
			}
			float casterRadius = 0f;
			if (caster.Actor)
			{
				casterRadius = XUtility.GetHitRadius(caster.Actor.FixTransform);
			}
			this.MarkWarningMessage(basePoint.ApplyOffset(effect.offset), effect.range, casterRadius);
			this.MarkWarningMessage(basePoint.ApplyOffset(effect.offset), effect.range2, casterRadius);
		}

		protected void MarkWarningMessage(XPoint basePoint, List<int> area, float casterRadius)
		{
			if (area.get_Count() < 3)
			{
				return;
			}
			float theRange = 0f;
			float theAngle = 0f;
			float theWidth = 0f;
			float theHeight = 0f;
			int num = area.get_Item(0);
			GraghShape theAreaShape;
			if (num != 1)
			{
				if (num != 2)
				{
					return;
				}
				theAreaShape = GraghShape.Rect;
				theWidth = (float)area.get_Item(1) * 0.01f;
				theHeight = (float)area.get_Item(2) * 0.01f;
			}
			else
			{
				theAreaShape = GraghShape.Sector;
				theRange = (float)area.get_Item(1) * 0.01f;
				theAngle = (float)area.get_Item(2);
			}
			this.lastWarningMessage.Add(new WarningMessage(basePoint, theAreaShape, theRange, theAngle, theWidth, theHeight, casterRadius));
		}

		public bool ExecuteWarningMessage(Action successCallBack = null, Action moveEndCallBack = null)
		{
			if (this.owner == null)
			{
				return false;
			}
			if (this.lastWarningMessage == null)
			{
				return false;
			}
			List<WarningMessage> list = new List<WarningMessage>();
			list.AddRange(this.lastWarningMessage);
			this.ClearWarningMessage();
			if (list.get_Count() < 1)
			{
				return false;
			}
			List<Vector3> list2 = new List<Vector3>();
			bool flag = true;
			int num = -1;
			using (List<WarningMessage>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WarningMessage current = enumerator.get_Current();
					num++;
					if (current.InArea(this.ownerActor.FixTransform.get_position(), 0f))
					{
						flag = false;
						list2.AddRange(this.GetAccuratePointByWarningMessage(current));
						break;
					}
				}
			}
			if (flag)
			{
				return false;
			}
			if (list2.get_Count() == 0)
			{
				return false;
			}
			List<Vector3> list3 = new List<Vector3>();
			Dictionary<Vector3, int> dictionary = new Dictionary<Vector3, int>();
			using (List<Vector3>.Enumerator enumerator2 = list2.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Vector3 current2 = enumerator2.get_Current();
					if (!dictionary.ContainsKey(current2))
					{
						dictionary.Add(current2, 0);
						list3.Add(current2);
					}
				}
			}
			for (int i = num; i < list.get_Count(); i++)
			{
				using (List<Vector3>.Enumerator enumerator3 = list3.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Vector3 current3 = enumerator3.get_Current();
						if (list.get_Item(i).InArea(current3, 0f))
						{
							Dictionary<Vector3, int> dictionary2;
							Dictionary<Vector3, int> expr_16F = dictionary2 = dictionary;
							Vector3 vector;
							Vector3 expr_174 = vector = current3;
							int num2 = dictionary2.get_Item(vector);
							expr_16F.set_Item(expr_174, num2 + 1);
						}
					}
				}
			}
			int num3 = list.get_Count();
			using (Dictionary<Vector3, int>.Enumerator enumerator4 = dictionary.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					KeyValuePair<Vector3, int> current4 = enumerator4.get_Current();
					if (current4.get_Value() < num3)
					{
						num3 = current4.get_Value();
					}
				}
			}
			List<Vector3> list4 = new List<Vector3>();
			using (Dictionary<Vector3, int>.Enumerator enumerator5 = dictionary.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					KeyValuePair<Vector3, int> current5 = enumerator5.get_Current();
					if (current5.get_Value() == num3)
					{
						list4.Add(current5.get_Key());
					}
				}
			}
			if (list4.get_Count() == 0)
			{
				return false;
			}
			if (successCallBack != null)
			{
				successCallBack.Invoke();
			}
			Vector3 vector2 = list4.get_Item(0);
			float num4 = XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), vector2);
			for (int j = 1; j < list4.get_Count(); j++)
			{
				if (XUtility.DistanceNoY(this.ownerActor.FixTransform.get_position(), list4.get_Item(j)) < num4)
				{
					vector2 = list4.get_Item(j);
				}
			}
			this.ownerActor.MoveToPoint(vector2, 0f, moveEndCallBack);
			return true;
		}

		protected List<Vector3> GetAccuratePointByWarningMessage(WarningMessage message)
		{
			List<Vector3> list = new List<Vector3>();
			GraghShape graghShape = message.graghShape;
			if (graghShape != GraghShape.Sector)
			{
				if (graghShape == GraghShape.Rect)
				{
					list.AddRange(this.GetAccuratePointByRectangle(this.ownerActor.FixTransform.get_position(), message.fixBasePoint, message.width, message.height, XUtility.GetHitRadius(this.ownerActor.FixTransform), message.casterRadius));
				}
			}
			else if (message.IsCircle)
			{
				list.AddRange(this.GetAccuratePointByCircle(this.ownerActor.FixTransform.get_position(), message.fixBasePoint, message.range, XUtility.GetHitRadius(this.ownerActor.FixTransform), message.casterRadius));
			}
			else
			{
				list.AddRange(this.GetAccuratePointBySector(this.ownerActor.FixTransform.get_position(), message.fixBasePoint, message.range, message.angle, XUtility.GetHitRadius(this.ownerActor.FixTransform), message.casterRadius));
			}
			return list;
		}

		protected List<Vector3> GetAccuratePointByCircle(Vector3 curPosition, XPoint basePoint, float range, float hitRange, float casterRadius)
		{
			Vector3 vector = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector2 = basePoint.position - vector * range;
			List<Vector3> list = new List<Vector3>();
			SortedDictionary<float, List<Vector3>> sortedDictionary = new SortedDictionary<float, List<Vector3>>();
			Vector3 vector3 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y - 90f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector4 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y + 90f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector5 = vector2 + vector3 * (range + hitRange + this.safeDistance);
			Vector3 vector6 = vector2 + vector4 * (range + hitRange + this.safeDistance);
			float num = XUtility.DistanceNoY(curPosition, vector5);
			float num2 = XUtility.DistanceNoY(curPosition, vector6);
			if (SystemConfig.IsOpenEffectDrawLine)
			{
				Debug.DrawLine(curPosition, vector5, Color.get_blue(), 2f);
				Debug.DrawLine(curPosition, vector6, Color.get_white(), 2f);
			}
			if (!sortedDictionary.ContainsKey(num))
			{
				sortedDictionary.Add(num, new List<Vector3>());
			}
			sortedDictionary.get_Item(num).Add(vector5);
			if (!sortedDictionary.ContainsKey(num2))
			{
				sortedDictionary.Add(num2, new List<Vector3>());
			}
			sortedDictionary.get_Item(num2).Add(vector6);
			using (SortedDictionary<float, List<Vector3>>.Enumerator enumerator = sortedDictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<float, List<Vector3>> current = enumerator.get_Current();
					using (List<Vector3>.Enumerator enumerator2 = current.get_Value().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Vector3 current2 = enumerator2.get_Current();
							list.Add(current2);
						}
					}
				}
			}
			return list;
		}

		protected List<Vector3> GetAccuratePointBySector(Vector3 curPosition, XPoint basePoint, float range, float angle, float hitRange, float casterRadius)
		{
			List<Vector3> list = new List<Vector3>();
			SortedDictionary<float, List<Vector3>> sortedDictionary = new SortedDictionary<float, List<Vector3>>();
			Vector3 vector = Vector3.get_zero();
			Vector3 vector2 = Vector3.get_zero();
			if (angle < 180f)
			{
				Vector3 vector3 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y, basePoint.rotation.get_eulerAngles().z) * Vector3.get_left();
				Vector3 vector4 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y, basePoint.rotation.get_eulerAngles().z) * Vector3.get_right();
				vector = basePoint.position + vector3 * (casterRadius + hitRange + this.safeDistance);
				vector2 = basePoint.position + vector4 * (casterRadius + hitRange + this.safeDistance);
				float num = XUtility.DistanceNoY(curPosition, vector);
				float num2 = XUtility.DistanceNoY(curPosition, vector2);
				if (!sortedDictionary.ContainsKey(num))
				{
					sortedDictionary.Add(num, new List<Vector3>());
				}
				sortedDictionary.get_Item(num).Add(vector);
				if (!sortedDictionary.ContainsKey(num2))
				{
					sortedDictionary.Add(num2, new List<Vector3>());
				}
				sortedDictionary.get_Item(num2).Add(vector2);
				if (SystemConfig.IsOpenEffectDrawLine)
				{
					Debug.DrawLine(curPosition, vector, Color.get_blue(), 2f);
					Debug.DrawLine(curPosition, vector2, Color.get_white(), 2f);
				}
			}
			else if (angle < 350f)
			{
				Vector3 vector5 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y - angle / 2f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
				Vector3 vector6 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y + angle / 2f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
				vector = basePoint.position + vector5 * (casterRadius + hitRange + this.safeDistance);
				vector2 = basePoint.position + vector6 * (casterRadius + hitRange + this.safeDistance);
				float num = XUtility.DistanceNoY(curPosition, vector);
				float num2 = XUtility.DistanceNoY(curPosition, vector2);
				if (!sortedDictionary.ContainsKey(num))
				{
					sortedDictionary.Add(num, new List<Vector3>());
				}
				sortedDictionary.get_Item(num).Add(vector);
				if (!sortedDictionary.ContainsKey(num2))
				{
					sortedDictionary.Add(num2, new List<Vector3>());
				}
				sortedDictionary.get_Item(num2).Add(vector2);
			}
			Random random = new Random();
			Vector3 vector7 = Vector3.get_zero();
			using (SortedDictionary<float, List<Vector3>>.Enumerator enumerator = sortedDictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<float, List<Vector3>> current = enumerator.get_Current();
					List<Vector3> list2 = new List<Vector3>();
					list2.AddRange(current.get_Value());
					for (int i = 0; i < list2.get_Count(); i++)
					{
						int num3 = random.Next(i, list2.get_Count());
						vector7 = list2.get_Item(i);
						list2.set_Item(i, list2.get_Item(num3));
						list2.set_Item(num3, vector7);
					}
					using (List<Vector3>.Enumerator enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Vector3 current2 = enumerator2.get_Current();
							list.Add(current2);
						}
					}
				}
			}
			return list;
		}

		protected List<Vector3> GetAccuratePointByRectangle(Vector3 curPosition, XPoint basePoint, float width, float height, float hitRange, float casterRadius)
		{
			List<Vector3> list = new List<Vector3>();
			SortedDictionary<float, List<Vector3>> sortedDictionary = new SortedDictionary<float, List<Vector3>>();
			Vector3 vector = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y - 95f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector2 = Quaternion.Euler(basePoint.rotation.get_eulerAngles().x, basePoint.rotation.get_eulerAngles().y + 95f, basePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector3 = basePoint.position + vector * (casterRadius + hitRange + this.safeDistance);
			Vector3 vector4 = basePoint.position + vector2 * (casterRadius + hitRange + this.safeDistance);
			float num = XUtility.DistanceNoY(curPosition, vector3);
			float num2 = XUtility.DistanceNoY(curPosition, vector4);
			if (SystemConfig.IsOpenEffectDrawLine)
			{
				Debug.DrawLine(curPosition, vector3, Color.get_blue(), 2f);
				Debug.DrawLine(curPosition, vector4, Color.get_white(), 2f);
			}
			if (!sortedDictionary.ContainsKey(num))
			{
				sortedDictionary.Add(num, new List<Vector3>());
			}
			sortedDictionary.get_Item(num).Add(vector3);
			if (!sortedDictionary.ContainsKey(num2))
			{
				sortedDictionary.Add(num2, new List<Vector3>());
			}
			sortedDictionary.get_Item(num2).Add(vector4);
			using (SortedDictionary<float, List<Vector3>>.Enumerator enumerator = sortedDictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<float, List<Vector3>> current = enumerator.get_Current();
					using (List<Vector3>.Enumerator enumerator2 = current.get_Value().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Vector3 current2 = enumerator2.get_Current();
							list.Add(current2);
						}
					}
				}
			}
			return list;
		}

		public bool HasWarningMessage()
		{
			return this.lastWarningMessage.get_Count() > 0;
		}

		public void ClearWarningMessage()
		{
			this.lastWarningMessage.Clear();
		}
	}
}
