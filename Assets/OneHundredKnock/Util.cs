using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using UnityEngine;

namespace OneHundredKnock
{
    public class Util
    {
        public static Mat LoadTexture(string path)
        {
            var srcTex = Resources.Load(path) as Texture2D;
            var srcMat = new Mat(srcTex.height, srcTex.width, CvType.CV_8UC4);
            Utils.texture2DToMat(srcTex, srcMat);
            return srcMat;
        }

        public static Mat LoadTexture(string path, int cvType)
        {
            var srcTex = Resources.Load(path) as Texture2D;
            var srcMat = new Mat(srcTex.height, srcTex.width, cvType);
            Utils.texture2DToMat(srcTex, srcMat);
            return srcMat;
        }
        public static Texture2D MatToTexture2D(Mat imgMat)
        {
            var texture = new Texture2D(imgMat.cols(), imgMat.rows(), TextureFormat.RGBA32, false);
            Utils.matToTexture2D(imgMat, texture);
            return texture;
        }
    }
}