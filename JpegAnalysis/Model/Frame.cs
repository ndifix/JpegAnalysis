using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JpegAnalysis.Model.Segment;

namespace JpegAnalysis.Model
{
	class Frame
	{
		Image image = new Image();

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
			if (marker.Id=="EOI")
			{
				throw new Exception($"開始マーカーが不整合。:{marker.toString()}");
			}

			Console.WriteLine("ファイルの解析を開始。");
			while (binList.Count > 0)
			{
				marker.Read(binList);
				if (marker.Id != null)
				{
					if (marker.Id == "EOI")
					{
						Console.WriteLine("ファイルの解析を終了。");
						return;
					}

					SegmentBase segment = new SegmentBase(marker);
					segment.ReadSegment(binList);
					if (marker.Id == "SOS")
					{
						image.ReadImageData(binList);
					}
				}
				else
				{
					throw new Exception($"ファイルが壊れています。{marker.toString()}");
				}
			}

		}
	}
}
