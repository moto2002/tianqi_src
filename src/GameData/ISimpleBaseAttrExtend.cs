using System;

namespace GameData
{
	internal interface ISimpleBaseAttrExtend
	{
		void SetValue(AttrType type, int value, bool isFirstTry);

		void SetValue(AttrType type, long value, bool isFirstTry);

		long GetValue(AttrType type);

		void OnAttrChanged(AttrType type, long oldValue, long newValue);
	}
}
