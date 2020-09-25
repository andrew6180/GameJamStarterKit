using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameJamStarterKit.UI
{
    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour
    {
        public float Percent
        {
            get => percent;
            set => percent = Mathf.Clamp(value, 0f, 1f);
        }

        [Range(0, 1)]
        [SerializeField]
        private float percent = 0.5f;

        public Image Fill;
        
        private void Update()
        {
            if (Fill == null)
                return;
            
            if (Fill.type != Image.Type.Filled)
                Fill.type = Image.Type.Filled;
            Fill.fillAmount = Percent;
        }
        
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/Progress Bar") ] 
        private static void Create()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
                canvas = new GameObject("Canvas", typeof(Canvas)).GetComponent<Canvas>();
            
            var go = new GameObject("Progress Bar", typeof(ProgressBar));
            go.transform.SetParent(canvas.transform, true);
            var progressBar = go.GetComponent<ProgressBar>();
            Selection.activeObject = go;
            var rectTransform = go.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 150);

            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameJamStarterKit/UI/Textures/White_1x1.png");
            // create background.
            var backgroundGo = new GameObject("Background", typeof(Image));
            var backgroundImage = backgroundGo.GetComponent<Image>();
            backgroundImage.sprite = sprite;
            backgroundGo.transform.SetParent(go.transform, false);
            SetupChildTransform(backgroundGo, Vector2.zero, Vector2.zero);

            var fillGo = new GameObject("Fill", typeof(Image));
            fillGo.transform.SetParent(go.transform, false);
            SetupChildTransform(fillGo, new Vector2(1, 1), new Vector2(-1, -1));
            
            progressBar.Fill = fillGo.GetComponent<Image>();
            progressBar.Fill.sprite = sprite;
            progressBar.Fill.type = Image.Type.Filled;
            progressBar.Fill.fillMethod = Image.FillMethod.Horizontal;
            progressBar.Fill.fillAmount = 0.5f;
            progressBar.Fill.color = Color.red;
            
            var overlayGo = new GameObject("Overlay", typeof(Image));
            overlayGo.transform.SetParent(go.transform, false);
            var overlayImage = overlayGo.GetComponent<Image>();
            overlayImage.sprite = sprite;
            SetupChildTransform(overlayGo, Vector2.zero, Vector2.zero);
            overlayGo.SetActive(false);
        }

        private static void SetupChildTransform(GameObject backgroundGo, Vector2 offsetMin, Vector2 offsetMax)
        {
            var rectTransform = backgroundGo.GetComponent<RectTransform>();

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero + offsetMin;
            rectTransform.offsetMax = Vector2.zero + offsetMax;
        }
#endif
    }
}