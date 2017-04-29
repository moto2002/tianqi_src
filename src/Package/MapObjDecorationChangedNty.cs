using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1089), ForSend(1089), ProtoContract(Name = "MapObjDecorationChangedNty")]
	[Serializable]
	public class MapObjDecorationChangedNty : IExtensible
	{
		public static readonly short OP = 1089;

		private long _objId;

		private MapObjDecorations _decorations;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "objId", DataFormat = DataFormat.TwosComplement)]
		public long objId
		{
			get
			{
				return this._objId;
			}
			set
			{
				this._objId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "decorations", DataFormat = DataFormat.Default)]
		public MapObjDecorations decorations
		{
			get
			{
				return this._decorations;
			}
			set
			{
				this._decorations = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
