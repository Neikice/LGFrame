using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logtest : MonoBehaviour
{
    public LogOne Prefab;
    public Transform prefabParent;
    public Transform prefabDeactiveParent;
    public Scrollbar scrollbar;

    private LogOnePool pool;

    public int count;

    readonly Stack<Log> logs = new Stack<Log>();
    bool collapse;

    struct Log
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }

    void Awake()
    {
        pool = new LogOnePool("logtest" ,this.Prefab, this.prefabParent, this.prefabDeactiveParent,this.count);
        pool.PreloadAsync(this.count);
    }
    // Use this for initialization
    void Start()
    {
        Application.logMessageReceived += ReciveLog;
    }



    void ReciveLog(string message, string stackTrace, LogType type)
    {
        var temp = new Log { message = message, stackTrace = stackTrace, type = type};
        logs.Push(temp);

        var log = pool.Spawn();
        log.Set(message, stackTrace, type);
        this.scrollbar.value = 0;
    }

    void forlog()
    {
        for (int i = 0; i < logs.Count; i++)
        {
            
        }
    }
}
