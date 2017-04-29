using System;
using System.Collections.Generic;

namespace GameData
{
	public class CfgFormulaNegAmend<T> : CfgFormula<T> where T : struct
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
				this.AddArg(value);
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

		public CfgFormulaNegAmend()
		{
			this.attrCoder = new AttrCoder();
			this.val = default(T);
		}

		public CfgFormulaNegAmend(T initArg, AttrCoder coder)
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
			double num = 0.0;
			this.index = 0;
			while (this.index < this.args.get_Count())
			{
				if ((double)this.args.get_Item(this.index) * 0.001 >= 1.0)
				{
					num = 1.0;
					break;
				}
				if (this.args.get_Item(this.index) != 0L)
				{
					num += 1.0 / (1.0 - (double)this.args.get_Item(this.index) * 0.001) - 1.0;
					num *= 100000.0;
					num = Math.Floor(num);
					num *= 1E-05;
				}
				this.index++;
			}
			num = 1.0 - 1.0 / (1.0 + num);
			num *= 1000.0;
			num = Math.Floor(num);
			this.val = (T)((object)Convert.ChangeType(num, typeof(T)));
		}

		public override T TryAddValue(T arg)
		{
			this.tempVal = 0.0;
			this.tempArgs.Clear();
			this.tempArgs.AddRange(this.args);
			this.tempArgs.Add(CfgFormula<T>.ValueToLong(arg));
			this.index = 0;
			while (this.index < this.tempArgs.get_Count())
			{
				if ((double)this.tempArgs.get_Item(this.index) * 0.001 >= 1.0)
				{
					this.tempVal = 1.0;
					break;
				}
				if (this.tempArgs.get_Item(this.index) != 0L)
				{
					this.tempVal += 1.0 / (1.0 - (double)this.tempArgs.get_Item(this.index) * 0.001) - 1.0;
					this.tempVal *= 100000.0;
					this.tempVal = Math.Floor(this.tempVal);
					this.tempVal *= 1E-05;
				}
				this.index++;
			}
			this.tempVal = 1.0 - 1.0 / (1.0 + this.tempVal);
			this.tempVal *= 1000.0;
			this.tempVal = Math.Floor(this.tempVal);
			return (T)((object)Convert.ChangeType(this.tempVal, typeof(T)));
		}
	}
}
