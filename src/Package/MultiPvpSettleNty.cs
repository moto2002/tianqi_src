using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(165), ForSend(165), ProtoContract(Name = "MultiPvpSettleNty")]
	[Serializable]
	public class MultiPvpSettleNty : IExtensible
	{
		public static readonly short OP = 165;

		private bool _isWin;

		private readonly List<MultiPvpRoleInfo> _infoList = new List<MultiPvpRoleInfo>();

		private readonly List<DropItem> _rewards = new List<DropItem>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "isWin", DataFormat = DataFormat.Default)]
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

		[ProtoMember(1, Name = "infoList", DataFormat = DataFormat.Default)]
		public List<MultiPvpRoleInfo> infoList
		{
			get
			{
				return this._infoList;
			}
		}

		[ProtoMember(3, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<DropItem> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
