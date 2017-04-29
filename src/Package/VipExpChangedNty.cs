using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5614), ForSend(5614), ProtoContract(Name = "VipExpChangedNty")]
	[Serializable]
	public class VipExpChangedNty : IExtensible
	{
		public static readonly short OP = 5614;

		private float _vipExp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "vipExp", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float vipExp
		{
			get
			{
				return this._vipExp;
			}
			set
			{
				this._vipExp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
