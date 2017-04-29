using System;
using System.Collections.Generic;
using System.IO;

namespace Spine
{
	public class SkeletonJson
	{
		private AttachmentLoader attachmentLoader;

		public float Scale
		{
			get;
			set;
		}

		public SkeletonJson(params Atlas[] atlasArray) : this(new AtlasAttachmentLoader(atlasArray))
		{
		}

		public SkeletonJson(AttachmentLoader attachmentLoader)
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
			using (StreamReader streamReader = new StreamReader(path))
			{
				SkeletonData skeletonData = this.ReadSkeletonData(streamReader);
				skeletonData.name = Path.GetFileNameWithoutExtension(path);
				result = skeletonData;
			}
			return result;
		}

		public SkeletonData ReadSkeletonData(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader cannot be null.");
			}
			SkeletonData skeletonData = new SkeletonData();
			Dictionary<string, object> dictionary = Json.Deserialize(reader) as Dictionary<string, object>;
			if (dictionary == null)
			{
				throw new Exception("Invalid JSON.");
			}
			if (dictionary.ContainsKey("skeleton"))
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary.get_Item("skeleton");
				skeletonData.hash = (string)dictionary2.get_Item("hash");
				skeletonData.version = (string)dictionary2.get_Item("spine");
				skeletonData.width = this.GetFloat(dictionary2, "width", 0f);
				skeletonData.height = this.GetFloat(dictionary2, "height", 0f);
			}
			List<object> list = (List<object>)dictionary.get_Item("bones");
			for (int i = 0; i < list.get_Count(); i++)
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)list.get_Item(i);
				BoneData boneData = null;
				if (dictionary3.ContainsKey("parent"))
				{
					boneData = skeletonData.FindBone((string)dictionary3.get_Item("parent"));
					if (boneData == null)
					{
						throw new Exception("Parent bone not found: " + dictionary3.get_Item("parent"));
					}
				}
				BoneData boneData2 = new BoneData((string)dictionary3.get_Item("name"), boneData);
				boneData2.length = this.GetFloat(dictionary3, "length", 0f) * this.Scale;
				boneData2.x = this.GetFloat(dictionary3, "x", 0f) * this.Scale;
				boneData2.y = this.GetFloat(dictionary3, "y", 0f) * this.Scale;
				boneData2.rotation = this.GetFloat(dictionary3, "rotation", 0f);
				boneData2.scaleX = this.GetFloat(dictionary3, "scaleX", 1f);
				boneData2.scaleY = this.GetFloat(dictionary3, "scaleY", 1f);
				boneData2.flipX = this.GetBoolean(dictionary3, "flipX", false);
				boneData2.flipY = this.GetBoolean(dictionary3, "flipY", false);
				boneData2.inheritScale = this.GetBoolean(dictionary3, "inheritScale", true);
				boneData2.inheritRotation = this.GetBoolean(dictionary3, "inheritRotation", true);
				skeletonData.bones.Add(boneData2);
			}
			if (dictionary.ContainsKey("ik"))
			{
				List<object> list2 = (List<object>)dictionary.get_Item("ik");
				for (int j = 0; j < list2.get_Count(); j++)
				{
					Dictionary<string, object> dictionary4 = (Dictionary<string, object>)list2.get_Item(j);
					IkConstraintData ikConstraintData = new IkConstraintData((string)dictionary4.get_Item("name"));
					List<object> list3 = (List<object>)dictionary4.get_Item("bones");
					for (int k = 0; k < list3.get_Count(); k++)
					{
						string text = (string)list3.get_Item(k);
						BoneData boneData3 = skeletonData.FindBone(text);
						if (boneData3 == null)
						{
							throw new Exception("IK bone not found: " + text);
						}
						ikConstraintData.bones.Add(boneData3);
					}
					string text2 = (string)dictionary4.get_Item("target");
					ikConstraintData.target = skeletonData.FindBone(text2);
					if (ikConstraintData.target == null)
					{
						throw new Exception("Target bone not found: " + text2);
					}
					ikConstraintData.bendDirection = ((!this.GetBoolean(dictionary4, "bendPositive", true)) ? -1 : 1);
					ikConstraintData.mix = this.GetFloat(dictionary4, "mix", 1f);
					skeletonData.ikConstraints.Add(ikConstraintData);
				}
			}
			if (dictionary.ContainsKey("slots"))
			{
				List<object> list4 = (List<object>)dictionary.get_Item("slots");
				for (int l = 0; l < list4.get_Count(); l++)
				{
					Dictionary<string, object> dictionary5 = (Dictionary<string, object>)list4.get_Item(l);
					string name = (string)dictionary5.get_Item("name");
					string text3 = (string)dictionary5.get_Item("bone");
					BoneData boneData4 = skeletonData.FindBone(text3);
					if (boneData4 == null)
					{
						throw new Exception("Slot bone not found: " + text3);
					}
					SlotData slotData = new SlotData(name, boneData4);
					if (dictionary5.ContainsKey("color"))
					{
						string hexString = (string)dictionary5.get_Item("color");
						slotData.r = this.ToColor(hexString, 0);
						slotData.g = this.ToColor(hexString, 1);
						slotData.b = this.ToColor(hexString, 2);
						slotData.a = this.ToColor(hexString, 3);
					}
					if (dictionary5.ContainsKey("attachment"))
					{
						slotData.attachmentName = (string)dictionary5.get_Item("attachment");
					}
					if (dictionary5.ContainsKey("blend"))
					{
						slotData.blendMode = (BlendMode)((int)Enum.Parse(typeof(BlendMode), (string)dictionary5.get_Item("blend"), false));
					}
					else
					{
						slotData.blendMode = BlendMode.normal;
					}
					skeletonData.slots.Add(slotData);
				}
			}
			if (dictionary.ContainsKey("skins"))
			{
				using (Dictionary<string, object>.Enumerator enumerator = ((Dictionary<string, object>)dictionary.get_Item("skins")).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> current = enumerator.get_Current();
						Skin skin = new Skin(current.get_Key());
						using (Dictionary<string, object>.Enumerator enumerator2 = ((Dictionary<string, object>)current.get_Value()).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, object> current2 = enumerator2.get_Current();
								int slotIndex = skeletonData.FindSlotIndex(current2.get_Key());
								using (Dictionary<string, object>.Enumerator enumerator3 = ((Dictionary<string, object>)current2.get_Value()).GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										KeyValuePair<string, object> current3 = enumerator3.get_Current();
										Attachment attachment = this.ReadAttachment(skin, current3.get_Key(), (Dictionary<string, object>)current3.get_Value());
										if (attachment != null)
										{
											skin.AddAttachment(slotIndex, current3.get_Key(), attachment);
										}
									}
								}
							}
						}
						skeletonData.skins.Add(skin);
						if (skin.name == "default")
						{
							skeletonData.defaultSkin = skin;
						}
					}
				}
			}
			if (dictionary.ContainsKey("events"))
			{
				using (Dictionary<string, object>.Enumerator enumerator4 = ((Dictionary<string, object>)dictionary.get_Item("events")).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						KeyValuePair<string, object> current4 = enumerator4.get_Current();
						Dictionary<string, object> map = (Dictionary<string, object>)current4.get_Value();
						EventData eventData = new EventData(current4.get_Key());
						eventData.Int = this.GetInt(map, "int", 0);
						eventData.Float = this.GetFloat(map, "float", 0f);
						eventData.String = this.GetString(map, "string", null);
						skeletonData.events.Add(eventData);
					}
				}
			}
			if (dictionary.ContainsKey("animations"))
			{
				using (Dictionary<string, object>.Enumerator enumerator5 = ((Dictionary<string, object>)dictionary.get_Item("animations")).GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						KeyValuePair<string, object> current5 = enumerator5.get_Current();
						this.ReadAnimation(current5.get_Key(), (Dictionary<string, object>)current5.get_Value(), skeletonData);
					}
				}
			}
			skeletonData.bones.TrimExcess();
			skeletonData.slots.TrimExcess();
			skeletonData.skins.TrimExcess();
			skeletonData.events.TrimExcess();
			skeletonData.animations.TrimExcess();
			skeletonData.ikConstraints.TrimExcess();
			return skeletonData;
		}

		private Attachment ReadAttachment(Skin skin, string name, Dictionary<string, object> map)
		{
			if (map.ContainsKey("name"))
			{
				name = (string)map.get_Item("name");
			}
			AttachmentType attachmentType = AttachmentType.region;
			if (map.ContainsKey("type"))
			{
				attachmentType = (AttachmentType)((int)Enum.Parse(typeof(AttachmentType), (string)map.get_Item("type"), false));
			}
			string path = name;
			if (map.ContainsKey("path"))
			{
				path = (string)map.get_Item("path");
			}
			switch (attachmentType)
			{
			case AttachmentType.region:
			{
				RegionAttachment regionAttachment = this.attachmentLoader.NewRegionAttachment(skin, name, path);
				if (regionAttachment == null)
				{
					return null;
				}
				regionAttachment.Path = path;
				regionAttachment.x = this.GetFloat(map, "x", 0f) * this.Scale;
				regionAttachment.y = this.GetFloat(map, "y", 0f) * this.Scale;
				regionAttachment.scaleX = this.GetFloat(map, "scaleX", 1f);
				regionAttachment.scaleY = this.GetFloat(map, "scaleY", 1f);
				regionAttachment.rotation = this.GetFloat(map, "rotation", 0f);
				regionAttachment.width = this.GetFloat(map, "width", 32f) * this.Scale;
				regionAttachment.height = this.GetFloat(map, "height", 32f) * this.Scale;
				regionAttachment.UpdateOffset();
				if (map.ContainsKey("color"))
				{
					string hexString = (string)map.get_Item("color");
					regionAttachment.r = this.ToColor(hexString, 0);
					regionAttachment.g = this.ToColor(hexString, 1);
					regionAttachment.b = this.ToColor(hexString, 2);
					regionAttachment.a = this.ToColor(hexString, 3);
				}
				return regionAttachment;
			}
			case AttachmentType.boundingbox:
			{
				BoundingBoxAttachment boundingBoxAttachment = this.attachmentLoader.NewBoundingBoxAttachment(skin, name);
				if (boundingBoxAttachment == null)
				{
					return null;
				}
				boundingBoxAttachment.vertices = this.GetFloatArray(map, "vertices", this.Scale);
				return boundingBoxAttachment;
			}
			case AttachmentType.mesh:
			{
				MeshAttachment meshAttachment = this.attachmentLoader.NewMeshAttachment(skin, name, path);
				if (meshAttachment == null)
				{
					return null;
				}
				meshAttachment.Path = path;
				meshAttachment.vertices = this.GetFloatArray(map, "vertices", this.Scale);
				meshAttachment.triangles = this.GetIntArray(map, "triangles");
				meshAttachment.regionUVs = this.GetFloatArray(map, "uvs", 1f);
				meshAttachment.UpdateUVs();
				if (map.ContainsKey("color"))
				{
					string hexString2 = (string)map.get_Item("color");
					meshAttachment.r = this.ToColor(hexString2, 0);
					meshAttachment.g = this.ToColor(hexString2, 1);
					meshAttachment.b = this.ToColor(hexString2, 2);
					meshAttachment.a = this.ToColor(hexString2, 3);
				}
				meshAttachment.HullLength = this.GetInt(map, "hull", 0) * 2;
				if (map.ContainsKey("edges"))
				{
					meshAttachment.Edges = this.GetIntArray(map, "edges");
				}
				meshAttachment.Width = (float)this.GetInt(map, "width", 0) * this.Scale;
				meshAttachment.Height = (float)this.GetInt(map, "height", 0) * this.Scale;
				return meshAttachment;
			}
			case AttachmentType.skinnedmesh:
			{
				SkinnedMeshAttachment skinnedMeshAttachment = this.attachmentLoader.NewSkinnedMeshAttachment(skin, name, path);
				if (skinnedMeshAttachment == null)
				{
					return null;
				}
				skinnedMeshAttachment.Path = path;
				float[] floatArray = this.GetFloatArray(map, "uvs", 1f);
				float[] floatArray2 = this.GetFloatArray(map, "vertices", 1f);
				List<float> list = new List<float>(floatArray.Length * 3 * 3);
				List<int> list2 = new List<int>(floatArray.Length * 3);
				float scale = this.Scale;
				int i = 0;
				int num = floatArray2.Length;
				while (i < num)
				{
					int num2 = (int)floatArray2[i++];
					list2.Add(num2);
					int num3 = i + num2 * 4;
					while (i < num3)
					{
						list2.Add((int)floatArray2[i]);
						list.Add(floatArray2[i + 1] * scale);
						list.Add(floatArray2[i + 2] * scale);
						list.Add(floatArray2[i + 3]);
						i += 4;
					}
				}
				skinnedMeshAttachment.bones = list2.ToArray();
				skinnedMeshAttachment.weights = list.ToArray();
				skinnedMeshAttachment.triangles = this.GetIntArray(map, "triangles");
				skinnedMeshAttachment.regionUVs = floatArray;
				skinnedMeshAttachment.UpdateUVs();
				if (map.ContainsKey("color"))
				{
					string hexString3 = (string)map.get_Item("color");
					skinnedMeshAttachment.r = this.ToColor(hexString3, 0);
					skinnedMeshAttachment.g = this.ToColor(hexString3, 1);
					skinnedMeshAttachment.b = this.ToColor(hexString3, 2);
					skinnedMeshAttachment.a = this.ToColor(hexString3, 3);
				}
				skinnedMeshAttachment.HullLength = this.GetInt(map, "hull", 0) * 2;
				if (map.ContainsKey("edges"))
				{
					skinnedMeshAttachment.Edges = this.GetIntArray(map, "edges");
				}
				skinnedMeshAttachment.Width = (float)this.GetInt(map, "width", 0) * this.Scale;
				skinnedMeshAttachment.Height = (float)this.GetInt(map, "height", 0) * this.Scale;
				return skinnedMeshAttachment;
			}
			default:
				return null;
			}
		}

		private float[] GetFloatArray(Dictionary<string, object> map, string name, float scale)
		{
			List<object> list = (List<object>)map.get_Item(name);
			float[] array = new float[list.get_Count()];
			if (scale == 1f)
			{
				int i = 0;
				int count = list.get_Count();
				while (i < count)
				{
					array[i] = (float)list.get_Item(i);
					i++;
				}
			}
			else
			{
				int j = 0;
				int count2 = list.get_Count();
				while (j < count2)
				{
					array[j] = (float)list.get_Item(j) * scale;
					j++;
				}
			}
			return array;
		}

		private int[] GetIntArray(Dictionary<string, object> map, string name)
		{
			List<object> list = (List<object>)map.get_Item(name);
			int[] array = new int[list.get_Count()];
			int i = 0;
			int count = list.get_Count();
			while (i < count)
			{
				array[i] = (int)((float)list.get_Item(i));
				i++;
			}
			return array;
		}

		private float GetFloat(Dictionary<string, object> map, string name, float defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (float)map.get_Item(name);
		}

		private int GetInt(Dictionary<string, object> map, string name, int defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (int)((float)map.get_Item(name));
		}

		private bool GetBoolean(Dictionary<string, object> map, string name, bool defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (bool)map.get_Item(name);
		}

		private string GetString(Dictionary<string, object> map, string name, string defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (string)map.get_Item(name);
		}

		private float ToColor(string hexString, int colorIndex)
		{
			if (hexString.get_Length() != 8)
			{
				throw new ArgumentException("Color hexidecimal length must be 8, recieved: " + hexString);
			}
			return (float)Convert.ToInt32(hexString.Substring(colorIndex * 2, 2), 16) / 255f;
		}

		private void ReadAnimation(string name, Dictionary<string, object> map, SkeletonData skeletonData)
		{
			ExposedList<Timeline> exposedList = new ExposedList<Timeline>();
			float num = 0f;
			float scale = this.Scale;
			if (map.ContainsKey("slots"))
			{
				using (Dictionary<string, object>.Enumerator enumerator = ((Dictionary<string, object>)map.get_Item("slots")).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> current = enumerator.get_Current();
						string key = current.get_Key();
						int slotIndex = skeletonData.FindSlotIndex(key);
						Dictionary<string, object> dictionary = (Dictionary<string, object>)current.get_Value();
						using (Dictionary<string, object>.Enumerator enumerator2 = dictionary.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, object> current2 = enumerator2.get_Current();
								List<object> list = (List<object>)current2.get_Value();
								string key2 = current2.get_Key();
								if (key2 == "color")
								{
									ColorTimeline colorTimeline = new ColorTimeline(list.get_Count());
									colorTimeline.slotIndex = slotIndex;
									int num2 = 0;
									for (int i = 0; i < list.get_Count(); i++)
									{
										Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list.get_Item(i);
										float time = (float)dictionary2.get_Item("time");
										string hexString = (string)dictionary2.get_Item("color");
										colorTimeline.SetFrame(num2, time, this.ToColor(hexString, 0), this.ToColor(hexString, 1), this.ToColor(hexString, 2), this.ToColor(hexString, 3));
										this.ReadCurve(colorTimeline, num2, dictionary2);
										num2++;
									}
									exposedList.Add(colorTimeline);
									num = Math.Max(num, colorTimeline.frames[colorTimeline.FrameCount * 5 - 5]);
								}
								else if (key2 == "attachment")
								{
									AttachmentTimeline attachmentTimeline = new AttachmentTimeline(list.get_Count());
									attachmentTimeline.slotIndex = slotIndex;
									int num3 = 0;
									for (int j = 0; j < list.get_Count(); j++)
									{
										Dictionary<string, object> dictionary3 = (Dictionary<string, object>)list.get_Item(j);
										float time2 = (float)dictionary3.get_Item("time");
										attachmentTimeline.SetFrame(num3++, time2, (string)dictionary3.get_Item("name"));
									}
									exposedList.Add(attachmentTimeline);
									num = Math.Max(num, attachmentTimeline.frames[attachmentTimeline.FrameCount - 1]);
								}
							}
						}
					}
				}
			}
			if (map.ContainsKey("bones"))
			{
				using (Dictionary<string, object>.Enumerator enumerator3 = ((Dictionary<string, object>)map.get_Item("bones")).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						KeyValuePair<string, object> current3 = enumerator3.get_Current();
						string key3 = current3.get_Key();
						int num4 = skeletonData.FindBoneIndex(key3);
						if (num4 == -1)
						{
							throw new Exception("Bone not found: " + key3);
						}
						Dictionary<string, object> dictionary4 = (Dictionary<string, object>)current3.get_Value();
						using (Dictionary<string, object>.Enumerator enumerator4 = dictionary4.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								KeyValuePair<string, object> current4 = enumerator4.get_Current();
								List<object> list2 = (List<object>)current4.get_Value();
								string key4 = current4.get_Key();
								if (key4 == "rotate")
								{
									RotateTimeline rotateTimeline = new RotateTimeline(list2.get_Count());
									rotateTimeline.boneIndex = num4;
									int num5 = 0;
									for (int k = 0; k < list2.get_Count(); k++)
									{
										Dictionary<string, object> dictionary5 = (Dictionary<string, object>)list2.get_Item(k);
										float time3 = (float)dictionary5.get_Item("time");
										rotateTimeline.SetFrame(num5, time3, (float)dictionary5.get_Item("angle"));
										this.ReadCurve(rotateTimeline, num5, dictionary5);
										num5++;
									}
									exposedList.Add(rotateTimeline);
									num = Math.Max(num, rotateTimeline.frames[rotateTimeline.FrameCount * 2 - 2]);
								}
								else if (key4 == "translate" || key4 == "scale")
								{
									float num6 = 1f;
									TranslateTimeline translateTimeline;
									if (key4 == "scale")
									{
										translateTimeline = new ScaleTimeline(list2.get_Count());
									}
									else
									{
										translateTimeline = new TranslateTimeline(list2.get_Count());
										num6 = scale;
									}
									translateTimeline.boneIndex = num4;
									int num7 = 0;
									for (int l = 0; l < list2.get_Count(); l++)
									{
										Dictionary<string, object> dictionary6 = (Dictionary<string, object>)list2.get_Item(l);
										float time4 = (float)dictionary6.get_Item("time");
										float num8 = (!dictionary6.ContainsKey("x")) ? 0f : ((float)dictionary6.get_Item("x"));
										float num9 = (!dictionary6.ContainsKey("y")) ? 0f : ((float)dictionary6.get_Item("y"));
										translateTimeline.SetFrame(num7, time4, num8 * num6, num9 * num6);
										this.ReadCurve(translateTimeline, num7, dictionary6);
										num7++;
									}
									exposedList.Add(translateTimeline);
									num = Math.Max(num, translateTimeline.frames[translateTimeline.FrameCount * 3 - 3]);
								}
								else if (key4 == "flipX" || key4 == "flipY")
								{
									bool flag = key4 == "flipX";
									FlipXTimeline flipXTimeline = (!flag) ? new FlipYTimeline(list2.get_Count()) : new FlipXTimeline(list2.get_Count());
									flipXTimeline.boneIndex = num4;
									string text = (!flag) ? "y" : "x";
									int num10 = 0;
									for (int m = 0; m < list2.get_Count(); m++)
									{
										Dictionary<string, object> dictionary7 = (Dictionary<string, object>)list2.get_Item(m);
										float time5 = (float)dictionary7.get_Item("time");
										flipXTimeline.SetFrame(num10, time5, dictionary7.ContainsKey(text) && (bool)dictionary7.get_Item(text));
										num10++;
									}
									exposedList.Add(flipXTimeline);
									num = Math.Max(num, flipXTimeline.frames[flipXTimeline.FrameCount * 2 - 2]);
								}
							}
						}
					}
				}
			}
			if (map.ContainsKey("ik"))
			{
				using (Dictionary<string, object>.Enumerator enumerator5 = ((Dictionary<string, object>)map.get_Item("ik")).GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						KeyValuePair<string, object> current5 = enumerator5.get_Current();
						IkConstraintData item = skeletonData.FindIkConstraint(current5.get_Key());
						List<object> list3 = (List<object>)current5.get_Value();
						IkConstraintTimeline ikConstraintTimeline = new IkConstraintTimeline(list3.get_Count());
						ikConstraintTimeline.ikConstraintIndex = skeletonData.ikConstraints.IndexOf(item);
						int num11 = 0;
						for (int n = 0; n < list3.get_Count(); n++)
						{
							Dictionary<string, object> dictionary8 = (Dictionary<string, object>)list3.get_Item(n);
							float time6 = (float)dictionary8.get_Item("time");
							float mix = (!dictionary8.ContainsKey("mix")) ? 1f : ((float)dictionary8.get_Item("mix"));
							bool flag2 = !dictionary8.ContainsKey("bendPositive") || (bool)dictionary8.get_Item("bendPositive");
							ikConstraintTimeline.SetFrame(num11, time6, mix, (!flag2) ? -1 : 1);
							this.ReadCurve(ikConstraintTimeline, num11, dictionary8);
							num11++;
						}
						exposedList.Add(ikConstraintTimeline);
						num = Math.Max(num, ikConstraintTimeline.frames[ikConstraintTimeline.FrameCount * 3 - 3]);
					}
				}
			}
			if (map.ContainsKey("ffd"))
			{
				using (Dictionary<string, object>.Enumerator enumerator6 = ((Dictionary<string, object>)map.get_Item("ffd")).GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						KeyValuePair<string, object> current6 = enumerator6.get_Current();
						Skin skin = skeletonData.FindSkin(current6.get_Key());
						using (Dictionary<string, object>.Enumerator enumerator7 = ((Dictionary<string, object>)current6.get_Value()).GetEnumerator())
						{
							while (enumerator7.MoveNext())
							{
								KeyValuePair<string, object> current7 = enumerator7.get_Current();
								int slotIndex2 = skeletonData.FindSlotIndex(current7.get_Key());
								using (Dictionary<string, object>.Enumerator enumerator8 = ((Dictionary<string, object>)current7.get_Value()).GetEnumerator())
								{
									while (enumerator8.MoveNext())
									{
										KeyValuePair<string, object> current8 = enumerator8.get_Current();
										List<object> list4 = (List<object>)current8.get_Value();
										FFDTimeline fFDTimeline = new FFDTimeline(list4.get_Count());
										Attachment attachment = skin.GetAttachment(slotIndex2, current8.get_Key());
										if (attachment == null)
										{
											throw new Exception("FFD attachment not found: " + current8.get_Key());
										}
										fFDTimeline.slotIndex = slotIndex2;
										fFDTimeline.attachment = attachment;
										int num12;
										if (attachment is MeshAttachment)
										{
											num12 = ((MeshAttachment)attachment).vertices.Length;
										}
										else
										{
											num12 = ((SkinnedMeshAttachment)attachment).Weights.Length / 3 * 2;
										}
										int num13 = 0;
										for (int num14 = 0; num14 < list4.get_Count(); num14++)
										{
											Dictionary<string, object> dictionary9 = (Dictionary<string, object>)list4.get_Item(num14);
											float[] array;
											if (!dictionary9.ContainsKey("vertices"))
											{
												if (attachment is MeshAttachment)
												{
													array = ((MeshAttachment)attachment).vertices;
												}
												else
												{
													array = new float[num12];
												}
											}
											else
											{
												List<object> list5 = (List<object>)dictionary9.get_Item("vertices");
												array = new float[num12];
												int @int = this.GetInt(dictionary9, "offset", 0);
												if (scale == 1f)
												{
													int num15 = 0;
													int count = list5.get_Count();
													while (num15 < count)
													{
														array[num15 + @int] = (float)list5.get_Item(num15);
														num15++;
													}
												}
												else
												{
													int num16 = 0;
													int count2 = list5.get_Count();
													while (num16 < count2)
													{
														array[num16 + @int] = (float)list5.get_Item(num16) * scale;
														num16++;
													}
												}
												if (attachment is MeshAttachment)
												{
													float[] vertices = ((MeshAttachment)attachment).vertices;
													for (int num17 = 0; num17 < num12; num17++)
													{
														array[num17] += vertices[num17];
													}
												}
											}
											fFDTimeline.SetFrame(num13, (float)dictionary9.get_Item("time"), array);
											this.ReadCurve(fFDTimeline, num13, dictionary9);
											num13++;
										}
										exposedList.Add(fFDTimeline);
										num = Math.Max(num, fFDTimeline.frames[fFDTimeline.FrameCount - 1]);
									}
								}
							}
						}
					}
				}
			}
			if (map.ContainsKey("drawOrder") || map.ContainsKey("draworder"))
			{
				List<object> list6 = (List<object>)map.get_Item((!map.ContainsKey("drawOrder")) ? "draworder" : "drawOrder");
				DrawOrderTimeline drawOrderTimeline = new DrawOrderTimeline(list6.get_Count());
				int count3 = skeletonData.slots.Count;
				int num18 = 0;
				for (int num19 = 0; num19 < list6.get_Count(); num19++)
				{
					Dictionary<string, object> dictionary10 = (Dictionary<string, object>)list6.get_Item(num19);
					int[] array2 = null;
					if (dictionary10.ContainsKey("offsets"))
					{
						array2 = new int[count3];
						for (int num20 = count3 - 1; num20 >= 0; num20--)
						{
							array2[num20] = -1;
						}
						List<object> list7 = (List<object>)dictionary10.get_Item("offsets");
						int[] array3 = new int[count3 - list7.get_Count()];
						int num21 = 0;
						int num22 = 0;
						for (int num23 = 0; num23 < list7.get_Count(); num23++)
						{
							Dictionary<string, object> dictionary11 = (Dictionary<string, object>)list7.get_Item(num23);
							int num24 = skeletonData.FindSlotIndex((string)dictionary11.get_Item("slot"));
							if (num24 == -1)
							{
								throw new Exception("Slot not found: " + dictionary11.get_Item("slot"));
							}
							while (num21 != num24)
							{
								array3[num22++] = num21++;
							}
							int num25 = num21 + (int)((float)dictionary11.get_Item("offset"));
							array2[num25] = num21++;
						}
						while (num21 < count3)
						{
							array3[num22++] = num21++;
						}
						for (int num26 = count3 - 1; num26 >= 0; num26--)
						{
							if (array2[num26] == -1)
							{
								array2[num26] = array3[--num22];
							}
						}
					}
					drawOrderTimeline.SetFrame(num18++, (float)dictionary10.get_Item("time"), array2);
				}
				exposedList.Add(drawOrderTimeline);
				num = Math.Max(num, drawOrderTimeline.frames[drawOrderTimeline.FrameCount - 1]);
			}
			if (map.ContainsKey("events"))
			{
				List<object> list8 = (List<object>)map.get_Item("events");
				EventTimeline eventTimeline = new EventTimeline(list8.get_Count());
				int num27 = 0;
				for (int num28 = 0; num28 < list8.get_Count(); num28++)
				{
					Dictionary<string, object> dictionary12 = (Dictionary<string, object>)list8.get_Item(num28);
					EventData eventData = skeletonData.FindEvent((string)dictionary12.get_Item("name"));
					if (eventData == null)
					{
						throw new Exception("Event not found: " + dictionary12.get_Item("name"));
					}
					Event @event = new Event(eventData);
					@event.Int = this.GetInt(dictionary12, "int", eventData.Int);
					@event.Float = this.GetFloat(dictionary12, "float", eventData.Float);
					@event.String = this.GetString(dictionary12, "string", eventData.String);
					eventTimeline.SetFrame(num27++, (float)dictionary12.get_Item("time"), @event);
				}
				exposedList.Add(eventTimeline);
				num = Math.Max(num, eventTimeline.frames[eventTimeline.FrameCount - 1]);
			}
			exposedList.TrimExcess();
			skeletonData.animations.Add(new Animation(name, exposedList, num));
		}

		private void ReadCurve(CurveTimeline timeline, int frameIndex, Dictionary<string, object> valueMap)
		{
			if (!valueMap.ContainsKey("curve"))
			{
				return;
			}
			object obj = valueMap.get_Item("curve");
			if (obj.Equals("stepped"))
			{
				timeline.SetStepped(frameIndex);
			}
			else if (obj is List<object>)
			{
				List<object> list = (List<object>)obj;
				timeline.SetCurve(frameIndex, (float)list.get_Item(0), (float)list.get_Item(1), (float)list.get_Item(2), (float)list.get_Item(3));
			}
		}
	}
}
