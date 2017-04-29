using System;

public class AttrCoder
{
	protected FloatCoder floatCoder = new FloatCoder();

	protected DoubleCoder doubleCoder = new DoubleCoder();

	protected IntCoder intCoder = new IntCoder();

	protected LongCoder longCoder = new LongCoder();

	public T Encode<T>(T origin, string typeName) where T : struct
	{
		if (typeName == "Int32")
		{
			return (T)((object)Convert.ChangeType(this.intCoder.Encode((int)((object)origin)), typeof(T)));
		}
		if (typeName == "Single")
		{
			return (T)((object)Convert.ChangeType(this.floatCoder.Encode((float)((object)origin)), typeof(T)));
		}
		if (typeName == "Int64")
		{
			return (T)((object)Convert.ChangeType(this.longCoder.Encode((long)((object)origin)), typeof(T)));
		}
		return origin;
	}

	public T Decode<T>(T origin, string typeName) where T : struct
	{
		if (typeName == "Int32")
		{
			return (T)((object)Convert.ChangeType(this.intCoder.Decode((int)((object)origin)), typeof(T)));
		}
		if (typeName == "Single")
		{
			return (T)((object)Convert.ChangeType(this.floatCoder.Decode((float)((object)origin)), typeof(T)));
		}
		if (typeName == "Int64")
		{
			return (T)((object)Convert.ChangeType(this.longCoder.Decode((long)((object)origin)), typeof(T)));
		}
		return origin;
	}
}
