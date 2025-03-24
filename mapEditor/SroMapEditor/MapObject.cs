using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using SroMapEditor.DDSReader;

namespace SroMapEditor;

internal class MapObject
{
	public int nameI;

	public float X;

	public float Y;

	public float Z;

	public float Theta;

	public int ID;

	public List<int> groups;

	public string Unknown1;

	public string Unknown2;

	public bool DistFade;

	public string materialPath;

	private string pathPrefix;

	private float[][,] Verts;

	private float[][,] UV;

	private int[][,] Faces;

	private string[] Tex;

	private int[] TexIDs;

	private int readingMesh;

	public float[] boundingBoxp1;

	public float[] boundingBoxp2;

	public float[] rotBoundingBoxp1;

	public float[] rotBoundingBoxp2;

	public MapObject()
	{
		groups = new List<int>();
	}

	public void LoadFiles(string pathPref, string path)
	{
		readingMesh = 0;
		pathPrefix = pathPref;
		BinaryReader binaryReader = new BinaryReader(File.Open(pathPref + path, FileMode.Open));
		binaryReader.BaseStream.Position = 12L;
		int num = binaryReader.ReadInt32();
		int num2 = binaryReader.ReadInt32();
		int num3 = binaryReader.ReadInt32();
		binaryReader.BaseStream.Position = num + 8;
		int count = binaryReader.ReadInt32();
		string text = new string(binaryReader.ReadChars(count));
		binaryReader.BaseStream.Position = num2;
		int num4 = binaryReader.ReadInt32();
		string[] array = new string[num4];
		for (int i = 0; i < num4; i++)
		{
			int num5 = binaryReader.ReadInt32();
			if (num5 < 20)
			{
				num5 = binaryReader.ReadInt32();
			}
			array[i] = new string(binaryReader.ReadChars(num5));
		}
		binaryReader.BaseStream.Position = num3;
		if (binaryReader.ReadInt32() == 0)
		{
			DistFade = false;
		}
		else
		{
			DistFade = true;
		}
		binaryReader.Close();
		materialPath = pathPref + text;
		binaryReader.Close();
		num4 = array.Length;
		Verts = new float[num4][,];
		UV = new float[num4][,];
		Faces = new int[num4][,];
		Tex = new string[num4];
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			readModel(pathPref + text2);
		}
		float num6 = Verts[0][0, 0];
		float num7 = Verts[0][0, 1];
		float num8 = Verts[0][0, 2];
		float num9 = Verts[0][0, 0];
		float num10 = Verts[0][0, 1];
		float num11 = Verts[0][0, 2];
		for (int k = 0; k < Verts.Length; k++)
		{
			for (int l = 0; l < Verts[k].Length / 3; l++)
			{
				if (Verts[k][l, 0] < num6)
				{
					num6 = Verts[k][l, 0];
				}
				if (Verts[k][l, 1] < num7)
				{
					num7 = Verts[k][l, 1];
				}
				if (Verts[k][l, 2] < num8)
				{
					num8 = Verts[k][l, 2];
				}
				if (Verts[k][l, 0] > num9)
				{
					num9 = Verts[k][l, 0];
				}
				if (Verts[k][l, 1] > num10)
				{
					num10 = Verts[k][l, 1];
				}
				if (Verts[k][l, 2] > num11)
				{
					num11 = Verts[k][l, 2];
				}
			}
		}
		boundingBoxp1 = new float[3] { num6, num7, num8 };
		boundingBoxp2 = new float[3] { num9, num10, num11 };
		rotBoundingBoxp1 = new float[3] { num6, num7, num8 };
		rotBoundingBoxp2 = new float[3] { num9, num10, num11 };
		setRotation(Theta);
	}

	public void Draw()
	{
		if (Faces == null)
		{
			return;
		}
		for (int i = 0; i < Faces.Length; i++)
		{
			GL.Enable(EnableCap.Texture2D);
			if (TexIDs != null)
			{
				GL.BindTexture(TextureTarget.Texture2D, TexIDs[i]);
			}
			GL.Begin(BeginMode.Triangles);
			if (Faces[i] != null)
			{
				for (int j = 0; j < Faces[i].Length / 3; j++)
				{
					GL.TexCoord2(UV[i][Faces[i][j, 0], 0], UV[i][Faces[i][j, 0], 1]);
					GL.Vertex3(Verts[i][Faces[i][j, 0], 0], Verts[i][Faces[i][j, 0], 2], Verts[i][Faces[i][j, 0], 1]);
					GL.TexCoord2(UV[i][Faces[i][j, 1], 0], UV[i][Faces[i][j, 1], 1]);
					GL.Vertex3(Verts[i][Faces[i][j, 1], 0], Verts[i][Faces[i][j, 1], 2], Verts[i][Faces[i][j, 1], 1]);
					GL.TexCoord2(UV[i][Faces[i][j, 2], 0], UV[i][Faces[i][j, 2], 1]);
					GL.Vertex3(Verts[i][Faces[i][j, 2], 0], Verts[i][Faces[i][j, 2], 2], Verts[i][Faces[i][j, 2], 1]);
				}
			}
			GL.End();
			GL.Disable(EnableCap.Texture2D);
		}
	}

	public void drawBoundingBox()
	{
		if (boundingBoxp1 != null)
		{
			GL.Color3(byte.MaxValue, (byte)0, (byte)0);
			GL.Begin(BeginMode.LineStrip);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp1[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp1[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp1[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp1[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp2[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp2[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp2[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp2[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp1[2], boundingBoxp1[1]);
			GL.End();
			GL.Begin(BeginMode.Lines);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp2[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp2[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp1[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp1[0], boundingBoxp1[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp1[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp2[2], boundingBoxp1[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp1[2], boundingBoxp2[1]);
			GL.Vertex3(boundingBoxp2[0], boundingBoxp2[2], boundingBoxp2[1]);
			GL.End();
			GL.Color3(byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}

	public void drawGroups()
	{
		GL.Color4(byte.MaxValue, byte.MaxValue, (byte)0, (byte)100);
		for (int i = 0; i < groups.Count; i++)
		{
			GL.Begin(BeginMode.TriangleStrip);
			GL.Vertex3(groups[i] % 24 * 80, Math.Floor((double)groups[i] / 24.0) * 320.0, 500.0);
			GL.Vertex3((groups[i] % 24 + 1) * 80, Math.Floor((double)groups[i] / 24.0) * 320.0, 500.0);
			GL.Vertex3(groups[i] % 24 * 80, (Math.Floor((double)groups[i] / 24.0) + 1.0) * 320.0, 500.0);
			GL.Vertex3((groups[i] % 24 + 1) * 80, (Math.Floor((double)groups[i] / 24.0) + 1.0) * 320.0, 500.0);
			GL.End();
		}
		GL.Color4(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
	}

	public void setRotation(float rot)
	{
		Theta = rot;
		rotBoundingBoxp2[0] = boundingBoxp1[0] * (float)Math.Cos(Theta) - boundingBoxp1[2] * (float)Math.Sin(Theta);
		rotBoundingBoxp2[2] = boundingBoxp1[0] * (float)Math.Sin(Theta) + boundingBoxp1[2] * (float)Math.Cos(Theta);
		rotBoundingBoxp1[0] = boundingBoxp2[0] * (float)Math.Cos(Theta) - boundingBoxp2[2] * (float)Math.Sin(Theta);
		rotBoundingBoxp1[2] = boundingBoxp2[0] * (float)Math.Sin(Theta) + boundingBoxp2[2] * (float)Math.Cos(Theta);
		if (rotBoundingBoxp1[2] > rotBoundingBoxp2[2])
		{
			float num = rotBoundingBoxp1[2];
			rotBoundingBoxp1[2] = rotBoundingBoxp2[2];
			rotBoundingBoxp2[2] = num;
		}
		if (rotBoundingBoxp1[0] > rotBoundingBoxp2[0])
		{
			float num2 = rotBoundingBoxp1[0];
			rotBoundingBoxp1[0] = rotBoundingBoxp2[0];
			rotBoundingBoxp2[0] = num2;
		}
	}

	public void calcGroups()
	{
		if (boundingBoxp1 != null)
		{
			int num = (int)Math.Floor((rotBoundingBoxp1[0] + X) / 80f);
			int num2 = (int)Math.Floor((rotBoundingBoxp1[2] + Z) / 320f);
			int num3 = (int)Math.Floor((rotBoundingBoxp2[0] + X) / 80f);
			int num4 = (int)Math.Floor((rotBoundingBoxp2[2] + Z) / 320f);
			List<int> list = new List<int>();
			if (num < 2)
			{
				num = 2;
			}
			if (num3 < 2)
			{
				num3 = 2;
			}
			if (num > 23)
			{
				num = 23;
			}
			if (num3 > 23)
			{
				num3 = 23;
			}
			if (num != num3)
			{
				if ((num - 2) % 4 != 0)
				{
					while ((num - 2) % 4 != 0)
					{
						num--;
						if (num < 0)
						{
							num = 2;
							break;
						}
					}
				}
				if ((num3 - 2) % 4 != 0)
				{
					while ((num3 - 2) % 4 != 0)
					{
						num3++;
						if (num3 > 23)
						{
							num3 = 23;
							break;
						}
					}
				}
				for (int i = 0; i < (num3 - num) / 4 + 1; i++)
				{
					list.Add(num + 4 * i);
				}
			}
			else
			{
				if ((num - 2) % 4 != 0)
				{
					while ((num - 2) % 4 != 0)
					{
						num++;
						if (num < 23)
						{
							num = 23;
							break;
						}
					}
				}
				list.Add(num);
			}
			List<int> list2 = new List<int>();
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num2 > 5)
			{
				num2 = 5;
			}
			if (num4 > 5)
			{
				num4 = 5;
			}
			if (num2 != num4)
			{
				for (int j = 0; j < num4 - num2 + 1; j++)
				{
					list2.Add(num2 + j);
				}
			}
			else
			{
				list2.Add(num2);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				for (int l = 0; l < list.Count; l++)
				{
					groups.Add(list2[k] * 24 + list[l]);
				}
			}
			return;
		}
		int num5 = (int)Math.Floor(X / 80f);
		while ((num5 - 2) % 4 != 0)
		{
			num5++;
			if (num5 < 23)
			{
				num5 = 23;
				break;
			}
		}
		int num6 = (int)Math.Floor(Z / 320f);
		groups.Add(num5 + 24 * num6);
	}

	public MeshTexture[] readMaterial()
	{
		BinaryReader binaryReader = new BinaryReader(File.Open(materialPath, FileMode.Open));
		binaryReader.BaseStream.Position = 12L;
		int num = binaryReader.ReadInt32();
		MeshTexture[] array = new MeshTexture[num];
		for (int i = 0; i < num; i++)
		{
			int count = binaryReader.ReadInt32();
			string tName = new string(binaryReader.ReadChars(count));
			binaryReader.BaseStream.Position += 72L;
			int count2 = binaryReader.ReadInt32();
			string text = new string(binaryReader.ReadChars(count2));
			binaryReader.BaseStream.Position += 7L;
			if (text != "")
			{
				int num2 = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, num2);
				Bitmap bitmap = null;
				string text2 = "";
				text2 = ((!text.Contains('\\')) ? (materialPath.Substring(0, materialPath.LastIndexOf('\\') + 1) + text) : (pathPrefix + text));
				byte[] array2 = new byte[0];
				if (!File.Exists(text2))
				{
					text2 = text2.Replace(".ddj", ".dds");
					array2 = File.ReadAllBytes(text2);
				}
				else
				{
					array2 = File.ReadAllBytes(text2).Skip(20).ToArray();
				}
				DDSImage dDSImage = new DDSImage(array2);
				bitmap = dDSImage.BitmapImage;
				dDSImage = null;
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
				bitmap.UnlockBits(bitmapData);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 9729);
				GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, 8448);
				array[i] = new MeshTexture(tName, num2);
			}
		}
		binaryReader.Close();
		binaryReader.Dispose();
		return array;
	}

	private void readModel(string path)
	{
		BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
		binaryReader.BaseStream.Position = 12L;
		int num = binaryReader.ReadInt32();
		binaryReader.BaseStream.Position += 4L;
		int num2 = binaryReader.ReadInt32();
		binaryReader.BaseStream.Position = 72L;
		int num3 = binaryReader.ReadInt32();
		binaryReader.BaseStream.Position += num3;
		int count = binaryReader.ReadInt32();
		Tex[readingMesh] = new string(binaryReader.ReadChars(count));
		binaryReader.BaseStream.Position = num;
		int num4 = binaryReader.ReadInt32();
		Verts[readingMesh] = new float[num4, 3];
		UV[readingMesh] = new float[num4, 2];
		for (int i = 0; i < num4; i++)
		{
			Verts[readingMesh][i, 0] = binaryReader.ReadSingle();
			Verts[readingMesh][i, 1] = binaryReader.ReadSingle();
			Verts[readingMesh][i, 2] = binaryReader.ReadSingle();
			binaryReader.BaseStream.Position += 12L;
			UV[readingMesh][i, 0] = binaryReader.ReadSingle();
			UV[readingMesh][i, 1] = binaryReader.ReadSingle();
			binaryReader.BaseStream.Position += 16L;
			int num5 = Math.Abs(binaryReader.ReadInt32());
			if (num5 < 0 || num5 > 10)
			{
				binaryReader.BaseStream.Position -= 8L;
			}
		}
		binaryReader.BaseStream.Position = num2;
		int num6 = binaryReader.ReadInt32();
		Faces[readingMesh] = new int[num6, 3];
		for (int j = 0; j < num6; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				Faces[readingMesh][j, k] = binaryReader.ReadInt16();
			}
		}
		binaryReader.Close();
		readingMesh++;
	}

	public void FindTex(List<string> texNames, List<int> texIDs)
	{
		if (Tex == null)
		{
			return;
		}
		TexIDs = new int[Tex.Length];
		for (int i = 0; i < Tex.Length; i++)
		{
			if (texNames.IndexOf(Tex[i]) != -1)
			{
				TexIDs[i] = texIDs[texNames.IndexOf(Tex[i])];
			}
		}
	}
}
