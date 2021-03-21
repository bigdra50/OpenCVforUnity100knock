using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.A
{
    /// <summary>
    /// チャネル入れ替え
    /// 画像を読み込み, RGBをBGRの順に入れ替える
    /// </summary>
    public class Q01 : MonoBehaviour
    {
        void Start()
        {
            var src= Util.LoadTexture("imori_256x256");
            var dst = new Mat();
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGBA2BGRA);
            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}