using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace KoikatsuCharaExtractor
{
    class KoikatsuSaveExtractor
    {
        const string signatureString = "【KoiKatuChara】";
        byte[] signature = Encoding.UTF8.GetBytes(signatureString);
        byte[] m_saveData;

        public KoikatsuSaveExtractor(string saveFilePath)
        {
            m_saveData = File.ReadAllBytes(saveFilePath);
        }

        private bool Compare(byte[] op1, int offset, int length, byte[] op2)
        {
            for(int i=0; i < length; i++)
            {
                if (op1[offset + i] != op2[i]) return false;
            }
            return true;
        }

        public void SearchAndSave(string outDirPath)
        {
            int count = 1;

            for (int pos = 0; pos < m_saveData.Length; pos++)
            {
                if (Compare(m_saveData, pos, signature.Length, signature))
                {
                    Debug.WriteLine("file{0:D06}: start={1:D}", count, pos);
                    int length = SaveData(m_saveData, pos - 1 - 4, Path.Combine(outDirPath, string.Format("{0:D06}.png", count)));
                    pos += length;
                    Debug.WriteLine("file{0:D06}: end={1:D}", count, pos);
                    count++;
                }
            }
        }

        private int SaveData(byte[] data, int offset, string filename)
        {
            int length = 4;

            var sig = new BinaryString(data, offset + length);
            length += sig.TotalLength;
            var version = new BinaryString(data, offset + length);
            length += version.TotalLength;

            int pngLength = BitConverter.ToInt32(data, offset + length);
            int pngOffset = offset + length + 4;
            length += 4 + pngLength; // size(=4) + body

            int packedHeaderLen = BitConverter.ToInt32(data, offset + length);
            length += 4 + packedHeaderLen;
            int packedDataLen = BitConverter.ToInt32(data, offset + length);
            length += 8 + packedDataLen;

            using (var ost = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                ost.Write(data, pngOffset, pngLength);
                ost.Write(data, offset, length);
            }
            return length;
        }
    }
}
