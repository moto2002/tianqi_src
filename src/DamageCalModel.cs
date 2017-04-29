using Package;
using System;
using System.Collections.Generic;

public class DamageCalModel
{
	public long id;

	public long damage;

	public long heal;

	public long total;

	public long amount;

	public string name;

	public int iconId;

	public List<DamageCalModel> listChildren = new List<DamageCalModel>();

	public bool open;

	public GameObjectType.ENUM ModelObjType = GameObjectType.ENUM.Soldier;

	public DamageCalModel(long idParm, GameObjectType.ENUM objType)
	{
		this.id = idParm;
		this.ModelObjType = objType;
	}
}
