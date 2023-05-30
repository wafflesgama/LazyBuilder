using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteAlways]
public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher instance;
    private static readonly Queue<Action> queue = new Queue<Action>();
    private static float maxMsPerCycle = 5000;

    private readonly Stopwatch watch = new Stopwatch();
    public static UnityMainThreadDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                var gameObj = GameObject.Find("UnityMainThreadDispatcher");
                if (gameObj == null)
                    gameObj = new GameObject("UnityMainThreadDispatcher");

                instance = gameObj.AddComponent<UnityMainThreadDispatcher>();
            }
            return instance;
        }
    }

    private void Update()
    {
        if (queue.Count == 0) return;

        lock (queue)
        {
            watch.Reset();
            watch.Start();
            while (watch.ElapsedMilliseconds < maxMsPerCycle)
            {
                if (queue.Count == 0) return;

                var task = queue.Dequeue();

                task.Invoke();
                UnityEngine.Debug.Log("Running Queue");
            }
            watch.Stop();
        }
    }

    public Task RunOnMainThreadAsync(Action action)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        UnityEngine.Debug.Log("Added To Queue");
        lock (queue)
        {
            queue.Enqueue(() =>
            {
                action.Invoke();
                taskCompletionSource.SetResult(true);
            });
        }
        return taskCompletionSource.Task;
    }
}
