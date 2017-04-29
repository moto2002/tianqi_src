using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(545), ForSend(545), ProtoContract(Name = "ResRefineBody")]
	[Serializable]
	public class ResRefineBody : IExtensible
	{
		public static readonly short OP = 545;

		private int _curExp;

		private int _stage;

		private readonly List<BrightPoint> _brightPoint = new List<BrightPoint>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "curExp", DataFormat = DataFormat.ZigZag)]
		public int curExp
		{
			get
			{
				return this._curExp;
			}
			set
			{
				this._curExp = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "stage", DataFormat = DataFormat.ZigZag)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(3, Name = "brightPoint", DataFormat = DataFormat.Default)]
		public List<BrightPoint> brightPoint
		{
			get
			{
				return this._brightPoint;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
