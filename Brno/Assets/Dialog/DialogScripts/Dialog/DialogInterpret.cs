using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{

    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;
    private int kCliickCounter;
	public bool DestroyOnEnd = true;
    

    private string descriptionText = "přeskočit dialog";
    private KeyCode key = KeyCode.Space;
	public void Init()
	{
		dialog.WasPlayed = false; // odebrat


		dialog.Init();
		dialog.OnEnd.AddListener( () =>
		{
			UIManager.Instance.ShowUI();
			OnDialogEnd.Invoke();
			if (DestroyOnEnd)
			{
				Destroy(gameObject);
			}

			
		});
		dialog.OnStart.AddListener (() =>
		{
			UIManager.Instance.HideUI();
			OnDialogStart.Invoke();

		});

	}
	private void Awake()
    {
		if (dialog == null) return;
		Init();

    }

    private void Update()
    {
	
        if (dialog!=null&&dialog.IsPlaying && Input.GetKeyDown(KeyCode.Space))
        {
			
			DialogManager.Instance.SkipAtention.gameObject.SetActive(true);
            kCliickCounter++;

            if (kCliickCounter == 2)
            {
			DialogManager.Instance.SkipAtention.gameObject.SetActive(false);

				kCliickCounter = 0;
                dialog.OnEnd.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsEnable && !dialog.WasPlayed)
        {
            dialog.OnStart.Invoke();

        }
    }

}

