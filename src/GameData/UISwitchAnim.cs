using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "UISwitchAnim")]
	[Serializable]
	public class UISwitchAnim : IExtensible
	{
		private int _id;

		private int _ShowAnim;

		private int _HideAnim;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "ShowAnim", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ShowAnim
		{
			get
			{
				return this._ShowAnim;
			}
			set
			{
				this._ShowAnim = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "HideAnim", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HideAnim
		{
			get
			{
				return this._HideAnim;
			}
			set
			{
				this._HideAnim = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
