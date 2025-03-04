using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Lynx.Tools
{
    /// <summary>
    /// Represents a binary data writer with support for big-endian and little-endian formats.
    /// </summary>
    public class BinaryDataWriter : IDisposable
    {
        /// <summary>
        /// The underlying stream of the binary data writer.
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// Gets or sets a value indicating whether the data is in big-endian format.
        /// </summary>
        public bool BigEndian { get; set; } = false;

        /// <summary>
        /// Gets the length of the underlying stream.
        /// </summary>
        public long Length => _stream.Length;

        /// <summary>
        /// Gets the base stream of the binary data writer.
        /// </summary>
        public Stream BaseStream => _stream;

        /// <summary>
        /// Gets the current position within the underlying stream.
        /// </summary>
        public long Position => _stream.Position;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class with byte data.
        /// </summary>
        /// <param name="data">The byte data to initialize the binary data writer.</param>
        public BinaryDataWriter(byte[] data)
        {
            _stream = new MemoryStream(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class with a stream.
        /// </summary>
        /// <param name="stream">The stream to initialize the binary data writer.</param>
        public BinaryDataWriter(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Disposes the binary data writer and releases any resources associated with it.
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
        }

        /// <summary>
        /// Skips the specified number of bytes in the underlying stream.
        /// </summary>
        /// <param name="size">The number of bytes to skip.</param>
        public void Skip(uint size)
        {
            _stream.Seek(size, SeekOrigin.Current);
        }

        /// <summary>
        /// Seeks to the specified position in the underlying stream.
        /// </summary>
        /// <param name="position">The position to seek to.</param>
        public void Seek(uint position)
        {
            _stream.Seek(position, SeekOrigin.Begin);
        }

        /// <summary>
        /// Prints the current position in the underlying stream.
        /// </summary>
        public void PrintPosition()
        {
            Console.WriteLine(_stream.Position.ToString("X"));
        }

        /// <summary>
        /// Writes a byte array to the underlying stream.
        /// </summary>
        /// <param name="data">The byte array to write.</param>
        public void Write(byte[] data)
        {
            if (BigEndian && data.Length > 1)
            {
                Array.Reverse(data);
            }

            _stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Writes a byte value to the underlying stream.
        /// </summary>
        /// <param name="value">The byte value to write.</param>
        public void Write(byte value)
        {
            _stream.WriteByte(value);
        }

        /// <summary>
        /// Writes a short value to the underlying stream.
        /// </summary>
        /// <param name="value">The short value to write.</param>
        public void Write(short value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an integer value to the underlying stream.
        /// </summary>
        /// <param name="value">The integer value to write.</param>
        public void Write(int value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a long value to the underlying stream.
        /// </summary>
        /// <param name="value">The long value to write.</param>
        public void Write(long value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned short value to the underlying stream.
        /// </summary>
        /// <param name="value">The unsigned short value to write.</param>
        public void Write(ushort value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned integer value to the underlying stream.
        /// </summary>
        /// <param name="value">The unsigned integer value to write.</param>
        public void Write(uint value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned long value to the underlying stream.
        /// </summary>
        /// <param name="value">The unsigned long value to write.</param>
        public void Write(ulong value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a float value to the underlying stream.
        /// </summary>
        /// <param name="value">The float value to write.</param>
        public void Write(float value)
        {
            Write(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a 24-bit integer value to the underlying stream.
        /// </summary>
        /// <param name="value">The 24-bit integer value to write.</param>
        public void WriteInt24(int value)
        {
            byte[] bytes = new byte[4];

            // Convert the integer to a 4-byte array
            BitConverter.GetBytes(value).CopyTo(bytes, 0);

            if (BigEndian)
            {
                Array.Reverse(bytes, 0, 3);
            }

            // Write the last 3 bytes of the array to the stream
            _stream.Write(bytes, 0, 3);
        }

        /// <summary>
        /// Writes alignment bytes to the underlying stream.
        /// </summary>
        /// <param name="alignment">The alignment size.</param>
        /// <param name="alignmentByte">The byte value to use for alignment.</param>
        public void WriteAlignment(int alignment = 16, byte alignmentByte = 0x0)
        {
            var remainder = BaseStream.Position % alignment;
            if (remainder <= 0) return;
            for (var i = 0; i < alignment - remainder; i++)
                Write(alignmentByte);
        }

        /// <summary>
        /// Writes default alignment bytes (alignment on 16) to the underlying stream.
        /// </summary>
        public void WriteAlignment()
        {
            Write((byte)0x00);
            WriteAlignment(16, 0xFF);
        }

        /// <summary>
        /// Writes a structure to the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of structure to write.</typeparam>
        /// <param name="structure">The structure to write.</param>
        public void WriteStruct<T>(T structure)
        {
            byte[] bytes = new byte[Marshal.SizeOf(typeof(T))];

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(structure, handle.AddrOfPinnedObject(), false);
            handle.Free();

            Write(bytes);
        }

        /// <summary>
        /// Writes multiple structures to the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of structures to write.</typeparam>
        /// <param name="structures">The structures to write.</param>
        public void WriteMultipleStruct<T>(IEnumerable<T> structures)
        {
            foreach (T structure in structures)
            {
                WriteStruct(structure);
            }
        }
    }
}