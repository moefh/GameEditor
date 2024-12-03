using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.Misc
{
    public enum ByteOrder {
        LittleEndian,
        BigEndian,
    }

    public class MemoryStreamIO
    {
        private readonly byte[] data;
        private readonly ByteOrder byteOrder;
        private int pos;

        public int Position { get { return pos; } }
        
        public int Length { get { return data.Length; } }

        public MemoryStreamIO(byte[] data, ByteOrder byteOrder) {
            this.data = data;
            this.byteOrder = byteOrder;
            pos = 0;
        }

        public void Seek(int pos) {
            this.pos = pos;
        }

        private void EnsureLength(int len) {
            if (pos + len > data.Length) {
                throw new Exception("access outside buffer bounds");
            }
        }

        public void WriteTag(string tag) {
            if (tag.Length != 4) throw new Exception("invalid tag length");
            EnsureLength(4);
            for (int i = 0; i < 4; i++) {
                data[pos+i] = (byte) tag[i];
            }
            pos += 4;
        }

        public void WriteString(string s, int len) {
            for (int i = 0; i < len; i++) {
                if (i >= s.Length) {
                    WriteU8(0x20);
                } else {
                    byte b = (byte) s[i];
                    WriteU8(byte.Clamp(b, 0x20, 0x7e));
                }
            }
        }

        public void WriteU32(uint u32) {
            EnsureLength(4);
            switch (byteOrder) {
            case ByteOrder.LittleEndian:
                data[pos++] = (byte) ((u32 >>  0) & 0xff);
                data[pos++] = (byte) ((u32 >>  8) & 0xff);
                data[pos++] = (byte) ((u32 >> 16) & 0xff);
                data[pos++] = (byte) ((u32 >> 24) & 0xff);
                break;
            case ByteOrder.BigEndian:
                data[pos++] = (byte) ((u32 >> 24) & 0xff);
                data[pos++] = (byte) ((u32 >> 16) & 0xff);
                data[pos++] = (byte) ((u32 >>  8) & 0xff);
                data[pos++] = (byte) ((u32 >>  0) & 0xff);
                break;
            }
        }

        public void WriteU16(ushort u16) {
            EnsureLength(2);
            switch (byteOrder) {
            case ByteOrder.LittleEndian:
                data[pos++] = (byte) ((u16 >>  0) & 0xff);
                data[pos++] = (byte) ((u16 >>  8) & 0xff);
                break;
            case ByteOrder.BigEndian:
                data[pos++] = (byte) ((u16 >>  8) & 0xff);
                data[pos++] = (byte) ((u16 >>  0) & 0xff);
                break;
            }
        }

        public void WriteU8(byte u8) {
            EnsureLength(1);
            data[pos++] = u8;
        }

        public void WriteBytes(byte[] data, int offset, int count) {
            EnsureLength(count);
            for (int i = 0; i < count; i++) {
                this.data[pos++] = data[offset+i];
            }
        }

        public string ReadString(int numBytes) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numBytes; i++) {
                byte b = ReadU8();
                if (b < 32 || b > 126) b = 32;
                sb.Append((char) b);
            }
            return sb.ToString();
        }

        public string ReadTag() {
            EnsureLength(4);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++) {
                byte b = data[pos++];
                if (b < 32 || b > 126) b = 32;
                sb.Append((char) b);
            }
            return sb.ToString();
        }

        public void ReadBytes(byte[] data, int offset, int count) {
            EnsureLength(count);
            Array.Copy(this.data, pos, data, offset, count);
            pos += count;
        }

        public void ReadSBytes(sbyte[] data, int offset, int count) {
            EnsureLength(count);
            for (int i = 0; i < count; i++) {
                data[i+offset] = (sbyte) this.data[pos+i];
            }
            pos += count;
        }

        public uint ReadU32() {
            EnsureLength(4);
            uint val = byteOrder switch {
                ByteOrder.LittleEndian => (((uint)data[pos + 0]) <<  0) |
                                          (((uint)data[pos + 1]) <<  8) |
                                          (((uint)data[pos + 2]) << 16) |
                                          (((uint)data[pos + 3]) << 24),
                ByteOrder.BigEndian =>    (((uint)data[pos + 3]) <<  0) |
                                          (((uint)data[pos + 2]) <<  8) |
                                          (((uint)data[pos + 1]) << 16) |
                                          (((uint)data[pos + 0]) << 24),
                _ => 0,
            };
            pos += 4;
            return val;
        }

        public ushort ReadU16() {
            EnsureLength(2);
            ushort val = byteOrder switch {
                ByteOrder.LittleEndian => (ushort)((data[pos+0] << 0) | (data[pos+1] << 8)),
                ByteOrder.BigEndian =>    (ushort)((data[pos+1] << 0) | (data[pos+0] << 8)),
                _ => 0,
            };
            pos += 2;
            return val;
        }

        public byte ReadU8() {
            EnsureLength(1);
            return data[pos++];
        }

    }
}
