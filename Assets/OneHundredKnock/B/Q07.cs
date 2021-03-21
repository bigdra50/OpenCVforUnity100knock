using OpenCVForUnity.CoreModule;
using UnityEngine;

namespace OneHundredKnock.B
{
    /// <summary>
    /// 平均プーリング
    /// 画像をグリッド分割し. 各領域内の平均値でその領域内の値を埋める.
    /// </summary>
    public class Q07 : MonoBehaviour
    {
        [SerializeField] private int _grid = 16;

        private void Start()
        {
            var src = Util.LoadTexture("imori_256x256");
            var dst = new Mat(src.rows(), src.cols(), CvType.CV_8UC4);
            var color = new byte[4];
            for (var x = 0; x < src.width(); x += _grid)
            {
                for (var y = 0; y < src.height(); y += _grid)
                {
                    // グリッド内の領域の色の平均を求める
                    var additive = new int[4];
                    for (var dx = 0; dx < _grid; dx++)
                    {
                        for (var dy = 0; dy < _grid; dy++)
                        {
                            src.get(x + dx, y + dy, color);
                            for (var i = 0; i < 3; i++)
                            {
                                additive[i] += color[i];
                            }
                        }
                    }
                    for (var i = 0; i < 3; i++)
                    {
                        color[i] = (byte) (additive[i] / (_grid * _grid));
                    }

                    for (var dx = 0; dx < _grid; dx++)
                    {
                        for (var dy = 0; dy < _grid; dy++)
                        {
                            dst.put(x + dx, y + dy, color);
                        }
                    }
                }
            }

            GetComponent<Renderer>().material.mainTexture = Util.MatToTexture2D(dst);
        }
    }
}