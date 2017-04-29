using Spine;
using System;
using System.IO;
using UnityEngine;

public class AtlasAsset : ScriptableObject
{
	public TextAsset atlasFile;

	public Material[] materials;

	private Atlas atlas;

	public void Reset()
	{
		this.atlas = null;
	}

	public Atlas GetAtlas()
	{
		if (this.atlasFile == null)
		{
			Debug.LogError("Atlas file not set for atlas asset: " + base.get_name(), this);
			this.Reset();
			return null;
		}
		if (this.materials == null || this.materials.Length == 0)
		{
			Debug.LogError("Materials not set for atlas asset: " + base.get_name(), this);
			this.Reset();
			return null;
		}
		if (this.atlas != null)
		{
			return this.atlas;
		}
		Atlas result;
		try
		{
			this.atlas = new Atlas(new StringReader(this.atlasFile.get_text()), string.Empty, new MaterialsTextureLoader(this));
			this.atlas.FlipV();
			result = this.atlas;
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"Error reading atlas file for atlas asset: ",
				base.get_name(),
				"\n",
				ex.get_Message(),
				"\n",
				ex.get_StackTrace()
			}), this);
			result = null;
		}
		return result;
	}

	public Sprite GenerateSprite(string name, out Material material)
	{
		AtlasRegion atlasRegion = this.atlas.FindRegion(name);
		Sprite result = null;
		material = null;
		if (atlasRegion != null)
		{
		}
		return result;
	}

	public Mesh GenerateMesh(string name, Mesh mesh, out Material material, float scale = 0.01f)
	{
		AtlasRegion atlasRegion = this.atlas.FindRegion(name);
		material = null;
		if (atlasRegion != null)
		{
			if (mesh == null)
			{
				mesh = new Mesh();
				mesh.set_name(name);
			}
			Vector3[] array = new Vector3[4];
			Vector2[] array2 = new Vector2[4];
			Color[] colors = new Color[]
			{
				Color.get_white(),
				Color.get_white(),
				Color.get_white(),
				Color.get_white()
			};
			int[] triangles = new int[]
			{
				0,
				1,
				2,
				2,
				3,
				0
			};
			float num = (float)atlasRegion.width / -2f;
			float num2 = num * -1f;
			float num3 = (float)atlasRegion.height / 2f;
			float num4 = num3 * -1f;
			array[0] = new Vector3(num, num4, 0f) * scale;
			array[1] = new Vector3(num, num3, 0f) * scale;
			array[2] = new Vector3(num2, num3, 0f) * scale;
			array[3] = new Vector3(num2, num4, 0f) * scale;
			float u = atlasRegion.u;
			float v = atlasRegion.v;
			float u2 = atlasRegion.u2;
			float v2 = atlasRegion.v2;
			if (!atlasRegion.rotate)
			{
				array2[0] = new Vector2(u, v2);
				array2[1] = new Vector2(u, v);
				array2[2] = new Vector2(u2, v);
				array2[3] = new Vector2(u2, v2);
			}
			else
			{
				array2[0] = new Vector2(u2, v2);
				array2[1] = new Vector2(u, v2);
				array2[2] = new Vector2(u, v);
				array2[3] = new Vector2(u2, v);
			}
			mesh.set_triangles(new int[0]);
			mesh.set_vertices(array);
			mesh.set_uv(array2);
			mesh.set_colors(colors);
			mesh.set_triangles(triangles);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			material = (Material)atlasRegion.page.rendererObject;
		}
		else
		{
			mesh = null;
		}
		return mesh;
	}
}
