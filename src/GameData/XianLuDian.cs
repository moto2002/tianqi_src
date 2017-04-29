using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XianLuDian")]
	[Serializable]
	public class XianLuDian : IExtensible
	{
		private int _points;

		private readonly List<int> _coordinate = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "points", DataFormat = DataFormat.TwosComplement)]
		public int points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
			}
		}

		[ProtoMember(2, Name = "coordinate", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinate
		{
			get
			{
				return this._coordinate;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
