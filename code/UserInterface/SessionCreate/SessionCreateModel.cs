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

            // �Z�b�V�������쐬���āA�ڑ�����
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

        /// <summary> �L���� </summary>
        public override void SetEnable()
        {
            base.SetEnable();

            _sessionName = "";
        }

        private async void SessionCreate()
        {
            // �Z�b�V�������쐬����
            var result = await LobbyConnection.Instance.CreateLobbyRunner(_sessionName);

            // �ڑ�����
            if(result.Ok)
            {
                // ���݂̃Q�[���V�[�����A�����[�h
                SceneController.Instance.UnloadAllScene();

                // �V�[������̌������`�F�b�N����
                if (LobbyConnection.Instance.IsSceneAuthority)
                {
                    Debug.Log("Host �ł��BScene �̓ǂݍ��݂����s���܂��B");

                    await LobbyConnection.Instance.LoadingLobbyScene();
                }
            }
            else
            {
                // �G���[
            }
        }
    }
}
