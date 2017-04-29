using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(846), ForSend(846), ProtoContract(Name = "DefendFightUpdateNty")]
	[Serializable]
	public class DefendFightUpdateNty : IExtensible
	{
		public static readonly short OP = 846;

		private bool _open;

		private DefendFightMode.DFMD _mode;

		private readonly List<int> _dungeonIds = new List<int>();

		private int _countDown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "open", DataFormat = DataFormat.Default)]
		public bool open
		{
			get
			{
				return this._open;
			}
			set
			{
				this._open = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "mode", DataFormat = DataFormat.TwosComplement)]
		public DefendFightMode.DFMD mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		[ProtoMember(3, Name = "dungeonIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> dungeonIds
		{
			get
			{
				return this._dungeonIds;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "countDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countDown
		{
			get
			{
				return this._countDown;
			}
			set
			{
				this._countDown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
