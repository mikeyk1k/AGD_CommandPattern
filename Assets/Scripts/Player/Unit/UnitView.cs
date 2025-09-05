using System;
using UnityEngine;
using UnityEngine.UI;

namespace Command.Player
{
    public class UnitView : MonoBehaviour
    {
        public UnitController Controller;

        [SerializeField] private SpriteRenderer unitIndicator;
        [SerializeField] private Image healthBar;
        private Animator unitAnimator;
        private event Action OnActionAnimationCompleted;

        private void Awake()
        {
            unitAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            PlayAnimation(UnitAnimations.IDLE);
        }

        public void PlayAnimation(UnitAnimations animationToPlay)
        {
            unitAnimator.Play(animationToPlay.ToString(), 0);
        }

        public void PlayAnimation(UnitAnimations animationToPlay, Action onAnimationComplete)
        {
            OnActionAnimationCompleted = onAnimationComplete;
            unitAnimator.Play(animationToPlay.ToString(), 0);
        }

        public void SetUnitIndicator(bool setActive) => unitIndicator.gameObject.SetActive(setActive);

        public void UpdateHealthBar(float currentHealthRatio) => healthBar.transform.localScale = new Vector3(currentHealthRatio, 1, 1);

        public void OnActionAnimationComplete()
        {
            OnActionAnimationCompleted?.Invoke();
            OnActionAnimationCompleted = null;
            Controller.OnActionExecuted();
        }
    }

    public enum UnitAnimations
    {
        IDLE,
        ACTION1,
        ACTION2,
        HIT,
        DEATH
    }

}
