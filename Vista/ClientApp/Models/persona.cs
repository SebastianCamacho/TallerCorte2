using Entity;
namespace Vista.ClientApp.Models
{
    public class PersonaInputModel
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public string Departamento { get; set; } 
        public string Ciudad { get; set; }
        public Ayudas Ayuda { get; set; }   
        
    }

    public class PersonaViewModel : PersonaInputModel
    {
        public PersonaViewModel()
        {

        }
        public PersonaViewModel(Persona persona)
        {
            Identificacion = persona.Identificacion;
            Nombre = persona.Nombre;
            Apellido=persona.Apellidos;
            Sexo = persona.Sexo;
            Edad = persona.Edad;
            Departamento=persona.Departamento;
            Ciudad=persona.Ciudad;
            Ayuda=persona.Ayuda;     
        }
        
    }
    
}