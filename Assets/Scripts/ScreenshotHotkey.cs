using UnityEngine;

public class ScreenshotHotkey : MonoBehaviour
{
    public int supersize = 1; // 1 = normal resolution, 2 = double size

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            string file = "screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            ScreenCapture.CaptureScreenshot(file, supersize);
            Debug.Log("Saved screenshot to: " + file);
        }
    }
}
