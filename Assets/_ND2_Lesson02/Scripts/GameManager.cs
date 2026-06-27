using UnityEngine;

//===使用するデザインパターン===//
//-シングルトンパターン(Single ton Pattern)-
//このオブジェクト内に、「ただ1つのみに存在するオブジェクト」を作る時に使用するパターン。
//どのオブジェクトからでも即座にアクセスできる！！
public class GameManager : MonoBehaviour
{
    //静的(static)な変数
    //↓[static]を付けた変数はどこからでも参照（アクセス）できる！！
    public static GameManager Instance {  get; private set; }

    //===各種変数を宣言===//
    public BasePlayer playerPrefab;//BasePlayer(プレハブ用変数)
    public BaseEnemy enemyPrefab;//BaseEnemy(プレハブ用変数)
    public BaseBullet bulletPrefab;//baseBullet(プレハブ用変数)

    public BasePlayer[] players;//インスタンス用変数
    public BaseEnemy[] enemies;//インスタンス用変数
    public BaseBullet[] bullets;//インスタンス用変数

    //Startの上位互換※Startよりも速く実行される
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;              //変数に自身のクラスを代入
        DontDestroyOnLoad(gameObject);//シーンが変わっても破棄されない設定
    }

    void Start()
    {
        //===各配列の初期化===//
        players = new BasePlayer[1];//プレイヤーを n体 で初期化
        enemies = new BaseEnemy[100];//敵を100体で初期化
        bullets = new BaseBullet[100];//弾丸を100発で初期化

        //===キャラクタースポーンさせる===//
        //indexの初期値を０とし、indexが”１より小さい時だけ”；indexを1ずつ増やしながら繰り返す
        for (int index = 0; index < 1; index++)
        {
            players[index] = Instantiate(playerPrefab);//プレハブインスタンスの生成(BasePlayer)
        }

        //indexの初期値を０とし、indexが”100より小さい時だけ”；indexを1ずつ増やしながら繰り返す
        for (int index = 0; index < 100; index++)
        {
            enemies[index] = Instantiate(enemyPrefab);//プレハブインスタンスの生成(BaseEnemy)
        }

        //indexの初期値を０とし、indexが”100より小さい時だけ”；indexを1ずつ増やしながら繰り返す
        for (int index = 0; index < 100; index++)
        {
            bullets[index] = Instantiate(bulletPrefab);//プレハブインスタンスの生成(BaseBullet)
        }
        //===キャラクターを初期化する===//
        for (int index = 0; index < 1; index++)
        {
            players[index].Initialize(this,new Vector2(-3, 0));
        }

        for (int index = 0; index < 100; index++)
        {
            Vector2 randomPos = Vector2.zero;
            randomPos.x = 15;
            randomPos.y = Random.Range(-5.0f, 5f);

            enemies[index].Initialize(randomPos);
        }

        for (int index = 0; index < 100; index++)
        {
            bullets[index].Initialize(players[0].transform.position,Vector3.right);
        }

    }

    void Update()
    {
        //プレイヤーを全員動かす
        for (int index = 0; index < 1; index++)
        {
            players[index].Movement();
            players[index].Shot();
        }

        //敵も全員動かす
        for (int index = 0; index < 100; index++)
        {
            enemies[index].Movement();
        }

        //弾丸も全員動かす
        for (int index = 0; index < 100; index++)
        {
            bullets[index].Movement();
        }
    }

    //===オブジェクトを登録するメソッド===//
    public void EnterPlayer(BasePlayer player)
    {
        //初期化ができていなければ何もしない...
        if (players == null || players.Length == 0)
        {
            Debug.Log("配列の初期化に失敗していますよ...");
            return;   //強制終了...
        }

        //配列の中身をすべてチェック
        for (int index = 0; index < players.Length; index++)
        {
            Debug.Log($"配列の{index}番目の中身 >> {players[index]}");
            if (players[index] == null)
            {
                Debug.Log($"{index}番目に中身がない(null)なので、{player}を代入します。");
                players[index] = player;

                return;   //入れたら終了
            }
        }

        Debug.Log($"空きがないので、{player}を設定できませんでした...");
    }

    public void EnterEnemy(BaseEnemy enemy)
    {
        //初期化ができていなければ何もしない...
        if (enemies == null || enemies.Length == 0)
        {
            Debug.Log("配列の初期化に失敗していますよ...");
            return;   //強制終了...
        }

        //配列の中身をすべてチェック
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log($"配列の{i}番目の中身 >> {enemies[i]}");
            if (enemies[i] == null)
            {
                Debug.Log($"{i}番目に中身がない(null)なので、{enemy}を代入します。");
                enemies[i] = enemy;

                return;   //入れたら終了
            }
        }

        Debug.Log($"空きがないので、{enemy}を設定できませんでした...");
    }
}
