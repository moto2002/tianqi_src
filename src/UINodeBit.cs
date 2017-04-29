using System;

public class UINodeBit
{
	public const int NoEventsUIRoot = 1;

	public const int NormalUIRoot = 2;

	public const int MiddleUIRoot = 4;

	public const int TopUIRoot = 8;

	public const int T2Root = 16;

	public const int T3Root = 32;

	public const int BitDefault = 7;

	public static bool IsContainNode(int dst_nodes, int node)
	{
		return (dst_nodes & node) > 0;
	}
}
