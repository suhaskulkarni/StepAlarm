using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace StepAlarm.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmSettingPage : Page
    {
        public AlarmSettingPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel.ViewModelLocator.AlarmSettingsPageViewModel;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Music_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StepAlarmToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void ListPickerFlyout_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            if(args != null)
            {
                (this.DataContext as ViewModel.AlarmSettingsPageViewModel).SnoozeNumber = Convert.ToUInt32(args.AddedItems.FirstOrDefault());
            }
        }

        private void NumberOfSteps_ItemsPicked(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            if (args != null)
            {
                (this.DataContext as ViewModel.AlarmSettingsPageViewModel).StepsNumber = Convert.ToUInt32(args.AddedItems.FirstOrDefault());
            }
        }

        private void DayItems_SelectionChanged(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            var dataContext = this.DataContext as ViewModel.AlarmSettingsPageViewModel;
            if(args != null)
            {
                foreach (var item in args.AddedItems)
                {
                    if (String.IsNullOrWhiteSpace(dataContext.RepeatAlarm) || dataContext.RepeatAlarm.Contains(dataContext.ResourceContentLoader.GetString("RepeatOnce")))
                    {
                        dataContext.RepeatAlarm = dataContext.ResourceContentLoader.GetString(item.ToString()).Substring(0, 3);
                    }
                    else
                    {
                        dataContext.RepeatAlarm = string.Empty;
                        dataContext.RepeatAlarm = String.Format(dataContext.RepeatAlarm + "," + dataContext.ResourceContentLoader.GetString(item.ToString()).Substring(0, 3));
                    }
                }
            }
        }
    }
}
