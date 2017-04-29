using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(652), ForSend(652), ProtoContract(Name = "ExploreBlockReq")]
	[Serializable]
	public class ExploreBlockReq : IExtensible
	{
		public static readonly short OP = 652;

		private string _sourceBlockId;

		private string _targetBlockId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sourceBlockId", DataFormat = DataFormat.Default)]
		public string sourceBlockId
		{
			get
			{
				return this._sourceBlockId;
			}
			set
			{
				this._sourceBlockId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "targetBlockId", DataFormat = DataFormat.Default)]
		public string targetBlockId
		{
			get
			{
				return this._targetBlockId;
			}
			set
			{
				this._targetBlockId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
