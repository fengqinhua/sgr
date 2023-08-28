/**************************************************************
 * 
 * 唯一标识：4527e7fd-7be6-4db1-be23-48709c098faa
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/8/28 18:21:28
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Utilities;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Indentity.Utilities
{
    public static class CaptchaHelper
    {
        /// <summary>
        /// 生成验证码（可能包含数字和字母）
        /// </summary>
        /// <param name="length"></param>
        /// <param name="useNum"></param>
        /// <param name="useLetter"></param>
        /// <returns></returns>
        public static Tuple<string, byte[]> CreateCaptcha(int length = 4, bool useNum = true, bool useLetter = true)
        {
            return CreateCaptchaCode(RandomHelper.GetRandomString(length, useNum, false, useLetter, false));
        }

        /// <summary>
        /// 生成验证码(算术计算)
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, byte[]> CreateArithmeticCaptcha()
        {
            char[] operators = { '+', '-', '*' };

            int first = RandomHelper.GetRandom(1,9);
            int second = RandomHelper.GetRandom(1, 9);
            char operatorChar = operators[RandomHelper.GetRandom(0, operators.Length)];
            int value;
            switch (operatorChar)
            {
                case '+':
                    value = first + second; break;
                case '*':
                    value = first * second; break;
                case '-':
                    if (first < second)
                    {
                        (second, first) = (first, second);
                    }
                    value = first - second;
                    break;
                default:
                    throw new ArgumentException();
            }

            string code = $"{first}{operatorChar}{second}=?";
            var result = CreateCaptchaCode(code);
            return new Tuple<string, byte[]>(value.ToString(), result.Item2);
        }

        private static Tuple<string, byte[]> CreateCaptchaCode(string code)
        {
            int width = 150;
            int height = 50;

            //创建bitmap位图
            using SKBitmap image = new(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            //创建画笔
            using SKCanvas canvas = new(image);
            //填充背景颜色为白色
            canvas.DrawColor(SKColors.White);

            //画图片的背景噪音线
            for (int i = 0; i < (width * height * 0.015); i++)
            {
                using SKPaint drawStyle = new();
                drawStyle.Color = new(Convert.ToUInt32(RandomHelper.GetRandom(int.MaxValue)));

                canvas.DrawLine(RandomHelper.GetRandom(0, width), RandomHelper.GetRandom(0, height), RandomHelper.GetRandom(0, width), RandomHelper.GetRandom(0, height), drawStyle);
            }

            //将文字写到画布上
            using (SKPaint drawStyle = new())
            {
                drawStyle.Color = SKColors.Red;
                drawStyle.IsAntialias = true;
                drawStyle.TextSize = height;
                drawStyle.StrokeWidth = 1;

                float emHeight = height - (float)height * (float)0.14;
                float emWidth = ((float)width / code.Length) - ((float)width * (float)0.15);

                canvas.DrawText(code, emWidth, emHeight, drawStyle);
            }

            //画图片的前景噪音点
            for (int i = 0; i < (width * height * 0.6); i++)
            {
                image.SetPixel(RandomHelper.GetRandom(0, width), RandomHelper.GetRandom(0, height), new SKColor(Convert.ToUInt32(RandomHelper.GetRandom(int.MaxValue))));
            }

            using var img = SKImage.FromBitmap(image);
            using SKData p = img.Encode(SKEncodedImageFormat.Png, 100);
            return new Tuple<string, byte[]>(code, p.ToArray());

        }

    }
}
