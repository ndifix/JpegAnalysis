using System;
using System.Collections.Generic;
using System.Text;

namespace JpegAnalysis.Model.Segment
{
	class SOSSegment : SegmentBase
	{
		#region Property
		/// <summary>
		/// number of image components in scan
		/// </summary>
		public int Ns { get; private set; }

		/// <summary>
		/// start of spectral selection
		/// </summary>
		public int Ss { get; private set; }

		/// <summary>
		/// end of spectral selection
		/// </summary>
		public int Se { get; private set; }

		/// <summary>
		/// successive approximation bit position high
		/// </summary>
		public int Ah { get; private set; }

		/// <summary>
		/// successive approximation bit position low
		/// </summary>
		public int Al { get; private set; }

		#endregion

		public SOSSegment(Marker marker) : base(marker)
		{ }

		public override void ReadSegment(List<byte> bytes)
		{
			base.ReadSegment(bytes);
			Ns = param[0];
			Ss = param[1 + 2 * Ns];
			Se = param[2 + 2 * Ns];
			Ah = param[3 + 2 * Ns] / 16;
			Al = param[3 + 2 * Ns] % 16;
		}

		/// <summary>
		/// scan component selector
		/// </summary>
		public int C(int index)
		{
			if (index < 0 || index >= Ns)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[1 + 2 * index];
		}

		/// <summary>
		/// DC table destination selector
		/// </summary>
		public int Td(int index)
		{
			if (index < 0 || index >= Ns)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[2 + 2 * index] / 16;
		}

		/// <summary>
		/// AC table destination selector
		/// </summary>
		public int Ta(int index)
		{
			if (index < 0 || index >= Ns)
			{
				throw new Exception("範囲外参照です。");
			}
			return param[2 + 2 * index] % 16;
		}
	}
}
