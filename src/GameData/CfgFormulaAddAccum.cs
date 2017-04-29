using System;

namespace GameData
{
	public class CfgFormulaAddAccum<T> : CfgFormula<T> where T : struct
	{
		private T fakeVal;

		private AttrCoder attrCoder;

		public override T Val
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		private T val
		{
			get
			{
				return this.attrCoder.Decode<T>(this.fakeVal, this.typeName);
			}
			set
			{
				this.fakeVal = this.attrCoder.Encode<T>(value, this.typeName);
			}
		}

		public CfgFormulaAddAccum()
		{
			this.attrCoder = new AttrCoder();
			this.val = default(T);
		}

		public CfgFormulaAddAccum(T initArg, AttrCoder coder)
		{
			this.attrCoder = coder;
			this.val = initArg;
		}

		public override void AddArg(T arg)
		{
			long num = CfgFormula<T>.ValueToLong(arg);
			long num2 = CfgFormula<T>.ValueToLong(this.val);
			this.val = (T)((object)Convert.ChangeType(num2 + num, typeof(T)));
		}

		public override void RemoveArg(T arg)
		{
			long num = CfgFormula<T>.ValueToLong(arg);
			long num2 = CfgFormula<T>.ValueToLong(this.val);
			this.val = (T)((object)Convert.ChangeType(num2 - num, typeof(T)));
		}

		public override T TryAddValue(T arg)
		{
			long num = CfgFormula<T>.ValueToLong(arg);
			long num2 = CfgFormula<T>.ValueToLong(this.val);
			return (T)((object)Convert.ChangeType(num2 + num, typeof(T)));
		}
	}
}
