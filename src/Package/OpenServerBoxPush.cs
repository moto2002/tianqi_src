using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7109), ForSend(7109), ProtoContract(Name = "OpenServerBoxPush")]
	[Serializable]
	public class OpenServerBoxPush : IExtensible
	{
		public static readonly short OP = 7109;

		private readonly List<ItemInfo1> _infos = new List<ItemInfo1>();

		private int _boxFlag = -1;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<ItemInfo1> infos
		{
			get
			{
				return this._infos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "boxFlag", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int boxFlag
		{
			get
			{
				return this._boxFlag;
			}
			set
			{
				this._boxFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
