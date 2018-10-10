using System.ComponentModel;

namespace HouseofCat.Models
{
    public static class Enums
    {
        /// <summary>
        /// CompressionMethod helps specify the compression method desired.
        /// </summary>
        public enum CompressionMethod
        {
            /// <summary>
            /// Compression method using builtin .NET GzipStream.
            /// </summary>
            Gzip,
            /// <summary>
            /// Compression method using builtin .NET DeflateStream.
            /// </summary>
            Deflate,
            /// <summary>
            /// Compression method using LZ4NET.
            /// </summary>
            LZ4,
            /// <summary>
            /// Compression method using LZ4NET wrap/unwrap in the LZ4Codec.
            /// </summary>
            LZ4Codec
        }

        /// <summary>
        /// SerializationMethod helps specify the serialization method desired.
        /// </summary>
        public enum SerializationMethod
        {
            /// <summary>
            /// Serialize object as a string using Utf8Json.
            /// </summary>
            JsonString,
            /// <summary>
            /// Serialize object as Utf8Json.
            /// </summary>
            Utf8Json,
            /// <summary>
            /// Serialize object as ZeroFormat.
            /// </summary>
            ZeroFormat
        }

        /// <summary>
        /// Allows for quickling setting ContentEncoding for RabbitMQ IBasicProperties.
        /// </summary>
        public enum ContentEncoding
        {
            /// <summary>
            /// ContentEncoding.Gzip
            /// </summary>
            [Description("gzip")]
            Gzip,
            /// <summary>
            /// ContentEncoding.Br
            /// </summary>
            [Description("br")]
            Brotli,
            /// <summary>
            /// ContentEncoding.Compress
            /// </summary>
            [Description("compress")]
            Compress,
            /// <summary>
            /// ContentEncoding.Deflate
            /// </summary>
            [Description("deflate")]
            Deflate,
            /// <summary>
            /// ContentEncoding.Binary
            /// </summary>
            [Description("binary")]
            Binary,
            /// <summary>
            /// ContentEncoding.Base64
            /// </summary>
            [Description("base64")]
            Base64,
        }

        /// <summary>
        /// Allows for quickling setting ContentType for RabbitMQ IBasicProperties.
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            /// ContentType.Javascript
            /// </summary>
            [Description("application/javascript;")]
            Javascript,
            /// <summary>
            /// ContentType.Json
            /// </summary>
            [Description("application/json;")]
            Json,
            /// <summary>
            /// ContentType.Urlencoded
            /// </summary>
            [Description("application/x-www-form-urlencoded;")]
            Urlencoded,
            /// <summary>
            /// ContentType.Xml
            /// </summary>
            [Description("application/xml;")]
            Xml,
            /// <summary>
            /// ContentType.Zip
            /// </summary>
            [Description("application/zip;")]
            Zip,
            /// <summary>
            /// ContentType.Pdf
            /// </summary>
            [Description("application/pdf;")]
            Pdf,
            /// <summary>
            /// ContentType.Sql
            /// </summary>
            [Description("application/sql;")]
            Sql,
            /// <summary>
            /// ContentType.Graphql
            /// </summary>
            [Description("application/graphql;")]
            Graphql,
            /// <summary>
            /// ContentType.Ldjson
            /// </summary>
            [Description("application/ld+json;")]
            Ldjson,
            /// <summary>
            /// ContentType.Msword
            /// </summary>
            [Description("application/msword(.doc);")]
            Msword,
            /// <summary>
            /// ContentType.Openword
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document(.docx);")]
            Openword,
            /// <summary>
            /// ContentType.Excel
            /// </summary>
            [Description("application/vnd.ms-excel(.xls);")]
            Excel,
            /// <summary>
            /// ContentType.Openexcel
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet(.xlsx);")]
            Openexcel,
            /// <summary>
            /// ContentType.Powerpoint
            /// </summary>
            [Description("application/vnd.ms-powerpoint(.ppt);")]
            Powerpoint,
            /// <summary>
            /// ContentType.Openpowerpoint
            /// </summary>
            [Description("application/vnd.openxmlformats-officedocument.presentationml.presentation(.pptx);")]
            Openpowerpoint,
            /// <summary>
            /// ContentType.Opendocument
            /// </summary>
            [Description("application/vnd.oasis.opendocument.text(.odt);")]
            Opendocument,
            /// <summary>
            /// ContentType.Audiompeg
            /// </summary>
            [Description("audio/mpeg;")]
            Audiompeg,
            /// <summary>
            /// ContentType.Audiovorbis
            /// </summary>
            [Description("audio/vorbis;")]
            Audiovorbis,
            /// <summary>
            /// ContentType.Multiformdata
            /// </summary>
            [Description("multipart/form-data;")]
            Multiformdata,
            /// <summary>
            /// ContentType.Textcss
            /// </summary>
            [Description("text/css;")]
            Textcss,
            /// <summary>
            /// ContentType.Texthtml
            /// </summary>
            [Description("text/html;")]
            Texthtml,
            /// <summary>
            /// ContentType.Textcsv
            /// </summary>
            [Description("text/csv;")]
            Textcsv,
            /// <summary>
            /// ContentType.Textplain
            /// </summary>
            [Description("text/plain;")]
            Textplain,
            /// <summary>
            /// ContentType.Png
            /// </summary>
            [Description("image/png;")]
            Png,
            /// <summary>
            /// ContentType.Jpeg
            /// </summary>
            [Description("image/jpeg;")]
            Jpeg,
            /// <summary>
            /// ContentType.Gif
            /// </summary>
            [Description("image/gif;")]
            Gif
        }

        /// <summary>
        /// Allows for quickling combining Charset with ContentType for RabbitMQ IBasicProperties.
        /// </summary>
        public enum Charset
        {
            /// <summary>
            /// Charset.Utf8
            /// </summary>
            [Description("charset=utf-8")]
            Utf8,
            /// <summary>
            /// Charset.Utf16
            /// </summary>
            [Description("charset=utf-16")]
            Utf16,
            /// <summary>
            /// Charset.Utf32
            /// </summary>
            [Description("charset=utf-32")]
            Utf32,
        }

        /// <summary>
        /// Allow identifying status of the Thread in the ThreadContainer for more complex operations.
        /// </summary>
        public enum ThreadStatus
        {
            /// <summary>
            /// NoThread means the ThreadContainer is empty.
            /// </summary>
            NoThread,
            /// <summary>
            /// Idle means the Thread in the ThreadContainer is doing nothing.
            /// </summary>
            Idle,
            /// <summary>
            /// Processings means the thread in the ThreadContainer is doing work.
            /// </summary>
            Processing
        }

        /// <summary>
        /// PerformanceCounter names for Ado.Net PerformanceCounters
        /// </summary>
        public enum AdoNetPerformanceCounters
        {
            /// <summary>
            /// NumberOfActiveConnectionPools (Index Value = 0)
            /// </summary>
            NumberOfActiveConnectionPools = 0,
            /// <summary>
            /// NumberOfReclaimedConnections (Index Value = 1)
            /// </summary>
            NumberOfReclaimedConnections = 1,
            /// <summary>
            /// HardConnectsPerSecond (Index Value = 2)
            /// </summary>
            HardConnectsPerSecond = 2,
            /// <summary>
            /// HardDisconnectsPerSecond (Index Value = 3)
            /// </summary>
            HardDisconnectsPerSecond = 3,
            /// <summary>
            /// NumberOfActiveConnectionPoolGroups (Index Value = 4)
            /// </summary>
            NumberOfActiveConnectionPoolGroups = 4,
            /// <summary>
            /// NumberOfInactiveConnectionPoolGroups (Index Value = 5)
            /// </summary>
            NumberOfInactiveConnectionPoolGroups = 5,
            /// <summary>
            /// NumberOfInactiveConnectionPools (Index Value = 6)
            /// </summary>
            NumberOfInactiveConnectionPools = 6,
            /// <summary>
            /// NumberOfNonPooledConnections (Index Value = 7)
            /// </summary>
            NumberOfNonPooledConnections = 7,
            /// <summary>
            /// NumberOfPooledConnections (Index Value = 8)
            /// </summary>
            NumberOfPooledConnections = 8,
            /// <summary>
            /// NumberOfStasisConnections (Index Value = 9)
            /// </summary>
            NumberOfStasisConnections = 9,
            /// <summary>
            /// SoftConnectsPerSecond (Index Value = 10)
            /// </summary>
            SoftConnectsPerSecond = 10,
            /// <summary>
            /// SoftDisconnectsPerSecond (Index Value = 11)
            /// </summary>
            SoftDisconnectsPerSecond = 11,
            /// <summary>
            /// NumberOfActiveConnections (Index Value = 12)
            /// </summary>
            NumberOfActiveConnections = 12,
            /// <summary>
            /// NumberOfFreeConnections (Index Value = 13)
            /// </summary>
            NumberOfFreeConnections = 13
        }
    }
}
