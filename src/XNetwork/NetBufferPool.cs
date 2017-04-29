using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XNetwork
{
	public class NetBufferPool
	{
		protected static NetBufferPool instance;

		protected Queue<NetBuffer> pool = new Queue<NetBuffer>();

		protected bool enableRecycling = true;

		public static NetBufferPool Instance
		{
			get
			{
				if (NetBufferPool.instance == null)
				{
					NetBufferPool.instance = new NetBufferPool();
				}
				return NetBufferPool.instance;
			}
		}

		public bool EnableRecycling
		{
			get
			{
				return this.enableRecycling;
			}
			set
			{
				this.enableRecycling = value;
			}
		}

		protected NetBufferPool()
		{
		}

		public void Init()
		{
			this.EnableRecycling = true;
		}

		public void Release()
		{
			this.EnableRecycling = false;
			Queue<NetBuffer> queue = this.pool;
			lock (queue)
			{
				this.EnableRecycling = false;
				while (this.pool.get_Count() > 0)
				{
					this.pool.Dequeue().Dispose();
				}
				this.pool.Clear();
			}
		}

		public NetBuffer GetNetBuffer()
		{
			Queue<NetBuffer> queue = this.pool;
			NetBuffer result;
			lock (queue)
			{
				NetBuffer netBuffer = (this.pool.get_Count() > 0) ? this.pool.Dequeue() : NetBuffer.TrueGetNetBuffer();
				result = netBuffer;
			}
			return result;
		}

		public void Recycle(NetBuffer netBuffer)
		{
			if (netBuffer == null)
			{
				return;
			}
			Queue<NetBuffer> queue = this.pool;
			lock (queue)
			{
				this.pool.Enqueue(netBuffer);
			}
		}

		protected static string CreateStackTrace(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(str);
			stringBuilder.Append("\n");
			StackTrace stackTrace = new StackTrace(true);
			StackFrame[] frames = stackTrace.GetFrames();
			for (int i = 0; i < frames.Length; i++)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					frames[i].GetFileName(),
					"__",
					frames[i].GetFileLineNumber(),
					"__",
					frames[i].GetMethod().get_Name(),
					"\n"
				}));
			}
			stringBuilder.Append("=========================================");
			return stringBuilder.ToString();
		}
	}
}
