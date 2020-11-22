using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JpegAnalysis.Model.Segment
{
	class QuantizeTable
	{
		/// <summary>
		/// table element precision
		/// </summary>
		public int Pq { get; private set; }

		/// <summary>
		/// table destination identifier
		/// </summary>
		public int Tq { get; private set; }

		public int[,] Q { get; }

		public QuantizeTable()
		{
			Q = new int[8, 8];
		}

		public void ReadTable(List<byte> bin)
		{
			Pq = bin[0] / 16;
			Tq = bin[0] % 16;
			if (Pq == 0)
			{
				for (int i = 0; i < 64; i++)
				{
					Q[i / 8, i % 8] = bin[i + 1];
				}
				bin.RemoveRange(0, 65);
			}
			if (Pq == 1)
			{
				for (int i = 0; i < 64; i++)
				{
					Q[i / 8, i % 8] = bin[2 * i + 1] * 256 + bin[2 * i + 2];
				}
				bin.RemoveRange(0, 129);
			}
		}
	}

	class DQTSegment : SegmentBase
	{
		public List<QuantizeTable> Tables { get; }

		public DQTSegment(Marker marker) : base(marker)
		{
			Tables = new List<QuantizeTable>();
		}

		public override void ReadSegment(List<byte> bytes)
		{
			base.ReadSegment(bytes);
			List<byte> bin = param.ToList();
			while (bin.Count > 0)
			{
				QuantizeTable table = new QuantizeTable();
				table.ReadTable(bin);
				Tables.Add(table);
			}
		}
	}
}
