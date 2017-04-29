using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(6266), ForSend(6266), ProtoContract(Name = "UpdateAwardPush")]
	[Serializable]
	public class UpdateAwardPush : IExtensible
	{
		public static readonly short OP = 6266;

		private readonly List<UpdateAcInfo> _ac = new List<UpdateAcInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "ac", DataFormat = DataFormat.Default)]
		public List<UpdateAcInfo> ac
		{
			get
			{
				return this._ac;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
