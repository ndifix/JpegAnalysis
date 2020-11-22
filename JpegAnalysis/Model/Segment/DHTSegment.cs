using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JpegAnalysis.Model.Segment
{
	class HuffmanTable
	{
		/// <summary>
		/// table class
		/// </summary>
		public int Tc { get; private set; }

		/// <summary>
		/// table destination identifier
		/// </summary>
		public int Th { get; private set; }

		public void ReadTable(List<byte> bin)
		{
			Tc = bin[0] / 16;
			Th = bin[0] % 16;

			int[] L = new int[16];
			for (int i = 0; i < 16; i++)
			{
				L[i] = bin[i + 1];
			}
			bin.RemoveRange(0, 17);
			List<int>[] V = new List<int>[16];
			for (int i = 0; i < 16; i++)
			{
				V[i] = new List<int>();
				for (int j = 0; j < L[i]; j++)
				{
					V[i].Add(bin[j]);
				}
				bin.RemoveRange(0, L[i]);
			}
		}
	}

	class DHTSegment : SegmentBase
	{
		public List<HuffmanTable> Tables { get; }

		public DHTSegment(Marker marker) : base(marker)
		{
			Tables = new List<HuffmanTable>();
		}

		public override void ReadSegment(List<byte> bytes)
		{
			base.ReadSegment(bytes);
			List<byte> bin = param.ToList();
			while (bin.Count > 0)
			{
				HuffmanTable table = new HuffmanTable();
				table.ReadTable(bin);
				Tables.Add(table);
			}
		}
	}
}
