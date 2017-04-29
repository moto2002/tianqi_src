using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(103), ForSend(103), ProtoContract(Name = "EliteAutoMatchRes")]
	[Serializable]
	public class EliteAutoMatchRes : IExtensible
	{
		public static readonly short OP = 103;

		private int _copyId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "copyId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
