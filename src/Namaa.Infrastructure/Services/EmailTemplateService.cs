using System.Net;
using Namaa.Application.Common.Interfaces;

namespace Namaa.Infrastructure.Services;
public sealed class EmailTemplateService : IEmailTemplateService
{
    public string BuildConfirmEmailBody(string displayName, string confirmLink)
    {
      var safeName = WebUtility.HtmlEncode(displayName);

    return  $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='utf-8' />
</head>
<body style='margin:0; padding:0; background:#ffffff;'>
  <div style='font-family: Arial, sans-serif; line-height:1.6; text-align:left; max-width:600px; margin:20px auto; color:#334155;'>
    
    <h2 style='color:#0f172a; margin:0 0 16px;'>Confirm your email</h2>

    <p style='margin:0 0 12px;'>Hi {safeName},</p>

    <p style='margin:0 0 24px;'>
      Welcome to Namaa! Please click the button below to verify your account and get started:
    </p>

    <div style='margin: 28px 0;'>
      <a href='{confirmLink}'
         style='display:inline-block; background-color:#15803d; color:#ffffff; padding:14px 28px;
                text-decoration:none; border-radius:8px; font-weight:600; font-size:16px;'>
        Confirm Email Address
      </a>
    </div>

    <p style='margin:24px 0 0; font-size:13px; color:#64748b;'>
      If you didn't create an account with Namaa, you can safely ignore this email.
    </p>

  </div>
</body>
</html>";
    }

    public string BuildForgotPasswordBody(string userName, string resetCode)
    {
        var safeName=WebUtility.HtmlEncode(userName);
        return $@"
        <div style='font-family: sans-serif; max-width: 600px; padding: 20px; border: 1px solid #ddd;'>
            <h2>Password Reset</h2>
            <p>Hi {safeName},</p>
            <p>Use the code below to reset your password. It expires in 15 minutes.</p>
            <div style='background: #f4f4f4; padding: 15px; font-size: 24px; font-weight: bold; text-align: center;'>
                {resetCode}
            </div>
            <p>If you didn't request this, you can safely ignore this email.</p>
        </div>";
    }
}