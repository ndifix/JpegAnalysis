using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JpegAnalysis.Model
{
	class Frame
	{
		private Task readTask;

		~Frame () {
			if (readTask != null)
			{
				readTask.Wait();
			}
		}

		public void ReadFrame(string inPath)
		{
			readTask = ReadFrameAsyncImpl(inPath);
			readTask.Wait();
		}

		private async Task ReadFrameAsyncImpl(string inPath)
		{
			if (!File.Exists(inPath))
			{
				throw new Exception($"存在しないファイルパスです。:{inPath}");
			}

			List<byte> binList;
			using (var fs = new FileStream(inPath, FileMode.Open))
			{
				byte[] binary = new byte[fs.Length];
				await fs.ReadAsync(binary, 0, (int)fs.Length);
				binList = new List<byte>(binary);
			}

			Marker marker = new Marker();
			marker.Read(binList);
			if (!marker.IsEOI())
			{
				throw new Exception($"開始マーカーが不整合。:{marker.toString()}");
			}
		}
	}
}
