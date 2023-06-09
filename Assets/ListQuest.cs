using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class ListQuest : MonoBehaviour
{
    public Transform titleParent; // ブロックの親オブジェクトのTransform
    public GameObject frefab_Title;


    [SerializeField] private TresureLocation _tresureLocation = default;
    [SerializeField] public TextMeshProUGUI _gpsTitle;
    [SerializeField] public TextMeshProUGUI _gpsText;



    [Serializable]
    private sealed class TresurePoint
    {
        public int id = 0;
        public string title;
        public string details;
        //public float latitude;
        //public float longitude;
    }

    [Serializable]
    private sealed class TresurePointList
    {
        public List<TresurePoint> points;
    }


    private IEnumerator Start()
    {
        return this.requestQuest(2);
    }

    public IEnumerator requestQuest(int id)
    {





        // sato : 35.409526440038384, 139.58180702923255
        this._tresureLocation.latitude = 35.409526440038384f;
        this._tresureLocation.longitude = 139.58180702923255f;

        // base : 35.40959143413617, 139.58512153244988
        //this._tresureLocation.latitude = 35.40959143413617f;
        //this._tresureLocation.longitude = 139.58512153244988f;



        var url = string.Format("https://locator.kysaeed.com/api/point");
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                Debug.Log(request.downloadHandler.text);
                this.parseResponse(request.downloadHandler.text);
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log
                (
                    @"サーバとの通信に失敗。
リクエストが接続できなかった、
セキュリティで保護されたチャネルを確立できなかったなど。"
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log
                (
                    @"サーバがエラー応答を返した。
サーバとの通信には成功したが、
接続プロトコルで定義されているエラーを受け取った。"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log
                (
                    @"データの処理中にエラーが発生。
リクエストはサーバとの通信に成功したが、
受信したデータの処理中にエラーが発生。
データが破損しているか、正しい形式ではないなど。"
                );
                break;

            default: throw new ArgumentOutOfRangeException();
        }
    }

    protected void parseResponse(string responseText)
    {
        var pointsInfo = JsonUtility.FromJson<TresurePointList>(responseText);


        foreach (var p in pointsInfo.points)
        {
            //Debug.Log(p.id + ": " + p.title);

            var obj = Instantiate(this.frefab_Title, this.titleParent);
            var t = obj.GetComponent<TitleScript>();
            t.setTitle(p.title);

        }




        //this._tresureLocation.latitude = point.latitude;
        //this._tresureLocation.longitude = point.longitude;

        //this._gpsTitle.text = point.title;
        //this._gpsText.text = point.details;

    }
}
