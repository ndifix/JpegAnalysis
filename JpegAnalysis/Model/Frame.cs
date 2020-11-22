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
		}

		private async Task ReadFrameAsyncImpl(string inPath)
		{
			if (!File.Exists(inPath))
			{
				throw new Exception($"存在しないファイルパスです。:{inPath}");
			}

			byte[] binary;
			using (var fs = new FileStream(inPath, FileMode.Open))
			{
				binary = new byte[fs.Length];
				await fs.ReadAsync(binary, 0, (int)fs.Length);
			}
		}
	}
}
