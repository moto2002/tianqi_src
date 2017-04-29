using Package;
using System;

public class ChannelBit
{
	public const int SingleWorld = 1;

	public const int Guild = 2;

	public const int Private = 4;

	public const int System = 8;

	public const int World = 16;

	public const int Team = 32;

	public const int Broadcast = 64;

	public const int TeamOrg = 128;

	public static int[] all_channelviews = new int[]
	{
		1,
		2,
		4,
		8,
		32,
		128
	};

	public static int Server2ClientChannel(ChannelType.CT channel2Server)
	{
		switch (channel2Server)
		{
		case ChannelType.CT.Faction:
			return 2;
		case ChannelType.CT.Private:
			return 4;
		case ChannelType.CT.SingleWorld:
			return 1;
		case ChannelType.CT.World:
			return 16;
		case ChannelType.CT.Team:
			return 32;
		case ChannelType.CT.System:
			return 8;
		case ChannelType.CT.TeamOrg:
			return 128;
		}
		return 1;
	}

	public static ChannelType.CT Client2ServerChannel(int channel2Client)
	{
		switch (channel2Client)
		{
		case 1:
			return ChannelType.CT.SingleWorld;
		case 2:
			return ChannelType.CT.Faction;
		case 3:
		case 5:
		case 6:
		case 7:
			IL_2A:
			if (channel2Client == 16)
			{
				return ChannelType.CT.World;
			}
			if (channel2Client == 32)
			{
				return ChannelType.CT.Team;
			}
			if (channel2Client != 128)
			{
				return ChannelType.CT.SingleWorld;
			}
			return ChannelType.CT.TeamOrg;
		case 4:
			return ChannelType.CT.Private;
		case 8:
			return ChannelType.CT.System;
		}
		goto IL_2A;
	}

	public static int GetDstChannels(int srcChannel, long senderUID)
	{
		int result = 0;
		switch (srcChannel)
		{
		case 1:
			result = 1;
			return result;
		case 2:
			result = 2;
			return result;
		case 3:
		case 5:
		case 6:
		case 7:
			IL_2C:
			if (srcChannel == 16)
			{
				result = 16;
				return result;
			}
			if (srcChannel == 32)
			{
				result = 32;
				return result;
			}
			if (srcChannel == 64)
			{
				result = 8;
				return result;
			}
			if (srcChannel != 128)
			{
				return result;
			}
			result = 128;
			return result;
		case 4:
			result = 4;
			return result;
		case 8:
			result = 8;
			return result;
		}
		goto IL_2C;
	}

	public static bool IsContainChannel(int dstChannels, int channel)
	{
		return (dstChannels & channel) > 0;
	}

	public static string GetChannelIcon(int channel)
	{
		if (channel == 1)
		{
			return "new_pindao_shijie";
		}
		if (channel == 2)
		{
			return "new_pindao_juntuan";
		}
		if (channel == 4)
		{
			return "new_pindao_siliao";
		}
		if (channel == 8 || channel == 64)
		{
			return "new_pindao_xitong";
		}
		if (channel == 32)
		{
			return "new_pindao_duiwu";
		}
		if (channel == 128)
		{
			return "new_pindao_zudui";
		}
		return "new_pindao_shijie";
	}
}
