using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienApp
{
    public static class GuiEmail
    {
        public static string taiKhoan = "thuvienhcmue@gmail.com";
        public static string matKhau = "vddc dvte zsfn gcwt";

        public static bool guiEmail(string emailTo, string content)
        {
            var fromAddress = new MailAddress(taiKhoan, "Quan ly thu vien");
            var toAddress = new MailAddress(emailTo, emailTo);
            string fromPassword = matKhau;
            string subject = "Thông báo hệ thống";
            string body = content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            try
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
