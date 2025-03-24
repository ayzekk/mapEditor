using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using SroMapEditor.DDSReader;

namespace SroMapEditor;

internal class Terrain
{
	private string pathPrefix = "Map\\tile2d\\";

	private float[][][] Heights = new float[36][][];

	private int[][][] Tex = new int[36][][];

	private List<int> FileTexIDs = new List<int>();

	private int[] TexIDs;

	private string[] TexPaths;

	public Terrain(string path)
	{
		getTexPaths();
		BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open));
		binaryReader.BaseStream.Position = 12L;
		for (int i = 0; i < 36; i++)
		{
			Heights[i] = new float[17][];
			Tex[i] = new int[17][];
			binaryReader.BaseStream.Position += 6L;
			for (int j = 0; j < 17; j++)
			{
				Heights[i][j] = new float[17];
				Tex[i][j] = new int[17];
			}
			for (int k = 0; k < 17; k++)
			{
				for (int l = 0; l < 17; l++)
				{
					Heights[i][l][k] = binaryReader.ReadSingle();
					Tex[i][l][k] = binaryReader.ReadByte();
					binaryReader.BaseStream.Position += 2L;
				}
			}
			binaryReader.BaseStream.Position += 546L;
		}
		binaryReader.Close();
		binaryReader.Dispose();
		getTex();
	}

	private void getTex()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < 36; i++)
		{
			for (int j = 0; j < 17; j++)
			{
				for (int k = 0; k < 17; k++)
				{
					if (!list.Contains(Tex[i][j][k]))
					{
						list.Add(Tex[i][j][k]);
						int num = GL.GenTexture();
						GL.BindTexture(TextureTarget.Texture2D, num);
						Bitmap bitmap = null;
						string text = pathPrefix + TexPaths[Tex[i][j][k]];
						byte[] array = new byte[0];
						if (!File.Exists(text))
						{
							text = text.Replace(".ddj", ".dds");
							array = File.ReadAllBytes(text);
						}
						else
						{
							array = File.ReadAllBytes(text).Skip(20).ToArray();
						}
						DDSImage dDSImage = new DDSImage(array);
						bitmap = dDSImage.BitmapImage;
						dDSImage = null;
						BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
						bitmap.UnlockBits(bitmapData);
						GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 9729);
						FileTexIDs.Add(Tex[i][j][k]);
						list2.Add(num);
					}
				}
			}
		}
		TexIDs = list2.ToArray();
		list2.Clear();
	}

	public void Draw()
	{
		GL.Color3(byte.MaxValue, byte.MaxValue, byte.MaxValue);
		for (int i = 0; i < 36; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				for (int k = 0; k < 16; k++)
				{
					GL.Enable(EnableCap.Texture2D);
					GL.BindTexture(TextureTarget.Texture2D, TexIDs[FileTexIDs.IndexOf(Tex[i][j][k])]);
					GL.Begin(BeginMode.TriangleStrip);
					GL.TexCoord2((float)j / 2f, (float)k / 2f);
					GL.Vertex3(j * 20 + i % 6 * 320, (double)(k * 20) + Math.Floor((double)i / 6.0) * 320.0, Heights[i][j][k]);
					GL.TexCoord2((float)(j + 1) / 2f, (float)k / 2f);
					GL.Vertex3((j + 1) * 20 + i % 6 * 320, (double)(k * 20) + Math.Floor((double)i / 6.0) * 320.0, Heights[i][j + 1][k]);
					GL.TexCoord2((float)j / 2f, (float)(k + 1) / 2f);
					GL.Vertex3(j * 20 + i % 6 * 320, (double)((k + 1) * 20) + Math.Floor((double)i / 6.0) * 320.0, Heights[i][j][k + 1]);
					GL.TexCoord2((float)(j + 1) / 2f, (float)(k + 1) / 2f);
					GL.Vertex3((j + 1) * 20 + i % 6 * 320, (double)((k + 1) * 20) + Math.Floor((double)i / 6.0) * 320.0, Heights[i][j + 1][k + 1]);
					GL.End();
					GL.Disable(EnableCap.Texture2D);
				}
			}
		}
	}

	private void getTexPaths()
	{
		if (!File.Exists("Map\\tile2d.ifo"))
		{
			MessageBox.Show("Could not find tile2d.ifo. Terminating application.");
			Environment.Exit(1);
		}
		string[] array = File.ReadAllLines("Map\\tile2d.ifo");
		TexPaths = new string[array.Length - 2];
		for (int i = 0; i < TexPaths.Length; i++)
		{
			string text = array[i + 2].Split(' ')[3];
			TexPaths[i] = text.Substring(1, text.Length - 2);
		}
	}
}
