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

            // �N���C�A���g�����擾
            string clientName = ClientDataController.Instance.ClientName;

            // �L�����N�^�[�^�C�v���擾
            var characterType = characterData.CharacterType;

            // �L�����N�^�[�^�C�v���A�N���C�A���g�f�[�^�ɕۑ����܂�
            ClientDataController.Instance.SetCharacterType(characterType);

            // �L�����N�^�[���X�|�[��������
            LobbySpawner.Instance.RPC_HostCharacterGenerated(clientName, characterType);

            _disableEvent.OnNext(Unit.Default);
        }

        public override void SetEnable()
        {
            base.SetEnable();

            // �L�����N�^�[�f�[�^���X�g���擾����
            List<CharacterData> characterDatas = CharacterDataController.Instance.CharacterDatas;
            // �C�x���g�Ńf�[�^���X�g�𑗐M����
            _selectionEvent.OnNext(characterDatas);
        }
    }
}


