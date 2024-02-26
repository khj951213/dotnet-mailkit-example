using JuneDev_Mailkit_Example;

var mailHandler = new MailHandler()
{
    Email = "your_email_address",
    Password = "your_email_password",
    Server = "your_email_server",
    Port = 587, // your_email_port_number
    Name = "your_name"
};

await mailHandler.SendEmail("Email from June", "Test email", "khj951213@gmail.com", "Hyun June Kim");
