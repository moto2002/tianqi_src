using System;

public class LocalDimensionSpirit
{
	public enum SpiritType
	{
		None,
		Self,
		Player,
		Pet,
		Monster
	}

	public const long ERROR_DATA = -1L;

	protected long id;

	protected int typeID;

	protected long ownerID;

	protected long curHp;

	protected bool isDead;

	public long ID
	{
		get
		{
			return this.id;
		}
		set
		{
			this.id = value;
		}
	}

	public int TypeID
	{
		get
		{
			return this.typeID;
		}
		set
		{
			this.typeID = value;
		}
	}

	public long OwnerID
	{
		get
		{
			return this.ownerID;
		}
		set
		{
			this.ownerID = value;
		}
	}

	public long CurHp
	{
		get
		{
			return this.curHp;
		}
		set
		{
			this.curHp = value;
		}
	}

	public bool IsDead
	{
		get
		{
			return this.isDead;
		}
		set
		{
			this.isDead = value;
		}
	}

	public static LocalDimensionSpirit.SpiritType GetSpiritType(bool isEntitySelf, bool isEntityPlayer, bool isEntityPet, bool isEntityMonster)
	{
		if (isEntitySelf)
		{
			return LocalDimensionSpirit.SpiritType.Self;
		}
		if (isEntityPlayer)
		{
			return LocalDimensionSpirit.SpiritType.Player;
		}
		if (isEntityPet)
		{
			return LocalDimensionSpirit.SpiritType.Pet;
		}
		if (isEntityMonster)
		{
			return LocalDimensionSpirit.SpiritType.Monster;
		}
		return LocalDimensionSpirit.SpiritType.None;
	}
}
