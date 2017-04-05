using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;using UniRx;public class LogOne : MonoBehaviour{
    [SerializeField]
    private Text message;
    [SerializeField]
    private GameObject stackTrace;
    [SerializeField]
    private Text stackTraceText;

    public Text Message { get { return message; } }
    public GameObject StackTrace { get { return stackTrace; } }

    private Button button;
    private string stackText;


    static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(this.showStackTrace);

    }    public void Set(string message, string stackTarce, LogType color)
    {
        this.Message.text = message;
        this.Message.color = logTypeColors[color];

        this.stackText = stackTarce;
    }    void showStackTrace()
    {
        //this.Message.gameObject.SetActive(!this.Message.gameObject.activeInHierarchy);
        this.StackTrace.SetActive(!this.StackTrace.activeInHierarchy);
        this.stackTraceText.text = this.stackText;
    }}