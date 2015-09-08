using LeBetaTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LeBetaTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int k = 0;
        int repeatInt = 0;
        int sideInt = 0;
        private TipClass tipClass;
        private LimitClass limitclass;
        public MainPage()
        {
            this.InitializeComponent();
            tipClass = new TipClass();
            limitclass = new LimitClass();
        }

        private async void AppBarButton_Click_Help(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("This app contains a random assortment of programming practice.");
            messageDialog.Commands.Add(new UICommand("Close"));
            messageDialog.CancelCommandIndex = 0;
            await messageDialog.ShowAsync();
        }

        private void billAmountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = "";
        }

        private void performCalculation()
        {
            tipClass.CalculateTip(billAmountTextBox.Text, tipAmountTextBox.Text);
            tipFinalTextBlock.Text = tipClass.TipAmount;
            billFinalTextBlock.Text = tipClass.TotalAmount;
            debugTextBlock.Text = tipClass.Equation;
        }

        private void billAmountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = tipClass.BillAmount;
        }

        private void billAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            performCalculation();
        }

        private void tipAmountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            tipAmountTextBox.Text = "";
        }

        private void tipAmountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            tipAmountTextBox.Text = tipClass.InputTipAmount;
        }

        private void tipAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            performCalculation();
        }

        private async void AppBarButton_Click_Rate(object sender, RoutedEventArgs e)
        {
            String pfn = Package.Current.Id.FamilyName;
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=" + pfn + ""));
        }

        private async void AppBarButton_Click_Debug(object sender, RoutedEventArgs e)
        {
            if (k == 1)
            {
                debugTextBlock.Visibility = Visibility.Collapsed;
                debugTitleTextBlock.Visibility = Visibility.Collapsed;
                k = 0;
                debugTitleTextBlock.Text = "Debug Info is disabled.";
            }
            else if (k == 0)
            {
                debugTextBlock.Visibility = Visibility.Visible;
                debugTitleTextBlock.Visibility = Visibility.Visible;
                k = 1;
                debugTitleTextBlock.Text = "Debug Info is enabled.";
            }
            else
            {
                var messageDialog = new MessageDialog("Something weird happened while performing ''Toggle Debug.''");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
        }

        private void ts_Repeat_Toggled(object sender, RoutedEventArgs e)
        {
            if (ts_Repeat.IsOn == true)
                tb_Repeat_Value.Visibility = Visibility.Visible;
            if (ts_Repeat.IsOn == false)
                tb_Repeat_Value.Visibility = Visibility.Collapsed;
        }

        private void calculateButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            limitclass.CalculateExpression(tb_Input_Equation.Text);
            if (ts_Repeat.IsOn == false)
            {
                if (Convert.ToString(limitclass.LimitResult) == "NaN") //apparently limitclass.LimitResult == double.NaN never is true...?
                {
                    tbl_Output.Text = "Please check your equation, invalid symbols were found.";
                }
                else
                {
                    tbl_Output.Text = Convert.ToString(limitclass.LimitResult);
                }
            }
            else if (ts_Repeat.IsOn == true)
            {
                bool valid = true;
                bool print = false;
                double result = 0.0;
                try
                {
                    repeatInt = Convert.ToInt32(tb_Repeat_Value.Text);
                }
                catch
                {
                    valid = false;
                }
                if (valid)
                {
                    if (repeatInt >= 0)
                    {
                        for (int i = 0; i < repeatInt; i++)
                        {
                            result += limitclass.LimitResult;
                        }
                        print = true;
                    }
                    else
                    {
                        tbl_Output.Text = "I'm not going to try repeating equations a negative number of times. Nice try though";
                    }
                }
                else
                {
                    tbl_Output.Text = "Entering an actual NUMBER for the NUMBER of summations would be nice...";
                }

                if (print == true)
                    tbl_Output.Text = Convert.ToString(result);

            }
        }

        private void limitCalculateButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                limitclass.CalculateLimit(tb_LimitInput_Equation.Text, Convert.ToDouble(tb_LimitInput_Value), sideInt);
                tbl_LimitOutput.Text = Convert.ToString(limitclass.LimitResult);
            }
            catch
            {
                tbl_LimitOutput.Text = "x should approach a number, not whatever you put in there.";
            }
        }
    }
}
