using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "ChengHaoXianShi")]
	[Serializable]
	public class ChengHaoXianShi : IExtensible
	{
		private string _area;

		private string _myName;

		private string _otherName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "area", DataFormat = DataFormat.Default)]
		public string area
		{
			get
			{
				return this._area;
			}
			set
			{
				this._area = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "myName", DataFormat = DataFormat.Default)]
		public string myName
		{
			get
			{
				return this._myName;
			}
			set
			{
				this._myName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "otherName", DataFormat = DataFormat.Default)]
		public string otherName
		{
			get
			{
				return this._otherName;
			}
			set
			{
				this._otherName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
