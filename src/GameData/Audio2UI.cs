using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Audio2UI")]
	[Serializable]
	public class Audio2UI : IExtensible
	{
		private int _id;

		private int _widgetId;

		private int _audioId;

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

		[ProtoMember(3, IsRequired = false, Name = "widgetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int widgetId
		{
			get
			{
				return this._widgetId;
			}
			set
			{
				this._widgetId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "audioId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int audioId
		{
			get
			{
				return this._audioId;
			}
			set
			{
				this._audioId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
