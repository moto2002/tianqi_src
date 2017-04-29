using System;

public class IcmpPacket
{
	public byte Type;

	public byte SubCode;

	public ushort CheckSum;

	public ushort Identifier;

	public ushort SequenceNumber;

	public byte[] Data;
}
