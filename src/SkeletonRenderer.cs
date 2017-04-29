using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SkeletonRenderer : MonoBehaviour
{
	private class MeshState
	{
		public class SingleMeshState
		{
			public bool immutableTriangles;

			public bool requiresUpdate;

			public readonly ExposedList<Attachment> attachments = new ExposedList<Attachment>();

			public readonly ExposedList<bool> attachmentsFlipState = new ExposedList<bool>();

			public readonly ExposedList<SkeletonRenderer.MeshState.AddSubmeshArguments> addSubmeshArguments = new ExposedList<SkeletonRenderer.MeshState.AddSubmeshArguments>();

			public void UpdateAttachmentCount(int attachmentCount)
			{
				this.attachmentsFlipState.GrowIfNeeded(attachmentCount);
				this.attachmentsFlipState.Count = attachmentCount;
				this.attachments.GrowIfNeeded(attachmentCount);
				this.attachments.Count = attachmentCount;
			}
		}

		public struct AddSubmeshArguments
		{
			public Material material;

			public int startSlot;

			public int endSlot;

			public int triangleCount;

			public int firstVertex;

			public bool isLastSubmesh;

			public bool Equals(ref SkeletonRenderer.MeshState.AddSubmeshArguments other)
			{
				return this.startSlot == other.startSlot && this.endSlot == other.endSlot && this.triangleCount == other.triangleCount && this.firstVertex == other.firstVertex;
			}
		}

		public int vertexCount;

		public readonly SkeletonRenderer.MeshState.SingleMeshState buffer = new SkeletonRenderer.MeshState.SingleMeshState();

		public readonly SkeletonRenderer.MeshState.SingleMeshState stateMesh1 = new SkeletonRenderer.MeshState.SingleMeshState();

		public readonly SkeletonRenderer.MeshState.SingleMeshState stateMesh2 = new SkeletonRenderer.MeshState.SingleMeshState();
	}

	public delegate void SkeletonRendererDelegate(SkeletonRenderer skeletonRenderer);

	public SkeletonRenderer.SkeletonRendererDelegate OnReset;

	public SkeletonDataAsset skeletonDataAsset;

	public string initialSkinName;

	public bool calculateNormals;

	public bool calculateTangents;

	public float zSpacing;

	public bool renderMeshes = true;

	public bool immutableTriangles;

	public bool frontFacing;

	public bool logErrors;

	[SpineSlot("", "", false)]
	public string[] submeshSeparators = new string[0];

	[HideInInspector]
	public List<Slot> submeshSeparatorSlots = new List<Slot>();

	[NonSerialized]
	public bool valid;

	[NonSerialized]
	public Skeleton skeleton;

	private MeshRenderer meshRenderer;

	private MeshFilter meshFilter;

	private Mesh mesh1;

	private Mesh mesh2;

	private bool useMesh1;

	private float[] tempVertices = new float[8];

	private Vector3[] vertices;

	private Color32[] colors;

	private Vector2[] uvs;

	private Material[] sharedMaterials = new Material[0];

	private SkeletonRenderer.MeshState meshState = new SkeletonRenderer.MeshState();

	private readonly ExposedList<Material> submeshMaterials = new ExposedList<Material>();

	private readonly ExposedList<Submesh> submeshes = new ExposedList<Submesh>();

	private SkeletonUtilitySubmeshRenderer[] submeshRenderers;

	private bool IsAwake;

	public virtual void Awake()
	{
		if (this.IsAwake)
		{
			return;
		}
		this.IsAwake = true;
		this.Reset();
	}

	public virtual void OnDestroy()
	{
		if (this.mesh1 != null)
		{
			if (Application.get_isPlaying())
			{
				Object.Destroy(this.mesh1);
			}
			else
			{
				Object.DestroyImmediate(this.mesh1);
			}
		}
		if (this.mesh2 != null)
		{
			if (Application.get_isPlaying())
			{
				Object.Destroy(this.mesh2);
			}
			else
			{
				Object.DestroyImmediate(this.mesh2);
			}
		}
		this.mesh1 = null;
		this.mesh2 = null;
	}

	public virtual void Reset()
	{
		if (this.meshFilter != null)
		{
			this.meshFilter.set_sharedMesh(null);
		}
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		if (this.meshRenderer != null)
		{
			this.meshRenderer.set_sharedMaterial(null);
		}
		if (this.mesh1 != null)
		{
			if (Application.get_isPlaying())
			{
				Object.Destroy(this.mesh1);
			}
			else
			{
				Object.DestroyImmediate(this.mesh1);
			}
		}
		if (this.mesh2 != null)
		{
			if (Application.get_isPlaying())
			{
				Object.Destroy(this.mesh2);
			}
			else
			{
				Object.DestroyImmediate(this.mesh2);
			}
		}
		this.meshState = new SkeletonRenderer.MeshState();
		this.mesh1 = null;
		this.mesh2 = null;
		this.vertices = null;
		this.colors = null;
		this.uvs = null;
		this.sharedMaterials = new Material[0];
		this.submeshMaterials.Clear(true);
		this.submeshes.Clear(true);
		this.skeleton = null;
		this.valid = false;
		if (!this.skeletonDataAsset)
		{
			if (this.logErrors)
			{
				Debug.LogError("Missing SkeletonData asset.", this);
			}
			return;
		}
		SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(false);
		if (skeletonData == null)
		{
			return;
		}
		this.valid = true;
		this.meshFilter = base.GetComponent<MeshFilter>();
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		this.mesh1 = SkeletonRenderer.newMesh();
		this.mesh2 = SkeletonRenderer.newMesh();
		this.vertices = new Vector3[0];
		this.skeleton = new Skeleton(skeletonData);
		if (this.initialSkinName != null && this.initialSkinName.get_Length() > 0 && this.initialSkinName != "default")
		{
			this.skeleton.SetSkin(this.initialSkinName);
		}
		this.submeshSeparatorSlots.Clear();
		for (int i = 0; i < this.submeshSeparators.Length; i++)
		{
			this.submeshSeparatorSlots.Add(this.skeleton.FindSlot(this.submeshSeparators[i]));
		}
		this.CollectSubmeshRenderers();
		this.LateUpdate();
		if (this.OnReset != null)
		{
			this.OnReset(this);
		}
	}

	[DebuggerHidden]
	public IEnumerator ResetAsync(Action finishCallback)
	{
		SkeletonRenderer.<ResetAsync>c__IteratorB <ResetAsync>c__IteratorB = new SkeletonRenderer.<ResetAsync>c__IteratorB();
		<ResetAsync>c__IteratorB.finishCallback = finishCallback;
		<ResetAsync>c__IteratorB.<$>finishCallback = finishCallback;
		<ResetAsync>c__IteratorB.<>f__this = this;
		return <ResetAsync>c__IteratorB;
	}

	public void CollectSubmeshRenderers()
	{
		this.submeshRenderers = base.GetComponentsInChildren<SkeletonUtilitySubmeshRenderer>();
	}

	private static Mesh newMesh()
	{
		Mesh mesh = new Mesh();
		mesh.set_name("Skeleton Mesh");
		mesh.set_hideFlags(61);
		mesh.MarkDynamic();
		return mesh;
	}

	public virtual void LateUpdate()
	{
		if (!this.valid)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int firstVertex = 0;
		int startSlot = 0;
		Material material = null;
		ExposedList<Slot> drawOrder = this.skeleton.drawOrder;
		Slot[] items = drawOrder.Items;
		int count = drawOrder.Count;
		int count2 = this.submeshSeparatorSlots.get_Count();
		bool flag = this.renderMeshes;
		SkeletonRenderer.MeshState.SingleMeshState buffer = this.meshState.buffer;
		ExposedList<Attachment> attachments = buffer.attachments;
		attachments.Clear(true);
		buffer.UpdateAttachmentCount(count);
		Attachment[] items2 = attachments.Items;
		ExposedList<bool> attachmentsFlipState = buffer.attachmentsFlipState;
		bool[] items3 = buffer.attachmentsFlipState.Items;
		ExposedList<SkeletonRenderer.MeshState.AddSubmeshArguments> addSubmeshArguments = buffer.addSubmeshArguments;
		addSubmeshArguments.Clear(false);
		SkeletonRenderer.MeshState.SingleMeshState singleMeshState = (!this.useMesh1) ? this.meshState.stateMesh2 : this.meshState.stateMesh1;
		ExposedList<Attachment> attachments2 = singleMeshState.attachments;
		Attachment[] items4 = attachments2.Items;
		ExposedList<bool> attachmentsFlipState2 = singleMeshState.attachmentsFlipState;
		bool[] items5 = attachmentsFlipState2.Items;
		bool flag2 = singleMeshState.requiresUpdate || drawOrder.Count != attachments2.Count || this.immutableTriangles != singleMeshState.immutableTriangles;
		int i = 0;
		while (i < count)
		{
			Slot slot = items[i];
			Bone bone = slot.bone;
			Attachment attachment = slot.attachment;
			bool flag3 = bone.worldScaleY >= 0f == bone.worldScaleX >= 0f;
			bool flag4 = this.frontFacing && bone.worldFlipX != bone.worldFlipY == flag3;
			items3[i] = flag4;
			items2[i] = attachment;
			flag2 = (flag2 || attachment != items4[i] || flag4 != items5[i]);
			RegionAttachment regionAttachment = attachment as RegionAttachment;
			object rendererObject;
			int num3;
			int num4;
			if (regionAttachment != null)
			{
				rendererObject = regionAttachment.RendererObject;
				num3 = 4;
				num4 = 6;
				goto IL_266;
			}
			if (flag)
			{
				MeshAttachment meshAttachment = attachment as MeshAttachment;
				if (meshAttachment != null)
				{
					rendererObject = meshAttachment.RendererObject;
					num3 = meshAttachment.vertices.Length >> 1;
					num4 = meshAttachment.triangles.Length;
					goto IL_266;
				}
				SkinnedMeshAttachment skinnedMeshAttachment = attachment as SkinnedMeshAttachment;
				if (skinnedMeshAttachment != null)
				{
					rendererObject = skinnedMeshAttachment.RendererObject;
					num3 = skinnedMeshAttachment.uvs.Length >> 1;
					num4 = skinnedMeshAttachment.triangles.Length;
					goto IL_266;
				}
			}
			IL_310:
			i++;
			continue;
			IL_266:
			Material material2 = (Material)((AtlasRegion)rendererObject).page.rendererObject;
			if ((material != null && material.GetInstanceID() != material2.GetInstanceID()) || (count2 > 0 && this.submeshSeparatorSlots.Contains(slot)))
			{
				addSubmeshArguments.Add(new SkeletonRenderer.MeshState.AddSubmeshArguments
				{
					material = material,
					startSlot = startSlot,
					endSlot = i,
					triangleCount = num2,
					firstVertex = firstVertex,
					isLastSubmesh = false
				});
				num2 = 0;
				firstVertex = num;
				startSlot = i;
			}
			material = material2;
			num2 += num4;
			num += num3;
			goto IL_310;
		}
		material = this.skeletonDataAsset.atlasAssets[0].materials[0];
		addSubmeshArguments.Add(new SkeletonRenderer.MeshState.AddSubmeshArguments
		{
			material = material,
			startSlot = startSlot,
			endSlot = count,
			triangleCount = num2,
			firstVertex = firstVertex,
			isLastSubmesh = true
		});
		flag2 = (flag2 || this.sharedMaterials.Length != addSubmeshArguments.Count || this.CheckIfMustUpdateMeshStructure(addSubmeshArguments));
		if (!flag2)
		{
			SkeletonRenderer.MeshState.AddSubmeshArguments[] items6 = addSubmeshArguments.Items;
			int j = 0;
			int num5 = this.sharedMaterials.Length;
			while (j < num5)
			{
				if (this.sharedMaterials[j] != items6[j].material)
				{
					flag2 = true;
					break;
				}
				j++;
			}
		}
		if (flag2)
		{
			this.submeshMaterials.Clear(true);
			SkeletonRenderer.MeshState.AddSubmeshArguments[] items7 = addSubmeshArguments.Items;
			int k = 0;
			int count3 = addSubmeshArguments.Count;
			while (k < count3)
			{
				this.AddSubmesh(items7[k], attachmentsFlipState);
				k++;
			}
			if (this.submeshMaterials.Count == this.sharedMaterials.Length)
			{
				this.submeshMaterials.CopyTo(this.sharedMaterials);
			}
			else
			{
				this.sharedMaterials = this.submeshMaterials.ToArray();
			}
			if (this.meshRenderer.get_sharedMaterials() == null || this.meshRenderer.get_sharedMaterials().Length == 0 || this.meshRenderer.get_sharedMaterials()[0] == null)
			{
				this.meshRenderer.set_sharedMaterials(this.sharedMaterials);
			}
		}
		Vector3[] array = this.vertices;
		bool flag5 = num > array.Length;
		if (flag5)
		{
			array = (this.vertices = new Vector3[num]);
			this.colors = new Color32[num];
			this.uvs = new Vector2[num];
			this.mesh1.Clear();
			this.mesh2.Clear();
			this.meshState.stateMesh1.requiresUpdate = true;
			this.meshState.stateMesh2.requiresUpdate = true;
		}
		else
		{
			Vector3 zero = Vector3.get_zero();
			int l = num;
			int vertexCount = this.meshState.vertexCount;
			while (l < vertexCount)
			{
				array[l] = zero;
				l++;
			}
		}
		this.meshState.vertexCount = num;
		float num6 = this.zSpacing;
		float[] array2 = this.tempVertices;
		Vector2[] array3 = this.uvs;
		Color32[] array4 = this.colors;
		int num7 = 0;
		float num8 = this.skeleton.a * 255f;
		float r = this.skeleton.r;
		float g = this.skeleton.g;
		float b = this.skeleton.b;
		Vector3 vector;
		Vector3 vector2;
		if (num == 0)
		{
			vector = new Vector3(0f, 0f, 0f);
			vector2 = new Vector3(0f, 0f, 0f);
		}
		else
		{
			vector.x = 2.14748365E+09f;
			vector.y = 2.14748365E+09f;
			vector2.x = -2.14748365E+09f;
			vector2.y = -2.14748365E+09f;
			if (num6 > 0f)
			{
				vector.z = 0f;
				vector2.z = num6 * (float)(count - 1);
			}
			else
			{
				vector.z = num6 * (float)(count - 1);
				vector2.z = 0f;
			}
			int num9 = 0;
			do
			{
				Slot slot2 = items[num9];
				Attachment attachment2 = slot2.attachment;
				RegionAttachment regionAttachment2 = attachment2 as RegionAttachment;
				if (regionAttachment2 != null)
				{
					regionAttachment2.ComputeWorldVertices(slot2.bone, array2);
					float z = (float)num9 * num6;
					float num10 = array2[0];
					float num11 = array2[1];
					float num12 = array2[2];
					float num13 = array2[3];
					float num14 = array2[4];
					float num15 = array2[5];
					float num16 = array2[6];
					float num17 = array2[7];
					array[num7].x = num10;
					array[num7].y = num11;
					array[num7].z = z;
					array[num7 + 1].x = num16;
					array[num7 + 1].y = num17;
					array[num7 + 1].z = z;
					array[num7 + 2].x = num12;
					array[num7 + 2].y = num13;
					array[num7 + 2].z = z;
					array[num7 + 3].x = num14;
					array[num7 + 3].y = num15;
					array[num7 + 3].z = z;
					Color32 color;
					color.a = (byte)(num8 * slot2.a * regionAttachment2.a);
					color.r = (byte)(r * slot2.r * regionAttachment2.r * (float)color.a);
					color.g = (byte)(g * slot2.g * regionAttachment2.g * (float)color.a);
					color.b = (byte)(b * slot2.b * regionAttachment2.b * (float)color.a);
					array4[num7] = color;
					array4[num7 + 1] = color;
					array4[num7 + 2] = color;
					array4[num7 + 3] = color;
					float[] array5 = regionAttachment2.uvs;
					array3[num7].x = array5[0];
					array3[num7].y = array5[1];
					array3[num7 + 1].x = array5[6];
					array3[num7 + 1].y = array5[7];
					array3[num7 + 2].x = array5[2];
					array3[num7 + 2].y = array5[3];
					array3[num7 + 3].x = array5[4];
					array3[num7 + 3].y = array5[5];
					if (num10 < vector.x)
					{
						vector.x = num10;
					}
					else if (num10 > vector2.x)
					{
						vector2.x = num10;
					}
					if (num12 < vector.x)
					{
						vector.x = num12;
					}
					else if (num12 > vector2.x)
					{
						vector2.x = num12;
					}
					if (num14 < vector.x)
					{
						vector.x = num14;
					}
					else if (num14 > vector2.x)
					{
						vector2.x = num14;
					}
					if (num16 < vector.x)
					{
						vector.x = num16;
					}
					else if (num16 > vector2.x)
					{
						vector2.x = num16;
					}
					if (num11 < vector.y)
					{
						vector.y = num11;
					}
					else if (num11 > vector2.y)
					{
						vector2.y = num11;
					}
					if (num13 < vector.y)
					{
						vector.y = num13;
					}
					else if (num13 > vector2.y)
					{
						vector2.y = num13;
					}
					if (num15 < vector.y)
					{
						vector.y = num15;
					}
					else if (num15 > vector2.y)
					{
						vector2.y = num15;
					}
					if (num17 < vector.y)
					{
						vector.y = num17;
					}
					else if (num17 > vector2.y)
					{
						vector2.y = num17;
					}
					num7 += 4;
				}
				else if (flag)
				{
					MeshAttachment meshAttachment2 = attachment2 as MeshAttachment;
					if (meshAttachment2 != null)
					{
						int num18 = meshAttachment2.vertices.Length;
						if (array2.Length < num18)
						{
							array2 = (this.tempVertices = new float[num18]);
						}
						meshAttachment2.ComputeWorldVertices(slot2, array2);
						Color32 color;
						color.a = (byte)(num8 * slot2.a * meshAttachment2.a);
						color.r = (byte)(r * slot2.r * meshAttachment2.r * (float)color.a);
						color.g = (byte)(g * slot2.g * meshAttachment2.g * (float)color.a);
						color.b = (byte)(b * slot2.b * meshAttachment2.b * (float)color.a);
						float[] array6 = meshAttachment2.uvs;
						float z2 = (float)num9 * num6;
						int m = 0;
						while (m < num18)
						{
							float num19 = array2[m];
							float num20 = array2[m + 1];
							array[num7].x = num19;
							array[num7].y = num20;
							array[num7].z = z2;
							array4[num7] = color;
							array3[num7].x = array6[m];
							array3[num7].y = array6[m + 1];
							if (num19 < vector.x)
							{
								vector.x = num19;
							}
							else if (num19 > vector2.x)
							{
								vector2.x = num19;
							}
							if (num20 < vector.y)
							{
								vector.y = num20;
							}
							else if (num20 > vector2.y)
							{
								vector2.y = num20;
							}
							m += 2;
							num7++;
						}
					}
					else
					{
						SkinnedMeshAttachment skinnedMeshAttachment2 = attachment2 as SkinnedMeshAttachment;
						if (skinnedMeshAttachment2 != null)
						{
							int num21 = skinnedMeshAttachment2.uvs.Length;
							if (array2.Length < num21)
							{
								array2 = (this.tempVertices = new float[num21]);
							}
							skinnedMeshAttachment2.ComputeWorldVertices(slot2, array2);
							Color32 color;
							color.a = (byte)(num8 * slot2.a * skinnedMeshAttachment2.a);
							color.r = (byte)(r * slot2.r * skinnedMeshAttachment2.r * (float)color.a);
							color.g = (byte)(g * slot2.g * skinnedMeshAttachment2.g * (float)color.a);
							color.b = (byte)(b * slot2.b * skinnedMeshAttachment2.b * (float)color.a);
							float[] array7 = skinnedMeshAttachment2.uvs;
							float z3 = (float)num9 * num6;
							int n = 0;
							while (n < num21)
							{
								float num22 = array2[n];
								float num23 = array2[n + 1];
								array[num7].x = num22;
								array[num7].y = num23;
								array[num7].z = z3;
								array4[num7] = color;
								array3[num7].x = array7[n];
								array3[num7].y = array7[n + 1];
								if (num22 < vector.x)
								{
									vector.x = num22;
								}
								else if (num22 > vector2.x)
								{
									vector2.x = num22;
								}
								if (num23 < vector.y)
								{
									vector.y = num23;
								}
								else if (num23 > vector2.y)
								{
									vector2.y = num23;
								}
								n += 2;
								num7++;
							}
						}
					}
				}
			}
			while (++num9 < count);
		}
		Mesh mesh = (!this.useMesh1) ? this.mesh2 : this.mesh1;
		this.meshFilter.set_sharedMesh(mesh);
		mesh.set_vertices(array);
		mesh.set_colors32(array4);
		mesh.set_uv(array3);
		if (flag2)
		{
			int count4 = this.submeshMaterials.Count;
			mesh.set_subMeshCount(count4);
			for (int num24 = 0; num24 < count4; num24++)
			{
				mesh.SetTriangles(this.submeshes.Items[num24].triangles, num24);
			}
			singleMeshState.requiresUpdate = false;
		}
		Vector3 vector3 = vector2 - vector;
		Vector3 vector4 = vector + vector3 * 0.5f;
		mesh.set_bounds(new Bounds(vector4, vector3));
		if (flag5 && this.calculateNormals)
		{
			Vector3[] array8 = new Vector3[num];
			Vector3 vector5 = new Vector3(0f, 0f, -1f);
			for (int num25 = 0; num25 < num; num25++)
			{
				array8[num25] = vector5;
			}
			((!this.useMesh1) ? this.mesh1 : this.mesh2).set_vertices(array);
			this.mesh1.set_normals(array8);
			this.mesh2.set_normals(array8);
			if (this.calculateTangents)
			{
				Vector4[] array9 = new Vector4[num];
				Vector3 vector6 = new Vector3(0f, 0f, 1f);
				for (int num26 = 0; num26 < num; num26++)
				{
					array9[num26] = vector6;
				}
				this.mesh1.set_tangents(array9);
				this.mesh2.set_tangents(array9);
			}
		}
		singleMeshState.immutableTriangles = this.immutableTriangles;
		attachments2.Clear(true);
		attachments2.GrowIfNeeded(attachments.Capacity);
		attachments2.Count = attachments.Count;
		attachments.CopyTo(attachments2.Items);
		attachmentsFlipState2.GrowIfNeeded(attachmentsFlipState.Capacity);
		attachmentsFlipState2.Count = attachmentsFlipState.Count;
		attachmentsFlipState.CopyTo(attachmentsFlipState2.Items);
		singleMeshState.addSubmeshArguments.GrowIfNeeded(addSubmeshArguments.Capacity);
		singleMeshState.addSubmeshArguments.Count = addSubmeshArguments.Count;
		addSubmeshArguments.CopyTo(singleMeshState.addSubmeshArguments.Items);
		if (this.submeshRenderers.Length > 0)
		{
			for (int num27 = 0; num27 < this.submeshRenderers.Length; num27++)
			{
				SkeletonUtilitySubmeshRenderer skeletonUtilitySubmeshRenderer = this.submeshRenderers[num27];
				if (skeletonUtilitySubmeshRenderer.submeshIndex < this.sharedMaterials.Length)
				{
					skeletonUtilitySubmeshRenderer.SetMesh(this.meshRenderer, (!this.useMesh1) ? this.mesh2 : this.mesh1, this.sharedMaterials[skeletonUtilitySubmeshRenderer.submeshIndex]);
				}
				else
				{
					skeletonUtilitySubmeshRenderer.GetComponent<Renderer>().set_enabled(false);
				}
			}
		}
		this.useMesh1 = !this.useMesh1;
	}

	private bool CheckIfMustUpdateMeshStructure(ExposedList<SkeletonRenderer.MeshState.AddSubmeshArguments> workingAddSubmeshArguments)
	{
		SkeletonRenderer.MeshState.SingleMeshState singleMeshState = (!this.useMesh1) ? this.meshState.stateMesh2 : this.meshState.stateMesh1;
		ExposedList<SkeletonRenderer.MeshState.AddSubmeshArguments> addSubmeshArguments = singleMeshState.addSubmeshArguments;
		int count = workingAddSubmeshArguments.Count;
		if (addSubmeshArguments.Count != count)
		{
			return true;
		}
		for (int i = 0; i < count; i++)
		{
			if (!addSubmeshArguments.Items[i].Equals(ref workingAddSubmeshArguments.Items[i]))
			{
				return true;
			}
		}
		return false;
	}

	private void AddSubmesh(SkeletonRenderer.MeshState.AddSubmeshArguments submeshArguments, ExposedList<bool> flipStates)
	{
		int count = this.submeshMaterials.Count;
		this.submeshMaterials.Add(submeshArguments.material);
		if (this.submeshes.Count <= count)
		{
			this.submeshes.Add(new Submesh());
		}
		else if (this.immutableTriangles)
		{
			return;
		}
		Submesh submesh = this.submeshes.Items[count];
		int[] array = submesh.triangles;
		int triangleCount = submeshArguments.triangleCount;
		int num = submeshArguments.firstVertex;
		int num2 = array.Length;
		if (submeshArguments.isLastSubmesh && num2 > triangleCount)
		{
			for (int i = triangleCount; i < num2; i++)
			{
				array[i] = 0;
			}
			submesh.triangleCount = triangleCount;
		}
		else if (num2 != triangleCount)
		{
			array = (submesh.triangles = new int[triangleCount]);
			submesh.triangleCount = 0;
		}
		if (!this.renderMeshes && !this.frontFacing)
		{
			if (submesh.firstVertex != num || submesh.triangleCount < triangleCount)
			{
				submesh.triangleCount = triangleCount;
				submesh.firstVertex = num;
				int j = 0;
				while (j < triangleCount)
				{
					array[j] = num;
					array[j + 1] = num + 2;
					array[j + 2] = num + 1;
					array[j + 3] = num + 2;
					array[j + 4] = num + 3;
					array[j + 5] = num + 1;
					j += 6;
					num += 4;
				}
			}
			return;
		}
		Slot[] items = this.skeleton.DrawOrder.Items;
		bool[] items2 = flipStates.Items;
		int num3 = 0;
		int k = submeshArguments.startSlot;
		int endSlot = submeshArguments.endSlot;
		while (k < endSlot)
		{
			Attachment attachment = items[k].attachment;
			bool flag = items2[k];
			if (attachment is RegionAttachment)
			{
				if (!flag)
				{
					array[num3] = num;
					array[num3 + 1] = num + 2;
					array[num3 + 2] = num + 1;
					array[num3 + 3] = num + 2;
					array[num3 + 4] = num + 3;
					array[num3 + 5] = num + 1;
				}
				else
				{
					array[num3] = num + 1;
					array[num3 + 1] = num + 2;
					array[num3 + 2] = num;
					array[num3 + 3] = num + 1;
					array[num3 + 4] = num + 3;
					array[num3 + 5] = num + 2;
				}
				num3 += 6;
				num += 4;
			}
			else
			{
				MeshAttachment meshAttachment = attachment as MeshAttachment;
				int num4;
				int[] triangles;
				if (meshAttachment != null)
				{
					num4 = meshAttachment.vertices.Length >> 1;
					triangles = meshAttachment.triangles;
				}
				else
				{
					SkinnedMeshAttachment skinnedMeshAttachment = attachment as SkinnedMeshAttachment;
					if (skinnedMeshAttachment == null)
					{
						goto IL_333;
					}
					num4 = skinnedMeshAttachment.uvs.Length >> 1;
					triangles = skinnedMeshAttachment.triangles;
				}
				if (flag)
				{
					int l = 0;
					int num5 = triangles.Length;
					while (l < num5)
					{
						array[num3 + 2] = num + triangles[l];
						array[num3 + 1] = num + triangles[l + 1];
						array[num3] = num + triangles[l + 2];
						l += 3;
						num3 += 3;
					}
				}
				else
				{
					int m = 0;
					int num6 = triangles.Length;
					while (m < num6)
					{
						array[num3] = num + triangles[m];
						m++;
						num3++;
					}
				}
				num += num4;
			}
			IL_333:
			k++;
		}
	}
}
