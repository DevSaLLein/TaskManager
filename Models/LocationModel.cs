using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TasManager.Models;

namespace TaskManager.Model
{
    public class LocationModel
    (
        string cep, 
        string logradouro, 
        string complemento, 
        string bairro, 
        string localidade, 
        string uf, 
        string ddd
    )
    {
        [Key]
        [JsonIgnore]
        public string Cep { get; init; } = cep;   
        
        public string Logradouro { get; set; } = logradouro;
        
        public string Complemento { get; set; } = complemento;
        
        public string Bairro { get; set; } = bairro;
        
        public string Localidade { get; set; } = localidade;

        public string Uf { get; set; } = uf;

        public string Ddd { get; set; }   = ddd;
    }
}