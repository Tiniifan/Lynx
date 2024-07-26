﻿namespace Lynx.Level5.Compression
{
    public interface ICompression
    {
        byte[] Compress(byte[] data);

        byte[] Decompress(byte[] data);
    }
}