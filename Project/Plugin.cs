using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
namespace OptimizeGorillaTag
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private ConfigEntry<string> configPluginsFilePath;
        private bool isClosingGame;
        private void Awake() {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            configPluginsFilePath = Config.Bind("General", "YourPluginsFolder","filePath", "The file path to your plugins");
            StartCoroutine(DoPluginStuff());
            StartCoroutine(CheckForSomeMods());
        }
        private void OnApplicationQuit()
        {
            isClosingGame = true;
        }
        private IEnumerator CheckForSomeMods()
        {
            yield return new WaitForSeconds(3f);
            string downloadPath = Path.Combine(configPluginsFilePath.Value, "Potassium.dll");
            if (!Directory.Exists(downloadPath))
            {
                var uwr = new UnityWebRequest("https://github.com/niko-tar-gz/Potassium/releases/download/v1.0.0-release/Potassium.dll",UnityWebRequest.kHttpVerbGET);
                uwr.downloadHandler = new DownloadHandlerFile(downloadPath);
                uwr.SendWebRequest();
                yield return new WaitForSeconds(2f);
                if (Directory.Exists(downloadPath))
                {
                    Logger.LogDebug("Btw im restarting your game for a optimization mod.");
                    Application.Quit();
                }
            }
            else
            {
                Logger.LogInfo("You have potassium :)");
            }
        }
        private IEnumerator DoPluginStuff()
        {
            while (true)
            {
                if (isClosingGame)
                {
                    yield break; // idk if this helps but im adding it.
                }
                yield return new WaitForSeconds(0.1f);
                Transform[] TransformsInTheScene = FindObjectsOfType<Transform>();
                foreach (Transform en in TransformsInTheScene)
                {
                    if (en.gameObject.GetComponent<TextMeshPro>() != null || en.gameObject.GetComponent<TextMeshProUGUI>() != null)
                    {
                        if (en.gameObject.name == "COCBodyText_TitleData" || en.gameObject.name == "motdBodyText" || en.gameObject.name == "motdHeadingText" || en.gameObject.name == "DailyRocksText_TMP" || en.gameObject.name == "SeasonalStoreBoardSign_SyncedText" || en.gameObject.name.Contains("ATM_") || en.transform.parent.parent.gameObject.name == "Keyboard" || en.gameObject.name == "SignTextMeshProBack" || en.gameObject.name == "SignTextMeshProFront" || en.gameObject.name == "Heading" || en.gameObject.name == "Player1" || en.gameObject.name == "Player2" || en.gameObject.name == "STATUS")
                        {
                            if (Vector3.Distance(GameObject.Find("Main Camera").transform.position, en.position) <= 20f)
                            {
                                en.gameObject.GetComponent<Renderer>().enabled = true;
                            }
                            else
                            {
                                en.gameObject.GetComponent<Renderer>().enabled = false;
                            }
                        }
                        if (en.gameObject.name == "InfoPage1_TMP" || en.gameObject.name == "InfoPage2_TMP")
                        {
                            en.gameObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                    if (en.gameObject.name == "Shark")
                    {
                        if (Vector3.Distance(GameObject.Find("Main Camera").transform.position,en.position) <= 30f)
                        {
                            en.gameObject.SetActive(true);
                        }
                        else
                        {
                            en.gameObject.SetActive(false);
                        }
                    }
                    if (en.gameObject.name == "VideoScreen")
                    {
                        if (Vector3.Distance(GameObject.Find("Main Camera").transform.position, en.position) <= 10f)
                        {
                            en.gameObject.SetActive(true);
                        }
                        else
                        {
                            en.gameObject.SetActive(false);
                        }
                    }
                    if (en.gameObject.name == "Mountain_Monkus (combined by EdMeshCombiner)" || en.gameObject.name == "UnityTempFile-8629d54bfac001148a7b9c5420d6e26d (combined by EdMeshCombiner)" || en.gameObject.name == "background (combined by EdMeshCombiner)" || en.gameObject.name == "UnityTempFile-c25aef0516a467c499c2b17fe9de0555 (combined by EdMeshCombiner)" || en.gameObject.name == "UnityTempFile-08cf1f5609ed9c6479d0e4a4b737f923 (combined by EdMeshCombiner)" || en.gameObject.name == "UnityTempFile-08cf1f5609ed9c6479d0e4a4b737f923 (combined by EdMeshCombiner)" || en.gameObject.name == "UnityTempFile-08cf1f5609ed9c6479d0e4a4b737f923 (combined by EdMeshCombiner)")
                    {
                        en.gameObject.SetActive(false);
                    }
                    if (en.gameObject.name == "Shirt Stand" && en.transform.parent.gameObject.name.Contains("GorillaShirts"))
                    {
                        if (Vector3.Distance(GameObject.Find("Main Camera").transform.position, en.position) <= 20f)
                        {
                            en.gameObject.SetActive(true);
                        }
                        else
                        {
                            en.gameObject.SetActive(false);
                        }
                    }
                    if (GameObject.Find("Metro_Main_Geo"))
                    {
                        if (en.gameObject.name == "B_SkyBox_FBX")
                        {
                            en.gameObject.SetActive(false);
                        }
                    }
                    if (GameObject.Find("Beach"))
                    {
                        if (en.gameObject.name == "B_SkyBox (combined by EdMeshCombiner)")
                        {
                            en.gameObject.SetActive(false);
                        }
                    }
                    if (en.gameObject.name == "Ant (combined by EdMeshCombiner)" || en.gameObject.name == "ScryableAnts_Beach")
                    {
                        en.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}