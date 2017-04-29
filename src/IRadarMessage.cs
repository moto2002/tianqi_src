using System;
using System.Collections.Generic;

public interface IRadarMessage
{
	int MinimapName
	{
		get;
	}

	string Minimap
	{
		get;
	}

	int TitleIcon
	{
		get;
	}

	List<float> AnchorPoint
	{
		get;
	}

	List<RadarItemMessage> RadarItemList
	{
		get;
	}

	int LinkWay
	{
		get;
	}
}
