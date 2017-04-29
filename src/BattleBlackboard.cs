using GameData;
using System;
using System.Collections.Generic;

public class BattleBlackboard
{
	public class PlayerData
	{
		public Action<int, long> SetPlayerID;

		public Action<int, int> SetPlayerHead;

		public Action<int, int> SetPlayerLevel;

		public Action<int, string> SetPlayerName;

		public Action<int, int> SetPlayerVipLv;

		public Action<int, long, long> SetPlayerHp;

		protected int index;

		protected long playerID;

		protected int playerHead;

		protected string playerName = string.Empty;

		protected int playerLv;

		protected int playerVipLv;

		protected long playerHpLmt;

		protected long playerHp;

		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		public long PlayerID
		{
			get
			{
				return this.playerID;
			}
			set
			{
				this.playerID = value;
				if (this.SetPlayerID != null)
				{
					this.SetPlayerID.Invoke(this.Index, value);
				}
			}
		}

		public int PlayerHead
		{
			get
			{
				return this.playerHead;
			}
			set
			{
				this.playerHead = value;
				if (this.SetPlayerHead != null)
				{
					this.SetPlayerHead.Invoke(this.Index, value);
				}
			}
		}

		public string PlayerName
		{
			get
			{
				return this.playerName;
			}
			set
			{
				this.playerName = value;
				if (this.SetPlayerName != null)
				{
					this.SetPlayerName.Invoke(this.Index, value);
				}
			}
		}

		public int PlayerLv
		{
			get
			{
				return this.playerLv;
			}
			set
			{
				this.playerLv = value;
				if (this.SetPlayerLevel != null)
				{
					this.SetPlayerLevel.Invoke(this.Index, value);
				}
			}
		}

		public int PlayerVipLv
		{
			get
			{
				return this.playerVipLv;
			}
			set
			{
				this.playerVipLv = value;
				if (this.SetPlayerVipLv != null)
				{
					this.SetPlayerVipLv.Invoke(this.Index, value);
				}
			}
		}

		public long PlayerHpLmt
		{
			get
			{
				return this.playerHpLmt;
			}
			set
			{
				this.playerHpLmt = value;
				if (this.SetPlayerHp != null)
				{
					this.SetPlayerHp.Invoke(this.Index, this.PlayerHp, value);
				}
			}
		}

		public long PlayerHp
		{
			get
			{
				return this.playerHp;
			}
			set
			{
				this.playerHp = value;
				if (this.SetPlayerHp != null)
				{
					this.SetPlayerHp.Invoke(this.Index, value, this.PlayerHpLmt);
				}
			}
		}
	}

	protected static BattleBlackboard instance;

	protected bool hasInit;

	protected bool isWinEnd;

	protected int finishTime;

	protected int selfHead;

	protected int selfLv;

	protected string selfName = string.Empty;

	protected int selfVipLv;

	protected long selfFighting;

	protected long selfHpLmt;

	protected long selfHp;

	protected int selfPetNum;

	protected float selfLowestHPPercentage = 1f;

	protected XDict<int, int> selfSkillIcon = new XDict<int, int>();

	protected XDict<int, float> selfSkillBound = new XDict<int, float>();

	protected XDict<int, KeyValuePair<float, DateTime>> selfSkillCD = new XDict<int, KeyValuePair<float, DateTime>>();

	protected int selfActPoint;

	protected int selfActPointLmt;

	protected int selfMaxCombo;

	protected int selfCombo;

	protected int selfHit;

	protected uint comboTimer = 4294967295u;

	protected uint comboInterval = 5000u;

	protected bool selfFuse;

	protected int selfDeadCount;

	protected bool isAnyNPCDead;

	protected bool isAllNPCDead;

	protected bool isAllNPCArrived;

	protected XDict<long, BattleBlackboard.PlayerData> playerDatas = new XDict<long, BattleBlackboard.PlayerData>();

	protected XDict<int, KeyValuePair<float, DateTime>> petCD = new XDict<int, KeyValuePair<float, DateTime>>();

	protected float petLowestHPPercentage = 1f;

	protected bool isAllPetAlive = true;

	protected List<long> allAttendPet = new List<long>();

	protected List<long> allFusePet = new List<long>();

	protected bool isKillCountDownEnd;

	protected Dictionary<int, int> deadMonserCount = new Dictionary<int, int>();

	public bool hasSetBoss;

	protected int bossHead;

	protected string bossName = string.Empty;

	protected int bossLv;

	protected long bossHpLmt;

	protected long bossHp;

	protected int bossVpLmt;

	protected int bossVp;

	protected bool bossWeak;

	protected bool bossDead;

	protected DateTime bossBornTime;

	protected DateTime bossDeadTime;

	public static BattleBlackboard Instance
	{
		get
		{
			if (BattleBlackboard.instance == null)
			{
				BattleBlackboard.instance = new BattleBlackboard();
			}
			return BattleBlackboard.instance;
		}
	}

	protected bool HasInit
	{
		get
		{
			return this.hasInit;
		}
		set
		{
			this.hasInit = value;
		}
	}

	public bool IsWinEnd
	{
		get
		{
			return this.isWinEnd;
		}
		set
		{
			this.isWinEnd = value;
		}
	}

	public int FinishTime
	{
		get
		{
			return this.finishTime;
		}
		set
		{
			this.finishTime = value;
		}
	}

	public int SelfHead
	{
		get
		{
			return this.selfHead;
		}
		set
		{
			this.selfHead = value;
			this.SetSelfHead(value);
		}
	}

	public int SelfLv
	{
		get
		{
			return this.selfLv;
		}
		set
		{
			this.selfLv = value;
			this.SetSelfLevel(value);
		}
	}

	public string SelfName
	{
		get
		{
			return this.selfName;
		}
		set
		{
			this.selfName = value;
			this.SetSelfName(value);
		}
	}

	public int SelfVipLv
	{
		get
		{
			return this.selfVipLv;
		}
		set
		{
			this.selfVipLv = value;
			this.SetSelfVipLv(value);
		}
	}

	public long SelfFighting
	{
		get
		{
			return this.selfFighting;
		}
		set
		{
			this.selfFighting = value;
			this.SetSelfFighting(value);
		}
	}

	public long SelfHpLmt
	{
		get
		{
			return this.selfHpLmt;
		}
		set
		{
			this.selfHpLmt = value;
			this.SetSelfHp(this.SelfHp, value);
		}
	}

	public long SelfHp
	{
		get
		{
			return this.selfHp;
		}
		set
		{
			this.selfHp = value;
			if (this.selfHpLmt > 0L)
			{
				float num = (float)((double)this.selfHp / (double)this.selfHpLmt);
				if (num < this.SelfLowestHPPercentage)
				{
					this.SelfLowestHPPercentage = num;
				}
			}
			this.SetSelfHp(value, this.SelfHpLmt);
		}
	}

	public int SelfPetNum
	{
		get
		{
			return this.selfPetNum;
		}
		set
		{
			this.selfPetNum = value;
		}
	}

	public float SelfLowestHPPercentage
	{
		get
		{
			return this.selfLowestHPPercentage * 100f;
		}
		set
		{
			this.selfLowestHPPercentage = value;
		}
	}

	public float SelfCurrentHPPercentage
	{
		get
		{
			return (this.selfHpLmt > 0L) ? ((float)this.selfHp / (float)this.selfHpLmt * 100f) : 0f;
		}
	}

	public XDict<int, int> SelfSkillIcon
	{
		get
		{
			return this.selfSkillIcon;
		}
	}

	public XDict<int, float> SelfSkillBound
	{
		get
		{
			return this.selfSkillBound;
		}
	}

	public XDict<int, KeyValuePair<float, DateTime>> SelfSkillCD
	{
		get
		{
			return this.selfSkillCD;
		}
	}

	public int SelfActPoint
	{
		get
		{
			return this.selfActPoint;
		}
		set
		{
			this.selfActPoint = value;
			this.SetSelfActPoint(value);
		}
	}

	public int SelfActPointLmt
	{
		get
		{
			return this.selfActPointLmt;
		}
		set
		{
			this.selfActPointLmt = value;
			this.SetSelfActPointLmt(value);
		}
	}

	public int SelfMaxCombo
	{
		get
		{
			return this.selfMaxCombo;
		}
	}

	public int SelfCombo
	{
		get
		{
			return this.selfCombo;
		}
	}

	public int SelfHit
	{
		get
		{
			return this.selfHit;
		}
	}

	public bool ContinueCombo
	{
		set
		{
			TimerHeap.DelTimer(this.comboTimer);
			if (value)
			{
				this.selfCombo++;
				if (this.selfCombo > this.SelfMaxCombo)
				{
					this.selfMaxCombo = this.selfCombo;
				}
				this.SetSelfCombo(this.selfCombo);
				this.comboTimer = TimerHeap.AddTimer(this.comboInterval, 0, delegate
				{
					this.ContinueCombo = false;
				});
			}
			else
			{
				this.selfCombo = 0;
				this.selfHit++;
				this.SetSelfComboBreak();
			}
		}
	}

	public bool SelfFuse
	{
		get
		{
			return this.selfFuse;
		}
		set
		{
			this.selfFuse = value;
			this.SetSelfFuse(value);
		}
	}

	public bool SelfDead
	{
		get
		{
			return this.selfDeadCount > 0;
		}
		set
		{
			if (value)
			{
				this.selfDeadCount++;
			}
			else
			{
				this.selfDeadCount = 0;
			}
		}
	}

	public bool IsAllMateAlive
	{
		get
		{
			return !this.SelfDead && this.IsAllPetAlive;
		}
	}

	public bool IsAnyNPCDead
	{
		get
		{
			return this.isAnyNPCDead;
		}
		set
		{
			this.isAnyNPCDead = value;
		}
	}

	public bool IsAllNPCDead
	{
		get
		{
			return this.isAllNPCDead;
		}
		set
		{
			this.isAllNPCDead = value;
		}
	}

	public bool IsAllNPCArrived
	{
		get
		{
			return this.isAllNPCArrived;
		}
		set
		{
			this.isAllNPCArrived = value;
		}
	}

	public XDict<long, BattleBlackboard.PlayerData> PlayerDatas
	{
		get
		{
			return this.playerDatas;
		}
	}

	public KeyValuePair<long, long> PetHpMessage
	{
		set
		{
			if (value.get_Key() > 0L && value.get_Value() > 0L)
			{
				float num = (float)((double)value.get_Key() / (double)value.get_Value());
				if (num < this.petLowestHPPercentage)
				{
					this.petLowestHPPercentage = num;
				}
			}
		}
	}

	public XDict<int, KeyValuePair<float, DateTime>> PetCD
	{
		get
		{
			return this.petCD;
		}
	}

	public float PetLowestHPPercentage
	{
		get
		{
			return this.petLowestHPPercentage * 100f;
		}
		set
		{
			this.petLowestHPPercentage = value;
		}
	}

	public bool IsAllPetAlive
	{
		get
		{
			return this.allAttendPet.get_Count() > 0 && this.isAllPetAlive;
		}
		set
		{
			this.isAllPetAlive = value;
			if (!value)
			{
				this.petLowestHPPercentage = 0f;
			}
		}
	}

	public List<int> AllAttendPet
	{
		get
		{
			List<int> list = new List<int>();
			using (List<long>.Enumerator enumerator = this.allAttendPet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					long current = enumerator.get_Current();
					if (PetManager.Instance.GetPetInfo(current) != null)
					{
						list.Add(PetManager.Instance.GetPetInfo(current).petId);
					}
				}
			}
			return list;
		}
	}

	public long AttendPet
	{
		set
		{
			if (this.allAttendPet.Contains(value))
			{
				return;
			}
			this.allAttendPet.Add(value);
		}
	}

	public List<int> AllFusePet
	{
		get
		{
			List<int> list = new List<int>();
			using (List<long>.Enumerator enumerator = this.allFusePet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					long current = enumerator.get_Current();
					if (PetManager.Instance.GetPetInfo(current) != null)
					{
						list.Add(PetManager.Instance.GetPetInfo(current).petId);
					}
				}
			}
			return list;
		}
	}

	public long PetFuseElement
	{
		set
		{
			if (this.allFusePet.Contains(value))
			{
				return;
			}
			this.allFusePet.Add(value);
		}
	}

	public bool IsKillCountDownEnd
	{
		get
		{
			return this.isKillCountDownEnd;
		}
		set
		{
			this.isKillCountDownEnd = value;
		}
	}

	public Dictionary<int, int> DeadMonserCount
	{
		get
		{
			return new Dictionary<int, int>(this.deadMonserCount);
		}
	}

	public int MonsterDead
	{
		set
		{
			if (!this.deadMonserCount.ContainsKey(value))
			{
				this.deadMonserCount.Add(value, 0);
			}
			Dictionary<int, int> dictionary;
			Dictionary<int, int> expr_24 = dictionary = this.deadMonserCount;
			int num = dictionary.get_Item(value);
			expr_24.set_Item(value, num + 1);
		}
	}

	public bool HasSetBoss
	{
		get
		{
			return this.hasSetBoss;
		}
		set
		{
			this.hasSetBoss = value;
			this.ShowBoss(value);
		}
	}

	public int BossHead
	{
		get
		{
			return this.bossHead;
		}
		set
		{
			this.bossHead = value;
			this.SetBossHead(value);
		}
	}

	public string BossName
	{
		get
		{
			return this.bossName;
		}
		set
		{
			this.bossName = value;
			this.SetBossName(value);
		}
	}

	public int BossLv
	{
		get
		{
			return this.bossLv;
		}
		set
		{
			this.bossLv = value;
			this.SetBossLevel(value);
		}
	}

	public long BossHpLmt
	{
		get
		{
			return this.bossHpLmt;
		}
		set
		{
			this.bossHpLmt = value;
			this.SetBossHp(this.BossHp, value);
		}
	}

	public long BossHp
	{
		get
		{
			return this.bossHp;
		}
		set
		{
			this.bossHp = value;
			this.SetBossHp(value, this.BossHpLmt);
		}
	}

	public int BossVpLmt
	{
		get
		{
			return this.bossVpLmt;
		}
		set
		{
			this.bossVpLmt = value;
			this.SetBossVp(this.BossVp, value);
		}
	}

	public int BossVp
	{
		get
		{
			return this.bossVp;
		}
		set
		{
			this.bossVp = value;
			this.SetBossVp(value, this.BossVpLmt);
		}
	}

	public bool BossWeak
	{
		get
		{
			return this.bossWeak;
		}
		set
		{
			this.bossWeak = value;
			this.SetBossWeak(value);
		}
	}

	public bool BossDead
	{
		get
		{
			return this.bossDead;
		}
		set
		{
			this.bossDeadTime = DateTime.get_Now();
			this.bossDead = value;
			if (value)
			{
				this.SetBossDead();
			}
		}
	}

	public double BossLifeTime
	{
		get
		{
			return (this.bossDeadTime - this.bossBornTime).get_TotalSeconds();
		}
	}

	protected BattleBlackboard()
	{
	}

	public void Init()
	{
		if (this.HasInit)
		{
			return;
		}
		this.HasInit = true;
		this.ResetData();
		this.AddListener();
		this.comboInterval = (uint)(float.Parse(DataReader<GlobalParams>.Get("comboTime").value) * 1000f);
	}

	public void Release()
	{
		this.RemoveListener();
		this.ResetData();
		this.HasInit = false;
	}

	protected void AddListener()
	{
	}

	protected void RemoveListener()
	{
	}

	public void ResetData()
	{
		this.isWinEnd = false;
		this.finishTime = 0;
		this.selfLowestHPPercentage = 1f;
		this.selfPetNum = 0;
		this.selfSkillIcon.Clear();
		this.selfSkillBound.Clear();
		this.selfSkillCD.Clear();
		this.selfCombo = 0;
		this.selfMaxCombo = 0;
		this.selfHit = 0;
		TimerHeap.DelTimer(this.comboTimer);
		this.selfFuse = false;
		this.selfDeadCount = 0;
		this.playerDatas.Clear();
		this.petCD.Clear();
		this.petLowestHPPercentage = 1f;
		this.isAllPetAlive = true;
		this.allAttendPet.Clear();
		this.allFusePet.Clear();
		this.isAnyNPCDead = false;
		this.isAllNPCDead = false;
		this.isAllNPCArrived = false;
		this.isKillCountDownEnd = false;
		this.deadMonserCount.Clear();
		this.hasSetBoss = false;
		this.bossHpLmt = 0L;
		this.bossHp = 0L;
		this.bossDead = false;
		this.bossDeadTime = DateTime.get_Now();
		this.bossBornTime = DateTime.get_Now();
	}

	public void SetSelfProperty(int head, int level, int vipLevel, long fighting, long hp, long hpLmt, int actPoint)
	{
		this.SelfHead = head;
		this.SelfLv = level;
		this.SelfVipLv = vipLevel;
		this.SelfFighting = fighting;
		this.SelfHpLmt = hpLmt;
		this.SelfHp = hp;
		this.SelfActPoint = actPoint;
	}

	protected void SetSelfHead(int head)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfHead(head);
	}

	protected void SetSelfLevel(int level)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfLevel(level);
	}

	protected void SetSelfName(string name)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfName(name);
	}

	protected void SetSelfVipLv(int vipLevel)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfVipLv(vipLevel);
	}

	protected void SetSelfFighting(long fighting)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfFighting(fighting);
	}

	protected void SetSelfFuse(bool isFuse)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfFuse(isFuse);
	}

	protected void SetSelfHp(long hp, long hpLmt)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		if (hp < 0L)
		{
			hp = 0L;
		}
		battleUI.SetSelfHP(hp, hpLmt);
	}

	public void AddSelfSkill(int skillIndex, int iconID, float bound, KeyValuePair<float, DateTime> cd)
	{
		this.selfSkillIcon.Add(skillIndex, iconID);
		this.selfSkillBound.Add(skillIndex, bound);
		this.selfSkillCD.Add(skillIndex, cd);
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.AddSkill(skillIndex, iconID);
		battleUI.SetSkillEnable(skillIndex, (float)this.SelfActPoint >= bound);
		battleUI.SetSkillCD(skillIndex, cd.get_Key(), (float)((double)cd.get_Key() - (DateTime.get_Now() - cd.get_Value()).get_TotalMilliseconds()) / cd.get_Key());
	}

	public void RemoveSelfSkill(int skillIndex)
	{
		this.selfSkillIcon.Remove(skillIndex);
		this.selfSkillBound.Remove(skillIndex);
		this.selfSkillCD.Remove(skillIndex);
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.RemoveSkill(skillIndex);
		battleUI.ResetSkillCD(skillIndex);
	}

	public void ClearSelfSkill()
	{
		this.selfSkillIcon.Clear();
		this.selfSkillBound.Clear();
		this.selfSkillCD.Clear();
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.ClearSkill();
		battleUI.ClearSkillCD();
	}

	public void ClearSelfSkillCD()
	{
		this.selfSkillCD.Clear();
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.ClearSkillCD();
	}

	protected void SetSelfActPoint(int actPoint)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSelfActPoint(actPoint);
		for (int i = 0; i < this.selfSkillBound.Count; i++)
		{
			battleUI.SetSkillEnable(this.selfSkillBound.ElementKeyAt(i), (float)actPoint >= this.selfSkillBound.ElementValueAt(i));
		}
	}

	protected void SetSelfActPointLmt(int actPointLmt)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
	}

	public void SetSelfSkillCD(XDict<int, KeyValuePair<float, DateTime>> resetCD, bool isSetClick)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		for (int i = 0; i < resetCD.Count; i++)
		{
			int num = resetCD.ElementKeyAt(i);
			KeyValuePair<float, DateTime> keyValuePair = resetCD.ElementValueAt(i);
			if (this.selfSkillIcon.ContainsKey(num))
			{
				if (this.selfSkillBound.ContainsKey(num))
				{
					if (keyValuePair.get_Key() == 0f)
					{
						if (this.selfSkillCD.ContainsKey(num))
						{
							this.selfSkillCD.Remove(num);
							if (battleUI != null)
							{
								battleUI.ResetSkillCD(num);
							}
						}
					}
					else
					{
						if (this.selfSkillCD.ContainsKey(num))
						{
							this.selfSkillCD[num] = resetCD.ElementValueAt(i);
						}
						else
						{
							this.selfSkillCD.Add(num, resetCD.ElementValueAt(i));
						}
						if (battleUI != null)
						{
							if (isSetClick)
							{
								battleUI.SetClick(num);
							}
							battleUI.SetSkillCD(num, keyValuePair.get_Key(), (float)((double)keyValuePair.get_Key() - (DateTime.get_Now() - keyValuePair.get_Value()).get_TotalMilliseconds()) / keyValuePair.get_Key());
						}
					}
				}
			}
		}
	}

	protected void SetSelfSkillCD(int skillIndex, KeyValuePair<float, DateTime> cd)
	{
		if (!this.selfSkillIcon.ContainsKey(skillIndex))
		{
			return;
		}
		if (!this.selfSkillBound.ContainsKey(skillIndex))
		{
			return;
		}
		this.selfSkillCD.Add(skillIndex, cd);
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetSkillCD(skillIndex, cd.get_Key(), (float)((double)cd.get_Key() - (DateTime.get_Now() - cd.get_Value()).get_TotalMilliseconds()) / cd.get_Key());
	}

	protected void SetSelfCombo(int combo)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetCombo(true, combo);
	}

	protected void SetSelfComboBreak()
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetCombo(false, 0);
	}

	public void SetTeamMateProperty(long id, int head, string name, int level, int vipLevel, long hp, long hpLmt)
	{
		if (this.playerDatas.ContainsKey(id))
		{
			this.playerDatas[id].PlayerHead = head;
			this.playerDatas[id].PlayerName = name;
			this.playerDatas[id].PlayerLv = level;
			this.playerDatas[id].PlayerVipLv = vipLevel;
			this.playerDatas[id].PlayerHpLmt = hpLmt;
			this.playerDatas[id].PlayerHp = hp;
		}
		else
		{
			this.TryCreatePlayerData(id, head, name, level, vipLevel, hp, hpLmt);
		}
	}

	protected void TryCreatePlayerData(long id, int head, string name, int level, int vipLevel, long hp, long hpLmt)
	{
		if (this.playerDatas.Count >= 2)
		{
			return;
		}
		BattleBlackboard.PlayerData playerData = new BattleBlackboard.PlayerData();
		playerData.Index = this.playerDatas.Count;
		playerData.SetPlayerID = new Action<int, long>(this.SetPlayerID);
		playerData.SetPlayerHead = new Action<int, int>(this.SetPlayerHead);
		playerData.SetPlayerName = new Action<int, string>(this.SetPlayerName);
		playerData.SetPlayerLevel = new Action<int, int>(this.SetPlayerLevel);
		playerData.SetPlayerVipLv = new Action<int, int>(this.SetPlayerVipLv);
		playerData.SetPlayerHp = new Action<int, long, long>(this.SetPlayerHp);
		playerData.PlayerID = id;
		playerData.PlayerHead = head;
		playerData.PlayerName = name;
		playerData.PlayerLv = level;
		playerData.PlayerVipLv = vipLevel;
		playerData.PlayerHpLmt = hpLmt;
		playerData.PlayerHp = hp;
		this.playerDatas.Add(id, playerData);
	}

	protected void SetPlayerID(int index, long id)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerID(index, id);
	}

	protected void SetPlayerHead(int index, int icon)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerHead(index, icon);
	}

	protected void SetPlayerName(int index, string name)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerName(index, name);
	}

	protected void SetPlayerLevel(int index, int level)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerLevel(index, level);
	}

	protected void SetPlayerVipLv(int index, int vipLv)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerVipLv(index, vipLv);
	}

	protected void SetPlayerHp(int index, long hp, long hpLmt)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetPlayerHp(index, hp, hpLmt);
	}

	public void SetPetCountDown(int index, KeyValuePair<float, DateTime> cd)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (cd.get_Key() == 0f)
		{
			if (this.petCD.ContainsKey(index))
			{
				this.petCD.Remove(index);
				if (battleUI != null)
				{
					battleUI.ResetPetCountDown(index);
				}
			}
		}
		else
		{
			if (this.petCD.ContainsKey(index))
			{
				this.petCD[index] = cd;
			}
			else
			{
				this.petCD.Add(index, cd);
			}
			if (battleUI != null)
			{
				battleUI.SetPetCountDown(index, cd.get_Key(), (float)((double)cd.get_Key() - (DateTime.get_Now() - cd.get_Value()).get_TotalMilliseconds()) / cd.get_Key());
			}
		}
	}

	public void SetBossProperty(int head, string name, int lv, long hp, long hpLmt, int vp, int vpLmt)
	{
		if (!this.HasSetBoss)
		{
			this.bossBornTime = DateTime.get_Now();
		}
		this.HasSetBoss = true;
		this.BossHead = head;
		this.BossName = name;
		this.BossLv = lv;
		this.BossHpLmt = hpLmt;
		this.BossHp = hp;
		this.BossVpLmt = vpLmt;
		this.BossVp = vp;
	}

	protected void ShowBoss(bool isShow)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.ShowBoss(isShow);
	}

	protected void SetBossHead(int head)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossHead(head);
	}

	protected void SetBossName(string name)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossName(name);
	}

	protected void SetBossLevel(int level)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossLevel(level);
	}

	protected void SetBossHp(long hp, long hpLmt)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		if (hp < 0L)
		{
			hp = 0L;
		}
		battleUI.ShowBoss(hp != 0L && hpLmt != 0L);
		battleUI.SetBossHp(hp, hpLmt);
	}

	protected void SetBossVp(int vp, int vpLmt)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossVp(vp, vpLmt);
	}

	protected void SetBossWeak(bool isWeak)
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossWeak(isWeak);
	}

	protected void SetBossDead()
	{
		BattleUI battleUI = UIManagerControl.Instance.GetUIIfExist("BattleUI") as BattleUI;
		if (battleUI == null)
		{
			return;
		}
		battleUI.SetBossDead();
	}
}
