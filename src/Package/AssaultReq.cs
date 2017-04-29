using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(449), ForSend(449), ProtoContract(Name = "AssaultReq")]
	[Serializable]
	public class AssaultReq : IExtensible
	{
		public static readonly short OP = 449;

		private Pos _curPos;

		private Vector2 _curVector;

		private Pos _toPos;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "curPos", DataFormat = DataFormat.Default)]
		public Pos curPos
		{
			get
			{
				return this._curPos;
			}
			set
			{
				this._curPos = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "curVector", DataFormat = DataFormat.Default)]
		public Vector2 curVector
		{
			get
			{
				return this._curVector;
			}
			set
			{
				this._curVector = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "toPos", DataFormat = DataFormat.Default)]
		public Pos toPos
		{
			get
			{
				return this._toPos;
			}
			set
			{
				this._toPos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
