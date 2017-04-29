using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XianLuShengCheng")]
	[Serializable]
	public class XianLuShengCheng : IExtensible
	{
		private int _stage;

		private int _career;

		private int _begin;

		private readonly List<int> _end = new List<int>();

		private readonly List<int> _line = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "stage", DataFormat = DataFormat.TwosComplement)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "begin", DataFormat = DataFormat.TwosComplement)]
		public int begin
		{
			get
			{
				return this._begin;
			}
			set
			{
				this._begin = value;
			}
		}

		[ProtoMember(4, Name = "end", DataFormat = DataFormat.TwosComplement)]
		public List<int> end
		{
			get
			{
				return this._end;
			}
		}

		[ProtoMember(5, Name = "line", DataFormat = DataFormat.TwosComplement)]
		public List<int> line
		{
			get
			{
				return this._line;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
