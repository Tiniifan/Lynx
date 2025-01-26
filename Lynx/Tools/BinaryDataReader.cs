using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Lynx.Tools
{
    /// <summary>
    /// Represents a binary data reader with support for big-endian and little-endian formats.
    /// </summary>
    public class BinaryDataReader : IDisposable
    {
        /// <summary>
        /// The underlying stream of the binary data reader.
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
        /// Gets the base stream of the binary data reader.
        /// </summary>
        public Stream BaseStream => _stream;

        /// <summary>
        /// Gets the current position within the underlying stream.
        /// </summary>
        public long Position => _stream.Position;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataReader"/> class with byte data.
        /// </summary>
        /// <param name="data">The byte data to initialize the binary data reader.</param>
        public BinaryDataReader(byte[] data)
        {
            _stream = new MemoryStream(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataReader"/> class with a stream.
        /// </summary>
        /// <param name="stream">The stream to initialize the binary data reader.</param>
        public BinaryDataReader(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Disposes the binary data reader and releases any resources associated with it.
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
        }

        /// <summary>
        /// Reads a value of the specified type from the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of value to read.</typeparam>
        /// <returns>The value read from the stream.</returns>
        public T ReadValue<T>()
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] bytes = new byte[size];
            _stream.Read(bytes, 0, size);

            if (typeof(T) == typeof(byte))
            {
                return (T)(object)bytes[0];
            }

            if (BigEndian)
            {
                Array.Reverse(bytes);
            }

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T value = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return value;
        }

        /// <summary>
        /// Reads a 24-bit integer from the underlying stream.
        /// </summary>
        /// <returns>The 24-bit integer read from the stream.</returns>
        public int ReadInt24()
        {
            byte[] bytes = new byte[4];

            // Read the 3 bytes and store them in the last 3 bytes of the 4-byte array
            _stream.Read(bytes, 0, 3);

            if (BigEndian)
            {
                Array.Reverse(bytes, 0, 3);
            }

            // Convert the 4-byte array to an integer
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads multiple values of the specified type from the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of values to read.</typeparam>
        /// <param name="count">The number of values to read.</param>
        /// <returns>An array of values read from the stream.</returns>
        public T[] ReadMultipleValue<T>(int count)
        {
            return Enumerable.Range(0, count).Select(x => ReadValue<T>()).ToArray();
        }

        /// <summary>
        /// Reads a string from the underlying stream using the specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use for reading the string.</param>
        /// <returns>The string read from the stream.</returns>
        public string ReadString(Encoding encoding)
        {
            List<byte> bytes = new List<byte>();
            int b;

            while ((b = _stream.ReadByte()) != 0x0 && _stream.Position < _stream.Length)
            {
                bytes.Add((byte)b);
            }

            return encoding.GetString(bytes.ToArray());
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
        /// Gets a section of the specified size from the underlying stream.
        /// </summary>
        /// <param name="size">The size of the section to get.</param>
        /// <returns>A byte array containing the section data.</returns>
        public byte[] GetSection(int size)
        {
            long temp = _stream.Position;
            byte[] data = new byte[size];
            _stream.Read(data, 0, data.Length);
            return data;
        }

        /// <summary>
        /// Gets a section of the specified size from the specified offset in the underlying stream.
        /// </summary>
        /// <param name="offset">The offset to start reading from.</param>
        /// <param name="size">The size of the section to get.</param>
        /// <returns>A byte array containing the section data.</returns>
        public byte[] GetSection(uint offset, int size)
        {
            long temp = _stream.Position;
            Seek(offset);
            byte[] data = new byte[size];
            _stream.Read(data, 0, data.Length);
            Seek((uint)temp);
            return data;
        }

        /// <summary>
        /// Finds the specified value in the underlying stream starting from the specified position.
        /// </summary>
        /// <typeparam name="T">The type of value to find.</typeparam>
        /// <param name="search">The value to find.</param>
        /// <param name="start">The position to start searching from.</param>
        /// <returns>The position of the found value, or -1 if not found.</returns>
        public long Find<T>(T search, uint start) where T : struct, IEquatable<T>
        {
            int count = (int)(_stream.Length - start) / Marshal.SizeOf(typeof(T));

            long temp = _stream.Position;
            Seek(start);

            T[] tableSearch = ReadMultipleStruct<T>(count);
            int foundIndex = Array.IndexOf(tableSearch, search);

            Seek((uint)temp);

            if (foundIndex != -1)
            {
                return start + foundIndex * Marshal.SizeOf(typeof(T));
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Seeks to the position of the specified value in the underlying stream starting from the specified position.
        /// </summary>
        /// <typeparam name="T">The type of value to find.</typeparam>
        /// <param name="search">The value to find.</param>
        /// <param name="start">The position to start searching from.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the value is not found.</exception>
        public void SeekOf<T>(T search, uint start) where T : struct, IEquatable<T>
        {
            long pos = Find(search, start);

            if (pos != -1)
            {
                Seek((uint)pos);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Prints the current position in the underlying stream.
        /// </summary>
        public void PrintPosition()
        {
            Console.WriteLine(_stream.Position.ToString("X"));
        }

        /// <summary>
        /// Reads a structure of the specified type from the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of structure to read.</typeparam>
        /// <returns>The structure read from the stream.</returns>
        public T ReadStruct<T>()
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] bytes = new byte[size];
            _stream.Read(bytes, 0, size);

            if (BigEndian)
                Array.Reverse(bytes);

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }

        /// <summary>
        /// Reads multiple structures of the specified type from the underlying stream.
        /// </summary>
        /// <typeparam name="T">The type of structures to read.</typeparam>
        /// <param name="count">The number of structures to read.</param>
        /// <returns>An array of structures read from the stream.</returns>
        public T[] ReadMultipleStruct<T>(int count)
        {
            return Enumerable.Range(0, count).Select(x => ReadStruct<T>()).ToArray();
        }
    }
}
