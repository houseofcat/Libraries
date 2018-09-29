using LZ4;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using static HouseofCat.Models.Enums;

namespace HouseofCat.Library.IO
{
    /// <summary>
    /// CookedRabbit Utility class to help compress objects.
    /// </summary>
    public static class Compression
    {
        /// <summary>
        /// Compresses the byte[] with the optional compression method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<byte[]> CompressAsync(byte[] input, CompressionMethod method = CompressionMethod.LZ4)
        {
            byte[] output = null;

            switch (method)
            {
                case CompressionMethod.Gzip:
                    output = await CompressBytesWithGzipAsync(input);
                    break;
                case CompressionMethod.Deflate:
                    output = await CompressBytesWithDeflateAsync(input);
                    break;
                case CompressionMethod.LZ4:
                    output = await CompressBytesWithLZ4Async(input);
                    break;
                case CompressionMethod.LZ4Codec:
                    output = await WrapBytesWithLZ4Async(input);
                    break;
                default: break;
            }

            return output;
        }

        /// <summary>
        /// Decompresses the byte[] with the optional compression method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static async Task<byte[]> DecompressAsync(byte[] input, CompressionMethod method = CompressionMethod.LZ4)
        {
            byte[] output = null;

            switch (method)
            {
                case CompressionMethod.Gzip:
                    output = await DecompressBytesWithGzipAsync(input);
                    break;
                case CompressionMethod.Deflate:
                    output = await DecompressBytesWithDeflateAsync(input);
                    break;
                case CompressionMethod.LZ4:
                    output = await DecompressBytesWithLZ4Async(input);
                    break;
                case CompressionMethod.LZ4Codec:
                    output = await UnwrapBytesWithLZ4Async(input);
                    break;
                default: break;
            }

            return output;
        }

        /// <summary>
        /// Compresses a byte[] with Gzip.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> CompressBytesWithGzipAsync(byte[] input)
        {
            byte[] output = null;

            using (var compressedStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, false))
                { await gzipStream.WriteAsync(input, 0, input.Length); }

                output = compressedStream.ToArray();
            }

            return output;
        }

        /// <summary>
        /// Decompresses a byte[] with Gzip.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> DecompressBytesWithGzipAsync(byte[] input)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var compressedStream = new MemoryStream(input))
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress, false))
                { await gzipStream.CopyToAsync(outputStream); }

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// Compresses a byte[] with Deflate.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> CompressBytesWithDeflateAsync(byte[] input)
        {
            byte[] output = null;

            using (var memoryStream = new MemoryStream()) // Don't stack these usings, Gzip stream won't flush until dispose
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, false))
                { await deflateStream.WriteAsync(input, 0, input.Length); }

                output = memoryStream.ToArray();
            }

            return output;
        }

        /// <summary>
        /// Decompresses a byte[] with Deflate.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> DecompressBytesWithDeflateAsync(byte[] input)
        {
            byte[] output = null;

            using (var outboundStream = new MemoryStream()) // Don't stack this using, Gzip stream won't flush until dispose
            {
                using (var compressedStream = new MemoryStream(input))
                using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress, false))
                { await deflateStream.CopyToAsync(outboundStream); }

                output = outboundStream.ToArray();
            }

            return output;
        }

        /// <summary>
        /// Compresses a byte[] with LZ4.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> CompressBytesWithLZ4Async(byte[] input)
        {
            byte[] output = null;

            using (var outputStream = new MemoryStream())
            {
                using (var lz4Stream = new LZ4Stream(outputStream, LZ4StreamMode.Compress))
                { await lz4Stream.WriteAsync(input, 0, input.Length); }

                output = outputStream.ToArray();
            }

            return output;
        }

        /// <summary>
        /// Decompresses a byte[] with LZ4.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> DecompressBytesWithLZ4Async(byte[] input)
        {
            byte[] output = null;

            using (var outboundStream = new MemoryStream()) // Don't stack this using, Gzip stream won't flush until dispose
            {
                using (var compressedStream = new MemoryStream(input))
                using (var lz4Stream = new LZ4Stream(compressedStream, LZ4StreamMode.Decompress))
                {
                    await lz4Stream.CopyToAsync(outboundStream);
                }

                output = outboundStream.ToArray();
            }

            return output;
        }

        /// <summary>
        /// Compresses a byte[] with LZ4.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> WrapBytesWithLZ4Async(byte[] input)
        {
            return await Task.Run(() => { return LZ4Codec.Wrap(input); });
        }

        /// <summary>
        /// Decompresses a byte[] with LZ4.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> UnwrapBytesWithLZ4Async(byte[] input)
        {
            return await Task.Run(() => { return LZ4Codec.Unwrap(input); });
        }
    }
}
