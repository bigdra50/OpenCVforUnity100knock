using System;
using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// HSV変換
    /// 色相Hを反転(180を加算)してRGBになおす
    /// </summary>
    public class Q05 : MonoBehaviour
    {
        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);

            var rgba = new byte[4];
            for (var x = 0; x < src.width(); x++)
            {
                for (var y = 0; y < src.height(); y++)
                {
                    src.get(x, y, rgba);
                    var hsv = Rgb2Hsv(new Rgb(rgba[0], rgba[1], rgba[2]));
                    hsv.h += 180;
                    var rgb = Hsv2Rgb(hsv);
                    rgba[0] = (byte) rgb.r;
                    rgba[1] = (byte) rgb.g;
                    rgba[2] = (byte) rgb.b;
                    dst.put(x, y, rgba);
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }

        private Hsv Rgb2Hsv(Rgb rgb)
        {
            var r = rgb.r / 255f;
            var g = rgb.g / 255f;
            var b = rgb.b / 255f;

            var max = Math.Max(r, Math.Max(g, b));
            var min = Math.Min(r, Math.Min(g, b));

            var hsv = new Hsv();
            hsv.h = (int) (
                Math.Abs(min - max) < float.Epsilon ? 0f :
                Math.Abs(min - b) < float.Epsilon ? 60f * ((g - r) / (max - min)) + 60f :
                Math.Abs(min - r) < float.Epsilon ? 60f * ((b - g) / (max - min)) + 180f :
                60f * ((r - b) / (max - min)) + 300f);
            hsv.h %= 360;
            hsv.v = (int) (max * 100);
            hsv.s = (int) ((max - min) / max * 100);
            return hsv;
        }

        private Rgb Hsv2Rgb(Hsv hsv)
        {
            var v = hsv.v * .01f;
            if (hsv.s == 0) return new Rgb((int) (v * 255));
            hsv.h %= 360;
            var s = hsv.s * .01f;

            var hi = hsv.h / 60;
            var ha = hsv.h / 60f % 1;
            var a = (int) (v * 255f);
            var b = (int) (v * (1f - s) * 255f);
            var c = (int) (v * (1f - s * ha) * 255f);
            var d = (int) (v * (1f - s * (1f - ha)) * 255f);

            Rgb rgb;
            switch (hi)
            {
                case 0:
                    rgb = new Rgb(a, d, b);
                    break;
                case 1:
                    rgb = new Rgb(c, a, b);
                    break;
                case 2:
                    rgb = new Rgb(b, a, d);
                    break;
                case 3:
                    rgb = new Rgb(b, c, a);
                    break;
                case 4:
                    rgb = new Rgb(d, b, a);
                    break;
                case 5:
                    rgb = new Rgb(a, b, c);
                    break;
                default:
                    rgb = new Rgb(a);
                    break;
            }

            return rgb;
        }

        struct Rgb
        {
            public Rgb(byte col)
            {
                r = col;
                g = col;
                b = col;
            }

            public Rgb(int col)
            {
                r = col;
                g = col;
                b = col;
            }

            public Rgb(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public Rgb(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public int r;
            public int g;
            public int b;
        }

        struct Hsv
        {
            public int h;
            public int s;
            public int v;
        }
    }
}