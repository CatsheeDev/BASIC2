using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MathGameScript : MonoBehaviour
{
    
    private void Start()
    {
        this.gc.ActivateLearningGame();
        if (this.gc.notebooks == 1)
        {
            this.QueueAudio(this.bal_intro);
            this.QueueAudio(this.bal_howto);
        }
        this.NewProblem();
        if (this.gc.spoopMode)
        {
            this.baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
        }
    }

    
    private void Update()
    {
        if (!this.baldiAudio.isPlaying)
        {
            if (this.audioInQueue > 0 & !this.gc.spoopMode)
            {
                this.PlayQueue();
            }
            this.baldiFeed.SetBool("talking", false);
        }
        else
        {
            this.baldiFeed.SetBool("talking", true);
        }
        if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & this.questionInProgress)
        {
            this.questionInProgress = false;
            this.CheckAnswer();
        }
        if (this.problem > 3)
        {
            this.endDelay -= 1f * Time.unscaledDeltaTime;
            if (this.endDelay <= 0f)
            {
                GC.Collect();
                this.ExitGame();
            }
        }
    }

    
    private void NewProblem()
    {
        this.playerAnswer.text = string.Empty;
        this.problem++;
        this.playerAnswer.ActivateInputField();
        if (this.problem <= 3)
        {
            this.QueueAudio(this.bal_problems[this.problem - 1]);
            if ((this.gc.mode == "story" & (this.problem <= 2 || this.gc.notebooks <= 1)) || (this.gc.mode == "endless" & (this.problem <= 2 || this.gc.notebooks != 2)))
            {
                this.num1 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                this.num2 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                this.sign = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num1)]);
                if (this.sign == 0)
                {
                    this.solution = this.num1 + this.num2;
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n \n",
                        this.num1,
                        "+",
                        this.num2,
                        "="
                    });
                    this.QueueAudio(this.bal_plus);
                }
                else if (this.sign == 1)
                {
                    this.solution = this.num1 - this.num2;
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n \n",
                        this.num1,
                        "-",
                        this.num2,
                        "="
                    });
                    this.QueueAudio(this.bal_minus);
                }
                this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num2)]);
                this.QueueAudio(this.bal_equals);
            }
            else
            {
                this.impossibleMode = true;
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                this.QueueAudio(this.bal_screech);
                if (this.sign == 0)
                {
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                    this.QueueAudio(this.bal_plus);
                    this.QueueAudio(this.bal_screech);
                    this.QueueAudio(this.bal_times);
                    this.QueueAudio(this.bal_screech);
                }
                else if (this.sign == 1)
                {
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                    this.QueueAudio(this.bal_divided);
                    this.QueueAudio(this.bal_screech);
                    this.QueueAudio(this.bal_plus);
                    this.QueueAudio(this.bal_screech);
                }
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (this.sign == 0)
                {
                    this.questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                }
                else if (this.sign == 1)
                {
                    this.questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                }
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (this.sign == 0)
                {
                    this.questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                }
                else if (this.sign == 1)
                {
                    this.questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                }
                this.QueueAudio(this.bal_equals);
            }
            this.questionInProgress = true;
        }
        else
        {
            this.endDelay = 5f;
            if (!this.gc.spoopMode)
            {
                this.questionText.text = "WOW! YOU EXIST!";
            }
            else if (this.gc.mode == "endless" & this.problemsWrong <= 0)
            {
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.endlessHintText[num];
            }
            else if (this.gc.mode == "story" & this.problemsWrong >= 3)
            {
                this.questionText.text = "I HEAR MATH THAT BAD";
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
                if (this.baldiScript.isActiveAndEnabled) this.baldiScript.Hear(this.playerPosition, 7f);
                this.gc.failedNotebooks++;
            }
            else
            {
                int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.hintText[num2];
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
            }
        }
    }

    
    public void OKButton()
    {
        this.CheckAnswer();
    }

    
    public void CheckAnswer()
    {
        if (this.playerAnswer.text == "31718")
        {
            base.StartCoroutine(this.CheatText("THIS IS WHERE IT ALL BEGAN"));
            SceneManager.LoadSceneAsync("TestRoom");
        }
        else if (this.playerAnswer.text == "53045009")
        {
            base.StartCoroutine(this.CheatText("USE THESE TO STICK TO THE CEILING!"));
            this.gc.Fliparoo();
        }
        if (this.problem <= 3)
        {
            if (this.playerAnswer.text == this.solution.ToString() & !this.impossibleMode)
            {
                this.results[this.problem - 1].texture = this.correct;
                this.baldiAudio.Stop();
                this.ClearAudioQueue();
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
                this.QueueAudio(this.bal_praises[num]);
                this.NewProblem();
            }
            else
            {
                this.problemsWrong++;
                this.results[this.problem - 1].texture = this.incorrect;
                if (!this.gc.spoopMode)
                {
                    this.baldiFeed.SetTrigger("angry");
                    this.gc.ActivateSpoopMode();
                }
                if (this.gc.mode == "story")
                {
                    if (this.problem == 3)
                    {
                        this.baldiScript.GetAngry(1f);
                    }
                    else
                    {
                        this.baldiScript.GetTempAngry(0.25f);
                    }
                }
                else
                {
                    this.baldiScript.GetAngry(1f);
                }
                this.ClearAudioQueue();
                this.baldiAudio.Stop();
                this.NewProblem();
            }
        }
    }

    
    private void QueueAudio(AudioClip sound)
    {
        this.audioQueue[this.audioInQueue] = sound;
        this.audioInQueue++;
    }

    
    private void PlayQueue()
    {
        this.baldiAudio.PlayOneShot(this.audioQueue[0]);
        this.UnqueueAudio();
    }

    
    private void UnqueueAudio()
    {
        for (int i = 1; i < this.audioInQueue; i++)
        {
            this.audioQueue[i - 1] = this.audioQueue[i];
        }
        this.audioInQueue--;
    }

    
    private void ClearAudioQueue()
    {
        this.audioInQueue = 0;
    }

    
    private void ExitGame()
    {
        if (this.problemsWrong <= 0 & this.gc.mode == "endless")
        {
            this.baldiScript.GetAngry(-1f);
        }
        this.gc.DeactivateLearningGame(base.gameObject);
        BasicAPI.Events.OnMathGameEnd.Invoke();
    }

    
    public void ButtonPress(int value)
    {
        if (value >= 0 & value <= 9)
        {
            this.playerAnswer.text = this.playerAnswer.text + value;
        }
        else if (value == -1)
        {
            this.playerAnswer.text = this.playerAnswer.text + "-";
        }
        else
        {
            this.playerAnswer.text = string.Empty;
        }
    }

    
    private IEnumerator CheatText(string text)
    {
        for (; ; )
        {
            this.questionText.text = text;
            this.questionText2.text = string.Empty;
            this.questionText3.text = string.Empty;
            yield return new WaitForEndOfFrame();
        }
#pragma warning disable CS0162 
        yield break;
#pragma warning restore CS0162 
    }

    
    public GameControllerScript gc;

    
    public BaldiScript baldiScript;

    
    public Vector3 playerPosition;

    
    public GameObject mathGame;

    
    public RawImage[] results = new RawImage[3];

    
    public Texture correct;

    
    public Texture incorrect;

    
    public TMP_InputField playerAnswer;

    
    public TMP_Text questionText;

    
    public TMP_Text questionText2;

    
    public TMP_Text questionText3;

    
    public Animator baldiFeed;

    
    public Transform baldiFeedTransform;

    
    public AudioClip bal_plus;

    
    public AudioClip bal_minus;

    
    public AudioClip bal_times;

    
    public AudioClip bal_divided;

    
    public AudioClip bal_equals;

    
    public AudioClip bal_howto;

    
    public AudioClip bal_intro;

    
    public AudioClip bal_screech;

    
    public AudioClip[] bal_numbers = new AudioClip[10];

    
    public AudioClip[] bal_praises = new AudioClip[5];

    
    public AudioClip[] bal_problems = new AudioClip[3];

    
    public Button firstButton;

    
    private float endDelay;

    
    private int problem;

    
    private int audioInQueue;

    
    private float num1;

    
    private float num2;

    
    private float num3;

    
    private int sign;

    
    private float solution;

    
    private string[] hintText = new string[]
    {
        "I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG",
        "I HEAR EVERY DOOR YOU OPEN"
    };

    
    private string[] endlessHintText = new string[]
    {
        "That's more like it...",
        "Keep up the good work or see me after class..."
    };

    
    private bool questionInProgress;

    
    private bool impossibleMode;

    
    private bool joystickEnabled;

    
    private int problemsWrong;

    
    private AudioClip[] audioQueue = new AudioClip[20];

    
    public AudioSource baldiAudio;
}
