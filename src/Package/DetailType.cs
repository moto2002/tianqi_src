using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DetailType")]
	[Serializable]
	public class DetailType : IExtensible
	{
		[ProtoContract(Name = "DT")]
		public enum DT
		{
			[ProtoEnum(Name = "Equipment", Value = 0)]
			Equipment,
			[ProtoEnum(Name = "Pet", Value = 1)]
			Pet,
			[ProtoEnum(Name = "Fashion", Value = 2)]
			Fashion,
			[ProtoEnum(Name = "Audio", Value = 3)]
			Audio,
			[ProtoEnum(Name = "Face", Value = 4)]
			Face,
			[ProtoEnum(Name = "Role", Value = 5)]
			Role,
			[ProtoEnum(Name = "UI", Value = 6)]
			UI,
			[ProtoEnum(Name = "Guild", Value = 7)]
			Guild,
			[ProtoEnum(Name = "System", Value = 8)]
			System,
			[ProtoEnum(Name = "Interface", Value = 9)]
			Interface,
			[ProtoEnum(Name = "GuildQuestionNotice", Value = 701)]
			GuildQuestionNotice = 701,
			[ProtoEnum(Name = "GuildQuestion", Value = 702)]
			GuildQuestion,
			[ProtoEnum(Name = "GuildRightAnswer", Value = 703)]
			GuildRightAnswer,
			[ProtoEnum(Name = "Default", Value = 255)]
			Default = 255
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
