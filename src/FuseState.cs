using System;

public class FuseState
{
	public uint timerID;

	public int modelID;

	public IndexList<int, int> skill = new IndexList<int, int>();

	public long petID;

	public float casterHP;

	public float casterBoardHP;

	public float targetHP;

	public float targetBoardHP;

	public float fuseHP;
}
