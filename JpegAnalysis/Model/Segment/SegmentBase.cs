using System.Collections.Generic;

namespace JpegAnalysis.Model.Segment
{
	class SegmentBase
	{
		protected int length;

		protected byte[] param;

		public Marker SegMarker { get; private set; }

		public SegmentBase(Marker marker)
		{
			SegMarker = marker;
		}

		public virtual void ReadSegment(List<byte> bytes)
		{
			length = bytes[0] * 256 + bytes[1];
			param = bytes.GetRange(2, length - 2).ToArray();
			bytes.RemoveRange(0, length);
		}
	}
}
