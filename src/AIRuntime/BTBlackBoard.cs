using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIRuntime
{
	public class BTBlackBoard
	{
		public string aiState = AIState.THINK_STATE;

		public AIEvent aiEvent = AIEvent.MoveEnd;

		public uint enemyId;

		public object enemyTrap;

		public uint timeoutId;

		public Vector3 movePoint = default(Vector3);

		public int lastCastIndex = -1;

		public uint skillActTime;

		public ulong skillActTick;

		public uint navTargetDistance;

		public int[] skillUseCount = new int[15];

		public int skillReversal;

		public float speedFactor = 1f;

		public Vector3 lastCastCoord = default(Vector3);

		public int turnAngleDir = -1;

		public int LookOn_LastMode = -1;

		public int LookOn_Mode5Skill;

		public float LookOn_DistanceMax;

		public float LookOn_DistanceMin;

		public int[] LookOn_ModePercent = new int[6];

		public float[] LookOn_ModeInterval = new float[5];

		public ulong LookOn_Tick;

		public uint LookOn_ActTime;

		public Dictionary<uint, int> mHatred = new Dictionary<uint, int>();

		public uint patrolActTime;

		public int LookOn_Mode
		{
			get
			{
				return this.turnAngleDir;
			}
			set
			{
				this.turnAngleDir = value;
				if (value >= 0)
				{
					this.LookOn_LastMode = value;
				}
			}
		}

		public BTBlackBoard()
		{
			for (int i = 0; i < 15; i++)
			{
				this.skillUseCount[i] = 0;
			}
		}

		public void ChangeState(string newState)
		{
			this.aiState = newState;
		}

		public void ChangeEvent(AIEvent newEvent)
		{
			this.aiEvent = newEvent;
		}

		public void EditHatred(uint key, int value)
		{
			if (this.mHatred.ContainsKey(key))
			{
				this.mHatred.set_Item(key, value);
			}
			else
			{
				this.mHatred.Add(key, value);
			}
		}

		public bool HasHatred(uint key)
		{
			return this.mHatred.ContainsKey(key);
		}

		public int HatredCount()
		{
			return this.mHatred.get_Count();
		}
	}
}
