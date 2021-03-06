﻿using System.Threading.Tasks;

namespace CoolBytes.Services.Mailer
{
    public interface ISendValidator
    {
        Task<bool> IsSendingAllowed(IMailer mailer);
    }
}