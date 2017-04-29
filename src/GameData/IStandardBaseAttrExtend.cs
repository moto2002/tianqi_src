using System;

namespace GameData
{
	internal interface IStandardBaseAttrExtend : ISimpleBaseAttrExtend
	{
		void AddValue(AttrType type, int value, bool isFirstTry);

		void AddValue(AttrType type, long value, bool isFirstTry);

		void RemoveValue(AttrType type, int value, bool isFirstTry);

		void RemoveValue(AttrType type, long value, bool isFirstTry);

		void SwapValue(AttrType type, long oldValue, long newValue);

		long TryAddValue(AttrType type, long tryAddValue);

		long TryAddValue(AttrType type, XDict<AttrType, long> tryAddValues);

		void AddValuesByTemplateID(int templateID);

		void RemoveValuesByTemplateID(int templateID);
	}
}
