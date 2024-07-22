using Dungeon.Connection;
using Dungeon.Scene;
using R3;
using UnityEngine;

namespace Dungeon.UserInterface
{
    public class SessionCreateModel : ModelBase
    {
        private readonly Subject<Unit> _closeEvent = new Subject<Unit>();
        public Observable<Unit> CloseEvent => _closeEvent.AsObservable();

        private string _sessionName = "";

        public void OnClickConfirm()
        {
            Debug.Log("OnClickConfirm");

            if (_sessionName == "") return;

            // セッションを作成して、接続する
            SessionCreate();

            _disableEvent.OnNext(Unit.Default);
        }

        public void OnClickClose()
        {
            Debug.Log("OnClickClose");

            _closeEvent.OnNext(Unit.Default);
            _disableEvent.OnNext(Unit.Default);
        }

        public void SetSessionName(string name)
        {
            _sessionName = name;
        }

        /// <summary> 有効化 </summary>
        public override void SetEnable()
        {
            base.SetEnable();

            _sessionName = "";
        }

        private async void SessionCreate()
        {
            // セッションを作成する
            var result = await LobbyConnection.Instance.CreateLobbyRunner(_sessionName);

            // 接続成功
            if(result.Ok)
            {
                // 現在のゲームシーンをアンロード
                SceneController.Instance.UnloadAllScene();

                // シーン操作の権限をチェックする
                if (LobbyConnection.Instance.IsSceneAuthority)
                {
                    Debug.Log("Host です。Scene の読み込みを実行します。");

                    await LobbyConnection.Instance.LoadingLobbyScene();
                }
            }
            else
            {
                // エラー
            }
        }
    }
}
