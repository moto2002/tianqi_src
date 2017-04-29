using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XianLuZuoBiao")]
	[Serializable]
	public class XianLuZuoBiao : IExtensible
	{
		private int _lineId;

		private readonly List<int> _line = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "lineId", DataFormat = DataFormat.TwosComplement)]
		public int lineId
		{
			get
			{
				return this._lineId;
			}
			set
			{
				this._lineId = value;
			}
		}

		[ProtoMember(2, Name = "line", DataFormat = DataFormat.TwosComplement)]
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
