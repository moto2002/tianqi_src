using System;

public abstract class ValueCoder<T> where T : struct
{
	protected T mask;

	public abstract T Encode(T origin);

	public abstract T Decode(T current);
}
