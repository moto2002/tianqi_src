using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Spine
{
	public class SkeletonBinary
	{
		public const int TIMELINE_SCALE = 0;

		public const int TIMELINE_ROTATE = 1;

		public const int TIMELINE_TRANSLATE = 2;

		public const int TIMELINE_ATTACHMENT = 3;

		public const int TIMELINE_COLOR = 4;

		public const int TIMELINE_FLIPX = 5;

		public const int TIMELINE_FLIPY = 6;

		public const int CURVE_LINEAR = 0;

		public const int CURVE_STEPPED = 1;

		public const int CURVE_BEZIER = 2;

		private AttachmentLoader attachmentLoader;

		private char[] chars = new char[32];

		private byte[] buffer = new byte[4];

		public float Scale
		{
			get;
			set;
		}

		public SkeletonBinary(params Atlas[] atlasArray) : this(new AtlasAttachmentLoader(atlasArray))
		{
		}

		public SkeletonBinary(AttachmentLoader attachmentLoader)
		{
			if (attachmentLoader == null)
			{
				throw new ArgumentNullException("attachmentLoader cannot be null.");
			}
			this.attachmentLoader = attachmentLoader;
			this.Scale = 1f;
		}

		public SkeletonData ReadSkeletonData(string path)
		{
			SkeletonData result;
			using (BufferedStream bufferedStream = new BufferedStream(new FileStream(path, 3)))
			{
				SkeletonData skeletonData = this.ReadSkeletonData(bufferedStream);
				skeletonData.name = Path.GetFileNameWithoutExtension(path);
				result = skeletonData;
			}
			return result;
		}

		public SkeletonData ReadSkeletonData(Stream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input cannot be null.");
			}
			float scale = this.Scale;
			SkeletonData skeletonData = new SkeletonData();
			skeletonData.hash = this.ReadString(input);
			if (skeletonData.hash.get_Length() == 0)
			{
				skeletonData.hash = null;
			}
			skeletonData.version = this.ReadString(input);
			if (skeletonData.version.get_Length() == 0)
			{
				skeletonData.version = null;
			}
			skeletonData.width = this.ReadFloat(input);
			skeletonData.height = this.ReadFloat(input);
			bool flag = this.ReadBoolean(input);
			if (flag)
			{
				skeletonData.imagesPath = this.ReadString(input);
				if (skeletonData.imagesPath.get_Length() == 0)
				{
					skeletonData.imagesPath = null;
				}
			}
			int i = 0;
			int num = this.ReadInt(input, true);
			while (i < num)
			{
				string name = this.ReadString(input);
				BoneData parent = null;
				int num2 = this.ReadInt(input, true) - 1;
				if (num2 != -1)
				{
					parent = skeletonData.bones.Items[num2];
				}
				BoneData boneData = new BoneData(name, parent);
				boneData.x = this.ReadFloat(input) * scale;
				boneData.y = this.ReadFloat(input) * scale;
				boneData.scaleX = this.ReadFloat(input);
				boneData.scaleY = this.ReadFloat(input);
				boneData.rotation = this.ReadFloat(input);
				boneData.length = this.ReadFloat(input) * scale;
				boneData.flipX = this.ReadBoolean(input);
				boneData.flipY = this.ReadBoolean(input);
				boneData.inheritScale = this.ReadBoolean(input);
				boneData.inheritRotation = this.ReadBoolean(input);
				if (flag)
				{
					this.ReadInt(input);
				}
				skeletonData.bones.Add(boneData);
				i++;
			}
			int j = 0;
			int num3 = this.ReadInt(input, true);
			while (j < num3)
			{
				IkConstraintData ikConstraintData = new IkConstraintData(this.ReadString(input));
				int k = 0;
				int num4 = this.ReadInt(input, true);
				while (k < num4)
				{
					ikConstraintData.bones.Add(skeletonData.bones.Items[this.ReadInt(input, true)]);
					k++;
				}
				ikConstraintData.target = skeletonData.bones.Items[this.ReadInt(input, true)];
				ikConstraintData.mix = this.ReadFloat(input);
				ikConstraintData.bendDirection = (int)this.ReadSByte(input);
				skeletonData.ikConstraints.Add(ikConstraintData);
				j++;
			}
			int l = 0;
			int num5 = this.ReadInt(input, true);
			while (l < num5)
			{
				string name2 = this.ReadString(input);
				BoneData boneData2 = skeletonData.bones.Items[this.ReadInt(input, true)];
				SlotData slotData = new SlotData(name2, boneData2);
				int num6 = this.ReadInt(input);
				slotData.r = (float)(((long)num6 & (long)((ulong)-16777216)) >> 24) / 255f;
				slotData.g = (float)((num6 & 16711680) >> 16) / 255f;
				slotData.b = (float)((num6 & 65280) >> 8) / 255f;
				slotData.a = (float)(num6 & 255) / 255f;
				slotData.attachmentName = this.ReadString(input);
				slotData.blendMode = (BlendMode)this.ReadInt(input, true);
				skeletonData.slots.Add(slotData);
				l++;
			}
			Skin skin = this.ReadSkin(input, "default", flag);
			if (skin != null)
			{
				skeletonData.defaultSkin = skin;
				skeletonData.skins.Add(skin);
			}
			int m = 0;
			int num7 = this.ReadInt(input, true);
			while (m < num7)
			{
				skeletonData.skins.Add(this.ReadSkin(input, this.ReadString(input), flag));
				m++;
			}
			int n = 0;
			int num8 = this.ReadInt(input, true);
			while (n < num8)
			{
				EventData eventData = new EventData(this.ReadString(input));
				eventData.Int = this.ReadInt(input, false);
				eventData.Float = this.ReadFloat(input);
				eventData.String = this.ReadString(input);
				skeletonData.events.Add(eventData);
				n++;
			}
			int num9 = 0;
			int num10 = this.ReadInt(input, true);
			while (num9 < num10)
			{
				this.ReadAnimation(this.ReadString(input), input, skeletonData);
				num9++;
			}
			skeletonData.bones.TrimExcess();
			skeletonData.slots.TrimExcess();
			skeletonData.skins.TrimExcess();
			skeletonData.events.TrimExcess();
			skeletonData.animations.TrimExcess();
			skeletonData.ikConstraints.TrimExcess();
			return skeletonData;
		}

		[DebuggerHidden]
		public IEnumerator ReadSkeletonDataAsync(Stream input, Action<SkeletonData> finishCallback)
		{
			SkeletonBinary.<ReadSkeletonDataAsync>c__Iterator4 <ReadSkeletonDataAsync>c__Iterator = new SkeletonBinary.<ReadSkeletonDataAsync>c__Iterator4();
			<ReadSkeletonDataAsync>c__Iterator.input = input;
			<ReadSkeletonDataAsync>c__Iterator.finishCallback = finishCallback;
			<ReadSkeletonDataAsync>c__Iterator.<$>input = input;
			<ReadSkeletonDataAsync>c__Iterator.<$>finishCallback = finishCallback;
			<ReadSkeletonDataAsync>c__Iterator.<>f__this = this;
			return <ReadSkeletonDataAsync>c__Iterator;
		}

		private Skin ReadSkin(Stream input, string skinName, bool nonessential)
		{
			int num = this.ReadInt(input, true);
			if (num == 0)
			{
				return null;
			}
			Skin skin = new Skin(skinName);
			for (int i = 0; i < num; i++)
			{
				int slotIndex = this.ReadInt(input, true);
				int j = 0;
				int num2 = this.ReadInt(input, true);
				while (j < num2)
				{
					string text = this.ReadString(input);
					skin.AddAttachment(slotIndex, text, this.ReadAttachment(input, skin, text, nonessential));
					j++;
				}
			}
			return skin;
		}

		private Attachment ReadAttachment(Stream input, Skin skin, string attachmentName, bool nonessential)
		{
			float scale = this.Scale;
			string text = this.ReadString(input);
			if (text == null)
			{
				text = attachmentName;
			}
			switch (input.ReadByte())
			{
			case 0:
			{
				string text2 = this.ReadString(input);
				if (text2 == null)
				{
					text2 = text;
				}
				RegionAttachment regionAttachment = this.attachmentLoader.NewRegionAttachment(skin, text, text2);
				if (regionAttachment == null)
				{
					return null;
				}
				regionAttachment.Path = text2;
				regionAttachment.x = this.ReadFloat(input) * scale;
				regionAttachment.y = this.ReadFloat(input) * scale;
				regionAttachment.scaleX = this.ReadFloat(input);
				regionAttachment.scaleY = this.ReadFloat(input);
				regionAttachment.rotation = this.ReadFloat(input);
				regionAttachment.width = this.ReadFloat(input) * scale;
				regionAttachment.height = this.ReadFloat(input) * scale;
				int num = this.ReadInt(input);
				regionAttachment.r = (float)(((long)num & (long)((ulong)-16777216)) >> 24) / 255f;
				regionAttachment.g = (float)((num & 16711680) >> 16) / 255f;
				regionAttachment.b = (float)((num & 65280) >> 8) / 255f;
				regionAttachment.a = (float)(num & 255) / 255f;
				regionAttachment.UpdateOffset();
				return regionAttachment;
			}
			case 1:
			{
				BoundingBoxAttachment boundingBoxAttachment = this.attachmentLoader.NewBoundingBoxAttachment(skin, text);
				if (boundingBoxAttachment == null)
				{
					return null;
				}
				boundingBoxAttachment.vertices = this.ReadFloatArray(input, scale);
				return boundingBoxAttachment;
			}
			case 2:
			{
				string text3 = this.ReadString(input);
				if (text3 == null)
				{
					text3 = text;
				}
				MeshAttachment meshAttachment = this.attachmentLoader.NewMeshAttachment(skin, text, text3);
				if (meshAttachment == null)
				{
					return null;
				}
				meshAttachment.Path = text3;
				meshAttachment.regionUVs = this.ReadFloatArray(input, 1f);
				meshAttachment.triangles = this.ReadShortArray(input);
				meshAttachment.vertices = this.ReadFloatArray(input, scale);
				meshAttachment.UpdateUVs();
				int num2 = this.ReadInt(input);
				meshAttachment.r = (float)(((long)num2 & (long)((ulong)-16777216)) >> 24) / 255f;
				meshAttachment.g = (float)((num2 & 16711680) >> 16) / 255f;
				meshAttachment.b = (float)((num2 & 65280) >> 8) / 255f;
				meshAttachment.a = (float)(num2 & 255) / 255f;
				meshAttachment.HullLength = this.ReadInt(input, true) * 2;
				if (nonessential)
				{
					meshAttachment.Edges = this.ReadIntArray(input);
					meshAttachment.Width = this.ReadFloat(input) * scale;
					meshAttachment.Height = this.ReadFloat(input) * scale;
				}
				return meshAttachment;
			}
			case 3:
			{
				string text4 = this.ReadString(input);
				if (text4 == null)
				{
					text4 = text;
				}
				SkinnedMeshAttachment skinnedMeshAttachment = this.attachmentLoader.NewSkinnedMeshAttachment(skin, text, text4);
				if (skinnedMeshAttachment == null)
				{
					return null;
				}
				skinnedMeshAttachment.Path = text4;
				float[] array = this.ReadFloatArray(input, 1f);
				int[] triangles = this.ReadShortArray(input);
				int num3 = this.ReadInt(input, true);
				List<float> list = new List<float>(array.Length * 3 * 3);
				List<int> list2 = new List<int>(array.Length * 3);
				for (int i = 0; i < num3; i++)
				{
					int num4 = (int)this.ReadFloat(input);
					list2.Add(num4);
					int num5 = i + num4 * 4;
					while (i < num5)
					{
						list2.Add((int)this.ReadFloat(input));
						list.Add(this.ReadFloat(input) * scale);
						list.Add(this.ReadFloat(input) * scale);
						list.Add(this.ReadFloat(input));
						i += 4;
					}
				}
				skinnedMeshAttachment.bones = list2.ToArray();
				skinnedMeshAttachment.weights = list.ToArray();
				skinnedMeshAttachment.triangles = triangles;
				skinnedMeshAttachment.regionUVs = array;
				skinnedMeshAttachment.UpdateUVs();
				int num6 = this.ReadInt(input);
				skinnedMeshAttachment.r = (float)(((long)num6 & (long)((ulong)-16777216)) >> 24) / 255f;
				skinnedMeshAttachment.g = (float)((num6 & 16711680) >> 16) / 255f;
				skinnedMeshAttachment.b = (float)((num6 & 65280) >> 8) / 255f;
				skinnedMeshAttachment.a = (float)(num6 & 255) / 255f;
				skinnedMeshAttachment.HullLength = this.ReadInt(input, true) * 2;
				if (nonessential)
				{
					skinnedMeshAttachment.Edges = this.ReadIntArray(input);
					skinnedMeshAttachment.Width = this.ReadFloat(input) * scale;
					skinnedMeshAttachment.Height = this.ReadFloat(input) * scale;
				}
				return skinnedMeshAttachment;
			}
			default:
				return null;
			}
		}

		private float[] ReadFloatArray(Stream input, float scale)
		{
			int num = this.ReadInt(input, true);
			float[] array = new float[num];
			if (scale == 1f)
			{
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadFloat(input);
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					array[j] = this.ReadFloat(input) * scale;
				}
			}
			return array;
		}

		private int[] ReadShortArray(Stream input)
		{
			int num = this.ReadInt(input, true);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (input.ReadByte() << 8) + input.ReadByte();
			}
			return array;
		}

		private int[] ReadIntArray(Stream input)
		{
			int num = this.ReadInt(input, true);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadInt(input, true);
			}
			return array;
		}

		private void ReadAnimation(string name, Stream input, SkeletonData skeletonData)
		{
			ExposedList<Timeline> exposedList = new ExposedList<Timeline>();
			float scale = this.Scale;
			float num = 0f;
			int i = 0;
			int num2 = this.ReadInt(input, true);
			while (i < num2)
			{
				int slotIndex = this.ReadInt(input, true);
				int j = 0;
				int num3 = this.ReadInt(input, true);
				while (j < num3)
				{
					int num4 = input.ReadByte();
					int num5 = this.ReadInt(input, true);
					int num6 = num4;
					if (num6 != 3)
					{
						if (num6 == 4)
						{
							ColorTimeline colorTimeline = new ColorTimeline(num5);
							colorTimeline.slotIndex = slotIndex;
							for (int k = 0; k < num5; k++)
							{
								float time = this.ReadFloat(input);
								int num7 = this.ReadInt(input);
								float r = (float)(((long)num7 & (long)((ulong)-16777216)) >> 24) / 255f;
								float g = (float)((num7 & 16711680) >> 16) / 255f;
								float b = (float)((num7 & 65280) >> 8) / 255f;
								float a = (float)(num7 & 255) / 255f;
								colorTimeline.SetFrame(k, time, r, g, b, a);
								if (k < num5 - 1)
								{
									this.ReadCurve(input, k, colorTimeline);
								}
							}
							exposedList.Add(colorTimeline);
							num = Math.Max(num, colorTimeline.frames[num5 * 5 - 5]);
						}
					}
					else
					{
						AttachmentTimeline attachmentTimeline = new AttachmentTimeline(num5);
						attachmentTimeline.slotIndex = slotIndex;
						for (int l = 0; l < num5; l++)
						{
							attachmentTimeline.SetFrame(l, this.ReadFloat(input), this.ReadString(input));
						}
						exposedList.Add(attachmentTimeline);
						num = Math.Max(num, attachmentTimeline.frames[num5 - 1]);
					}
					j++;
				}
				i++;
			}
			int m = 0;
			int num8 = this.ReadInt(input, true);
			while (m < num8)
			{
				int boneIndex = this.ReadInt(input, true);
				int n = 0;
				int num9 = this.ReadInt(input, true);
				while (n < num9)
				{
					int num10 = input.ReadByte();
					int num11 = this.ReadInt(input, true);
					switch (num10)
					{
					case 0:
					case 2:
					{
						float num12 = 1f;
						TranslateTimeline translateTimeline;
						if (num10 == 0)
						{
							translateTimeline = new ScaleTimeline(num11);
						}
						else
						{
							translateTimeline = new TranslateTimeline(num11);
							num12 = scale;
						}
						translateTimeline.boneIndex = boneIndex;
						for (int num13 = 0; num13 < num11; num13++)
						{
							translateTimeline.SetFrame(num13, this.ReadFloat(input), this.ReadFloat(input) * num12, this.ReadFloat(input) * num12);
							if (num13 < num11 - 1)
							{
								this.ReadCurve(input, num13, translateTimeline);
							}
						}
						exposedList.Add(translateTimeline);
						num = Math.Max(num, translateTimeline.frames[num11 * 3 - 3]);
						break;
					}
					case 1:
					{
						RotateTimeline rotateTimeline = new RotateTimeline(num11);
						rotateTimeline.boneIndex = boneIndex;
						for (int num14 = 0; num14 < num11; num14++)
						{
							rotateTimeline.SetFrame(num14, this.ReadFloat(input), this.ReadFloat(input));
							if (num14 < num11 - 1)
							{
								this.ReadCurve(input, num14, rotateTimeline);
							}
						}
						exposedList.Add(rotateTimeline);
						num = Math.Max(num, rotateTimeline.frames[num11 * 2 - 2]);
						break;
					}
					case 5:
					case 6:
					{
						FlipXTimeline flipXTimeline = (num10 != 5) ? new FlipYTimeline(num11) : new FlipXTimeline(num11);
						flipXTimeline.boneIndex = boneIndex;
						for (int num15 = 0; num15 < num11; num15++)
						{
							flipXTimeline.SetFrame(num15, this.ReadFloat(input), this.ReadBoolean(input));
						}
						exposedList.Add(flipXTimeline);
						num = Math.Max(num, flipXTimeline.frames[num11 * 2 - 2]);
						break;
					}
					}
					n++;
				}
				m++;
			}
			int num16 = 0;
			int num17 = this.ReadInt(input, true);
			while (num16 < num17)
			{
				IkConstraintData item = skeletonData.ikConstraints.Items[this.ReadInt(input, true)];
				int num18 = this.ReadInt(input, true);
				IkConstraintTimeline ikConstraintTimeline = new IkConstraintTimeline(num18);
				ikConstraintTimeline.ikConstraintIndex = skeletonData.ikConstraints.IndexOf(item);
				for (int num19 = 0; num19 < num18; num19++)
				{
					ikConstraintTimeline.SetFrame(num19, this.ReadFloat(input), this.ReadFloat(input), (int)this.ReadSByte(input));
					if (num19 < num18 - 1)
					{
						this.ReadCurve(input, num19, ikConstraintTimeline);
					}
				}
				exposedList.Add(ikConstraintTimeline);
				num = Math.Max(num, ikConstraintTimeline.frames[num18 * 3 - 3]);
				num16++;
			}
			int num20 = 0;
			int num21 = this.ReadInt(input, true);
			while (num20 < num21)
			{
				Skin skin = skeletonData.skins.Items[this.ReadInt(input, true)];
				int num22 = 0;
				int num23 = this.ReadInt(input, true);
				while (num22 < num23)
				{
					int slotIndex2 = this.ReadInt(input, true);
					int num24 = 0;
					int num25 = this.ReadInt(input, true);
					while (num24 < num25)
					{
						Attachment attachment = skin.GetAttachment(slotIndex2, this.ReadString(input));
						int num26 = this.ReadInt(input, true);
						FFDTimeline fFDTimeline = new FFDTimeline(num26);
						fFDTimeline.slotIndex = slotIndex2;
						fFDTimeline.attachment = attachment;
						for (int num27 = 0; num27 < num26; num27++)
						{
							float time2 = this.ReadFloat(input);
							int num28;
							if (attachment is MeshAttachment)
							{
								num28 = ((MeshAttachment)attachment).vertices.Length;
							}
							else
							{
								num28 = ((SkinnedMeshAttachment)attachment).weights.Length / 3 * 2;
							}
							int num29 = this.ReadInt(input, true);
							float[] array;
							if (num29 == 0)
							{
								if (attachment is MeshAttachment)
								{
									array = ((MeshAttachment)attachment).vertices;
								}
								else
								{
									array = new float[num28];
								}
							}
							else
							{
								array = new float[num28];
								int num30 = this.ReadInt(input, true);
								num29 += num30;
								if (scale == 1f)
								{
									for (int num31 = num30; num31 < num29; num31++)
									{
										array[num31] = this.ReadFloat(input);
									}
								}
								else
								{
									for (int num32 = num30; num32 < num29; num32++)
									{
										array[num32] = this.ReadFloat(input) * scale;
									}
								}
								if (attachment is MeshAttachment)
								{
									float[] vertices = ((MeshAttachment)attachment).vertices;
									int num33 = 0;
									int num34 = array.Length;
									while (num33 < num34)
									{
										array[num33] += vertices[num33];
										num33++;
									}
								}
							}
							fFDTimeline.SetFrame(num27, time2, array);
							if (num27 < num26 - 1)
							{
								this.ReadCurve(input, num27, fFDTimeline);
							}
						}
						exposedList.Add(fFDTimeline);
						num = Math.Max(num, fFDTimeline.frames[num26 - 1]);
						num24++;
					}
					num22++;
				}
				num20++;
			}
			int num35 = this.ReadInt(input, true);
			if (num35 > 0)
			{
				DrawOrderTimeline drawOrderTimeline = new DrawOrderTimeline(num35);
				int count = skeletonData.slots.Count;
				for (int num36 = 0; num36 < num35; num36++)
				{
					int num37 = this.ReadInt(input, true);
					int[] array2 = new int[count];
					for (int num38 = count - 1; num38 >= 0; num38--)
					{
						array2[num38] = -1;
					}
					int[] array3 = new int[count - num37];
					int num39 = 0;
					int num40 = 0;
					for (int num41 = 0; num41 < num37; num41++)
					{
						int num42 = this.ReadInt(input, true);
						while (num39 != num42)
						{
							array3[num40++] = num39++;
						}
						array2[num39 + this.ReadInt(input, true)] = num39++;
					}
					while (num39 < count)
					{
						array3[num40++] = num39++;
					}
					for (int num43 = count - 1; num43 >= 0; num43--)
					{
						if (array2[num43] == -1)
						{
							array2[num43] = array3[--num40];
						}
					}
					drawOrderTimeline.SetFrame(num36, this.ReadFloat(input), array2);
				}
				exposedList.Add(drawOrderTimeline);
				num = Math.Max(num, drawOrderTimeline.frames[num35 - 1]);
			}
			int num44 = this.ReadInt(input, true);
			if (num44 > 0)
			{
				EventTimeline eventTimeline = new EventTimeline(num44);
				for (int num45 = 0; num45 < num44; num45++)
				{
					float time3 = this.ReadFloat(input);
					EventData eventData = skeletonData.events.Items[this.ReadInt(input, true)];
					eventTimeline.SetFrame(num45, time3, new Event(eventData)
					{
						Int = this.ReadInt(input, false),
						Float = this.ReadFloat(input),
						String = (!this.ReadBoolean(input)) ? eventData.String : this.ReadString(input)
					});
				}
				exposedList.Add(eventTimeline);
				num = Math.Max(num, eventTimeline.frames[num44 - 1]);
			}
			exposedList.TrimExcess();
			skeletonData.animations.Add(new Animation(name, exposedList, num));
		}

		private void ReadCurve(Stream input, int frameIndex, CurveTimeline timeline)
		{
			int num = input.ReadByte();
			if (num != 1)
			{
				if (num == 2)
				{
					timeline.SetCurve(frameIndex, this.ReadFloat(input), this.ReadFloat(input), this.ReadFloat(input), this.ReadFloat(input));
				}
			}
			else
			{
				timeline.SetStepped(frameIndex);
			}
		}

		private sbyte ReadSByte(Stream input)
		{
			int num = input.ReadByte();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			return (sbyte)num;
		}

		private bool ReadBoolean(Stream input)
		{
			return input.ReadByte() != 0;
		}

		private float ReadFloat(Stream input)
		{
			this.buffer[3] = (byte)input.ReadByte();
			this.buffer[2] = (byte)input.ReadByte();
			this.buffer[1] = (byte)input.ReadByte();
			this.buffer[0] = (byte)input.ReadByte();
			return BitConverter.ToSingle(this.buffer, 0);
		}

		private int ReadInt(Stream input)
		{
			return (input.ReadByte() << 24) + (input.ReadByte() << 16) + (input.ReadByte() << 8) + input.ReadByte();
		}

		private int ReadInt(Stream input, bool optimizePositive)
		{
			int num = input.ReadByte();
			int num2 = num & 127;
			if ((num & 128) != 0)
			{
				num = input.ReadByte();
				num2 |= (num & 127) << 7;
				if ((num & 128) != 0)
				{
					num = input.ReadByte();
					num2 |= (num & 127) << 14;
					if ((num & 128) != 0)
					{
						num = input.ReadByte();
						num2 |= (num & 127) << 21;
						if ((num & 128) != 0)
						{
							num = input.ReadByte();
							num2 |= (num & 127) << 28;
						}
					}
				}
			}
			return (!optimizePositive) ? (num2 >> 1 ^ -(num2 & 1)) : num2;
		}

		private string ReadString(Stream input)
		{
			int num = this.ReadInt(input, true);
			int num2 = num;
			if (num2 == 0)
			{
				return null;
			}
			if (num2 != 1)
			{
				num--;
				char[] array = this.chars;
				if (array.Length < num)
				{
					array = (this.chars = new char[num]);
				}
				int i = 0;
				int num3 = 0;
				while (i < num)
				{
					num3 = input.ReadByte();
					if (num3 > 127)
					{
						break;
					}
					array[i++] = (char)num3;
				}
				if (i < num)
				{
					this.ReadUtf8_slow(input, num, i, num3);
				}
				return new string(array, 0, num);
			}
			return string.Empty;
		}

		private void ReadUtf8_slow(Stream input, int charCount, int charIndex, int b)
		{
			char[] array = this.chars;
			while (true)
			{
				switch (b >> 4)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					array[charIndex] = (char)b;
					break;
				case 12:
				case 13:
					array[charIndex] = (char)((b & 31) << 6 | (input.ReadByte() & 63));
					break;
				case 14:
					array[charIndex] = (char)((b & 15) << 12 | (input.ReadByte() & 63) << 6 | (input.ReadByte() & 63));
					break;
				}
				if (++charIndex >= charCount)
				{
					break;
				}
				b = (input.ReadByte() & 255);
			}
		}
	}
}
