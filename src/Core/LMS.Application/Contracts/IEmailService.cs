﻿using LMS.Application.Models;

namespace LMS.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
