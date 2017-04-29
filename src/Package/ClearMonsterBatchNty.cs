using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(875), ForSend(875), ProtoContract(Name = "ClearMonsterBatchNty")]
	[Serializable]
	public class ClearMonsterBatchNty : IExtensible
	{
		public static readonly short OP = 875;

		private bool _isBossBatch;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isBossBatch", DataFormat = DataFormat.Default)]
		public bool isBossBatch
		{
			get
			{
				return this._isBossBatch;
			}
			set
			{
				this._isBossBatch = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
