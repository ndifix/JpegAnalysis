using System;
using System.Collections.Generic;
using System.Text;

namespace JpegAnalysis.Model
{
	class Marker
	{
		byte[] marker = new byte[2];

		public void Read(List<byte> bytes) {
			marker[0] = bytes[0];
			marker[1] = bytes[1];
			bytes.RemoveRange(0, 2);
		}

		public bool IsEOI()
		{
			return marker[0] == 0xff && marker[1] == 0xd8;
		}

		public string toString()
		{
			return BitConverter.ToString(marker).Replace("-", "");
		}
	}
}
