using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5442), ForSend(5442), ProtoContract(Name = "ChangeCareerRes")]
	[Serializable]
	public class ChangeCareerRes : IExtensible
	{
		public static readonly short OP = 5442;

		private int _dstCareer;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dstCareer", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dstCareer
		{
			get
			{
				return this._dstCareer;
			}
			set
			{
				this._dstCareer = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
