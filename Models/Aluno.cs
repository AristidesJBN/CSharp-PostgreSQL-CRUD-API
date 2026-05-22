namespace EscolaAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Curso { get; set; } = null!;

        public DateTime DataNascimento { get; set; }
    }
}