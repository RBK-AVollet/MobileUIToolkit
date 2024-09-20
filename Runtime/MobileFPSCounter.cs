using TMPro;
using UnityEngine;

namespace MobileDevEnv {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MobileFPSCounter : MonoBehaviour {
        [Header("Settings")]
        [SerializeField] Gradient _statusGradient;
        
        TextMeshProUGUI _text;

        const float k_updateInterval = 0.1f;

        float _accum;
        float _frames;
        float _timeLeft;

        void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Update() {
            _timeLeft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            _frames++;

            if (_timeLeft <= 0.0f) {
                float fps = _accum / _frames;
                
                _timeLeft += k_updateInterval;
                _accum = 0.0f;
                _frames = 0;

                // Rounded by 2 decimal places
                _text.text = $"{fps:F2} FPS";
                _text.color = _statusGradient.Evaluate(Mathf.Clamp01(fps / 60.0f));
            }
        }
    }
}