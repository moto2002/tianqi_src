using GameData;
using System;

public class SimpleBaseAttrExtend : ISimpleBaseAttrExtend
{
	public Action<AttrType, long, long> AttrChangedDelegate;

	public Action AttrAssignedDelegate;

	public virtual void SetValue(AttrType type, int value, bool isFirstTry)
	{
	}

	public virtual void SetValue(AttrType type, long value, bool isFirstTry)
	{
	}

	public virtual long GetValue(AttrType type)
	{
		return 0L;
	}

	public void OnAttrChanged(AttrType type, long oldValue, long newValue)
	{
		if (this.AttrChangedDelegate == null)
		{
			return;
		}
		this.AttrChangedDelegate.Invoke(type, oldValue, newValue);
	}
}
