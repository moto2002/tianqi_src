using System;
using System.Collections.Generic;

public class OrganHandler
{
	public enum OrganType
	{

	}

	private static OrganHandler m_instance;

	private List<BaseOrgan> organs = new List<BaseOrgan>();

	private BaseOrgan organ;

	public static OrganHandler Instance
	{
		get
		{
			if (OrganHandler.m_instance == null)
			{
				OrganHandler.m_instance = new OrganHandler();
			}
			return OrganHandler.m_instance;
		}
	}

	public void Add(BaseOrgan item)
	{
		if (!this.organs.Exists((BaseOrgan e) => e == item))
		{
			this.organs.Add(item);
		}
	}

	public void Remove(BaseOrgan item)
	{
		if (!this.organs.Exists((BaseOrgan e) => e == item))
		{
			this.organs.Remove(item);
		}
	}

	public void Update()
	{
		if (this.organs.get_Count() > 0)
		{
			for (int i = 0; i < this.organs.get_Count(); i++)
			{
				this.organ = this.organs.get_Item(i);
				if (this.organ.isTrigger && this.organ.UpdatePos())
				{
					this.organ.isTrigger = false;
				}
			}
		}
	}
}
