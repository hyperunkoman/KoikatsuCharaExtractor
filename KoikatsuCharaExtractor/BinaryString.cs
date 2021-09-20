using System;
using System.Collections.Generic;
using System.Text;

namespace KoikatsuCharaExtractor
{
    class BinaryString
    {
        string m_string;
        int m_lengthLen;

        public int Length { get; protected set; }

        public int TotalLength { get => Length + m_lengthLen; }

        public BinaryString(byte[] data, int offset = 0)
        {
            m_lengthLen = 1; // TODO: 手抜き
            Length = data[offset]; // TODO: 手抜き
            m_string = Encoding.UTF8.GetString(data, offset + m_lengthLen, Length);
        }

        public static implicit operator string(BinaryString op)
        {
            return op.m_string;
        }
    }
}
