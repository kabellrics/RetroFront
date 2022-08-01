using RetroFront.UWPAdmin.Core.APIHelper;
using RetroFront.UWPAdmin.Core.Models;
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
        public async Task<String> ShowImgScrapeChoice(DisplayGame Game, ScraperType typeImg)
        {
            var vm = new ImgScrapeChoiceViewModel(Game, typeImg);
            ImgScrapeChoiceContentDialog contentDialog = new ImgScrapeChoiceContentDialog(vm);
            var dialog = await contentDialog.ShowAsync();
            if (dialog == ContentDialogResult.Secondary)
            {
                return vm.ImgToChange;
            }
            return null;
        }
        public async Task<DisplayGame> ShowMetadataScrapeChoice(DisplayGame Game)
        {
            var vm = new MetadataScrapeChooseViewModel(Game);
            MetadataScrapeChooseContentDialog contentDialog = new MetadataScrapeChooseContentDialog(vm);
            var dialog = await contentDialog.ShowAsync();
            if (dialog == ContentDialogResult.Secondary)
            {
                return vm.CurrentGame;
            }
            return null;
        }
        public async Task DLLFile(String source, String dest, string typeElement, string nameElement)
        {
            try
            {
                var vm = new FileModalDLLViewModel(source, dest, typeElement, nameElement);
                FileDLLContentDialog contentDialog = new FileDLLContentDialog(vm);
                await contentDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public async Task WriteFile(byte[] source, String dest, string typeElement, string nameElement)
        {
            try
            {
                var vm = new FileModalDLLViewModel(source, dest, typeElement, nameElement);
                FileDLLContentDialog contentDialog = new FileDLLContentDialog(vm);
                await contentDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public async Task<String> ShowIMGProposal(int gameId, ScraperSource CurrentScrapeSource, ScraperType CurrentScraperType)
        {
            var vm = new ImgProposalViewModel(gameId, CurrentScrapeSource, CurrentScraperType);
            ImgProposalContentDialog contentDialog = new ImgProposalContentDialog(vm);
            var dialog = await contentDialog.ShowAsync();
            if (dialog == ContentDialogResult.Secondary)
            {
                return vm.Result;
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

        public async Task<String> FolderPicker()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                //// Application now has read/write access to all contents in the picked folder
                //// (including other sub-folder contents)
                //Windows.Storage.AccessCache.StorageApplicationPermissions.
                //FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                return folder.Path;
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
