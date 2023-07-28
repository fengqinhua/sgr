/**************************************************************
 * 
 * 唯一标识：c705173d-16a0-4494-83b3-86c97e93b5a3
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:36:08
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Generator
{
    /// <summary>
    /// 缺省的字符串型唯一标识生成器
    /// </summary>
    public class DefaultStringIdGenerator : IStringIdGenerator
    {
        // Some confusing chars are ignored: http://www.crockford.com/wrmg/base32.html
        private static readonly string _encode32Chars = "0123456789abcdefghjkmnpqrstvwxyz";

        /// <summary>
        /// 生成不重复的字符串型唯一标识
        /// </summary>
        /// <returns></returns>
        public string GenerateUniqueId()
        {
            // Generate a base32 Guid value
            var guid = Guid.NewGuid().ToByteArray();

            var hs = BitConverter.ToInt64(guid, 0);
            var ls = BitConverter.ToInt64(guid, 8);

            return ToBase32(hs, ls);
        }

        private string ToBase32(long hs, long ls)
        {
            var charBuffer = new char[26];

            charBuffer[0] = _encode32Chars[(int)(hs >> 60) & 31];
            charBuffer[1] = _encode32Chars[(int)(hs >> 55) & 31];
            charBuffer[2] = _encode32Chars[(int)(hs >> 50) & 31];
            charBuffer[3] = _encode32Chars[(int)(hs >> 45) & 31];
            charBuffer[4] = _encode32Chars[(int)(hs >> 40) & 31];
            charBuffer[5] = _encode32Chars[(int)(hs >> 35) & 31];
            charBuffer[6] = _encode32Chars[(int)(hs >> 30) & 31];
            charBuffer[7] = _encode32Chars[(int)(hs >> 25) & 31];
            charBuffer[8] = _encode32Chars[(int)(hs >> 20) & 31];
            charBuffer[9] = _encode32Chars[(int)(hs >> 15) & 31];
            charBuffer[10] = _encode32Chars[(int)(hs >> 10) & 31];
            charBuffer[11] = _encode32Chars[(int)(hs >> 5) & 31];
            charBuffer[12] = _encode32Chars[(int)hs & 31];

            charBuffer[13] = _encode32Chars[(int)(ls >> 60) & 31];
            charBuffer[14] = _encode32Chars[(int)(ls >> 55) & 31];
            charBuffer[15] = _encode32Chars[(int)(ls >> 50) & 31];
            charBuffer[16] = _encode32Chars[(int)(ls >> 45) & 31];
            charBuffer[17] = _encode32Chars[(int)(ls >> 40) & 31];
            charBuffer[18] = _encode32Chars[(int)(ls >> 35) & 31];
            charBuffer[19] = _encode32Chars[(int)(ls >> 30) & 31];
            charBuffer[20] = _encode32Chars[(int)(ls >> 25) & 31];
            charBuffer[21] = _encode32Chars[(int)(ls >> 20) & 31];
            charBuffer[22] = _encode32Chars[(int)(ls >> 15) & 31];
            charBuffer[23] = _encode32Chars[(int)(ls >> 10) & 31];
            charBuffer[24] = _encode32Chars[(int)(ls >> 5) & 31];
            charBuffer[25] = _encode32Chars[(int)ls & 31];

            return new string(charBuffer);
        }
    }

}
