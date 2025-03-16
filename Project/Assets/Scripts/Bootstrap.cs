using System;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour {
    private List<IDisposable> _disposables = new List<IDisposable>();

    protected void SetDisposables(List<IDisposable> list) {
        _disposables = list;
    }

    private void OnDestroy() {
        foreach(var disposable in _disposables) {
            disposable.Dispose();
        }
    }
}