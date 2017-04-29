using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3820), ForSend(3820), ProtoContract(Name = "SyncHttpInfoToPy")]
	[Serializable]
	public class SyncHttpInfoToPy : IExtensible
	{
		public static readonly short OP = 3820;

		private int _succ;

		private string _detailInfo;

		private int _sdkType;

		private string _url;

		private string _postFields;

		private int _requestType;

		private string _postBack;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "succ", DataFormat = DataFormat.TwosComplement)]
		public int succ
		{
			get
			{
				return this._succ;
			}
			set
			{
				this._succ = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "detailInfo", DataFormat = DataFormat.Default)]
		public string detailInfo
		{
			get
			{
				return this._detailInfo;
			}
			set
			{
				this._detailInfo = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "sdkType", DataFormat = DataFormat.TwosComplement)]
		public int sdkType
		{
			get
			{
				return this._sdkType;
			}
			set
			{
				this._sdkType = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "url", DataFormat = DataFormat.Default)]
		public string url
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "postFields", DataFormat = DataFormat.Default)]
		public string postFields
		{
			get
			{
				return this._postFields;
			}
			set
			{
				this._postFields = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "requestType", DataFormat = DataFormat.TwosComplement)]
		public int requestType
		{
			get
			{
				return this._requestType;
			}
			set
			{
				this._requestType = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "postBack", DataFormat = DataFormat.Default)]
		public string postBack
		{
			get
			{
				return this._postBack;
			}
			set
			{
				this._postBack = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
