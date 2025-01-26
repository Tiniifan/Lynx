using System;
using System.IO;
using System.Drawing;

namespace Lynx.Tools
{
    /// <summary>
    /// Represents a sub-memory stream with an offset and size.
    /// </summary>
    public class SubMemoryStream
    {
        /// <summary>
        /// The offset of the sub-memory stream.
        /// </summary>
        public long Offset;

        /// <summary>
        /// The size of the sub-memory stream.
        /// </summary>
        public long Size;

        /// <summary>
        /// The byte content of the sub-memory stream.
        /// </summary>
        public byte[] ByteContent;

        /// <summary>
        /// The base stream of the sub-memory stream.
        /// </summary>
        public Stream BaseStream;

        /// <summary>
        /// The color associated with the sub-memory stream.
        /// </summary>
        public Color Color = Color.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubMemoryStream"/> class with byte data.
        /// </summary>
        /// <param name="data">The byte data to initialize the sub-memory stream.</param>
        public SubMemoryStream(byte[] data)
        {
            Offset = 0;
            Size = data.Length;
            ByteContent = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubMemoryStream"/> class with a base stream, offset, and size.
        /// </summary>
        /// <param name="baseStream">The base stream to initialize the sub-memory stream.</param>
        /// <param name="offset">The offset of the sub-memory stream.</param>
        /// <param name="size">The size of the sub-memory stream.</param>
        public SubMemoryStream(Stream baseStream, long offset, long size)
        {
            Offset = offset;
            Size = size;
            BaseStream = baseStream;
        }

        /// <summary>
        /// Reads the byte content from the base stream.
        /// </summary>
        public void Read()
        {
            ByteContent = new byte[Size];
            BaseStream.Seek(Offset, SeekOrigin.Begin);
            BaseStream.Read(ByteContent, 0, ByteContent.Length);
        }

        /// <summary>
        /// Seeks to the offset in the base stream.
        /// </summary>
        public void Seek()
        {
            BaseStream.Seek(Offset, SeekOrigin.Begin);
        }

        /// <summary>
        /// Reads bytes from the base stream into a buffer.
        /// </summary>
        /// <param name="buffer">The buffer to read bytes into.</param>
        /// <param name="offset">The offset in the buffer to start writing bytes.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            // Adjust count to read within the available range
            long remainingBytes = (Offset + Size) - BaseStream.Position;
            if (remainingBytes <= 0)
                return 0;

            int bytesToRead = (int)Math.Min(count, remainingBytes);

            // Read bytes from the base stream
            int bytesRead = BaseStream.Read(buffer, offset, bytesToRead);

            return bytesRead;
        }

        /// <summary>
        /// Copies the content of the sub-memory stream to another stream.
        /// </summary>
        /// <param name="destination">The destination stream to copy the content to.</param>
        public void CopyTo(Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (BaseStream == null && ByteContent != null)
            {
                destination.Write(ByteContent, 0, ByteContent.Length);
                return;
            }

            if (!BaseStream.CanRead)
            {
                throw new InvalidOperationException("SubMemoryStream n'est pas lisible.");
            }

            if (ByteContent == null || ByteContent.Length == 0)
            {
                long offset = Offset;
                long length = Size;

                byte[] buffer = new byte[4096];

                BaseStream.Seek(offset, SeekOrigin.Begin);

                int bytesRead;
                while (length > 0 && (bytesRead = BaseStream.Read(buffer, 0, (int)Math.Min(length, buffer.Length))) > 0)
                {
                    destination.Write(buffer, 0, bytesRead);
                    length -= bytesRead;
                }
            }
            else
            {
                destination.Write(ByteContent, 0, ByteContent.Length);
            }
        }
    }
}

