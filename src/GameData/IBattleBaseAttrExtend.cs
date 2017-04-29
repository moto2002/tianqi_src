using System;

namespace GameData
{
	internal interface IBattleBaseAttrExtend : ISimpleBaseAttrExtend, IStandardBaseAttrExtend
	{
		BuffCtrlAttrs GetBuffCtrlAttrs(int elementType);

		void SetBuffCtrlAttrs(BuffCtrlAttrs attrs);
	}
}
