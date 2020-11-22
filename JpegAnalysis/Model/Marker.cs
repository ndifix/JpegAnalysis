using System;
using System.Collections.Generic;
using System.Text;

namespace JpegAnalysis.Model
{
	class Marker
	{
		byte[] marker = new byte[2];

		public string Id
		{
			get
			{
				if (marker[0] != 0xff) return null;
				if (marker[1] == 0xd8) return "SOI";
				if (marker[1] == 0xd9) return "EOI";
				if (marker[1] >= 0xe0 && marker[1] <= 0xef) return "APPn";
				if (marker[1] == 0xdb) return "DQT";
				if (marker[1] == 0xc4) return "DHT";
				if ((marker[1] / 16 == 12 && marker[1] % 4 != 0) || marker[1] == 0xc0) return "SOF";
				if (marker[1] == 0xda) return "SOS";

				return null;
			}
		}

		public void Read(List<byte> bytes) {
			marker[0] = bytes[0];
			marker[1] = bytes[1];
			bytes.RemoveRange(0, 2);
		}

		public string toString()
		{
			return BitConverter.ToString(marker).Replace("-", "");
		}
	}
}
