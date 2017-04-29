using GameData;
using System;
using UnityEngine;

public class StandardBaseAttrExtend : ISimpleBaseAttrExtend, IStandardBaseAttrExtend
{
	public Action<AttrType, long, long> AttrChangedDelegate;

	public Action AttrAssignedDelegate;

	public void AddValue(AttrType type, int value, bool isFirstTry)
	{
		if (value != 0)
		{
			CfgFormula<int> cfgFormula = this.GetCfgFormula<int>(type);
			if (cfgFormula == null)
			{
				if (isFirstTry)
				{
					this.AddValue(type, (long)value, false);
				}
			}
			else
			{
				int val = cfgFormula.Val;
				cfgFormula.AddArg(value);
				if (cfgFormula.Val != val)
				{
					this.OnAttrChanged(type, (long)val, (long)cfgFormula.Val);
				}
			}
		}
	}

	public void AddValue(AttrType type, long value, bool isFirstTry)
	{
		if (value != 0L)
		{
			CfgFormula<long> cfgFormula = this.GetCfgFormula<long>(type);
			if (cfgFormula == null)
			{
				if (isFirstTry)
				{
					this.AddValue(type, (int)value, false);
				}
			}
			else
			{
				long val = cfgFormula.Val;
				cfgFormula.AddArg(value);
				if (cfgFormula.Val != val)
				{
					this.OnAttrChanged(type, val, cfgFormula.Val);
				}
			}
		}
	}

	public void RemoveValue(AttrType type, int value, bool isFirstTry)
	{
		if (value != 0)
		{
			CfgFormula<int> cfgFormula = this.GetCfgFormula<int>(type);
			if (cfgFormula == null)
			{
				if (isFirstTry)
				{
					this.RemoveValue(type, (long)value, false);
				}
			}
			else
			{
				int val = cfgFormula.Val;
				cfgFormula.RemoveArg(value);
				if (cfgFormula.Val != val)
				{
					this.OnAttrChanged(type, (long)val, (long)cfgFormula.Val);
				}
			}
		}
	}

	public void RemoveValue(AttrType type, long value, bool isFirstTry)
	{
		if (value != 0L)
		{
			CfgFormula<long> cfgFormula = this.GetCfgFormula<long>(type);
			if (cfgFormula == null)
			{
				if (isFirstTry)
				{
					this.RemoveValue(type, (int)value, false);
				}
			}
			else
			{
				long val = cfgFormula.Val;
				cfgFormula.RemoveArg(value);
				if (cfgFormula.Val != val)
				{
					this.OnAttrChanged(type, val, cfgFormula.Val);
				}
			}
		}
	}

	public void SetValue(AttrType type, int value, bool isFirstTry)
	{
		CfgFormula<int> cfgFormula = this.GetCfgFormula<int>(type);
		if (cfgFormula == null)
		{
			if (isFirstTry)
			{
				this.SetValue(type, (long)value, false);
			}
		}
		else
		{
			int val = cfgFormula.Val;
			cfgFormula.Val = value;
			if (cfgFormula.Val != val)
			{
				this.OnAttrChanged(type, (long)val, (long)value);
			}
		}
	}

	public void SetValue(AttrType type, long value, bool isFirstTry)
	{
		CfgFormula<long> cfgFormula = this.GetCfgFormula<long>(type);
		if (cfgFormula == null)
		{
			if (isFirstTry)
			{
				this.SetValue(type, (int)value, false);
			}
		}
		else
		{
			long val = cfgFormula.Val;
			cfgFormula.Val = value;
			if (cfgFormula.Val != val)
			{
				this.OnAttrChanged(type, val, value);
			}
		}
	}

	public virtual long GetValue(AttrType type)
	{
		return 0L;
	}

	public virtual long TryAddValue(AttrType type, long tryAddValue)
	{
		return 0L;
	}

	public long TryAddValue(AttrType type, XDict<AttrType, long> tryAddValues)
	{
		if (tryAddValues != null && tryAddValues.ContainsKey(type))
		{
			return this.TryAddValue(type, tryAddValues[type]);
		}
		return this.GetValue(type);
	}

	public void SwapValue(AttrType type, long oldValue, long newValue)
	{
		this.RemoveValue(type, oldValue, true);
		this.AddValue(type, newValue, true);
	}

	public void OnAttrChanged(AttrType type, long oldValue, long newValue)
	{
		if (this.AttrChangedDelegate == null)
		{
			return;
		}
		this.AttrChangedDelegate.Invoke(type, oldValue, newValue);
	}

	protected virtual CfgFormula<T> GetCfgFormula<T>(AttrType type) where T : struct
	{
		return null;
	}

	public void AddValuesByTemplateID(int templateID)
	{
		Attrs attrs = DataReader<Attrs>.Get(templateID);
		if (attrs == null)
		{
			Debug.LogError("Could not found attr template config, templId: " + templateID);
			return;
		}
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			this.AddValue((AttrType)attrs.attrs.get_Item(i), attrs.values.get_Item(i), true);
		}
	}

	public void RemoveValuesByTemplateID(int templateID)
	{
		Attrs attrs = DataReader<Attrs>.Get(templateID);
		if (attrs == null)
		{
			Debug.LogError("Could not found attr template config, templId: " + templateID);
			return;
		}
		for (int i = 0; i < attrs.attrs.get_Count(); i++)
		{
			this.RemoveValue((AttrType)attrs.attrs.get_Item(i), attrs.values.get_Item(i), true);
		}
	}
}
