using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SroMapEditor.DDSReader;

public class DDSImage
{
	private struct Colour8888
	{
		public byte r;

		public byte g;

		public byte b;

		public byte a;
	}

	public const string SUPPORTED_ENCODERS = "DXT1 DXT3";

	private const uint FOURCC_DXT1 = 827611204u;

	private const uint FOURCC_DXT2 = 844388420u;

	private const uint FOURCC_DXT3 = 861165636u;

	private const uint FOURCC_DXT4 = 877942852u;

	private const uint FOURCC_DXT5 = 894720068u;

	private const uint FOURCC_ATI1 = 826889281u;

	private const uint FOURCC_ATI2 = 843666497u;

	private const uint FOURCC_RXGB = 1111971922u;

	private const uint FOURCC_DOLLARNULL = 36u;

	private const uint FOURCC_oNULL = 111u;

	private const uint FOURCC_pNULL = 112u;

	private const uint FOURCC_qNULL = 113u;

	private const uint FOURCC_rNULL = 114u;

	private const uint FOURCC_sNULL = 115u;

	private const uint FOURCC_tNULL = 116u;

	private const uint DDS_LINEARSIZE = 524288u;

	private const uint DDS_PITCH = 8u;

	private const uint DDS_FOURCC = 4u;

	private const uint DDS_LUMINANCE = 131072u;

	private const uint DDS_ALPHAPIXELS = 1u;

	private static byte[] DDS_HEADER = Convert.FromBase64String("RERTIA==");

	private byte[] signature;

	private uint size1;

	private uint flags1;

	private uint height;

	private uint width;

	private uint linearsize;

	private uint depth;

	private uint mipmapcount;

	private uint alphabitdepth;

	private uint size2;

	private uint flags2;

	private uint fourcc;

	private uint rgbbitcount;

	private uint rbitmask;

	private uint bbitmask;

	private uint gbitmask;

	private uint alphabitmask;

	private uint ddscaps1;

	private uint ddscaps2;

	private uint ddscaps3;

	private uint ddscaps4;

	private uint texturestage;

	private PixelFormat CompFormat;

	private uint blocksize;

	private uint bpp;

	private uint bps;

	private uint sizeofplane;

	private uint compsize;

	private byte[] compdata;

	private byte[] rawidata;

	private BinaryReader br;

	private Bitmap img;

	public Bitmap BitmapImage => img;

	public unsafe DDSImage(byte[] ddsimage)
	{
		MemoryStream memoryStream = new MemoryStream(ddsimage.Length);
		memoryStream.Write(ddsimage, 0, ddsimage.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		br = new BinaryReader(memoryStream);
		signature = br.ReadBytes(4);
		if (!IsByteArrayEqual(signature, DDS_HEADER))
		{
			Console.WriteLine("Got header of '" + Encoding.ASCII.GetString(signature, 0, signature.Length) + "'.");
		}
		size1 = br.ReadUInt32();
		flags1 = br.ReadUInt32();
		height = br.ReadUInt32();
		width = br.ReadUInt32();
		linearsize = br.ReadUInt32();
		depth = br.ReadUInt32();
		mipmapcount = br.ReadUInt32();
		alphabitdepth = br.ReadUInt32();
		for (int i = 0; i < 10; i++)
		{
			br.ReadUInt32();
		}
		size2 = br.ReadUInt32();
		flags2 = br.ReadUInt32();
		fourcc = br.ReadUInt32();
		rgbbitcount = br.ReadUInt32();
		rbitmask = br.ReadUInt32();
		gbitmask = br.ReadUInt32();
		bbitmask = br.ReadUInt32();
		alphabitmask = br.ReadUInt32();
		ddscaps1 = br.ReadUInt32();
		ddscaps2 = br.ReadUInt32();
		ddscaps3 = br.ReadUInt32();
		ddscaps4 = br.ReadUInt32();
		texturestage = br.ReadUInt32();
		if (depth == 0)
		{
			depth = 1u;
		}
		if ((flags2 & 4) != 0)
		{
			blocksize = (width + 3) / 4 * ((height + 3) / 4) * depth;
			switch (fourcc)
			{
			case 827611204u:
				CompFormat = PixelFormat.DXT1;
				blocksize *= 8u;
				break;
			case 844388420u:
				CompFormat = PixelFormat.DXT2;
				blocksize *= 16u;
				break;
			case 861165636u:
				CompFormat = PixelFormat.DXT3;
				blocksize *= 16u;
				break;
			case 877942852u:
				CompFormat = PixelFormat.DXT4;
				blocksize *= 16u;
				break;
			case 894720068u:
				CompFormat = PixelFormat.DTX5;
				blocksize *= 16u;
				break;
			case 826889281u:
				CompFormat = PixelFormat.ATI1N;
				blocksize *= 8u;
				break;
			case 843666497u:
				CompFormat = PixelFormat.THREEDC;
				blocksize *= 16u;
				break;
			case 1111971922u:
				CompFormat = PixelFormat.RXGB;
				blocksize *= 16u;
				break;
			case 36u:
				CompFormat = PixelFormat.A16B16G16R16;
				blocksize = width * height * depth * 8;
				break;
			case 111u:
				CompFormat = PixelFormat.R16F;
				blocksize = width * height * depth * 2;
				break;
			case 112u:
				CompFormat = PixelFormat.G16R16F;
				blocksize = width * height * depth * 4;
				break;
			case 113u:
				CompFormat = PixelFormat.A16B16G16R16F;
				blocksize = width * height * depth * 8;
				break;
			case 114u:
				CompFormat = PixelFormat.R32F;
				blocksize = width * height * depth * 4;
				break;
			case 115u:
				CompFormat = PixelFormat.G32R32F;
				blocksize = width * height * depth * 8;
				break;
			case 116u:
				CompFormat = PixelFormat.A32B32G32R32F;
				blocksize = width * height * depth * 16;
				break;
			default:
				CompFormat = PixelFormat.UNKNOWN;
				blocksize *= 16u;
				break;
			}
		}
		else
		{
			if ((flags2 & 0x20000) != 0)
			{
				if ((flags2 & 1) != 0)
				{
					CompFormat = PixelFormat.LUMINANCE_ALPHA;
				}
				else
				{
					CompFormat = PixelFormat.LUMINANCE;
				}
			}
			else if ((flags2 & 1) != 0)
			{
				CompFormat = PixelFormat.ARGB;
			}
			else
			{
				CompFormat = PixelFormat.RGB;
			}
			blocksize = width * height * depth * (rgbbitcount >> 3);
		}
		_ = CompFormat;
		_ = 19;
		if ((flags1 & 0x80008) == 0 || linearsize == 0)
		{
			flags1 |= 524288u;
			linearsize = blocksize;
		}
		ReadData();
		bpp = PixelFormatToBpp(CompFormat);
		bps = width * bpp * PixelFormatToBpc(CompFormat);
		sizeofplane = bps * height;
		rawidata = new byte[depth * sizeofplane + height * bps + width * bpp];
		switch (CompFormat)
		{
		case PixelFormat.ARGB:
		case PixelFormat.RGB:
		case PixelFormat.LUMINANCE:
		case PixelFormat.LUMINANCE_ALPHA:
			DecompressARGB();
			break;
		case PixelFormat.DXT1:
			DecompressDXT1();
			break;
		case PixelFormat.DXT3:
			DecompressDXT3();
			break;
		}
		img = new Bitmap((int)width, (int)height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		BitmapData bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		IntPtr scan = bitmapData.Scan0;
		int num = img.Width * img.Height * 4;
		byte* ptr = (byte*)(void*)scan;
		for (int j = 0; j < num; j += 4)
		{
			ptr[j] = rawidata[j + 2];
			ptr[j + 1] = rawidata[j + 1];
			ptr[j + 2] = rawidata[j];
			ptr[j + 3] = rawidata[j + 3];
		}
		img.UnlockBits(bitmapData);
		rawidata = null;
		compdata = null;
	}

	private static bool IsByteArrayEqual(byte[] arg0, byte[] arg1)
	{
		if (arg0.Length != arg1.Length)
		{
			return false;
		}
		for (int i = 0; i < arg0.Length; i++)
		{
			if (arg0[i] != arg1[i])
			{
				return false;
			}
		}
		return true;
	}

	private uint PixelFormatToBpp(PixelFormat pf)
	{
		switch (pf)
		{
		case PixelFormat.ARGB:
		case PixelFormat.LUMINANCE:
		case PixelFormat.LUMINANCE_ALPHA:
			return rgbbitcount / 8;
		case PixelFormat.RGB:
		case PixelFormat.THREEDC:
		case PixelFormat.RXGB:
			return 3u;
		case PixelFormat.ATI1N:
			return 1u;
		case PixelFormat.R16F:
			return 2u;
		case PixelFormat.A16B16G16R16:
		case PixelFormat.A16B16G16R16F:
		case PixelFormat.G32R32F:
			return 8u;
		case PixelFormat.A32B32G32R32F:
			return 16u;
		default:
			return 4u;
		}
	}

	private uint PixelFormatToBpc(PixelFormat pf)
	{
		switch (pf)
		{
		case PixelFormat.R16F:
		case PixelFormat.G16R16F:
		case PixelFormat.A16B16G16R16F:
			return 4u;
		case PixelFormat.R32F:
		case PixelFormat.G32R32F:
		case PixelFormat.A32B32G32R32F:
			return 4u;
		case PixelFormat.A16B16G16R16:
			return 2u;
		default:
			return 1u;
		}
	}

	private uint PixelFormatToChannelCount(PixelFormat pf)
	{
		switch (pf)
		{
		case PixelFormat.RGB:
		case PixelFormat.THREEDC:
		case PixelFormat.RXGB:
			return 3u;
		case PixelFormat.ATI1N:
		case PixelFormat.LUMINANCE:
		case PixelFormat.R16F:
		case PixelFormat.R32F:
			return 1u;
		case PixelFormat.LUMINANCE_ALPHA:
		case PixelFormat.G16R16F:
		case PixelFormat.G32R32F:
			return 2u;
		default:
			return 4u;
		}
	}

	private void ReadData()
	{
		compdata = null;
		if ((flags1 & 0x80000) > 1)
		{
			compdata = br.ReadBytes((int)linearsize);
			compsize = (uint)compdata.Length;
			return;
		}
		uint num = width * rgbbitcount / 8;
		compsize = num * height * depth;
		compdata = new byte[compsize];
		MemoryStream memoryStream = new MemoryStream((int)compsize);
		for (int i = 0; i < depth; i++)
		{
			for (int j = 0; j < height; j++)
			{
				byte[] array = br.ReadBytes((int)bps);
				memoryStream.Write(array, 0, array.Length);
			}
		}
		memoryStream.Seek(0L, SeekOrigin.Begin);
		memoryStream.Read(compdata, 0, compdata.Length);
		memoryStream.Close();
	}

	private void DecompressARGB()
	{
	}

	private void DecompressDXT1()
	{
		Colour8888[] array = new Colour8888[4];
		MemoryStream memoryStream = new MemoryStream(compdata.Length);
		memoryStream.Write(compdata, 0, compdata.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		BinaryReader binaryReader = new BinaryReader(memoryStream);
		array[0].a = byte.MaxValue;
		array[1].a = byte.MaxValue;
		array[2].a = byte.MaxValue;
		for (int i = 0; i < depth; i++)
		{
			for (int j = 0; j < height; j += 4)
			{
				for (int k = 0; k < width; k += 4)
				{
					ushort num = binaryReader.ReadUInt16();
					ushort num2 = binaryReader.ReadUInt16();
					ReadColour(num, ref array[0]);
					ReadColour(num2, ref array[1]);
					uint num3 = binaryReader.ReadUInt32();
					if (num > num2)
					{
						array[2].b = (byte)((2 * array[0].b + array[1].b + 1) / 3);
						array[2].g = (byte)((2 * array[0].g + array[1].g + 1) / 3);
						array[2].r = (byte)((2 * array[0].r + array[1].r + 1) / 3);
						array[3].b = (byte)((array[0].b + 2 * array[1].b + 1) / 3);
						array[3].g = (byte)((array[0].g + 2 * array[1].g + 1) / 3);
						array[3].r = (byte)((array[0].r + 2 * array[1].r + 1) / 3);
						array[3].a = byte.MaxValue;
					}
					else
					{
						array[2].b = (byte)((array[0].b + array[1].b) / 2);
						array[2].g = (byte)((array[0].g + array[1].g) / 2);
						array[2].r = (byte)((array[0].r + array[1].r) / 2);
						array[3].b = 0;
						array[3].g = 0;
						array[3].r = 0;
						array[3].a = 0;
					}
					int l = 0;
					int num4 = 0;
					for (; l < 4; l++)
					{
						int num5 = 0;
						while (num5 < 4)
						{
							int num6 = (int)((num3 & (3 << num4 * 2)) >> num4 * 2);
							if (k + num5 < width && j + l < height)
							{
								uint num7 = (uint)(i * sizeofplane + (j + l) * bps + (k + num5) * bpp);
								rawidata[num7] = array[num6].r;
								rawidata[num7 + 1] = array[num6].g;
								rawidata[num7 + 2] = array[num6].b;
								rawidata[num7 + 3] = array[num6].a;
							}
							num5++;
							num4++;
						}
					}
				}
			}
		}
	}

	private void DecompressDXT3()
	{
		Colour8888[] array = new Colour8888[4];
		MemoryStream memoryStream = new MemoryStream(compdata.Length);
		memoryStream.Write(compdata, 0, compdata.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		BinaryReader binaryReader = new BinaryReader(memoryStream);
		for (int i = 0; i < depth; i++)
		{
			for (int j = 0; j < height; j += 4)
			{
				for (int k = 0; k < width; k += 4)
				{
					byte[] array2 = binaryReader.ReadBytes(8);
					ushort data = binaryReader.ReadUInt16();
					ushort data2 = binaryReader.ReadUInt16();
					ReadColour(data, ref array[0]);
					ReadColour(data2, ref array[1]);
					uint num = binaryReader.ReadUInt32();
					array[2].b = (byte)((2 * array[0].b + array[1].b + 1) / 3);
					array[2].g = (byte)((2 * array[0].g + array[1].g + 1) / 3);
					array[2].r = (byte)((2 * array[0].r + array[1].r + 1) / 3);
					array[3].b = (byte)((array[0].b + 2 * array[1].b + 1) / 3);
					array[3].g = (byte)((array[0].g + 2 * array[1].g + 1) / 3);
					array[3].r = (byte)((array[0].r + 2 * array[1].r + 1) / 3);
					int l = 0;
					int num2 = 0;
					for (; l < 4; l++)
					{
						for (int m = 0; m < 4; m++)
						{
							int num3 = (int)((num & (3 << num2 * 2)) >> num2 * 2);
							if (k + m < width && j + l < height)
							{
								uint num4 = (uint)(i * sizeofplane + (j + l) * bps + (k + m) * bpp);
								rawidata[num4] = array[num3].r;
								rawidata[num4 + 1] = array[num3].g;
								rawidata[num4 + 2] = array[num3].b;
							}
							num2++;
						}
					}
					for (l = 0; l < 4; l++)
					{
						ushort num5 = (ushort)(array2[2 * l] + 256 * array2[2 * l + 1]);
						for (int m = 0; m < 4; m++)
						{
							if (k + m < width && j + l < height)
							{
								uint num4 = (uint)(i * sizeofplane + (j + l) * bps + (k + m) * bpp + 3);
								rawidata[num4] = (byte)(num5 & 0xF);
								rawidata[num4] = (byte)(rawidata[num4] | (rawidata[num4] << 4));
							}
							num5 >>= 4;
						}
					}
				}
			}
		}
	}

	private void ReadColour(ushort Data, ref Colour8888 op)
	{
		byte b = (byte)(Data & 0x1F);
		byte b2 = (byte)((Data & 0x7E0) >> 5);
		byte b3 = (byte)((Data & 0xF800) >> 11);
		op.r = (byte)(b3 * 255 / 31);
		op.g = (byte)(b2 * 255 / 63);
		op.b = (byte)(b * 255 / 31);
	}
}
