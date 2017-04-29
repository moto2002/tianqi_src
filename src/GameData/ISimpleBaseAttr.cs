using System;

namespace GameData
{
	internal interface ISimpleBaseAttr
	{
		int MoveSpeed
		{
			get;
			set;
		}

		int ActSpeed
		{
			get;
			set;
		}

		int Lv
		{
			get;
			set;
		}

		long Fighting
		{
			get;
			set;
		}

		int VipLv
		{
			get;
			set;
		}
	}
}
