using System.IO;
using PoViEmu.Base.Gfx;
using PoViEmu.Tests.Base;
using SixLabors.ImageSharp;

namespace PoViEmu.Tests.Gfx
{
    public static class ImageCheck
    {
        public static void DoShouldReadBinary(string dir, string fileName)
        {
            var file = Path.Combine(dir, fileName);
            var bytes = File.ReadAllBytes(file);

            var bFile = Path.Combine(dir, $"{fileName}.bmp");
            using var bStream = File.Create(bFile);
            ImageReader.FromPvToBmp(bytes, bStream);

            var pFile = Path.Combine(dir, $"{fileName}.png");
            using var pStream = File.Create(pFile);
            using var pImg = ImageReader.FromPvToBmp(bytes);
            pImg.SaveAsPng(pStream);
        }

        public static void DoShouldReadImage(string dir, string fileName)
        {
            var file = Path.Combine(dir, fileName);
            var bytes = File.ReadAllBytes(file);

            using var stream = new MemoryStream();
            ImageWriter.FromBmpToPv(bytes, stream);
            var actual = stream.ToArray();

            var bFile = Path.Combine(dir, $"{fileName}.p.bin");
            File.WriteAllBytes(bFile, actual);

            var baseName = file.Replace(Path.GetExtension(fileName), string.Empty);
            var expected = File.ReadAllBytes($"{baseName}.bin");
            TestTool.Equal(expected, actual);
        }
    }
}