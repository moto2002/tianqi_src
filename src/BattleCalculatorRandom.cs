using System;

public class BattleCalculatorRandom
{
	protected int maxIndex = 2147483647;

	protected int index;

	protected ulong next;

	protected ulong defaultNext;

	public int MaxIndex
	{
		get
		{
			return this.maxIndex;
		}
	}

	public BattleCalculatorRandom()
	{
		this.index = 0;
		this.next = (ulong)DateTime.get_Now().get_Ticks();
	}

	public BattleCalculatorRandom(long seed)
	{
		this.index = 0;
		this.next = (ulong)seed;
	}

	protected uint Rand()
	{
		this.next = this.next * 1103515245uL + 12345uL;
		return (uint)(this.next >> 32) & 2147483647u;
	}

	public void ResetSeed(long seed)
	{
		this.index = 0;
		this.maxIndex = 2147483647;
		this.next = (ulong)seed;
		this.defaultNext = (ulong)seed;
	}

	public void ResetSeed(long seed, int count)
	{
		this.index = 0;
		if (count > 0)
		{
			this.maxIndex = count;
		}
		else
		{
			this.maxIndex = 2147483647;
		}
		this.next = (ulong)seed;
		this.defaultNext = (ulong)seed;
	}

	public int Next(int min, int max, out int randomIndex)
	{
		randomIndex = this.index;
		if (max < min)
		{
			return min;
		}
		int result = (int)((long)min + (long)((ulong)this.Rand() % (ulong)((long)(max - min))));
		randomIndex = this.index;
		if (this.index + 1 >= this.maxIndex)
		{
			this.index = 0;
			this.next = this.defaultNext;
		}
		else
		{
			this.index++;
		}
		return result;
	}
}
