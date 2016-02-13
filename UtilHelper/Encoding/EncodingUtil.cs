using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilHelper.EncodingUtil
{
    public sealed class EncodingUtil
    {
        public Encoding ISO
        {
            get {return Encoding.GetEncoding(28591); }
        }
        public Encoding UTF8
        {
            get { return Encoding.UTF8; }
        }
        public byte[] IsoBytes(string s)
        {
            return ISO.GetBytes(s);
        }
        public string IsoString(byte[] bz)
        {
            return ISO.GetString(bz);
        }
        public byte[] Utf8Bytes(string s)
        {
            return UTF8.GetBytes(s);
        }
        public string Utf8String(byte[] bz)
        {
            return UTF8.GetString(bz);
        }
    }
}
