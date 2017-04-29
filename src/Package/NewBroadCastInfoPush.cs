using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(839), ForSend(839), ProtoContract(Name = "NewBroadCastInfoPush")]
	[Serializable]
	public class NewBroadCastInfoPush : IExtensible
	{
		public static readonly short OP = 839;

		private int _id;

		private bool _isAllScene;

		private readonly List<DetailInfo> _paramters = new List<DetailInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "isAllScene", DataFormat = DataFormat.Default)]
		public bool isAllScene
		{
			get
			{
				return this._isAllScene;
			}
			set
			{
				this._isAllScene = value;
			}
		}

		[ProtoMember(3, Name = "paramters", DataFormat = DataFormat.Default)]
		public List<DetailInfo> paramters
		{
			get
			{
				return this._paramters;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
