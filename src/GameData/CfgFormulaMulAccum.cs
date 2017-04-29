using System;
using System.Collections.Generic;

namespace GameData
{
	public class CfgFormulaMulAccum<T> : CfgFormula<T> where T : struct
	{
		private T fakeVal;

		private List<long> args = new List<long>();

		private AttrCoder attrCoder;

		private int index;

		private List<long> tempArgs = new List<long>();

		private double tempVal;

		public override T Val
		{
			get
			{
				return this.val;
			}
			set
			{
				this.args.Clear();
				this.args.Add(CfgFormula<T>.ValueToLong(value));
				this.Update();
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

		public CfgFormulaMulAccum()
		{
			this.attrCoder = new AttrCoder();
			this.val = default(T);
		}

		public CfgFormulaMulAccum(T initArg, AttrCoder coder)
		{
			this.attrCoder = coder;
			this.args.Add(CfgFormula<T>.ValueToLong(initArg));
			this.Update();
		}

		public override void AddArg(T arg)
		{
			this.args.Add(CfgFormula<T>.ValueToLong(arg));
			this.Update();
		}

		public override void RemoveArg(T arg)
		{
			long num = CfgFormula<T>.ValueToLong(arg);
			this.index = 0;
			while (this.index < this.args.get_Count())
			{
				if (this.args.get_Item(this.index) == num)
				{
					this.args.RemoveAt(this.index);
					this.Update();
					break;
				}
				this.index++;
			}
		}

		private void Update()
		{
			if (this.args.get_Count() == 0)
			{
				this.val = (T)((object)Convert.ChangeType(0, typeof(T)));
			}
			else if (this.args.get_Count() == 1)
			{
				this.val = (T)((object)Convert.ChangeType(this.args.get_Item(0), typeof(T)));
			}
			else if (this.args.get_Count() < 4)
			{
				double num = 1.0;
				this.index = 0;
				while (this.index < this.args.get_Count())
				{
					num *= 1000.0 + (double)this.args.get_Item(this.index);
					this.index++;
				}
				this.index = 0;
				while (this.index < this.args.get_Count())
				{
					num *= 0.001;
					this.index++;
				}
				num *= 1000.0;
				num -= 1000.0;
				num = Math.Floor(num);
				this.val = (T)((object)Convert.ChangeType(num, typeof(T)));
			}
			else
			{
				double num2 = 1.0;
				this.index = 0;
				while (this.index < this.args.get_Count())
				{
					num2 *= 1.0 + (double)this.args.get_Item(this.index) * 0.001;
					num2 *= 100000.0;
					num2 = Math.Floor(num2);
					num2 *= 1E-05;
					this.index++;
				}
				num2 *= 1000.0;
				num2 -= 1000.0;
				num2 = Math.Floor(num2);
				this.val = (T)((object)Convert.ChangeType(num2, typeof(T)));
			}
		}

		public override T TryAddValue(T arg)
		{
			this.tempArgs.Clear();
			this.tempArgs.AddRange(this.args);
			this.tempArgs.Add(CfgFormula<T>.ValueToLong(arg));
			if (this.args.get_Count() == 1)
			{
				return (T)((object)Convert.ChangeType(this.args.get_Item(0), typeof(T)));
			}
			if (this.args.get_Count() < 4)
			{
				this.tempVal = 1.0;
				this.index = 0;
				while (this.index < this.tempArgs.get_Count())
				{
					this.tempVal *= 1000.0 + (double)this.tempArgs.get_Item(this.index);
					this.index++;
				}
				this.index = 0;
				while (this.index < this.tempArgs.get_Count())
				{
					this.tempVal *= 0.001;
					this.index++;
				}
				this.tempVal *= 1000.0;
				this.tempVal -= 1000.0;
				this.tempVal = Math.Floor(this.tempVal);
				return (T)((object)Convert.ChangeType(this.tempVal, typeof(T)));
			}
			this.tempVal = 1.0;
			this.index = 0;
			while (this.index < this.tempArgs.get_Count())
			{
				this.tempVal *= 1.0 + (double)this.tempArgs.get_Item(this.index) * 0.001;
				this.tempVal *= 100000.0;
				this.tempVal = Math.Floor(this.tempVal);
				this.tempVal *= 1E-05;
				this.index++;
			}
			this.tempVal *= 1000.0;
			this.tempVal -= 1000.0;
			this.tempVal = Math.Floor(this.tempVal);
			return (T)((object)Convert.ChangeType(this.tempVal, typeof(T)));
		}
	}
}
