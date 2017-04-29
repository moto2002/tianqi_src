using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(168), ForSend(168), ProtoContract(Name = "TutorialMsg")]
	[Serializable]
	public class TutorialMsg : IExtensible
	{
		[ProtoContract(Name = "Info")]
		[Serializable]
		public class Info : IExtensible
		{
			private string _brief_info;

			private string _info;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "brief_info", DataFormat = DataFormat.Default)]
			public string brief_info
			{
				get
				{
					return this._brief_info;
				}
				set
				{
					this._brief_info = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
			public string info
			{
				get
				{
					return this._info;
				}
				set
				{
					this._info = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 168;

		private int _id;

		private TutorialMsg.Info _info;

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

		[ProtoMember(2, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public TutorialMsg.Info info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
