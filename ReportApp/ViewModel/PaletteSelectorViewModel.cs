using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ReportApp.Command;
using System.Collections.Generic;
using System.Windows.Input;
using Server.Local;
using Newtonsoft.Json;

namespace ReportApp.ViewModel
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel(bool isDark)
        {
            Swatches = new SwatchesProvider().Swatches;
            IsDark = isDark;
        }

        /// <summary>
        /// 定义主题切换
        /// </summary>
        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        /// <summary>
        /// 主题切换
        /// </summary>
        /// <param name="isDark"></param>
        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);

            DBKeeper.Instance.DBObject<U_Theme>().Update(new Theme
            {
                Dark = isDark
            });
        }

        public IEnumerable<Swatch> Swatches { get; }

        public bool IsDark { get; }

        /// <summary>
        /// 定义主色调按钮点击事件
        /// </summary>
        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        /// <summary>
        /// 主色调按钮点击事件
        /// </summary>
        /// <param name="swatch"></param>
        private static void ApplyPrimary(Swatch swatch)
        {           
            new PaletteHelper().ReplacePrimaryColor(swatch);

            DBKeeper.Instance.DBObject<U_Theme>().Update(new Theme
            {
                Primary = JsonConvert.SerializeObject(swatch)
            });
        }

        /// <summary>
        /// 定义行为色调按钮点击事件
        /// </summary>
        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));

        /// <summary>
        /// 行为色调按钮点击事件
        /// </summary>
        /// <param name="swatch">主题</param>
        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);

            DBKeeper.Instance.DBObject<U_Theme>().Update(new Theme
            {
                Accent = JsonConvert.SerializeObject(swatch)
            });
        }
    }
}
