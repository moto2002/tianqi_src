using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8273), ForSend(8273), ProtoContract(Name = "ResultProtectFightNty")]
	[Serializable]
	public class ResultProtectFightNty : IExtensible
	{
		public static readonly short OP = 8273;

		private bool _isWin;

		private readonly List<DropItem> _item = new List<DropItem>();

		private int _identityFlag;

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

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "identityFlag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int identityFlag
		{
			get
			{
				return this._identityFlag;
			}
			set
			{
				this._identityFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
