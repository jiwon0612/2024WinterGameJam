using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundDataSO _soundData;
    
    public override void PlayeFeedback()
    {
        SoundPlayer soundPlayer = PoolManager.Instance.Pop("SoundPlayer") as SoundPlayer;
        soundPlayer.PlaySound(_soundData);
    }

    public override void StopFeedback()
    {
        
    }
}
