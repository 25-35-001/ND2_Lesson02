using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    
    void Start()
    {
        ////↓GameManagerのInstanceは静的変数なので、GameManager.Instanceで直接参照できる。
        //Debug.Log($"エネミーから{GameManager.Instance}へアクセス。");
        //GameManager.Instance.EnterEnemy(this);
    }

    void Update()
    {
        
    }

    //===初期化メソッド===//
    public void Initialize(Vector2 position)
    { 
        transform.position = position;
    }

    //===移動メソッド===//
    public void Movement()
    {
        //Translate(移動方向(m) * 時間単位(s))で移動
        transform.Translate(Vector3.left * Time.deltaTime);
    }
}
