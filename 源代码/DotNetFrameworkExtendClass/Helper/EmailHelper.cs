using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 邮件发送辅助类
    /// </summary>
    public class EmailHelper : Singleton<EmailHelper>
    {
        private static System.Net.Mail.MailMessage MMessage = new System.Net.Mail.MailMessage();
        private static System.Net.Mail.SmtpClient smtp163 = new System.Net.Mail.SmtpClient("smtp.163.com", 25);
        private EmailHelper()
        {
        }
        /// <summary>
        /// 使用163邮箱发送邮件
        /// </summary>
        /// <param name="Form">发件箱的地址</param>
        /// <param name="UserID">发件箱的用户名</param>
        /// <param name="Password">发件箱的密码</param>
        /// <param name="To">发到哪个地址</param>
        /// <param name="Title">邮件标题</param>
        /// <param name="Content">邮件正文</param>
        /// <param name="Priority">优先级</param>
        public void ForNetease(string Form, string UserID, string Password, string To, string Title, string Content, System.Net.Mail.MailPriority Priority = System.Net.Mail.MailPriority.High)
        {
            MMessage.Priority = Priority;
            MMessage.From = new System.Net.Mail.MailAddress(Form);
            MMessage.To.Add(To);
            MMessage.Subject = Title;
            MMessage.Body = Content;
            MMessage.BodyEncoding = Encoding.UTF8;
            smtp163.Credentials = new System.Net.NetworkCredential(UserID, Password);
            smtp163.Send(MMessage);
        }
        /// <summary>
        /// 使用自定义的邮箱发送邮件
        /// </summary>
        /// <param name="Form">发件箱的地址</param>
        /// <param name="Smtp">发件箱的Smtp地址</param>
        /// <param name="SmtpPort">发件箱的Smtp端口号</param>
        /// <param name="UserID">发件箱的用户名</param>
        /// <param name="Password">发件箱的密码</param>
        /// <param name="To">发到哪个地址</param>
        /// <param name="Title">邮件标题</param>
        /// <param name="Content">邮件正文</param>
        /// <param name="Priority">优先级</param>
        public void ForCustom(string Form, string Smtp, int SmtpPort, string UserID, string Password, string To, string Title, string Content, System.Net.Mail.MailPriority Priority = System.Net.Mail.MailPriority.High)
        {
            MMessage.Priority = Priority;
            MMessage.From = new System.Net.Mail.MailAddress(Form);
            MMessage.To.Add(To);
            MMessage.Subject = Title;
            MMessage.Body = Content;
            MMessage.BodyEncoding = Encoding.UTF8;
            using (System.Net.Mail.SmtpClient smtpCustom = new System.Net.Mail.SmtpClient(Smtp, SmtpPort))
            {
                smtp163.Credentials = new System.Net.NetworkCredential(UserID, Password);
                smtp163.Send(MMessage);
            }
        }
    }
}
