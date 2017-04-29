using System;

namespace ProtoBuf.Meta
{
	public class TypeFormatEventArgs : EventArgs
	{
		private Type type;

		private string formattedName;

		private readonly bool typeFixed;

		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					if (this.typeFixed)
					{
						throw new InvalidOperationException("The type is fixed and cannot be changed");
					}
					this.type = value;
				}
			}
		}

		public string FormattedName
		{
			get
			{
				return this.formattedName;
			}
			set
			{
				if (this.formattedName != value)
				{
					if (!this.typeFixed)
					{
						throw new InvalidOperationException("The formatted-name is fixed and cannot be changed");
					}
					this.formattedName = value;
				}
			}
		}

		internal TypeFormatEventArgs(string formattedName)
		{
			if (Helpers.IsNullOrEmpty(formattedName))
			{
				throw new ArgumentNullException("formattedName");
			}
			this.formattedName = formattedName;
		}

		internal TypeFormatEventArgs(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			this.typeFixed = true;
		}
	}
}
