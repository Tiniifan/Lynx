using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lynx.Tools
{
    // Copyright (c) Damien Guard.  All rights reserved.
    // Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
    // You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0

    /// <summary>
    /// Represents a CRC32 hash algorithm.
    /// </summary>
    public sealed class Crc32 : HashAlgorithm
    {
        /// <summary>
        /// The default polynomial used for CRC32 calculation.
        /// </summary>
        public const UInt32 DefaultPolynomial = 0xedb88320u;

        /// <summary>
        /// The default seed value used for CRC32 calculation.
        /// </summary>      
        public const UInt32 DefaultSeed = 0xffffffffu;

        static UInt32[] defaultTable;

        readonly UInt32 seed;
        readonly UInt32[] table;
        UInt32 hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class with the default polynomial and seed.
        /// </summary>
        public Crc32()
            : this(DefaultPolynomial, DefaultSeed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc32"/> class with a specified polynomial and seed.
        /// </summary>
        /// <param name="polynomial">The polynomial to use for CRC32 calculation.</param>
        /// <param name="seed">The seed value to use for CRC32 calculation.</param>
        public Crc32(UInt32 polynomial, UInt32 seed)
        {
            if (!System.BitConverter.IsLittleEndian)
                throw new PlatformNotSupportedException("Not supported on Big Endian processors");

            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        /// <summary>
        /// Initializes the CRC32 hash algorithm.
        /// </summary>
        public override void Initialize()
        {
            hash = seed;
        }

        /// <summary>
        /// Computes the CRC32 hash value for the specified byte array.
        /// </summary>
        /// <param name="array">The input byte array.</param>
        /// <param name="ibStart">The starting index in the array.</param>
        /// <param name="cbSize">The number of bytes to use from the array.</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hash = CalculateHash(table, hash, array, ibStart, cbSize);
        }

        /// <summary>
        /// Finalizes the CRC32 hash computation.
        /// </summary>
        /// <returns>The computed hash value.</returns>
        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt32ToBigEndianBytes(~hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        /// <summary>
        /// Gets the size of the computed hash code in bits.
        /// </summary>
        public override int HashSize { get { return 32; } }

        /// <summary>
        /// Computes the CRC32 hash value for the specified byte array using the default seed.
        /// </summary>
        /// <param name="buffer">The input byte array.</param>
        /// <returns>The computed hash value.</returns>
        public static UInt32 Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        /// <summary>
        /// Computes the CRC32 hash value for the specified byte array using a specified seed.
        /// </summary>
        /// <param name="seed">The seed value to use for CRC32 calculation.</param>
        /// <param name="buffer">The input byte array.</param>
        /// <returns>The computed hash value.</returns>
        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return Compute(DefaultPolynomial, seed, buffer);
        }

        /// <summary>
        /// Computes the CRC32 hash value for the specified byte array using a specified polynomial and seed.
        /// </summary>
        /// <param name="polynomial">The polynomial to use for CRC32 calculation.</param>
        /// <param name="seed">The seed value to use for CRC32 calculation.</param>
        /// <param name="buffer">The input byte array.</param>
        /// <returns>The computed hash value.</returns>
        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Initializes the CRC32 table for the specified polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial to use for CRC32 calculation.</param>
        /// <returns>The initialized CRC32 table.</returns>
        static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            var createTable = new UInt32[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (UInt32)i;
                for (var j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        /// <summary>
        /// Calculates the CRC32 hash value for the specified byte array.
        /// </summary>
        /// <param name="table">The CRC32 table to use for calculation.</param>
        /// <param name="seed">The seed value to use for CRC32 calculation.</param>
        /// <param name="buffer">The input byte array.</param>
        /// <param name="start">The starting index in the array.</param>
        /// <param name="size">The number of bytes to use from the array.</param>
        /// <returns>The computed hash value.</returns>
        static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
        {
            var hash = seed;
            for (var i = start; i < start + size; i++)
                hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
            return hash;
        }

        /// <summary>
        /// Converts a 32-bit unsigned integer to a big-endian byte array.
        /// </summary>
        /// <param name="uint32">The 32-bit unsigned integer to convert.</param>
        /// <returns>The big-endian byte array.</returns>
        static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
        {
            var result = System.BitConverter.GetBytes(uint32);

            if (System.BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }
    }
}
