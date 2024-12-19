using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFeedback : Feedback
{
    [SerializeField] private float duration;
    private Volume _camVolume;
    
    private Tween _tween;

    private void Start()
    {
        _camVolume = Camera.main.GetComponent<CamVolume>().Volume;
    }

    public override void PlayeFeedback()
    {
        Vignette vignette;

        if (_camVolume.profile.TryGet(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 0.5f;
            _tween = DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x,0f ,duration)
                .OnComplete(() =>
                {
                    vignette.intensity.value = 0f;
                    vignette.active = false;
                });
        }
    }

    public override void StopFeedback()
    {
        if (_tween.IsActive())
            _tween.Complete();
    }
}
