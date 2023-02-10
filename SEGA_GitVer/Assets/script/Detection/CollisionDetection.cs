using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GodTouches;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject upperLeftDot;           // 左上
    [SerializeField] private GameObject upperCenterDot;         // 真上
    [SerializeField] private GameObject upperRightDot;          // 右上
    [SerializeField] private GameObject leftDot;                // 左
    [SerializeField] private GameObject centralDot;             // 真ん中
    [SerializeField] private GameObject rightDot;               // 右
    [SerializeField] private GameObject lowerLeftDot;           // 左下
    [SerializeField] private GameObject lowerCenterDot;         // 真下
    [SerializeField] private GameObject lowerRightDot;          // 右下


    /// <summary>
    /// 点の格納配列
    /// </summary>
    private GameObject[] dots;

    /// <summary>
    /// 通過場所の保存
    /// </summary>
    private List<GameObject> positionOfPassage;

    /// <summary>
    /// 光線
    /// </summary>
    private Ray ray;

    /// <summary>
    /// 光線のヒット
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// 取得したいレイヤー
    /// </summary>
    private int layerMask = 1 << 5;

    /// <summary>
    /// ドットの合計数
    /// </summary>
    private const int dotsTotalNumber = 9;

    private const float rayMaxDistance = 10.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // 点を取得
        dots = new GameObject[dotsTotalNumber] {   upperLeftDot , upperCenterDot , upperRightDot,
                                                        leftDot ,      centralDot ,     rightDot ,
                                                        lowerLeftDot, lowerCenterDot , lowerRightDot};

        positionOfPassage = new List<GameObject>(dotsTotalNumber);
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------

    /// <summary>
    /// 判定処理関数
    /// </summary>
    public void Drawing_Decision()
    {
        // マウスのスクリーンポジションの先に光線を出すための代入
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 光線を出したときにぶつかったら通る
        if (Physics.Raycast(ray, out hit, rayMaxDistance, layerMask))
        {
            // 最初の点ならば
            if (!FlagManager.is_firstTouchDot)
            {
                positionOfPassage.Clear();
                // 点の数だけ回る
                for (int i = 0; i < dots.Length; i++)
                {
                    // 今ヒットしているオブジェクトと点のオブジェクトが同じなら
                    if (hit.collider.gameObject == dots[i].gameObject)
                    {
                        // 通過場所格納リスと後方に追加
                        positionOfPassage.Add(hit.collider.gameObject);
                        // 効果音の再生
                        hit.collider.gameObject.GetComponent<PlaySoundSE>().OnPlaySounds();
                        FlagManager.is_firstTouchDot = true;
                    }
                }
            }
            else if (FlagManager.is_firstTouchDot)
            {
                for (int i = 0; i < positionOfPassage.Count; i++)
                {
                    // 今まで通った場所と今接触しているポジションが同じならばbreak
                    if (positionOfPassage[i].gameObject == hit.collider.gameObject)
                    {
                        break;
                    }
                    // iがpositionOfPassage.Count分回ったらAdd
                    else if (i == positionOfPassage.Count - 1)
                    {
                        // 通過場所格納リスと後方に追加
                        positionOfPassage.Add(hit.collider.gameObject);
                        // 効果音の再生
                        positionOfPassage[i].gameObject.GetComponent<PlaySoundSE>().OnPlaySounds();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 通過点のゲット関数
    /// </summary>
    /// <returns>positionOfPassage</returns>
    public List<GameObject> Get_TransitPoint()
    {
        return positionOfPassage;
    }

    /// <summary>
    /// 通過点の保存のクリア
    /// </summary>
    public void Clear_positionOfPassage()
    {
        positionOfPassage.Clear();
    }
}