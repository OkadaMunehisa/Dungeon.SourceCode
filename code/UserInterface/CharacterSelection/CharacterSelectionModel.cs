using Dungeon.Client;
using Dungeon.Characters;
using Dungeon.Spawner;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon.UserInterface
{
    public class CharacterSelectionModel :ModelBase
    {
        private readonly Subject<List<CharacterData>> _selectionEvent = new Subject<List<CharacterData>>();

        public Observable<List<CharacterData>> SelectionEvent => _selectionEvent.AsObservable();

        public void OnClickConfirm(CharacterData characterData)
        {
            Debug.Log("OnClickConfirm");

            // クライアント名を取得
            string clientName = ClientDataController.Instance.ClientName;

            // キャラクタータイプを取得
            var characterType = characterData.CharacterType;

            // キャラクタータイプを、クライアントデータに保存します
            ClientDataController.Instance.SetCharacterType(characterType);

            // キャラクターをスポーンさせる
            LobbySpawner.Instance.RPC_HostCharacterGenerated(clientName, characterType);

            _disableEvent.OnNext(Unit.Default);
        }

        public override void SetEnable()
        {
            base.SetEnable();

            // キャラクターデータリストを取得する
            List<CharacterData> characterDatas = CharacterDataController.Instance.CharacterDatas;
            // イベントでデータリストを送信する
            _selectionEvent.OnNext(characterDatas);
        }
    }
}


