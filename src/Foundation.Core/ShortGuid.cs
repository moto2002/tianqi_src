using System;

namespace Foundation.Core
{
	public struct ShortGuid
	{
		public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

		private Guid _guid;

		private string _value;

		public Guid Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if (value != this._guid)
				{
					this._guid = value;
					this._value = ShortGuid.Encode(value);
				}
			}
		}

		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					this._guid = ShortGuid.Decode(value);
				}
			}
		}

		public ShortGuid(string value)
		{
			this._value = value;
			this._guid = ShortGuid.Decode(value);
		}

		public ShortGuid(Guid guid)
		{
			this._value = ShortGuid.Encode(guid);
			this._guid = guid;
		}

		public override string ToString()
		{
			return this._value;
		}

		public override bool Equals(object obj)
		{
			if (obj is ShortGuid)
			{
				return this._guid.Equals(((ShortGuid)obj)._guid);
			}
			if (obj is Guid)
			{
				return this._guid.Equals((Guid)obj);
			}
			return obj is string && this._guid.Equals(((ShortGuid)obj)._guid);
		}

		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		public static ShortGuid NewGuid()
		{
			return new ShortGuid(Guid.NewGuid());
		}

		public static string Encode(string value)
		{
			Guid guid = new Guid(value);
			return ShortGuid.Encode(guid);
		}

		public static string Encode(Guid guid)
		{
			string text = Convert.ToBase64String(guid.ToByteArray());
			text = text.Replace("/", "_").Replace("+", "-");
			return text.Substring(0, 22);
		}

		public static Guid Decode(string value)
		{
			value = value.Replace("_", "/").Replace("-", "+");
			byte[] array = Convert.FromBase64String(value + "==");
			return new Guid(array);
		}

		public static bool operator ==(ShortGuid x, ShortGuid y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x._guid == y._guid;
		}

		public static bool operator !=(ShortGuid x, ShortGuid y)
		{
			return !(x == y);
		}

		public static implicit operator string(ShortGuid shortGuid)
		{
			return shortGuid._value;
		}

		public static implicit operator Guid(ShortGuid shortGuid)
		{
			return shortGuid._guid;
		}

		public static implicit operator ShortGuid(string shortGuid)
		{
			return new ShortGuid(shortGuid);
		}

		public static implicit operator ShortGuid(Guid guid)
		{
			return new ShortGuid(guid);
		}
	}
}
