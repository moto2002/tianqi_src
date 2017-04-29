using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuanLianXiTongRenWu")]
	[Serializable]
	public class GuanLianXiTongRenWu : IExtensible
	{
		private int _id;

		private int _linkSystem;

		private int _completeTime;

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

		[ProtoMember(3, IsRequired = false, Name = "linkSystem", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkSystem
		{
			get
			{
				return this._linkSystem;
			}
			set
			{
				this._linkSystem = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "completeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeTime
		{
			get
			{
				return this._completeTime;
			}
			set
			{
				this._completeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
