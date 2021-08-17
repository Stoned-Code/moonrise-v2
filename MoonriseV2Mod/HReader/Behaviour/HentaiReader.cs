using MelonLoader;
using MoonriseV2Mod.API;
using MoonriseV2Mod.Settings;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UshioUI;
using VRC.SDKBase;

namespace MoonriseV2Mod.HReader.Behaviour
{
    [RegisterTypeInIl2Cpp]
    public class HentaiReader : MonoBehaviour
    {
        public HentaiReader(IntPtr ptr) : base(ptr) { }
        internal static string WorkingUrl
        {
            [HideFromIl2Cpp]
            get
            {
                string tempUrl = $"https://nhentai-sc.loca.lt";
                WebRequest wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                wr.Timeout = 1500;
                wr.Method = "GET";

                string json = "";
                // MoonriseConsole.Log($"Checking {tempUrl}");
                try
                {
                    WebResponse res = wr.GetResponse();
                    // MoonriseConsole.Log($"Received response...");
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    {
                        json = sr.ReadToEnd();
                        // MoonriseConsole.Log(json);
                    }
                }

                catch
                {

                }

                PingResponse pRes = JsonConvert.DeserializeObject<PingResponse>(json);

                if (pRes != null && pRes.foundBackend)
                    return tempUrl;

                for (int i = 1; i < 10; i++)
                {
                    try
                    {
                        tempUrl = $"https://nhentai-sc-{i}.loca.lt";

                        wr.Abort();
                        wr = WebRequest.Create(tempUrl + "/md9fjtnj4dm");
                        wr.Timeout = 1500;
                        // MoonriseConsole.Log($"Checking {tempUrl}");
                        WebResponse res = wr.GetResponse();

                        using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                        {
                            json = sr.ReadToEnd();
                            // MoonriseConsole.Log(json);
                        }

                        pRes = JsonConvert.DeserializeObject<PingResponse>(json);

                        if (pRes.foundBackend)
                            return tempUrl;
                    }

                    catch { }
                }

                return "N/A";
            }
        }

        private Animator animator;
        private Texture2D cover;
        private Texture2D[] pages;
        private RawImage pageView;
        private Text tagText;
        private Text pageStatus;
        private Text titleText;
        private Toggle forceLargeToggle;
        private Button lastButton;
        private Button nextButton;
        private Button deleteButton;

        private int pageNumbers;
        private int currentPage = 0;
        private bool forceLarge = false;

        private BoxCollider readerCollider;
        private BoxCollider canvasCollider;

        private Slider loadingProgress;
        private Transform loadingElement;


        string coverUrl;
        bool loading = false;
        string[] imageUrls;

        private void Update()
        {
            if (animator == null) return;

            if (GetComponent<VRC_Pickup>().IsHeld)
            {
                if (MRConfiguration.config.enlargeEbookOnGrab)
                    animator.SetBool("screenLarge", true);
                else
                {
                    animator.SetBool("screenLarge", false);
                }
            }

            if (forceLarge)
            {
                if (animator.GetBool("screenLarge") != true)
                    animator.SetBool("screenLarge", true);
            }

            else
            {
                if (animator.GetBool("screenLarge") != false && !GetComponent<VRC_Pickup>().IsHeld)
                    animator.SetBool("screenLarge", false);
            }

        }

        [HideFromIl2Cpp]
        IEnumerator SetCover()
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(coverUrl);
            request.SendWebRequest();
            while (!request.isDone) yield return null;
            // yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
                MoonriseConsole.Log(request.error);
            else
            {
                var downloadHandler = request.downloadHandler.Cast<DownloadHandlerTexture>();
                downloadHandler.texture.wrapMode = TextureWrapMode.Clamp;

                cover = downloadHandler.texture;
            }

            request.Dispose();

            SetDisplayTexture(cover);
        }

        [HideFromIl2Cpp]
        IEnumerator GetAllImages()
        {
            loading = true;
            loadingProgress.maxValue = imageUrls.Length;
            loadingProgress.value = 0;

            pages = new Texture2D[imageUrls.Length];

            loadingElement.gameObject.SetActive(true);

            for (int i = 0; i < imageUrls.Length; i++)
            {
                if (imageUrls[i] == null) continue; 
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrls[i]);

                request.SendWebRequest();
                while (!request.isDone) yield return null;

                if (request.isNetworkError || request.isHttpError)
                    MoonriseConsole.ErrorLog(request.error);
                else
                {
                    var downloadHandler = request.downloadHandler.Cast<DownloadHandlerTexture>();
                    downloadHandler.texture.wrapMode = TextureWrapMode.Clamp;
                    pages[i] = downloadHandler.texture;
                }

                loadingProgress.value = i + 1;
                request.Dispose();
            }
            loadingProgress.value = imageUrls.Length;
            yield return new WaitForSeconds(2f);

            loadingElement.gameObject.SetActive(false);
            loading = false;
        }

        [HideFromIl2Cpp]
        public static void SpawnReader(string hentaiId)
        {
            if (hentaiId == "0") return;
            var tempObj = QXNzZXRCdW5kbGVz.EbookBundle.LoadAsset("EBook.prefab").Cast<GameObject>();
            tempObj.AddComponent<HentaiReader>();

            var reader = Instantiate(tempObj);
            reader.GetComponent<HentaiReader>().InitializeReader(hentaiId);
            var ypos = PlayerCheck.LocalVRCPlayer.field_Private_VRCAvatarManager_0.field_Private_VRC_AvatarDescriptor_0.ViewPosition.y * 0.8f;
            var forPos = new Vector3(PlayerCheck.LocalVRCPlayer.transform.forward.x, PlayerCheck.LocalVRCPlayer.transform.forward.y, PlayerCheck.LocalVRCPlayer.transform.forward.z * 0.69f);
            
            var pos = PlayerCheck.LocalVRCPlayer.transform.position +  forPos + new Vector3(0f, ypos, 0f);
            var rot = Quaternion.Euler(PlayerCheck.LocalVRCPlayer.transform.rotation.eulerAngles - new Vector3(30f, 180f, 0f));
            
            reader.transform.position = pos;
            reader.transform.rotation = rot;
        }

        [HideFromIl2Cpp]
        private void SetDisplayTexture(Texture2D tex)
        {
            pageView.texture = tex;
        }

        [HideFromIl2Cpp]
        public void InitializeReader(string hentaiId)
        {
            readerCollider = GetComponent<BoxCollider>();
            canvasCollider = transform.Find("Screen/Canvas").GetComponent<BoxCollider>();

            loadingProgress = transform.Find("Screen/Canvas/LoadingImage/Slider").GetComponent<Slider>();
            forceLargeToggle = transform.Find("Screen/Canvas/Buttons/Toggle").GetComponent<Toggle>();

            animator = GetComponent<Animator>();
            pageView = transform.Find("Screen/Canvas/PageDisplay").GetComponent<RawImage>();
            tagText = transform.Find("Screen/Canvas/TagsBackground/Tags").GetComponent<Text>();
            pageStatus = transform.Find("Screen/Canvas/TagsBackground/PageStatus").GetComponent<Text>();
            titleText = transform.Find("Screen/Canvas/BookTitle").GetComponent<Text>();
            lastButton = transform.Find("Screen/Canvas/Buttons/PreviousPage").GetComponent<Button>();
            nextButton = transform.Find("Screen/Canvas/Buttons/NextPage").GetComponent<Button>();
            deleteButton = transform.Find("Screen/Canvas/Buttons/Delete").GetComponent<Button>();
            loadingElement = transform.Find("Screen/Canvas/LoadingImage");

            loadingElement.gameObject.SetActive(false);
            forceLargeToggle.isOn = forceLarge;

            Physics.IgnoreCollision(readerCollider, PlayerCheck.LocalVRCPlayer.GetComponentInChildren<CharacterController>());
            Physics.IgnoreCollision(canvasCollider, PlayerCheck.LocalVRCPlayer.GetComponentInChildren<CharacterController>());

            lastButton.onClick.AddListener(new Action(LastPage));
            nextButton.onClick.AddListener(new Action(NextPage));
            deleteButton.onClick.AddListener(new Action(delegate { DeleteEbook(this.gameObject); }));
            forceLargeToggle.onValueChanged.AddListener(new Action<bool>((value) =>
            {
                MoonriseConsole.Log("Toggle State: " + value.ToString());
                forceLarge = value;
            }));

            GetComponent<VRC_Pickup>().AutoHold = VRC_Pickup.AutoHoldMode.Yes;

            string fulUrl = WorkingUrl + "/keig84ionjk4390f/" + hentaiId;

            WebRequest wr = WebRequest.Create(fulUrl);
            wr.Timeout = 1500;
            wr.Method = "GET";
            wr.Proxy = null;

            string json = "";

            WebResponse res = wr.GetResponse();

            using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            
            if (json == "No Book Found...")
            {
                DeleteEbook(this.gameObject);
                return;
            }

            BookInfo info = JsonConvert.DeserializeObject<BookInfo>(json);
            coverUrl = info.coverUrl;
            imageUrls = info.pageUrls;
            titleText.text = info.hentaiTitle;

            tagText.text = $"<color=cyan>Tags: {info.tags.Length}</color>\n";
            pageNumbers = info.pageCount;
            pageStatus.text = $"Page\n1/{pageNumbers}";

            for (int i = 0; i < info.tags.Length; i++)
            {
                string tag = info.tags[i];
                if (info.tags.Length != i + 1)
                    tagText.text += tag + ", ";
                else
                    tagText.text += tag;
            }
            
            MelonCoroutines.Start(SetCover());
            MelonCoroutines.Start(GetAllImages());

            UshioMenuApi.PopupUI($"Spawned \"{info.hentaiTitle}\" E-book", "N-Hentai Reader");
        }

        [HideFromIl2Cpp]
        public void LastPage()
        {
            if (loading) return;
            if (currentPage > 0)
                currentPage--;
            SetDisplayTexture(pages[currentPage]);
            pageStatus.text = $"Page\n{currentPage + 1}/{pageNumbers}";
        }

        [HideFromIl2Cpp]
        public void NextPage()
        {
            if (loading) return;
            if (currentPage < pageNumbers - 1)
                currentPage++;
            SetDisplayTexture(pages[currentPage]);
            pageStatus.text = $"Page\n{currentPage + 1}/{pageNumbers}";
        }

        [HideFromIl2Cpp]
        public static void DeleteEbook(GameObject obj) => Destroy(obj);
    }
}
