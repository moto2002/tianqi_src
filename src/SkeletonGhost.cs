using Spine;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkeletonRenderer))]
public class SkeletonGhost : MonoBehaviour
{
	public bool ghostingEnabled = true;

	public float spawnRate = 0.05f;

	public Color32 color = new Color32(255, 255, 255, 0);

	[Tooltip("Remember to set color alpha to 0 if Additive is true")]
	public bool additive = true;

	public int maximumGhosts = 10;

	public float fadeSpeed = 10f;

	public Shader ghostShader;

	[Range(0f, 1f), Tooltip("0 is Color and Alpha, 1 is Alpha only.")]
	public float textureFade = 1f;

	[Header("Sorting")]
	public bool sortWithDistanceOnly;

	public float zOffset;

	private float nextSpawnTime;

	private SkeletonGhostRenderer[] pool;

	private int poolIndex;

	private SkeletonRenderer skeletonRenderer;

	private MeshRenderer meshRenderer;

	private MeshFilter meshFilter;

	private Dictionary<Material, Material> materialTable = new Dictionary<Material, Material>();

	private void Start()
	{
		if (this.ghostShader == null)
		{
			this.ghostShader = Shader.Find("Spine/SkeletonGhost");
		}
		this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
		this.meshFilter = base.GetComponent<MeshFilter>();
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		this.nextSpawnTime = Time.get_time() + this.spawnRate;
		this.pool = new SkeletonGhostRenderer[this.maximumGhosts];
		for (int i = 0; i < this.maximumGhosts; i++)
		{
			GameObject gameObject = new GameObject(base.get_gameObject().get_name() + " Ghost", new Type[]
			{
				typeof(SkeletonGhostRenderer)
			});
			this.pool[i] = gameObject.GetComponent<SkeletonGhostRenderer>();
			gameObject.SetActive(false);
			gameObject.set_hideFlags(1);
		}
		if (this.skeletonRenderer is SkeletonAnimation)
		{
			((SkeletonAnimation)this.skeletonRenderer).state.Event += new AnimationState.EventDelegate(this.OnEvent);
		}
	}

	private void OnEvent(AnimationState state, int trackIndex, Event e)
	{
		if (e.Data.Name == "Ghosting")
		{
			this.ghostingEnabled = (e.Int > 0);
			if (e.Float > 0f)
			{
				this.spawnRate = e.Float;
			}
			if (e.String != null)
			{
				this.color = SkeletonGhost.HexToColor(e.String);
			}
		}
	}

	private void Ghosting(float val)
	{
		this.ghostingEnabled = (val > 0f);
	}

	private void Update()
	{
		if (!this.ghostingEnabled)
		{
			return;
		}
		if (Time.get_time() >= this.nextSpawnTime)
		{
			GameObject gameObject = this.pool[this.poolIndex].get_gameObject();
			Material[] sharedMaterials = this.meshRenderer.get_sharedMaterials();
			for (int i = 0; i < sharedMaterials.Length; i++)
			{
				Material material = sharedMaterials[i];
				Material material2;
				if (!this.materialTable.ContainsKey(material))
				{
					material2 = new Material(material);
					material2.set_shader(this.ghostShader);
					material2.set_color(Color.get_white());
					if (material2.HasProperty("_TextureFade"))
					{
						material2.SetFloat("_TextureFade", this.textureFade);
					}
					this.materialTable.Add(material, material2);
				}
				else
				{
					material2 = this.materialTable.get_Item(material);
				}
				sharedMaterials[i] = material2;
			}
			Transform transform = gameObject.get_transform();
			transform.set_parent(base.get_transform());
			this.pool[this.poolIndex].Initialize(this.meshFilter.get_sharedMesh(), sharedMaterials, this.color, this.additive, this.fadeSpeed, this.meshRenderer.get_sortingLayerID(), (!this.sortWithDistanceOnly) ? (this.meshRenderer.get_sortingOrder() - 1) : this.meshRenderer.get_sortingOrder());
			transform.set_localPosition(new Vector3(0f, 0f, this.zOffset));
			transform.set_localRotation(Quaternion.get_identity());
			transform.set_localScale(Vector3.get_one());
			transform.set_parent(null);
			this.poolIndex++;
			if (this.poolIndex == this.pool.Length)
			{
				this.poolIndex = 0;
			}
			this.nextSpawnTime = Time.get_time() + this.spawnRate;
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < this.maximumGhosts; i++)
		{
			if (this.pool[i] != null)
			{
				this.pool[i].Cleanup();
			}
		}
		using (Dictionary<Material, Material>.ValueCollection.Enumerator enumerator = this.materialTable.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Material current = enumerator.get_Current();
				Object.Destroy(current);
			}
		}
	}

	private static Color32 HexToColor(string hex)
	{
		if (hex.get_Length() < 6)
		{
			return Color.get_magenta();
		}
		hex = hex.Replace("#", string.Empty);
		byte b = byte.Parse(hex.Substring(0, 2), 515);
		byte b2 = byte.Parse(hex.Substring(2, 2), 515);
		byte b3 = byte.Parse(hex.Substring(4, 2), 515);
		byte b4 = 255;
		if (hex.get_Length() == 8)
		{
			b4 = byte.Parse(hex.Substring(6, 2), 515);
		}
		return new Color32(b, b2, b3, b4);
	}
}
