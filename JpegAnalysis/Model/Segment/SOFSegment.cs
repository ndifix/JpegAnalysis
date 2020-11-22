using System;
using System.Collections.Generic;
using System.Text;

namespace JpegAnalysis.Model.Segment
{
	class SOFSegment : SegmentBase
	{
		#region Property

		/// <summary>
		/// precision
		/// </summary>
		public int P { get; private set; }

		/// <summary>
		/// horizontal image size
		/// </summary>
		public int X { get; private set; }

		/// <summary>
		/// vertical image size
		/// </summary>
		public int Y { get; private set; }

		/// <summary>
		/// number of image components in frame
		/// </summary>
		public int Nf { get; private set; }

		#endregion

		public SOFSegment(Marker marker) : base(marker)
		{ }

		public override void ReadSegment(List<byte> bytes)
		{
			base.ReadSegment(bytes);
			P = param[0];
			Y = param[1] * 256 + param[2];
			X = param[3] * 256 + param[4];
			Nf = param[5];
		}

		/// <summary>
		/// component id
		/// </summary>
		public int C(int index)
		{
			if (index < 0 || index >= Nf)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[6 + 3 * index];
		}

		/// <summary>
		/// horizontal sampling factor
		/// </summary>
		public int H(int index)
		{
			if (index < 0 || index >= Nf)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[7 + 3 * index] / 16;
		}

		/// <summary>
		///  vertical sampling factor
		/// </summary>
		public int V(int index)
		{
			if (index < 0 || index >= Nf)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[7 + 3 * index] % 16;
		}

		/// <summary>
		/// quantization table destination selector
		/// </summary>
		public int Tq(int index)
		{
			if (index < 0 || index >= Nf)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[8 + 3 * index];
		}
	}
}
