using System;
using System.Reflection;

namespace ProtoBuf
{
	[AttributeUsage]
	public class ProtoMemberAttribute : Attribute, IComparable, IComparable<ProtoMemberAttribute>
	{
		internal MemberInfo Member;

		internal bool TagIsPinned;

		private string name;

		private DataFormat dataFormat;

		private int tag;

		private MemberSerializationOptions options;

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.dataFormat = value;
			}
		}

		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		public bool IsRequired
		{
			get
			{
				return (this.options & MemberSerializationOptions.Required) == MemberSerializationOptions.Required;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Required;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.Required;
				}
			}
		}

		public bool IsPacked
		{
			get
			{
				return (this.options & MemberSerializationOptions.Packed) == MemberSerializationOptions.Packed;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.Packed;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.Packed;
				}
			}
		}

		public bool OverwriteList
		{
			get
			{
				return (this.options & MemberSerializationOptions.OverwriteList) == MemberSerializationOptions.OverwriteList;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.OverwriteList;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.OverwriteList;
				}
			}
		}

		public bool AsReference
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReference) == MemberSerializationOptions.AsReference;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReference;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReference;
				}
				this.options |= MemberSerializationOptions.AsReferenceHasValue;
			}
		}

		internal bool AsReferenceHasValue
		{
			get
			{
				return (this.options & MemberSerializationOptions.AsReferenceHasValue) == MemberSerializationOptions.AsReferenceHasValue;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.AsReferenceHasValue;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.AsReferenceHasValue;
				}
			}
		}

		public bool DynamicType
		{
			get
			{
				return (this.options & MemberSerializationOptions.DynamicType) == MemberSerializationOptions.DynamicType;
			}
			set
			{
				if (value)
				{
					this.options |= MemberSerializationOptions.DynamicType;
				}
				else
				{
					this.options &= ~MemberSerializationOptions.DynamicType;
				}
			}
		}

		public MemberSerializationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		public ProtoMemberAttribute(int tag) : this(tag, false)
		{
		}

		internal ProtoMemberAttribute(int tag, bool forced)
		{
			if (tag <= 0 && !forced)
			{
				throw new ArgumentOutOfRangeException("tag");
			}
			this.tag = tag;
		}

		public int CompareTo(object other)
		{
			return this.CompareTo(other as ProtoMemberAttribute);
		}

		public int CompareTo(ProtoMemberAttribute other)
		{
			if (other == null)
			{
				return -1;
			}
			if (this == other)
			{
				return 0;
			}
			int num = this.tag.CompareTo(other.tag);
			if (num == 0)
			{
				num = string.CompareOrdinal(this.name, other.name);
			}
			return num;
		}

		internal void Rebase(int tag)
		{
			this.tag = tag;
		}
	}
}
