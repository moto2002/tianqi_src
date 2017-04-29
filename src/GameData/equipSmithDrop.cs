using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "equipSmithDrop")]
	[Serializable]
	public class equipSmithDrop : IExtensible
	{
		private int _id;

		private int _equipStep;

		private int _position;

		private int _dropId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "equipStep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipStep
		{
			get
			{
				return this._equipStep;
			}
			set
			{
				this._equipStep = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "dropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dropId
		{
			get
			{
				return this._dropId;
			}
			set
			{
				this._dropId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
