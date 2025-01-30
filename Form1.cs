using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace messenger
{
    public partial class Form1 : Form
    {

        private WebView2 webView;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Fake Messenger";

            this.Icon = new System.Drawing.Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unpacked", "fm.ico"));

            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Width = (int)(screenWidth * 0.65);
            this.Height = (int)(screenHeight * 0.70);

            this.Left = (int)(screenWidth * 0.05);
            this.Top = (int)(screenHeight * 0.2);

            this.StartPosition = FormStartPosition.Manual;

            webView = new WebView2()
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(webView);

            InitializeWebViewAsync();

        }

        private async void InitializeWebViewAsync()
        {
            try
            {
                string systemTheme = GetSystemTheme();
                string darkModeArg = systemTheme == "Dark" ? "--force-dark-mode" : "";
                var environment = await CoreWebView2Environment.CreateAsync();

                await webView.EnsureCoreWebView2Async(environment);

                webView.Source = new Uri("https://messenger.com");

                webView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;

                webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}");
            }
        }
private string GetSystemTheme()
{
    const string registryKeyPath =@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    const string registryValueName = "AppsUseLightTheme";

    object value = Registry.GetValue(registryKeyPath, registryValueName, 1);
    if ( value != null && value is int intValue)
    {
        return intValue == 0 ? "Dark" : "Light";
    }
    return "Light";
}
        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            try
            {
                var uri = new Uri(e.Uri);

                if (!uri.Host.EndsWith("messenger.com", StringComparison.OrdinalIgnoreCase))
                {
                    e.Cancel = true;

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = e.Uri,
                        UseShellExecute = true
                    });
                }
            }
                catch (Exception ex)
            {
                MessageBox.Show($"Failed to open link: {ex.Message}");
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            try
            {
                e.Handled = true;

                Process.Start(new ProcessStartInfo
                {
                    FileName = e.Uri,
                    UseShellExecute = true
                });
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open new window: {ex.Message}");
            }
        }
    }
}

