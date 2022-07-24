using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.ViewModels.Modals;
using RetroFront.UWPAdmin.Views.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RetroFront.UWPAdmin.Services
{
    public class DialogService
    {
        public async Task MessageDialogAsync(string title, string message)
        {
            await MessageDialogAsync(title, message, "OK");
        }

        public async Task MessageDialogAsync(string title, string message, string buttonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = buttonText
            };

            await dialog.ShowAsync();
        }

        public async Task<bool?> ConfirmationDialogAsync(string title)
        {
            return await ConfirmationDialogAsync(title, "OK", string.Empty, "Cancel");
        }

        public  async Task<bool> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText)
        {
            return (await ConfirmationDialogAsync(title, yesButtonText, noButtonText, string.Empty)).Value;
        }

        public async Task<bool?> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText, string cancelButtonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                //IsPrimaryButtonEnabled = true,
                PrimaryButtonText = yesButtonText,
                //IsSecondaryButtonEnabled = true,
                SecondaryButtonText = noButtonText,
                CloseButtonText = cancelButtonText
            };
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.None)
            {
                return null;
            }

            return (result == ContentDialogResult.Primary);
        }

        public async Task<string> InputStringDialogAsync(string title)
        {
            return await InputStringDialogAsync(title, string.Empty);
        }

        public async Task<string> InputStringDialogAsync(string title, string defaultText)
        {
            return await InputStringDialogAsync(title, defaultText, "OK", "Cancel");
        }

        public async Task<string> InputStringDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText)
        {
            var inputTextBox = new TextBox
            {
                AcceptsReturn = false,
                Height = 32,
                Text = defaultText,
                SelectionStart = defaultText.Length,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["CustomDialogBorderColor"])
            };
            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return inputTextBox.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> InputTextDialogAsync(string title)
        {
            return await InputTextDialogAsync(title, string.Empty);
        }

        public async Task<string> InputTextDialogAsync(string title, string defaultText)
        {
            return await InputTextDialogAsync(title, defaultText, "OK", "Cancel");
        }
        public async Task<Search> SearchSteamGridDBByName(string name, ScraperSource source)
        {
            var vm = new ScrapeResolverViewModel(name, source);
            ScrapeResolverContentDialog contentDialog = new ScrapeResolverContentDialog(vm);
            var dialog = await contentDialog.ShowAsync();
            if (dialog == ContentDialogResult.Secondary)
            {
                return vm.Resultgame;
            }
            return null;
        }
        public async Task<string> InputTextDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText)
        {
            var inputTextBox = new TextBox
            {
                AcceptsReturn = true,
                Height = 32 * 6,
                Text = defaultText,
                TextWrapping = TextWrapping.Wrap,
                SelectionStart = defaultText.Length,
                Opacity = 1,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["CustomDialogBorderColor"])
            };
            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return inputTextBox.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<String> FilePicker(List<String> exts = null)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            if(exts != null)
            {
                foreach(var ext in exts)
                {
                    picker.FileTypeFilter.Add(ext);
                }
            }
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                return file.Path;
            }
            else
            {
                return string.Empty;
            }
        }
        public async Task<IEnumerable<String>> MultipleFilePicker(List<String> exts = null)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            if(exts != null)
            {
                foreach(var ext in exts)
                {
                    picker.FileTypeFilter.Add(ext);
                }
            }
            var files = await picker.PickMultipleFilesAsync();
            if (files.Count >0 )
            {
                return files.Select(x=>x.Path);
            }
            else
            {
                return null;
            }
        }
    }
}
