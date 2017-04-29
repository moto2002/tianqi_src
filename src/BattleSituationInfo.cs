using System;

public class BattleSituationInfo
{
	public long id;

	public long num;

	public string name;

	public static int Comparition(BattleSituationInfo temp, BattleSituationInfo temp2)
	{
		if (temp.num > temp2.num)
		{
			return -1;
		}
		return 0;
	}
}
