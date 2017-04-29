using System;
using System.IO;

namespace ProtoBuf
{
	public sealed class BufferExtension : IExtension
	{
		private byte[] buffer;

		int IExtension.GetLength()
		{
			return (this.buffer != null) ? this.buffer.Length : 0;
		}

		Stream IExtension.BeginAppend()
		{
			return new MemoryStream();
		}

		void IExtension.EndAppend(Stream stream, bool commit)
		{
			try
			{
				int num;
				if (commit && (num = (int)stream.get_Length()) > 0)
				{
					MemoryStream memoryStream = (MemoryStream)stream;
					if (this.buffer == null)
					{
						this.buffer = memoryStream.ToArray();
					}
					else
					{
						int num2 = this.buffer.Length;
						byte[] to = new byte[num2 + num];
						Helpers.BlockCopy(this.buffer, 0, to, 0, num2);
						Helpers.BlockCopy(memoryStream.GetBuffer(), 0, to, num2, num);
						this.buffer = to;
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		Stream IExtension.BeginQuery()
		{
			return (this.buffer != null) ? new MemoryStream(this.buffer) : Stream.Null;
		}

		void IExtension.EndQuery(Stream stream)
		{
			try
			{
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}
	}
}
