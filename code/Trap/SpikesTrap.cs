using Fusion;
using System.Collections;
using UnityEngine;

namespace Dungeon.Trap
{
    public class SpikesTrap : NetworkBehaviour
    {
        [SerializeField] int _trapDamage = 15;
        [SerializeField] float _trapCooldownTime = 10f;
        [SerializeField] float _createDamageTime = 0.5f;

        // 周囲を検知する
        [SerializeField] BoxDetector _detector;

        // トラップのアニメーションを再生する
        [SerializeField] TrapAnimation _trapAnimation;

        // クールダウンを計測する
        private CooldownTimer _cooldown;
        // トラップの有効、無効を判定する
        private TrapActive _trapActive;

        public override void Spawned()
        {
            _cooldown = new CooldownTimer(Runner);
            _trapActive = new TrapActive();

            if (HasStateAuthority)
            {
                // トラップを有効にする
                _trapActive.ActivateTrap();
            }
        }

        public override void FixedUpdateNetwork()
        {
            // 状態権限者以外
            if (!HasStateAuthority)
                return;

            // トラップが有効な場合
            if (_trapActive.IsTrapActive)
            {
                // クールタイム中が終了しているなら
                if (_cooldown.IsNotCooldownActive)
                {
                    // トラップの周辺プレイヤーを検知
                    var detectorResult = _detector.DetectObject();

                    // 接触可能オブジェクトが１以上存在する場合
                    if (detectorResult.HitCount > 0)
                    {
                        // アニメーションを開始
                        _trapAnimation.AnimationTrigger();

                        // ダメージ処理の予約
                        StartCoroutine(DealDamageAfterDelay(_createDamageTime));
                        // クールタイムをスタート
                        _cooldown.StartCooldown(_trapCooldownTime);
                    }
                }
            }
        }

        private IEnumerator DealDamageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            // トラップの周辺プレイヤーを検知
            var detectorResult = _detector.DetectObject();

            // 接触可能オブジェクトが１以上存在する場合
            if (detectorResult.HitCount > 0)
            {
                for (int i = 0; i < detectorResult.HitCount; i++)
                {
                    IDamageable damageApplicable = detectorResult.HitColliders[i].GetComponent<IDamageable>();

                    if (damageApplicable != null)
                    {
                        // ダメージを与える
                        damageApplicable.ApplyDamage(_trapDamage);
                    }
                }
            }
        }
    }
}
