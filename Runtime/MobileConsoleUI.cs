using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MobileDevEnv {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MobileConsoleUI : MonoBehaviour {
        [FormerlySerializedAs("_maxLines")]
        [Header("Settings")]
        [SerializeField] int _maxLinesCount = 10;
        
        TextMeshProUGUI _text;
        string _log;
        int _lineCount;
        
        void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
            _log = "";
            _lineCount = 0;
        }

        void OnEnable() => Application.logMessageReceived += HandleLog;
        void OnDisable() => Application.logMessageReceived -= HandleLog;

        void HandleLog(string logString, string stackTrace, LogType type) {
            string color = GetColorFromType(type);
            logString = $"<color={color}>{logString}</color>";
            
            _log += logString + "\n";
            _lineCount++;

            if (_lineCount > _maxLinesCount) {
                _lineCount--;
                _log = DeleteLines(_log, 1);
            }
            
            _text.SetText(_log);
        }

        static string GetColorFromType(LogType type) {
            return type switch {
                LogType.Log => "white",
                LogType.Assert => "white",
                LogType.Warning => "yellow",
                LogType.Error => "red",
                LogType.Exception => "purple",
                _ => "white"
            };
        }
        
        static string DeleteLines(string message, int linesToRemove) {
            return message.Split(Environment.NewLine.ToCharArray(), linesToRemove + 1).Skip(linesToRemove).FirstOrDefault();
        }
    }
}
