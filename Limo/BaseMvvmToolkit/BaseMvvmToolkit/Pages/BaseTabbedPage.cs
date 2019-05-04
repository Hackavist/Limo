using System;
using BaseMvvmToolkit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;


namespace BaseMvvmToolkit.Pages
{
    public class BaseTabbedPage : TabbedPage, IBasePage
    {
        public BaseTabbedPage()
        {
        }
        public void SetBinding<TSource>(BindableProperty targetProperty, string path, BindingMode mode = BindingMode.Default,
            IValueConverter converter = null, string stringFormat = null)
        {
            this.SetBinding(targetProperty, path, mode,
                converter, stringFormat);  
        }

        public event PageClosedEventHandler PageClosing;

        public void OnPageClosing()
        {
            PageClosing?.Invoke(this, new EventArgs());
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is ITabbedViewModel viewModel && viewModel.ToolbarItems != null && viewModel.ToolbarItems.Count > 0)
            {
                foreach (var toolBarItem in viewModel.ToolbarItems)
                {
                    if (!(ToolbarItems.Contains(toolBarItem)))
                    {
                        ToolbarItems.Add(toolBarItem);
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            OnPageClosing();

            var viewModel = BindingContext as IBaseViewModel;
            var result = viewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(BaseTabbedPage), default(Color));
        /// <summary>
        /// property to change selected tab color
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(float), typeof(BaseTabbedPage), default(float));
        /// <summary>
        /// property to change fontsize
        /// </summary>
        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty HasIconProperty =
            BindableProperty.Create(nameof(HasIcon), typeof(bool), typeof(BaseTabbedPage), true);

        /// <summary>
        /// property to change title font famliy of tab
        /// </summary>
        public bool HasIcon
        {
            get => (bool)GetValue(HasIconProperty);
            set => SetValue(HasIconProperty, value);
        }

        public static readonly BindableProperty TitleFontFamilyProperty =
            BindableProperty.Create(nameof(TitleFontFamily), typeof(string), typeof(BaseTabbedPage), default(string));
        /// <summary>
        /// property to change title font famliy of tab
        /// </summary>
        public string TitleFontFamily
        {
            get => (string)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
        }

        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(nameof(Position), typeof(BarPosition), typeof(BaseTabbedPage), BarPosition.Top);

        /// <summary>
        /// property to change postion of tabbed page bar 
        /// </summary>
        public BarPosition Position
        {
            get => (BarPosition)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

    }
    public enum BarPosition
    {
        Top = 0,
        Buttom = 1
    }
}
