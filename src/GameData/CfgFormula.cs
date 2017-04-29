using System;

namespace GameData
{
	public abstract class CfgFormula<T> where T : struct
	{
		protected string typeName = string.Empty;

		public abstract T Val
		{
			get;
			set;
		}

		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		public CfgFormula()
		{
			this.typeName = typeof(T).get_Name();
		}

		public CfgFormula(T initVal)
		{
			this.typeName = typeof(T).get_Name();
		}

		public abstract void AddArg(T arg);

		public abstract void RemoveArg(T arg);

		public abstract T TryAddValue(T arg);

		protected static long ValueToLong(T value)
		{
			string name = value.GetType().get_Name();
			if (name == "Int32")
			{
				return (long)((int)((object)value));
			}
			if (name == "Int64")
			{
				return (long)((object)value);
			}
			return (long)((object)value);
		}
	}
}
