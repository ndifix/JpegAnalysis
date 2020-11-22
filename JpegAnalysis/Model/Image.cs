using System;
using System.Collections.Generic;
using System.Text;

namespace JpegAnalysis.Model
{
	class Image
	{
		private byte[] binary;

		public void ReadImageData(List<byte> bytes)
		{
			binary = bytes.GetRange(0,bytes.Count-2).ToArray();
			bytes.RemoveRange(0, bytes.Count - 2);
		}
	}
}
