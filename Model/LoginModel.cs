using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Model
{
    public class LoginModel
    {
        public Guid Id { get; init; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
        
        public string Token { get; set; } = string.Empty;

        public virtual ICollection<TaskItem> Tasks { get; set; }

        public LoginModel(string login, string senha, string token)
        {
            Login = login;
            Senha = senha;
            Token = token;
        }
    }
}
