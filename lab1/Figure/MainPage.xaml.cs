using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Figure
{
    public partial class MainPage : ContentPage
    {
        int a = 0, b = 0;
        string result;
        Label header;
        Picker picker;
        XLabs.Forms.Controls.CheckBox checkBox;
        XLabs.Forms.Controls.CheckBox checkBox1;
        Entry storona, hstorona, h1storona;
        public MainPage()
        {
            Grid g = new Grid
            {
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)}
                }
            };
            g.Padding = new Thickness(15, 0);
            g.VerticalOptions = LayoutOptions.Center;
            header = new Label
            {
                Text = "Выберите фигуру:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            picker = new Picker { Margin = new Thickness(90, 0) };
            picker.Items.Add("Квадрат");
            picker.Items.Add("Прямоугольник");
            picker.SelectedIndexChanged += picker_SelectedIndexChanged;
            storona = new Entry { Placeholder = "Введите сторону", IsVisible = false, HorizontalOptions = LayoutOptions.Center, Keyboard = Keyboard.Numeric };
            hstorona = new Entry { Placeholder = "Введите сторону a", IsVisible = false, Keyboard = Keyboard.Numeric };
            h1storona = new Entry { Placeholder = "Введите сторону b", IsVisible = false, Keyboard = Keyboard.Numeric };

            Button alertButton = new Button
            {
                Text = "Посчитать",
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            alertButton.Clicked += AlertButton_Clicked;
            checkBox = new XLabs.Forms.Controls.CheckBox { DefaultText = "Периметр", HorizontalOptions = LayoutOptions.End };
            checkBox1 = new XLabs.Forms.Controls.CheckBox { DefaultText = "Площадь" };
            g.Children.Add(header, 0, 0);
            Grid.SetColumnSpan(header, 2);
            g.Children.Add(picker, 0, 1);
            Grid.SetColumnSpan(picker, 2);
            g.Children.Add(storona, 0, 2);
            Grid.SetColumnSpan(storona, 2);
            g.Children.Add(hstorona, 0, 2);
            g.Children.Add(h1storona, 1, 2);
            g.Children.Add(checkBox, 0, 3);
            g.Children.Add(checkBox1, 1, 3);
            g.Children.Add(alertButton, 0, 4);
            Grid.SetColumnSpan(alertButton, 2);
            this.Content = g;
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            b = 1;
            if (picker.Items[picker.SelectedIndex] == "Квадрат")
            {
                if (a == 2)
                {
                    hstorona.IsVisible = false;
                    h1storona.IsVisible = false;
                }
                a = 1;
                storona.IsVisible = true;
            }
            else
            {
                if (a == 1)
                {
                    storona.IsVisible = false;
                }
                a = 2;
                hstorona.IsVisible = true;
                h1storona.IsVisible = true;
            }
        }
        private void Result()
        {
            if (b == 0)
            {
                result = "Выберите фигуру";
            }
            else if (((picker.Items[picker.SelectedIndex] == "Квадрат") && (string.IsNullOrWhiteSpace(storona.Text))) ||
                     ((picker.Items[picker.SelectedIndex] == "Прямоугольник") && ((string.IsNullOrWhiteSpace(hstorona.Text)
                     || string.IsNullOrWhiteSpace(h1storona.Text)))))
            {
                result = "Введите значения";
            }
            else if (((picker.Items[picker.SelectedIndex] == "Квадрат") && Convert.ToInt32(storona.Text) <= 0) || ((picker.Items[picker.SelectedIndex] == "Прямоугольник") && Convert.ToInt32(hstorona.Text) <= 0) || ((picker.Items[picker.SelectedIndex] == "Прямоугольник") && Convert.ToInt32(h1storona.Text) <= 0))
            {
                result = "Введите корректные значения";
            }
            else if (checkBox.Checked && checkBox1.Checked)
            {
                result = "Периметр = " +
                    Convert.ToString((picker.Items[picker.SelectedIndex] == "Квадрат") ?
                    (4 * Convert.ToInt32(storona.Text)) : 2 * (Convert.ToInt32(hstorona.Text)
                    + Convert.ToInt32(h1storona.Text))) +
                    "\nПлощадь = " +
                    Convert.ToString((picker.Items[picker.SelectedIndex] == "Квадрат") ?
                    (Math.Pow(Convert.ToInt32(storona.Text), 2)) : (Convert.ToInt32(hstorona.Text)
                    * Convert.ToInt32(h1storona.Text)));
            }
            else if (checkBox.Checked)
            {
                result = "Периметр = " +
                    Convert.ToString((picker.Items[picker.SelectedIndex] == "Квадрат") ?
                    (4 * Convert.ToInt32(storona.Text)) : 2 * (Convert.ToInt32(hstorona.Text)
                    + Convert.ToInt32(h1storona.Text)));
            }
            else if (checkBox1.Checked)
            {
                result = "Площадь = " +
                    Convert.ToString((picker.Items[picker.SelectedIndex] == "Квадрат") ?
                    (Math.Pow(Convert.ToInt32(storona.Text), 2)) : (Convert.ToInt32(hstorona.Text)
                    * Convert.ToInt32(h1storona.Text)));
            }

            else if (!checkBox.Checked && !checkBox.Checked)
            {
                result = "Поставьте галки";
            }
        }
        private void AlertButton_Clicked(object sender, EventArgs e)
        {
            Result();
            DisplayAlert("Результат", result, "ОK");
        }
    }
}
