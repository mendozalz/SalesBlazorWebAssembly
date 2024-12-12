using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
	public class Country
	{
        public int StatesNumber;

        public int Id { get; set; }

		[Display(Name = "País")]
		[Required(ErrorMessage = "El campo {0} es obligatorio")]
		[MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
		public string Name { get; set; } = null!;
		public ICollection<State>? States{ get; set; }
		public int StateNumber => States == null ? 0 : States.Count;
	}
}
