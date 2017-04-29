using System;

namespace GameData
{
	public class CfgFormulaBuilder<T> where T : struct
	{
		public static CfgFormula<T> Build(CfgFormulaType ty, AttrCoder coder)
		{
			return CfgFormulaBuilder<T>.Build(ty, default(T), coder);
		}

		public static CfgFormula<T> Build(CfgFormulaType type, T initValue, AttrCoder coder)
		{
			switch (type)
			{
			case CfgFormulaType.AddAccum:
				return new CfgFormulaAddAccum<T>(initValue, coder);
			case CfgFormulaType.NegAmend:
				return new CfgFormulaNegAmend<T>(initValue, coder);
			case CfgFormulaType.MulAccum:
				return new CfgFormulaMulAccum<T>(initValue, coder);
			default:
				throw new Exception("Not supported CfgFormulaType: " + type);
			}
		}
	}
}
