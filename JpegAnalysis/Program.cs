using System;
using System.IO;
using JpegAnalysis.Model;

namespace JpegAnalysis
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("invalid argument");
				return;
			}

			string inPath = args[0];
			Frame frame = new Frame();
			frame.ReadFrame(inPath);
		}
	}
}
