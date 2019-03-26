using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/New Dialog")]
public class Dialog : ScriptableObject, IInterpretable
{
	[SerializeField]
	private AudioClip clip;

	public AudioClip Clip { get { return clip; } }
	[ExecuteAlways]
	[SerializeField]
	private List<Dialogoure> subtitles = new List<Dialogoure>();

	public List<Dialogoure> Subtitles { get { return subtitles; } }
	private int subtitlesIndex = 0;
	private int maxIndex;
	public UnityEvent OnStart;
	public UnityEvent OnEnd;
	public UnityEvent OnSubtitlesChange;
	public bool IsPlaying { get; set; }
	public bool WasPlayed { get; set; }



	// Must be called before using Dialog
	public void Init()
	{
		WasPlayed = false;//odebrat
		subtitlesIndex = 0;
		maxIndex = subtitles.Count - 1;
		foreach (Dialogoure item in subtitles)
		{
			item.Time = item.StartTimer;

		}

		OnStart.AddListener(() =>
	   {
		   subtitlesIndex = 0;
		   if (subtitles[subtitlesIndex].Speaker != null)
		   {
			   DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + subtitles[subtitlesIndex].Text;
		   }
		   else
		   {
			   DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Text;
		   }

		   DialogManager.Instance.AudioPlayer.clip = clip;
		   DialogManager.Instance.AudioPlayer.Play();
		   IsPlaying = true;
		   DialogManager.Instance.StartCoroutine(Timer());
		   WasPlayed = true;
		   if (PlayerScript.Instance != null)
		   {
			   MouseManager.Instance.CanClick = false;
			   PlayerScript.Instance.Anim.SetFloat("Speed", 0);
		   }
		   DialogManager.Instance.SubtitleArea.gameObject.SetActive(true);

		   if (PlayerScript.Instance != null)
		   {
			   PlayerScript.Instance.Agent.isStopped = true;
		   }

	   });
		OnEnd.AddListener(() =>
		{
			DialogManager.Instance.StopAllCoroutines();

			DialogManager.Instance.SubtitleArea.text = "";

			DialogManager.Instance.AudioPlayer.clip = null;
			DialogManager.Instance.AudioPlayer.Stop();
			IsPlaying = false;
			if (PlayerScript.Instance != null)
			{
				MouseManager.Instance.CanClick = true;
				PlayerScript.Instance.Agent.isStopped = false;
			}



			DialogManager.Instance.SubtitleArea.gameObject.SetActive(false);




		});
		OnSubtitlesChange.AddListener(() =>
		{
			DialogManager.Instance.StopAllCoroutines();
			subtitlesIndex++;
			if (subtitles[subtitlesIndex].Speaker != null)
			{
				DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + subtitles[subtitlesIndex].Text;
			}
			else
			{
				DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Text;
			}
			DialogManager.Instance.StartCoroutine(Timer());


		});

	}

	public IEnumerator Timer()
	{

		yield return new WaitForSeconds(Subtitles[subtitlesIndex].StartTimer);

		if (subtitlesIndex < maxIndex)
		{
			OnSubtitlesChange.Invoke();
		}
		else
		{
			OnEnd.Invoke();
		}
	}
	
}




