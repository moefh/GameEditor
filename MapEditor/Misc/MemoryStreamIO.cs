using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public class MemoryStreamIO : MemoryStream
    {
        private readonly byte[] buf = new byte[4];

        public MemoryStreamIO(byte[] buffer) : base(buffer) {}

        public void WriteTag(string tag) {
            if (tag.Length != 4) throw new Exception("invalid tag length");
            for (int i = 0; i < 4; i++) {
                buf[i] = (byte) tag[i];
            }
            Write(buf, 0, 4);
        }

        public void WriteU32(uint u32) {
            buf[0] = (byte) ((u32 >>  0) & 0xff);
            buf[1] = (byte) ((u32 >>  8) & 0xff);
            buf[2] = (byte) ((u32 >> 16) & 0xff);
            buf[3] = (byte) ((u32 >> 24) & 0xff);
            Write(buf, 0, 4);
        }

        public void WriteU16(ushort u16) {
            buf[0] = (byte) ((u16 >>  0) & 0xff);
            buf[1] = (byte) ((u16 >>  8) & 0xff);
            Write(buf, 0, 2);
        }

        public void WriteU8(byte u8) {
            buf[0] = u8;
            Write(buf, 0, 1);
        }

        public uint ReadU32() {
            int n = Read(buf, 0, 4);
            if (n < 4) throw new Exception("trying to read past end of memory buffer");
            return ((((uint) buf[0]) <<  0) |
                    (((uint) buf[1]) <<  8) |
                    (((uint) buf[2]) << 16) |
                    (((uint) buf[3]) << 24));
        }

        public ushort ReadU16() {
            int n = Read(buf, 0, 2);
            if (n < 2) throw new Exception("trying to read past end of memory buffer");
            return (ushort) ((((uint) buf[0]) <<  0) |
                             (((uint) buf[1]) <<  8));
        }

        public byte ReadU8() {
            int n = Read(buf, 0, 1);
            if (n < 1) throw new Exception("trying to read past end of memory buffer");
            return buf[0];
        }

    }
}
