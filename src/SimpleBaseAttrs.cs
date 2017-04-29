using GameData;
using Package;
using System;
using UnityEngine;

public class SimpleBaseAttrs : SimpleBaseAttrExtend, ISimpleBaseAttr
{
	private int moveSpeed = 1000;

	private int actSpeed = 1000;

	private int lv = 1;

	private long fighting;

	private int vipLv;

	public int MoveSpeed
	{
		get
		{
			return this.moveSpeed;
		}
		set
		{
			int num = this.moveSpeed;
			this.moveSpeed = value;
			if (num != this.moveSpeed)
			{
				this.OnAttrChanged(GameData.AttrType.MoveSpeed, (long)num, (long)this.moveSpeed);
			}
		}
	}

	public int ActSpeed
	{
		get
		{
			return this.actSpeed;
		}
		set
		{
			int num = this.actSpeed;
			this.actSpeed = value;
			if (num != this.ActSpeed)
			{
				this.OnAttrChanged(GameData.AttrType.ActSpeed, (long)num, (long)this.actSpeed);
			}
		}
	}

	public int Lv
	{
		get
		{
			return this.lv;
		}
		set
		{
			int num = this.lv;
			this.lv = value;
			if (num != this.lv)
			{
				this.OnAttrChanged(GameData.AttrType.Lv, (long)num, (long)this.lv);
			}
		}
	}

	public long Fighting
	{
		get
		{
			return this.fighting;
		}
		set
		{
			long num = this.fighting;
			this.fighting = value;
			if (num != this.fighting)
			{
				this.OnAttrChanged(GameData.AttrType.Fighting, num, this.fighting);
			}
		}
	}

	public int VipLv
	{
		get
		{
			return this.vipLv;
		}
		set
		{
			int num = this.vipLv;
			this.vipLv = value;
			if (num != this.vipLv)
			{
				this.OnAttrChanged(GameData.AttrType.VipLv, (long)num, (long)this.vipLv);
			}
		}
	}

	public int RealMoveSpeed
	{
		get
		{
			return this.MoveSpeed;
		}
		set
		{
			this.MoveSpeed = value;
			this.OnAttrChanged(GameData.AttrType.RealMoveSpeed, (long)this.MoveSpeed, (long)this.MoveSpeed);
		}
	}

	public int RealActionSpeed
	{
		get
		{
			return this.ActSpeed;
		}
		set
		{
			this.ActSpeed = value;
			this.OnAttrChanged(GameData.AttrType.RealActionSpeed, (long)this.ActSpeed, (long)this.ActSpeed);
		}
	}

	public void AssignAllAttrs(SimpleBaseInfo origin)
	{
		this.MoveSpeed = origin.MoveSpeed;
		this.ActSpeed = origin.AtkSpeed;
		this.Lv = origin.Lv;
		this.Fighting = origin.Fighting;
		this.VipLv = origin.VipLv;
	}

	public void ResetAllAttrs()
	{
		this.moveSpeed = 1000;
		this.actSpeed = 1000;
		this.lv = 1;
		this.fighting = 0L;
		this.vipLv = 0;
	}

	public override void SetValue(GameData.AttrType type, int value, bool isFirstTry)
	{
		switch (type)
		{
		case GameData.AttrType.Fighting:
			this.Fighting = (long)value;
			return;
		case GameData.AttrType.Diamond:
		case GameData.AttrType.Gold:
			IL_1E:
			if (type == GameData.AttrType.MoveSpeed)
			{
				this.MoveSpeed = value;
				return;
			}
			if (type == GameData.AttrType.ActSpeed)
			{
				this.ActSpeed = value;
				return;
			}
			if (type != GameData.AttrType.Lv)
			{
				return;
			}
			this.Lv = value;
			return;
		case GameData.AttrType.VipLv:
			this.VipLv = value;
			return;
		}
		goto IL_1E;
	}

	public override void SetValue(GameData.AttrType type, long value, bool isFirstTry)
	{
		switch (type)
		{
		case GameData.AttrType.Fighting:
			this.Fighting = value;
			return;
		case GameData.AttrType.Diamond:
		case GameData.AttrType.Gold:
			IL_1E:
			if (type == GameData.AttrType.MoveSpeed)
			{
				this.MoveSpeed = (int)value;
				return;
			}
			if (type == GameData.AttrType.ActSpeed)
			{
				this.ActSpeed = (int)value;
				return;
			}
			if (type != GameData.AttrType.Lv)
			{
				return;
			}
			this.Lv = (int)value;
			return;
		case GameData.AttrType.VipLv:
			this.VipLv = (int)value;
			return;
		}
		goto IL_1E;
	}

	public override long GetValue(GameData.AttrType type)
	{
		switch (type)
		{
		case GameData.AttrType.Fighting:
			return this.Fighting;
		case GameData.AttrType.Diamond:
		case GameData.AttrType.Gold:
			IL_1E:
			if (type == GameData.AttrType.MoveSpeed)
			{
				return (long)this.MoveSpeed;
			}
			if (type == GameData.AttrType.ActSpeed)
			{
				return (long)this.ActSpeed;
			}
			if (type == GameData.AttrType.RealMoveSpeed)
			{
				return (long)this.RealMoveSpeed;
			}
			if (type == GameData.AttrType.RealActionSpeed)
			{
				return (long)this.RealActionSpeed;
			}
			if (type != GameData.AttrType.Lv)
			{
				Debug.LogError("未找到属性值:" + type);
				return 0L;
			}
			return (long)this.Lv;
		case GameData.AttrType.VipLv:
			return (long)this.VipLv;
		}
		goto IL_1E;
	}
}
