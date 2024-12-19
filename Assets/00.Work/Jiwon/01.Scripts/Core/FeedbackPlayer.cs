using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> feedbackToPlayer;

    private void Awake()
    {
        feedbackToPlayer = GetComponents<Feedback>().ToList();
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        feedbackToPlayer.ForEach(f => f.PlayeFeedback());
    }

    private void FinishFeedback()
    {
        feedbackToPlayer.ForEach(f => f.StopFeedback());
    }
}
