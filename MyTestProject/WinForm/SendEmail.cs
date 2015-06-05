using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace WinForm
{


    public class SendEmail
    {



        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool Send(ETalkMailMessage mail, string smptHost, string userName, string userPassword)
        {
            bool bResult = false;
            try
            {
                //Smtp Server
                SmtpClient client = new SmtpClient(smptHost);
                //client.Port = 25;
                //需要指定帐户信息
                client.UseDefaultCredentials = false;
    
                client.Credentials = new NetworkCredential(userName, userPassword);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mail);

                bResult = true;
            }
            catch (Exception ex)
            {

                throw new Exception("send mail failed", ex);
            }
            return bResult;
        }
    }

    public class ETalkMailMessage : MailMessage
    {
        public ETalkMailMessage()
            : base()
        {
        }

        public ETalkMailMessage(MailAddress from, MailAddress to)
            : base(from, to)
        {
        }

        public ETalkMailMessage(string from, string to)
            : base(from, to)
        {
        }

        public ETalkMailMessage(string from, string to, string subject, string body)
            : base(from, to, subject, body)
        {
        }

        public void AddAttachments(string[] fileNames, byte[][] attachs)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (fileNames[i] != null && fileNames[i] != string.Empty)
                {
                    Attachment mailAttach = new Attachment(ByteArrayToStream(attachs[i]), fileNames[i]);
                    this.Attachments.Add(mailAttach);
                }
            }
        }

        /// <summary>
        /// 字节数组转换为流
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private Stream ByteArrayToStream(byte[] byteArray)
        {
            MemoryStream mStream = new MemoryStream(byteArray);
            return mStream;
        }
    }

}
