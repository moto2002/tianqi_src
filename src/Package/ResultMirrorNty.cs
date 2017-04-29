using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(585), ForSend(585), ProtoContract(Name = "ResultMirrorNty")]
	[Serializable]
	public class ResultMirrorNty : IExtensible
	{
		public static readonly short OP = 585;

		private bool _isWin;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isWin", DataFormat = DataFormat.Default)]
		public bool isWin
		{
			get
			{
				return this._isWin;
			}
			set
			{
				this._isWin = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
