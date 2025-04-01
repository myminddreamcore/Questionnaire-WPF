using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string[] voprosy = { "1. Введите ваше ФИО:",
            "2. Введите ваш номер телефона: ",
            "3. Введите ваш возраст: ",
            "4. Выберите ваш пол? ",
            "5. Введите вашу дату рождения? ",
            "6. Насколько сильно вам нравится красный цвет?",
            "7. Ваше любимый день недели? ",
            "8. Ваш знак зодиака?  ",
            "9. Ваше любимое время года?",
            "10.Ваша почта? " };
        int index;
        int number = 0;
        public string answer;
        public MainWindow()
        {
            InitializeComponent();
            v.Text = "Анкета на разные темы";
            lb.Visibility = Visibility.Hidden;
            cb.Visibility = Visibility.Hidden;
            vvod.Visibility = Visibility.Hidden;
            v.Visibility = Visibility.Visible;
            dp.Visibility = Visibility.Hidden;
            r0.Visibility = Visibility.Hidden;
            r1.Visibility = Visibility.Hidden;
            r2.Visibility = Visibility.Hidden;
            r3.Visibility = Visibility.Hidden;
            next.Visibility = Visibility.Hidden;
            s1.Visibility = Visibility.Hidden;
            s1text.Visibility = Visibility.Hidden;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            number++;
            answer = $"\nАнкета{number}|";
            lb.Visibility = Visibility.Hidden;
            cb.Visibility = Visibility.Hidden;
            
            index = 0;
            start.Visibility = Visibility.Hidden;
            vvod.Visibility = Visibility.Visible;
            v.Visibility = Visibility.Visible;
            next.Visibility = Visibility.Visible;
            next.Visibility = Visibility.Visible;
            v.Text = voprosy[index];


        }
        private bool FIO(string input)
        {

            string p = @"^[а-яА-ЯёЁ]+(-[а-яА-ЯёЁ]+)?\s[а-яА-ЯёЁ]+(-[а-яА-ЯёЁ]+)?(\s[а-яА-ЯёЁ]+(-[а-яА-ЯёЁ]+)?)*$";
            Regex regex = new Regex(p);

            return regex.IsMatch(input);//является ли регулярным выражение
        }


        private bool Phone(string input)
        {

            string p = @"^(\+7|8)\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            Regex regex = new Regex(p);
            return regex.IsMatch(input);
        }
        private bool Pochta(string input)
        {
            string p = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(p);
            return regex.IsMatch(input);
        }
        private bool Day(string input)
        {
            bool p = false;
            string[] days = { "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            for (int i = 0; i < days.Length; i++)
            {
                if (input.ToLower() == days[i])
                {
                    p = true;
                    break;
                }
            }
            return p;
        }
        private bool Znak(string input)
        {

            bool p = false;
            string[] znaks = {
                    "Козерог", "Водолей", "Рыбы",
                    "Овен", "Телец", "Близнецы",
                    "Рак", "Лев", "Дева",
                    "Весы", "Скорпион", "Стрелец","козерог", "водолей", "рыбы",
                    "овен", "телец", "близнецы",
                    "рак", "лев", "дева",
                    "весы", "скорпион", "стрелец"
                };
            for (int i = 0; i < znaks.Length; i++)
            {
                if (input.ToLower() == znaks[i])
                {
                    p = true;
                    break;
                }
            }
            return p;
        }
        int age = 0;
        int month = 0;
        int day = 0;
        private void next_Click(object sender, RoutedEventArgs e)
        {
            bool isInputValid = false;


            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 6:
                case 7:
                case 9:

                    isInputValid = !string.IsNullOrEmpty(vvod.Text);
                    break;

                case 3:

                    isInputValid = (bool)r0.IsChecked || (bool)r1.IsChecked;
                    break;

                case 4:

                    isInputValid = dp.IsEnabled;
                    break;

                case 5:

                    isInputValid = !string.IsNullOrEmpty(s1text.Text);
                    break;


                case 8:

                    isInputValid = (bool)r0.IsChecked || (bool)r1.IsChecked || (bool)r2.IsChecked || (bool)r3.IsChecked;
                    break;


            }


            if (isInputValid)
            {
                switch (index)
                {
                    case 0:
                        if (FIO(vvod.Text) && vvod.Text.Length < 100)
                        {
                            answer += vvod.Text + "|";
                            vvod.Text = "";

                        }
                        else
                        {

                            MessageBox.Show("Неверный формат ФИО. Пример: Иванов Иван Иванович");
                            return;
                        }
                        break;
                    case 1:


                        if (Phone(vvod.Text))
                        {
                            answer += vvod.Text + "|";
                            vvod.Text = "";
                        }
                        else
                        {

                            MessageBox.Show("Неверный формат номера телефона. Пример: 8/+79990007778");
                            return;
                        }
                        break;
                    case 2:
                        
                        age = Convert.ToInt32(vvod.Text);
                        if (age > 14 && age < 111)
                        {
                            answer += vvod.Text + "|";
                            vvod.Text = "";
                            vvod.Visibility = Visibility.Hidden;
                            r0.Visibility = Visibility.Visible;
                            r0.Content = "женский";
                            r1.Content = "мужской";
                            r1.Visibility = Visibility.Visible;
                            r0.IsChecked = false;
                            r1.IsChecked = false;
                        }
                        else
                        {
                            MessageBox.Show("Возраст допускается только больше 14 и меньше 111.");
                            return;
                        }


                        break;
                    case 3:
                        if ((bool)r0.IsChecked)
                        {
                            answer += r0.Content.ToString() + "|";
                        }
                        else if ((bool)r1.IsChecked)
                        {
                            answer += r1.Content.ToString() + "|";
                        }
                        r0.IsChecked = false;
                        r1.IsChecked = false;
                        r0.Visibility = Visibility.Hidden;
                        r1.Visibility = Visibility.Hidden;
                        vvod.Visibility = Visibility.Hidden;
                        dp.Visibility = Visibility.Visible;
                        break;
                    case 4:

                        DateTime birthDate = dp.SelectedDate.Value;
                        DateTime today = DateTime.Today;
                        DateTime minBirthDate = today.AddYears(-age - 1).AddDays(1);
                        DateTime maxBirthDate = today.AddYears(-age);
                        month = dp.SelectedDate.Value.Month;
                        day = dp.SelectedDate.Value.Day;
                        if (birthDate >= minBirthDate && birthDate <= maxBirthDate)
                        {
                            answer += birthDate.ToString("dd.MM.yyyy") + "|";
                            dp.Visibility = Visibility.Hidden;
                            dp.SelectedDate = null;
                            s1.Visibility = Visibility.Visible;
                            s1text.Visibility = Visibility.Visible;
                        }
                        
                        
                        else
                        {
                            MessageBox.Show("Вы указали возраст, который не сходится с вводимой датой рождения.");
                            return;
                        }
                        
                        break;



                    case 5:
                        answer += s1text.Text + "|";
                        s1.Visibility = Visibility.Hidden;
                        s1.Value = 0;
                        s1text.Visibility = Visibility.Hidden;
                        vvod.Visibility = Visibility.Visible;

                        break;
                    case 6:

                        if (Day(vvod.Text))
                        {
                            answer += vvod.Text += "|";
                            vvod.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("нет такого дня недели.");
                            return;
                        }

                        break;
                    case 7:
                        
                        if (Znak(vvod.Text))
                        {
                            string u;
                            switch (month)
                            {
                                case 1:
                                    if (day <= 19)
                                        u = "Козерог";
                                    else
                                        u = "Водолей";
                                    break;

                                case 2:
                                    if (day <= 18)
                                        u = "Водолей";
                                    else
                                        u = "Рыбы";
                                    break;

                                case 3:
                                    if (day <= 20)
                                        u = "Рыбы";
                                    else
                                        u = "Овен";
                                    break;

                                case 4:
                                    if (day <= 19)
                                        u = "Овен";
                                    else
                                        u = "Телец";
                                    break;

                                case 5:
                                    if (day <= 20)
                                        u = "Телец";
                                    else
                                        u = "Близнецы";
                                    break;

                                case 6:
                                    if (day <= 20)
                                        u = "Близнецы";
                                    else
                                        u = "Рак";
                                    break;

                                case 7:
                                    if (day <= 22)
                                        u = "Рак";
                                    else
                                        u = "Лев";
                                    break;

                                case 8:
                                    if (day <= 22)
                                        u = "Лев";
                                    else
                                        u = "Дева";
                                    break;

                                case 9:
                                    if (day <= 22)
                                        u = "Дева";
                                    else
                                        u = "Весы";
                                    break;

                                case 10:
                                    if (day <= 22)
                                        u = "Весы";
                                    else
                                        u = "Скорпион";
                                    break;

                                case 11:
                                    if (day <= 21)
                                        u = "Скорпион";
                                    else
                                        u = "Стрелец";
                                    break;

                                case 12:
                                    if (day <= 21)
                                        u = "Стрелец";
                                    else
                                        u = "Козерог";
                                    break;

                                default:
                                    u = "Неверный месяц";
                                    break;
                            }
                            if (u.ToLower() == vvod.Text.ToLower())
                            {
                                answer += vvod.Text += "|";
                                vvod.Visibility = Visibility.Hidden;
                                r0.Content = "зима";
                                r1.Content = "весна";
                                r2.Content = "лето";
                                r3.Content = "осень";
                                r0.Visibility = Visibility.Visible;
                                r1.Visibility = Visibility.Visible;
                                r2.Visibility = Visibility.Visible;
                                r3.Visibility = Visibility.Visible;
                                r0.IsChecked = false;
                                r1.IsChecked = false;
                                r2.IsChecked = false;
                                r3.IsChecked = false;

                            }
                            else
                            {
                                MessageBox.Show("Знак зодиака не сходится с вашей датой рождения.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("нет такого знака зодиака");
                            return;
                        }
                        
                        
                        break;


                    case 8:
                        if ((bool)r0.IsChecked)
                        {
                            answer += r0.Content.ToString() + "|";
                        }
                        else if ((bool)r1.IsChecked)
                        {
                            answer += r1.Content.ToString() + "|";
                        }
                        else if ((bool)r2.IsChecked)
                        {
                            answer += r2.Content.ToString() + "|";
                        }
                        else if ((bool)r3.IsChecked)
                        {
                            answer += r3.Content.ToString() + "|";
                        }
                        r0.Visibility = Visibility.Hidden;
                        r1.Visibility = Visibility.Hidden;
                        r2.Visibility = Visibility.Hidden;
                        r3.Visibility = Visibility.Hidden;
                        vvod.Text = "";
                        vvod.Visibility = Visibility.Visible;
                        break;
                    case 9:
                        if (Pochta(vvod.Text))
                        {
                            answer += vvod.Text ;
                            r0.Visibility = Visibility.Hidden;
                            r1.Visibility = Visibility.Hidden;
                            r2.Visibility = Visibility.Hidden;
                            r3.Visibility = Visibility.Hidden;
                            vvod.Text = "";
                            vvod.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show("неверный формат почты. Пример: helloworld@mail.ru");
                            return;
                        }
                        break;
                }

                if (index == 9)
                {
                    string folderPath = System.IO.Path.Combine(@"C:\", "упп", "Анкета.txt");
                    

                    try
                    {
                        if (File.Exists(folderPath))
                        {

                            File.AppendAllText(folderPath, answer);
                            answer = "";
                            LoadAnketas();
                        }
                        else
                        {
                           //
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
                    }



                    
                    v.Text = $"ваши ответы записаны в анкету c вашим именем\n чтобы пройти заново нажмите старт";
                    vvod.Visibility = Visibility.Hidden;
                    next.Visibility = Visibility.Hidden;
                    start.Visibility = Visibility.Visible;
                    cb.Visibility = Visibility.Visible;
                    lb.Visibility = Visibility.Visible;
                }


                else if (index < 9)
                {
                    index++;
                    v.Text = voprosy[index];
                }

            }
            else
            {

                MessageBox.Show("вам нужно ответить на вопрос!");
            }
        }

        private void s1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s1text.Text = Math.Round(s1.Value).ToString();
        }
        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb.SelectedIndex == -1) return;
            string folderPath = System.IO.Path.Combine(@"C:\", "упп", "Анкета.txt");
            string[] allLines = File.ReadAllLines(folderPath);
            var anketaLines = allLines.Where(line => line.StartsWith("Анкета")).ToArray();
            string selectedAnketa = anketaLines[cb.SelectedIndex];
            string[] data = selectedAnketa.Split('|');
            if (data.Length >= 11)
            {
                lb.Text = $"ФИО: {data[1]}\nТелефон: {data[2]}\nВозраст: {data[3]}\n" +
                         $"Пол: {data[4]}\nДата рождения: {data[5]}\n" +
                         $"Красный: {data[6]}\nДень недели: {data[7]}\n" +
                         $"ЗЗ: {data[8]}\nВремя года: {data[9]}\nПочта: {data[10]}";
            }
        }

        private void LoadAnketas()
        {
            string folderPath = System.IO.Path.Combine(@"C:\", "упп", "Анкета.txt");

            if (File.Exists(folderPath))
            {
                cb.Items.Clear();
                string[] allLines = File.ReadAllLines(folderPath);
                var anketaLines = allLines.Where(line => line.StartsWith("Анкета")).ToArray();
                for (int i = 0; i < anketaLines.Length; i++)
                {
                    string name = anketaLines[i].Split('|')[1];
                    cb.Items.Add($"Анкета {i + 1}: {name}");
                }
            }
            else
            {
                MessageBox.Show("Файл не найден!");
            }
        }
        int m = 0;
        private void vvod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (index)
            {
                case 0:
                    
                    foreach (char c in e.Text)
                    {
                        if (!(c >= 'а' && c <= 'я') && !(c >= 'А' && c <= 'Я') && c != ' ' && c!='-' )
                        {
                            e.Handled = true; 
                            return;
                        }
                    }
                    e.Handled = false; 
                    break;
                     
                case 1:
                    if (vvod.CaretIndex == 0)
                    {
                        if (e.Text.StartsWith("+"))
                        {
                            vvod.Text = "+7(";
                            vvod.CaretIndex = 3;
                            e.Handled = true;
                            m = 16;
                            return;
                        }
                        else if (e.Text.StartsWith("8"))
                        {
                            vvod.Text = "8(";
                            vvod.CaretIndex = 2;
                            e.Handled = true;
                            m = 15;
                            return;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    if (vvod.Text.Length < m )
                    {
                        foreach (char c in e.Text)
                        {
                            
                            if (!char.IsDigit(c) && (c != '+' || vvod.CaretIndex != 0))
                            {
                                e.Handled = true;
                                return;
                            }
                            
                        }

                        
                        if (e.Text.Contains("+") && vvod.CaretIndex > 0)
                        {
                            e.Handled = true;
                            return;
                        }
                        if (vvod.Text.StartsWith("+7(") && vvod.Text.Length == 6)
                        {
                            vvod.Text += ")";
                            vvod.CaretIndex = 7;
                        }
                        else if (vvod.Text.StartsWith("+7(") && vvod.Text.Length == 10)
                        {
                            vvod.Text += "-";
                            vvod.CaretIndex = 11;
                        }
                        else if (vvod.Text.StartsWith("+7(") && vvod.Text.Length == 13)
                        {
                            vvod.Text += "-";
                            vvod.CaretIndex = 14;
                        }
                        else if (vvod.Text.StartsWith("8(") && vvod.Text.Length == 5)
                        {
                            vvod.Text += ")";
                            vvod.CaretIndex = 6;
                        }
                        else if (vvod.Text.StartsWith("8(") && vvod.Text.Length == 9)
                        {
                            vvod.Text += "-";
                            vvod.CaretIndex = 10;
                        }
                        else if (vvod.Text.StartsWith("8(") && vvod.Text.Length == 12)
                        {
                            vvod.Text += "-";
                            vvod.CaretIndex = 13;
                        }

                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 2:
                    if (vvod.Text.Length < 3)
                    {
                        foreach (char c in e.Text)
                        {
                            if (!char.IsDigit(c))
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 6:
                    foreach (char c in e.Text)
                    {
                        if (!(c >= 'а' && c <= 'я') && !(c >= 'А' && c <= 'Я'))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                    break;
                case 7:
                    foreach (char c in e.Text)
                    {
                        if (!(c >= 'а' && c <= 'я') && !(c >= 'А' && c <= 'Я'))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                    break;
                case 9:
                    foreach (char c in e.Text)
                    {
                        if (!(c >= 'а' && c <= 'я') && !(c >= 'А' && c <= 'Я') )
                        {
                            e.Handled = false;
                            return;
                        }
                    }
                    e.Handled = true;
                    break;

            }
        }

        private void dp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c!='.')
                {
                    e.Handled = true;
                    return;
                }
            }
            e.Handled = false;
            
        }

        private void vvod_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (index)
            {
                case 1:
                case 2:
                case 6:
                case 7:
                case 9:
               
                    if ( e.Key == Key.Space )
                    {
                        e.Handled = true;
                        return;
                    }

                    e.Handled = false;
                    break;

            }
        }

        private void dp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }

            e.Handled = false;
            
        }
    }
    
}
