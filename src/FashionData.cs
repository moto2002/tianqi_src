using System;

public class FashionData
{
	public enum FashionDataState
	{
		None,
		Own,
		Expired,
		Dressing
	}

	public const int TimeEternal = -1;

	public string dataID;

	public int time = -1;

	public FashionData.FashionDataState state;

	public bool isAddAttr = true;
}
