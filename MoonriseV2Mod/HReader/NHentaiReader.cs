using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using MoonriseV2Mod.HReader.Behaviour;
using MoonriseApi;
using MoonriseV2Mod.Settings;

namespace MoonriseV2Mod.HReader
{
    internal class NHentaiReader : MoonriseObject
    {
        public static NHentaiReader nhentaiReader;

        public NHentaiReader()
        {
            Moonrise.loadMenu += LoadMenu;
        }

        public override void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, TVJVc2Vy user)
        {
            if (!user.Lewd) return;
            MelonCoroutines.Start(LoadMenu(functions, socialInterractions));
        }

        [InitMethod]
        public static void Initialize()
        {
            nhentaiReader = new NHentaiReader();
            nhentaiReader.isInitialized = true;
        }

        public IEnumerator LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions)
        {
            while (!AddonMods.isInitialized) yield return null;

            QMNestedButton nhentai = AddonMods.MakeAddonNestedButton("N-Hentai\nReader", "Spawn an ebook with a selected hentai from those damn numbers.");
            string hentaiId = "";
            var idButton = new QMSingleButton(nhentai, 4, 0, "N-Hentai\nID:\n" + hentaiId, delegate { }, "");
            VXNoaW9SdWJ5TW9kaWZpZXJz.MakeTextOnly(idButton);

            var button1 = new QMSingleButton(nhentai, 1, 0, "1", delegate
            {
                hentaiId += "1";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 1 to hentai id.");

            var button2 = new QMSingleButton(nhentai, 2, 0, "2", delegate
            {
                hentaiId += "2";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 2 to hentai id.");

            var button3 = new QMSingleButton(nhentai, 3, 0, "3", delegate
            {
                hentaiId += "3";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 3 to hentai id.");

            var button4 = new QMSingleButton(nhentai, 1, 1, "4", delegate
            {
                hentaiId += "4";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 4 to hentai id.");

            var button5 = new QMSingleButton(nhentai, 2, 1, "5", delegate
            {
                hentaiId += "5";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 5 to hentai id.");

            var button6 = new QMSingleButton(nhentai, 3, 1, "6", delegate
            {
                hentaiId += "6";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 6 to hentai id.");

            var button7 = new QMSingleButton(nhentai, 1, 2, "7", delegate
            {
                hentaiId += "7";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 7 to hentai id.");

            var button8 = new QMSingleButton(nhentai, 2, 2, "8", delegate
            {
                hentaiId += "8";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 8 to hentai id.");

            var button9 = new QMSingleButton(nhentai, 3, 2, "9", delegate
            {
                hentaiId += "9";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 9 to hentai id.");

            var button0 = new QMSingleButton(nhentai, 4, 1, "0", delegate
            {
                hentaiId += "0";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Appends 0 to hentai id.");

            var spawnEbook = new QMSingleButton(nhentai, 4, 2, "Spawn\nE-Book", delegate
            {
                if (hentaiId != "")
                    HentaiReader.SpawnReader(hentaiId);
                hentaiId = "";
            }, "Spawns new ebook based on N-Hentai ID. (May cause a lag spike.)");

            var clearId = new QMSingleButton(nhentai, 5, 1, "Clear\nId", delegate
            {
                hentaiId = "";
                idButton.setButtonText("N-Hentai\nID:\n" + hentaiId);
            }, "Clears the Hentai id.");

            var enlargeOnGrabToggle = new QMToggleButton(nhentai, 5, -1, "Enlarge On\nGrab", delegate
            {
                MRConfiguration.config.EnlargeEbookOnGrab = true;
                MRConfiguration.config.WriteConfig();
            }, "Disabled", delegate
            {
                MRConfiguration.config.EnlargeEbookOnGrab = false;
                MRConfiguration.config.WriteConfig();
            }, "Toggles if the e-books UI will enlarge when grabbed", null, null, false, MRConfiguration.config.EnlargeEbookOnGrab);
        }
    }
}
