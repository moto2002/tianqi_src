using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "KuangChePinZhi")]
	[Serializable]
	public class KuangChePinZhi : IExtensible
	{
		private int _quality;

		private int _weight;

		private int _id;

		private int _parament;

		private int _name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "parament", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parament
		{
			get
			{
				return this._parament;
			}
			set
			{
				this._parament = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
