using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace VFEProps
{
    public class Window_PropsCategories : Window
    {

      
        public override Vector2 InitialSize => new Vector2(620f, 500f);
        private Vector2 scrollPosition = new Vector2(0, 0);
        public int columnCount = 8;
        private static readonly Color borderColor = new Color(0.13f, 0.13f, 0.13f);
        private static readonly Color fillColor = new Color(0, 0, 0, 0.1f);

        public Window_PropsCategories()
        {
          
           // closeOnClickedOutside = true;
            draggable = true;
            resizeable = true;
            preventCameraMotion = false;


        }

        public void OpenPropListWindow(PropCategoryDef category)
        {
            Window_PropsListing propListingWindow = new Window_PropsListing(category);
            
            Find.WindowStack.Add(propListingWindow);
            propListingWindow.windowRect = this.windowRect;
          
            Close();
        }

        public override void DoWindowContents(Rect inRect)
        {
            
            var outRect = new Rect(inRect);
            outRect.yMin += 40f;
            outRect.yMax -= 40f;
            outRect.width -= 16f;

            Text.Font = GameFont.Medium;
            var IntroLabel = new Rect(0, 0, 300, 32f);
            Widgets.Label(IntroLabel, "VFE_ChoosePropCategory".Translate());
            Text.Font = GameFont.Small;
            if (Widgets.ButtonImage(new Rect(outRect.xMax - 18f - 4f, 2f, 18f, 18f), TexButton.CloseXSmall))
            {
                Close();
            }

            List<PropCategoryDef> propCategories = StaticCollections.visibleCategories.OrderBy(x => x.priority).ToList();

           
            var viewRect = new Rect(0f, 0f, outRect.width - 16f, 104 * ((propCategories.Count / columnCount) + 1) + 20);
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
            try
            {
              
                for (var i = 0; i < propCategories.Count; i++)
                {

                    Rect rectIcon = new Rect((64 * (i % columnCount)) + 5 * (i % columnCount), viewRect.y  + (64 * (i / columnCount) + 20 * ((i / columnCount) + 1)), 64, 64);
            
                    GUI.DrawTexture(rectIcon, ContentFinder<Texture2D>.Get("UI/Categories/Props_CategoryBackground", true), ScaleMode.ScaleToFit, alphaBlend: true, 0f, Color.white, 0f, 0f);
                    GUI.DrawTexture(rectIcon, ContentFinder<Texture2D>.Get(propCategories[i].icon, true), ScaleMode.ScaleToFit, alphaBlend: true, 0f, Color.white, 0f, 0f);
                    TooltipHandler.TipRegion(rectIcon, propCategories[i].LabelCap+": "+ propCategories[i].description);
                    Text.Font = GameFont.Tiny;
                    var categoryTextRect = new Rect((64 * (i % columnCount)) + 5 * (i % columnCount), viewRect.y + 64 + (64 * (i / columnCount) + 20 * ((i / columnCount) + 1)), 64, 20);
                    Widgets.Label(categoryTextRect, propCategories[i].LabelCap);

                    if (Widgets.ButtonInvisible(rectIcon))
                    {
                        OpenPropListWindow(propCategories[i]);
                       
                    }
                }
            }
            finally
            {
                Widgets.EndScrollView();
            }
        }
    }
}