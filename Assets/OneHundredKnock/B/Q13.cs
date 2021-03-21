using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// MAX-MINフィルタ
    /// フィルタ内の画素の最大最小の差を出力するフィルタで, エッジ検出のフィルタの一つ
    /// エッジ検出では多くの場合, グレースケール画像に対してフィルタリングを行う
    /// </summary>
    public class Q13 : MonoBehaviour
    {
        [SerializeField] private int _size = 3;

        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            Imgproc.cvtColor(src, dst, Imgproc.COLOR_RGB2GRAY);
            var edgeCols = new byte[dst.rows(), dst.cols()];

            for (var x = 0; x < dst.width(); x++)
            {
                for (var y = 0; y < dst.height(); y++)
                {
                    var max = byte.MinValue;
                    var min = byte.MaxValue;
                    for (var dx = -_size / 2; dx <= _size / 2; dx++)
                    {
                        for (var dy = -_size / 2; dy <= _size / 2; dy++)
                        {
                            if (x + dx >= 0 && x + dx < dst.width() && y + dy >= 0 && y + dy < dst.height())
                            {
                                var col = new byte[4];
                                dst.get(x + dx, y + dy, col);
                                if (col[0] > max) max = col[0];
                                if (col[0] < min) min = col[0];
                            }
                        }
                    }

                    edgeCols[x, y] = (byte) (max - min);
                }
            }

            for (var x = 0; x < dst.width(); x++)
            {
                for (var y = 0; y < dst.height(); y++)
                {
                    dst.put(x, y, new[]
                    {
                        edgeCols[x, y],
                        edgeCols[x, y],
                        edgeCols[x, y],
                        edgeCols[x, y],
                    });
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}