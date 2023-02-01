using System.Threading;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        
        string email;
        string password;
        string EmailAndPassword;
        string AfterDog;
        string AfterPoint;
        string SecretCode;
        Dictionary<int, string> EmailsFor10Min = new Dictionary<int, string>()
        {
            [1] = "Fornewpk@yandex.by : 123123qwerqwe",
            [2] = "Apdo7kmmr@yandex.by : MihoyozxcMaster123",
            [3] = "eewgergergf@yandex.by : qweqweqwe",
            [4] = "dgdfgdfgdfg@mail.ru : nvjfh2134",
            [5] = "jkjfkmvdkm@mail.ru : zxcfeww"
        };
        
        while (true)
        {
            Random key = new Random();

            Console.WriteLine("Данная программа имеет 2 функции:");
            Console.WriteLine("Вывести - Вывод рандомной почты на определенное время");
            Console.WriteLine("Добавить - Добавление своей почты в базу данных");

            string FirstStep = Console.ReadLine();
            try
            {
                string SecondStep = FirstStep switch
                {
                    "Добавить" => "Добавить",
                    "Вывести" => "Вывести"
                };

                if (SecondStep == "Добавить")
                {
                    link:
                    Console.WriteLine("Введите почту и пароль, без клавиши enter, через ':' ");

                    email = Console.ReadLine();
                    password = Console.ReadLine();

                    int DogIndex = email.IndexOf("@");

                    if (DogIndex == -1)
                    {
                        Console.WriteLine("Нет собаки");
                        goto link;
                    }

                    AfterDog = email.Substring(DogIndex + 1);

                    int PointIndex = email.IndexOf(".");

                    if (PointIndex == -1)
                    {
                        Console.WriteLine("Нет точки");
                        goto link;
                    }

                    AfterPoint = email.Substring(PointIndex + 1);

                    if (AfterDog == email.Substring(PointIndex))
                    {
                        Console.WriteLine("Нет ничего после @");
                        goto link;
                    }

                    if (AfterPoint == "")
                    {
                        Console.WriteLine("Нет ничего после .");
                        goto link;
                    }

                    EmailAndPassword = email + " : " + password;
                    Console.WriteLine("Все верно(Да/Нет): ");
                    Console.WriteLine(EmailAndPassword);
                    string IsOkay = Console.ReadLine();

                    if (IsOkay == "Да")
                    {
                        SecretCode = Generate_Secret_code();
                        SendMail_Script(email, SecretCode);
                        Console.WriteLine("Введите число присалнное на почту: ");
                        string codeFromUser = Console.ReadLine();
                        if (codeFromUser == SecretCode)
                        {
                            Console.WriteLine("Успешно добавлено");
                            EmailsFor10Min.Add(EmailsFor10Min.Count + 1, EmailAndPassword);
                            Thread.Sleep(5000);
                            Console.Clear();
                        }
                    }
                    else
                    {
                        goto link;
                    }
                }

                else if (SecondStep == "Вывести")
                {
                    int KeyForDictionary = key.Next(1, EmailsFor10Min.Count + 1);
                    foreach (var Emails in EmailsFor10Min)
                    {
                        if (Emails.Key == KeyForDictionary)
                        {
                            Console.WriteLine($"{Emails.Value}");
                            Thread.Sleep(10000);
                            Console.Clear();
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Данные введены некоректно, попробуйте снова");
            }

            
        }
        Console.ReadKey();
        
    }

    public static string Generate_Secret_code()
    {   
        string SecretCode = "";
        Random number_Random = new Random();
        for (int i = 0; i < 6; i++)
        {
            int number = number_Random.Next(0, 10);
            string number_string = number.ToString();
            SecretCode += number_string;
        }
        return SecretCode;
    }

    public static void SendMail_Script(string AdressToSend, string secretCode)
    {
        MailAddress sender = new MailAddress("spiderruvi@gmail.com");
        MailAddress taker = new MailAddress(AdressToSend);
        MailMessage message_with_code = new MailMessage(sender, taker);
        message_with_code.Body = secretCode;


        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(sender.Address, "vpcoosvifaoxkeft");

        smtpClient.Send(message_with_code);
    }
}