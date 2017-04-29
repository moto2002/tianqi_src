using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.IO;

namespace BsDiff
{
	public class BinaryPatchUtility
	{
		private const long c_fileSignature = 3473478480300364610L;

		private const int c_headerSize = 32;

		public static void Create(byte[] oldData, byte[] newData, Stream output)
		{
			if (oldData == null)
			{
				throw new ArgumentNullException("oldData");
			}
			if (newData == null)
			{
				throw new ArgumentNullException("newData");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!output.get_CanSeek())
			{
				throw new ArgumentException("Output stream must be seekable.", "output");
			}
			if (!output.get_CanWrite())
			{
				throw new ArgumentException("Output stream must be writable.", "output");
			}
			byte[] array = new byte[32];
			BinaryPatchUtility.WriteInt64(3473478480300364610L, array, 0);
			BinaryPatchUtility.WriteInt64(0L, array, 8);
			BinaryPatchUtility.WriteInt64(0L, array, 16);
			BinaryPatchUtility.WriteInt64((long)newData.Length, array, 24);
			long position = output.get_Position();
			output.Write(array, 0, array.Length);
			int[] i = BinaryPatchUtility.SuffixSort(oldData);
			byte[] array2 = new byte[newData.Length];
			byte[] array3 = new byte[newData.Length];
			int num = 0;
			int num2 = 0;
			BZip2OutputStream bZip2OutputStream = new BZip2OutputStream(output);
			bZip2OutputStream.set_IsStreamOwner(false);
			using (BZip2OutputStream bZip2OutputStream2 = bZip2OutputStream)
			{
				int j = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				while (j < newData.Length)
				{
					int num8 = 0;
					int k;
					for (j = (k = j + num4); j < newData.Length; j++)
					{
						num4 = BinaryPatchUtility.Search(i, oldData, newData, j, 0, oldData.Length, out num3);
						while (k < j + num4)
						{
							if (k + num7 < oldData.Length && oldData[k + num7] == newData[k])
							{
								num8++;
							}
							k++;
						}
						if ((num4 == num8 && num4 != 0) || num4 > num8 + 8)
						{
							break;
						}
						if (j + num7 < oldData.Length && oldData[j + num7] == newData[j])
						{
							num8--;
						}
					}
					if (num4 != num8 || j == newData.Length)
					{
						int num9 = 0;
						int num10 = 0;
						int num11 = 0;
						int num12 = 0;
						while (num5 + num12 < j && num6 + num12 < oldData.Length)
						{
							if (oldData[num6 + num12] == newData[num5 + num12])
							{
								num9++;
							}
							num12++;
							if (num9 * 2 - num12 > num10 * 2 - num11)
							{
								num10 = num9;
								num11 = num12;
							}
						}
						int num13 = 0;
						if (j < newData.Length)
						{
							num9 = 0;
							int num14 = 0;
							int num15 = 1;
							while (j >= num5 + num15 && num3 >= num15)
							{
								if (oldData[num3 - num15] == newData[j - num15])
								{
									num9++;
								}
								if (num9 * 2 - num15 > num14 * 2 - num13)
								{
									num14 = num9;
									num13 = num15;
								}
								num15++;
							}
						}
						if (num5 + num11 > j - num13)
						{
							int num16 = num5 + num11 - (j - num13);
							num9 = 0;
							int num17 = 0;
							int num18 = 0;
							for (int l = 0; l < num16; l++)
							{
								if (newData[num5 + num11 - num16 + l] == oldData[num6 + num11 - num16 + l])
								{
									num9++;
								}
								if (newData[j - num13 + l] == oldData[num3 - num13 + l])
								{
									num9--;
								}
								if (num9 > num17)
								{
									num17 = num9;
									num18 = l + 1;
								}
							}
							num11 += num18 - num16;
							num13 -= num18;
						}
						for (int m = 0; m < num11; m++)
						{
							array2[num + m] = newData[num5 + m] - oldData[num6 + m];
						}
						for (int n = 0; n < j - num13 - (num5 + num11); n++)
						{
							array3[num2 + n] = newData[num5 + num11 + n];
						}
						num += num11;
						num2 += j - num13 - (num5 + num11);
						byte[] array4 = new byte[8];
						BinaryPatchUtility.WriteInt64((long)num11, array4, 0);
						bZip2OutputStream2.Write(array4, 0, 8);
						BinaryPatchUtility.WriteInt64((long)(j - num13 - (num5 + num11)), array4, 0);
						bZip2OutputStream2.Write(array4, 0, 8);
						BinaryPatchUtility.WriteInt64((long)(num3 - num13 - (num6 + num11)), array4, 0);
						bZip2OutputStream2.Write(array4, 0, 8);
						num5 = j - num13;
						num6 = num3 - num13;
						num7 = num3 - j;
					}
				}
			}
			long position2 = output.get_Position();
			BinaryPatchUtility.WriteInt64(position2 - position - 32L, array, 8);
			bZip2OutputStream = new BZip2OutputStream(output);
			bZip2OutputStream.set_IsStreamOwner(false);
			using (BZip2OutputStream bZip2OutputStream3 = bZip2OutputStream)
			{
				bZip2OutputStream3.Write(array2, 0, num);
			}
			long position3 = output.get_Position();
			BinaryPatchUtility.WriteInt64(position3 - position2, array, 16);
			bZip2OutputStream = new BZip2OutputStream(output);
			bZip2OutputStream.set_IsStreamOwner(false);
			using (BZip2OutputStream bZip2OutputStream4 = bZip2OutputStream)
			{
				bZip2OutputStream4.Write(array3, 0, num2);
			}
			long position4 = output.get_Position();
			output.set_Position(position);
			output.Write(array, 0, array.Length);
			output.set_Position(position4);
		}

		public static void Apply(Stream input, Func<Stream> openPatchStream, Stream output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (openPatchStream == null)
			{
				throw new ArgumentNullException("openPatchStream");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			long num2;
			long num3;
			long num4;
			using (Stream stream = openPatchStream.Invoke())
			{
				if (!stream.get_CanRead())
				{
					throw new ArgumentException("Patch stream must be readable.", "openPatchStream");
				}
				if (!stream.get_CanSeek())
				{
					throw new ArgumentException("Patch stream must be seekable.", "openPatchStream");
				}
				byte[] buf = BinaryPatchUtility.ReadExactly(stream, 32);
				long num = BinaryPatchUtility.ReadInt64(buf, 0);
				if (num != 3473478480300364610L)
				{
					throw new InvalidOperationException("Corrupt patch.");
				}
				num2 = BinaryPatchUtility.ReadInt64(buf, 8);
				num3 = BinaryPatchUtility.ReadInt64(buf, 16);
				num4 = BinaryPatchUtility.ReadInt64(buf, 24);
				if (num2 < 0L || num3 < 0L || num4 < 0L)
				{
					throw new InvalidOperationException("Corrupt patch.");
				}
			}
			byte[] array = new byte[1048576];
			byte[] array2 = new byte[1048576];
			using (Stream stream2 = openPatchStream.Invoke())
			{
				using (Stream stream3 = openPatchStream.Invoke())
				{
					using (Stream stream4 = openPatchStream.Invoke())
					{
						stream2.Seek(32L, 1);
						stream3.Seek(32L + num2, 1);
						stream4.Seek(32L + num2 + num3, 1);
						using (BZip2InputStream bZip2InputStream = new BZip2InputStream(stream2))
						{
							using (BZip2InputStream bZip2InputStream2 = new BZip2InputStream(stream3))
							{
								using (BZip2InputStream bZip2InputStream3 = new BZip2InputStream(stream4))
								{
									long[] array3 = new long[3];
									byte[] array4 = new byte[8];
									int num5 = 0;
									int num6 = 0;
									while ((long)num6 < num4)
									{
										for (int i = 0; i < 3; i++)
										{
											BinaryPatchUtility.ReadExactly(bZip2InputStream, array4, 0, 8);
											array3[i] = BinaryPatchUtility.ReadInt64(array4, 0);
										}
										if ((long)num6 + array3[0] > num4)
										{
											throw new InvalidOperationException("Corrupt patch.");
										}
										input.set_Position((long)num5);
										int num7;
										for (int j = (int)array3[0]; j > 0; j -= num7)
										{
											num7 = Math.Min(j, 1048576);
											BinaryPatchUtility.ReadExactly(bZip2InputStream2, array, 0, num7);
											int num8 = Math.Min(num7, (int)(input.get_Length() - input.get_Position()));
											BinaryPatchUtility.ReadExactly(input, array2, 0, num8);
											for (int k = 0; k < num8; k++)
											{
												byte[] expr_22A_cp_0 = array;
												int expr_22A_cp_1 = k;
												expr_22A_cp_0[expr_22A_cp_1] += array2[k];
											}
											output.Write(array, 0, num7);
											num6 += num7;
											num5 += num7;
										}
										if ((long)num6 + array3[1] > num4)
										{
											throw new InvalidOperationException("Corrupt patch.");
										}
										int num9;
										for (int j = (int)array3[1]; j > 0; j -= num9)
										{
											num9 = Math.Min(j, 1048576);
											BinaryPatchUtility.ReadExactly(bZip2InputStream3, array, 0, num9);
											output.Write(array, 0, num9);
											num6 += num9;
										}
										num5 = (int)((long)num5 + array3[2]);
									}
								}
							}
						}
					}
				}
			}
		}

		private static int CompareBytes(byte[] left, int leftOffset, byte[] right, int rightOffset)
		{
			int num = 0;
			while (num < left.Length - leftOffset && num < right.Length - rightOffset)
			{
				int num2 = (int)(left[num + leftOffset] - right[num + rightOffset]);
				if (num2 != 0)
				{
					return num2;
				}
				num++;
			}
			return 0;
		}

		private static int MatchLength(byte[] oldData, int oldOffset, byte[] newData, int newOffset)
		{
			int num = 0;
			while (num < oldData.Length - oldOffset && num < newData.Length - newOffset)
			{
				if (oldData[num + oldOffset] != newData[num + newOffset])
				{
					break;
				}
				num++;
			}
			return num;
		}

		private static int Search(int[] I, byte[] oldData, byte[] newData, int newOffset, int start, int end, out int pos)
		{
			if (end - start >= 2)
			{
				int num = start + (end - start) / 2;
				return (BinaryPatchUtility.CompareBytes(oldData, I[num], newData, newOffset) >= 0) ? BinaryPatchUtility.Search(I, oldData, newData, newOffset, start, num, out pos) : BinaryPatchUtility.Search(I, oldData, newData, newOffset, num, end, out pos);
			}
			int num2 = BinaryPatchUtility.MatchLength(oldData, I[start], newData, newOffset);
			int num3 = BinaryPatchUtility.MatchLength(oldData, I[end], newData, newOffset);
			if (num2 > num3)
			{
				pos = I[start];
				return num2;
			}
			pos = I[end];
			return num3;
		}

		private static void Split(int[] I, int[] v, int start, int len, int h)
		{
			if (len < 16)
			{
				int num;
				for (int i = start; i < start + len; i += num)
				{
					num = 1;
					int num2 = v[I[i] + h];
					int num3 = 1;
					while (i + num3 < start + len)
					{
						if (v[I[i + num3] + h] < num2)
						{
							num2 = v[I[i + num3] + h];
							num = 0;
						}
						if (v[I[i + num3] + h] == num2)
						{
							BinaryPatchUtility.Swap(ref I[i + num], ref I[i + num3]);
							num++;
						}
						num3++;
					}
					for (int j = 0; j < num; j++)
					{
						v[I[i + j]] = i + num - 1;
					}
					if (num == 1)
					{
						I[i] = -1;
					}
				}
			}
			else
			{
				int num4 = v[I[start + len / 2] + h];
				int num5 = 0;
				int num6 = 0;
				for (int k = start; k < start + len; k++)
				{
					if (v[I[k] + h] < num4)
					{
						num5++;
					}
					if (v[I[k] + h] == num4)
					{
						num6++;
					}
				}
				num5 += start;
				num6 += num5;
				int l = start;
				int num7 = 0;
				int num8 = 0;
				while (l < num5)
				{
					if (v[I[l] + h] < num4)
					{
						l++;
					}
					else if (v[I[l] + h] == num4)
					{
						BinaryPatchUtility.Swap(ref I[l], ref I[num5 + num7]);
						num7++;
					}
					else
					{
						BinaryPatchUtility.Swap(ref I[l], ref I[num6 + num8]);
						num8++;
					}
				}
				while (num5 + num7 < num6)
				{
					if (v[I[num5 + num7] + h] == num4)
					{
						num7++;
					}
					else
					{
						BinaryPatchUtility.Swap(ref I[num5 + num7], ref I[num6 + num8]);
						num8++;
					}
				}
				if (num5 > start)
				{
					BinaryPatchUtility.Split(I, v, start, num5 - start, h);
				}
				for (l = 0; l < num6 - num5; l++)
				{
					v[I[num5 + l]] = num6 - 1;
				}
				if (num5 == num6 - 1)
				{
					I[num5] = -1;
				}
				if (start + len > num6)
				{
					BinaryPatchUtility.Split(I, v, num6, start + len - num6, h);
				}
			}
		}

		private static int[] SuffixSort(byte[] oldData)
		{
			int[] array = new int[256];
			for (int i = 0; i < oldData.Length; i++)
			{
				byte b = oldData[i];
				array[(int)b]++;
			}
			for (int j = 1; j < 256; j++)
			{
				array[j] += array[j - 1];
			}
			for (int k = 255; k > 0; k--)
			{
				array[k] = array[k - 1];
			}
			array[0] = 0;
			int[] array2 = new int[oldData.Length + 1];
			for (int l = 0; l < oldData.Length; l++)
			{
				array2[++array[(int)oldData[l]]] = l;
			}
			int[] array3 = new int[oldData.Length + 1];
			for (int m = 0; m < oldData.Length; m++)
			{
				array3[m] = array[(int)oldData[m]];
			}
			for (int n = 1; n < 256; n++)
			{
				if (array[n] == array[n - 1] + 1)
				{
					array2[array[n]] = -1;
				}
			}
			array2[0] = -1;
			int num = 1;
			while (array2[0] != -(oldData.Length + 1))
			{
				int num2 = 0;
				int num3 = 0;
				while (num3 < oldData.Length + 1)
				{
					if (array2[num3] < 0)
					{
						num2 -= array2[num3];
						num3 -= array2[num3];
					}
					else
					{
						if (num2 != 0)
						{
							array2[num3 - num2] = -num2;
						}
						num2 = array3[array2[num3]] + 1 - num3;
						BinaryPatchUtility.Split(array2, array3, num3, num2, num);
						num3 += num2;
						num2 = 0;
					}
				}
				if (num2 != 0)
				{
					array2[num3 - num2] = -num2;
				}
				num += num;
			}
			for (int num4 = 0; num4 < oldData.Length + 1; num4++)
			{
				array2[array3[num4]] = num4;
			}
			return array2;
		}

		private static void Swap(ref int first, ref int second)
		{
			int num = first;
			first = second;
			second = num;
		}

		private static long ReadInt64(byte[] buf, int offset)
		{
			long num = (long)(buf[offset + 7] & 127);
			for (int i = 6; i >= 0; i--)
			{
				num *= 256L;
				num += (long)buf[offset + i];
			}
			if ((buf[offset + 7] & 128) != 0)
			{
				num = -num;
			}
			return num;
		}

		private static void WriteInt64(long value, byte[] buf, int offset)
		{
			long num = (value >= 0L) ? value : (-value);
			for (int i = 0; i < 8; i++)
			{
				buf[offset + i] = (byte)num;
				num >>= 8;
			}
			if (value < 0L)
			{
				int expr_3F_cp_1 = offset + 7;
				buf[expr_3F_cp_1] |= 128;
			}
		}

		private static byte[] ReadExactly(Stream stream, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			byte[] array = new byte[count];
			BinaryPatchUtility.ReadExactly(stream, array, 0, count);
			return array;
		}

		private static void ReadExactly(Stream stream, byte[] buffer, int offset, int count)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				int num = stream.Read(buffer, offset, count);
				if (num == 0)
				{
					throw new EndOfStreamException();
				}
				offset += num;
				count -= num;
			}
		}
	}
}
