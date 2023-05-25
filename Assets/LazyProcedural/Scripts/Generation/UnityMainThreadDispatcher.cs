using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher instance;
    private static readonly Queue<Func<Task>> queue = new Queue<Func<Task>>();
    private static bool isProcessingQueue;

    public static UnityMainThreadDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                var gameObject = new GameObject("UnityMainThreadDispatcher");
                instance = gameObject.AddComponent<UnityMainThreadDispatcher>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }
    }

    private void Update()
    {
        lock (queue)
        {
            if (queue.Count > 0)
            {
                isProcessingQueue = true;
                var task = queue.Dequeue();
                task.Invoke().ContinueWith(_ => { isProcessingQueue = false; });
            }
            else
            {
                isProcessingQueue = false;
            }
        }
    }

    public Task RunOnMainThreadAsync(Func<Task> action)
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        lock (queue)
        {
            queue.Enqueue(async () =>
            {
                await action.Invoke();
                taskCompletionSource.SetResult(true);
            });
        }
        return taskCompletionSource.Task;
    }
}
